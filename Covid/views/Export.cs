using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Covid.Models;
using Covid.Views;

using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Data.SQLite;

namespace Covid
{
    public class TestingUserId
    {
        int id;
        int status;
        public int Id { get => id; private set => id = value; }
        public int Status { get => status; private set => status = value; }

        public TestingUserId(int id, int status)
        {
            Id = id;
            Status = status;
        }
    }

    public partial class Export : Form
    {
        public Export()
        {
            InitializeComponent();
        }

        private void g2b_import_Click(object sender, EventArgs e) // ZOZNAM POZITIVNYCH
        {
            WritePdf(true);
        }


        private void guna2Button1_Click(object sender, EventArgs e) // ZOZNAM VSETKYCH TESTOVANYCH
        {
            WritePdf(false);
        }

        private void WritePdf(bool onlyPositive)
        {
            List<TestingUserId> testingList = new List<TestingUserId>();

            Connection db = new Connection();
            db.conn.Open();

            try
            {
                string stm = "SELECT * FROM testing LIMIT 10000"; // TODO: prediskutovat limit zaznamov (10000)
                SQLiteCommand cmd = new SQLiteCommand(stm, db.conn);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    testingList.Add(new TestingUserId(Convert.ToInt32(rdr["User_id"]), Convert.ToInt32(rdr["Is_negative"]))); // ekvivalnet Convert.ToInt32(rdr["id"]); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba pri načítaní záznamov o testovaných!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
                db.conn.Close();
                return;
            }

            List<string> zaznam = new List<string>();  // """8x stringov urcuje jeden zaznam""" 
            int poradoveCislo = 0;
            try
            {
                SQLiteCommand cmd;
                SQLiteDataReader rdr;
                foreach (var i in testingList)
                {
                    if (onlyPositive == true)
                        if (i.Status != 1) // 0-neurčený 1-pozitivny 2-negativny
                            continue;

                    string stm = $"SELECT * FROM user WHERE Id = \"{i.Id}\"";
                    cmd = new SQLiteCommand(stm, db.conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        zaznam.Add((++poradoveCislo).ToString()); // poradove cislo
                        zaznam.Add(rdr["Name"].ToString()); // meno // ekvivalent zaznam.Add(rdr.GetString(3));
                        zaznam.Add(rdr["Surname"].ToString()); // priezvisko
                        zaznam.Add(rdr["Birth_date"].ToString()); // datum narodenia
                        zaznam.Add(rdr["Identification_number"].ToString()); // rodneCislo
                        zaznam.Add(rdr["Address"].ToString()); // adresa
                        zaznam.Add(rdr["Phone"].ToString()); // tel.kontakt
                        zaznam.Add(" "); // poznamka
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba pri vypĺňaní záznamu o testovaných pre PDF!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
                db.conn.Close();
                return;
            }

            db.conn.Close();

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF|*.pdf";
                saveFileDialog.Title = "Export záznamov";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    if (saveFileDialog.FileName != "")
                    {
                        string DEST = saveFileDialog.FileName;
                        CreatePdf(DEST, zaznam);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba pri zapisovaní súboru na disk!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
                return;
            }
        }

        public virtual void CreatePdf(String dest, List<string> zaznam)
        {
            try
            {
                // DEFINOVANIE DOKUMENTU A STYLOV
                Document doc = new Document(new PdfDocument(new PdfWriter(dest)), PageSize.A4);
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN, PdfEncodings.CP1250, true);
                SolidBorder solid = new SolidBorder(0.5f);

                // HLAVICKA
                Table table = new Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth();

                Cell bunka = new Cell(1, 5).Add(new Paragraph("Hlásenie o testovaných osobách").SetTextAlignment(TextAlignment.CENTER).SetFont(font).SetUnderline(1, -1));
                bunka.SetBackgroundColor(ColorConstants.YELLOW);
                table.AddCell(bunka);

                bunka = new Cell(1, 2).Add(new Paragraph("ADRESA ODBERNÉHO MIESTA").SetFont(font));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderLeft(solid);
                table.AddCell(bunka);
                bunka = new Cell(1, 3).Add(new Paragraph("").SetBorder(Border.NO_BORDER));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderRight(solid);
                table.AddCell(bunka);

                bunka = new Cell(1, 2).Add(new Paragraph("obchodné meno").SetFont(font));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderLeft(solid);
                table.AddCell(bunka);
                bunka = new Cell(1, 2).Add(new Paragraph(""));
                bunka.SetBackgroundColor(new DeviceRgb(140, 220, 80), 0.4f);
                table.AddCell(bunka);
                bunka = new Cell(1, 1).Add(new Paragraph(""));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderRight(solid);
                table.AddCell(bunka);

                bunka = new Cell(1, 2).Add(new Paragraph("ulica").SetFont(font));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderLeft(solid);
                table.AddCell(bunka);
                bunka = new Cell(1, 2).Add(new Paragraph(""));
                bunka.SetBackgroundColor(new DeviceRgb(140, 220, 80), 0.4f);
                table.AddCell(bunka);
                bunka = new Cell().Add(new Paragraph(""));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderRight(solid);
                table.AddCell(bunka);

                bunka = new Cell(1, 2).Add(new Paragraph("PSČ, mesto").SetFont(font));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderLeft(solid);
                table.AddCell(bunka);
                bunka = new Cell(1, 2).Add(new Paragraph(""));
                bunka.SetBackgroundColor(new DeviceRgb(140, 220, 80), 0.4f);
                table.AddCell(bunka);
                bunka = new Cell().Add(new Paragraph(""));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderRight(solid);
                table.AddCell(bunka);

                bunka = new Cell(1, 5).Add(new Paragraph("\n"));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderLeft(solid);
                bunka.SetBorderRight(solid);
                table.AddCell(bunka);

                bunka = new Cell(1, 2).Add(new Paragraph("Meno, priezvisko, kontaktnej osoby").SetFont(font));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderLeft(solid);
                table.AddCell(bunka);
                bunka = new Cell(1, 2).Add(new Paragraph(""));
                bunka.SetBackgroundColor(new DeviceRgb(140, 220, 80), 0.4f);
                table.AddCell(bunka);
                bunka = new Cell().Add(new Paragraph(""));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderRight(solid);
                table.AddCell(bunka);

                bunka = new Cell(1, 2).Add(new Paragraph("Telefonický kontakt").SetFont(font));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderLeft(solid);
                table.AddCell(bunka);
                bunka = new Cell(1, 2).Add(new Paragraph(""));
                bunka.SetBackgroundColor(new DeviceRgb(140, 220, 80), 0.4f);
                table.AddCell(bunka);
                bunka = new Cell().Add(new Paragraph(""));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderRight(solid);
                table.AddCell(bunka);

                bunka = new Cell(1, 2).Add(new Paragraph("E-mail").SetFont(font));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderLeft(solid);
                table.AddCell(bunka);
                bunka = new Cell(1, 2).Add(new Paragraph(""));
                bunka.SetBackgroundColor(new DeviceRgb(140, 220, 80), 0.4f);
                table.AddCell(bunka);
                bunka = new Cell().Add(new Paragraph(""));
                bunka.SetBorder(Border.NO_BORDER);
                bunka.SetBorderRight(solid);
                table.AddCell(bunka);

                bunka = new Cell(1, 5).Add(new Paragraph("\n"));
                bunka.SetBorderTop(Border.NO_BORDER);
                table.AddCell(bunka);

                doc.Add(table);

                // ZAZNAMY
                table = new Table(new float[8]).UseAllAvailableWidth();
                //table = new Table(UnitValue.CreatePercentArray(new float[] { 1,4, 4, 3, 4, 8, 4, 1 })).UseAllAvailableWidth();

                Cell pc = new Cell().Add(new Paragraph("P.č.").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(pc);
                Cell meno = new Cell().Add(new Paragraph("Meno").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(meno);
                Cell priezvisko = new Cell().Add(new Paragraph("Priezvisko").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(priezvisko);
                Cell datumN = new Cell().Add(new Paragraph("Dátum narodenia").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(datumN);
                Cell rodneC = new Cell().Add(new Paragraph("Rodné číslo").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(rodneC);
                Cell adresa = new Cell().Add(new Paragraph("Adresa").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(adresa);
                Cell telK = new Cell().Add(new Paragraph("Tel.kontakt").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(telK);
                Cell poznamka = new Cell().Add(new Paragraph("Poznámka").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(poznamka);
                
                // ZAPIS DAT DO TABULKY PDF
                foreach (var i in zaznam)
                {
                    table.AddCell(new Cell().Add(new Paragraph(i).SetFont(font)));
                }

                doc.Add(table);
                doc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba pri generovaní PDF súboru!", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
                return;
            }
        }
    }
}
