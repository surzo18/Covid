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

namespace Covid
{
    public partial class Export : Form
    {
        public Export()
        {
            InitializeComponent();
        }

        private void g2b_import_Click(object sender, EventArgs e)
        {
            List<string> dd = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                dd.Add(i.ToString());
            }






            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF|*.pdf";
                saveFileDialog.Title = "Export záznamov";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    if (saveFileDialog.FileName != "")
                    {
                        string DEST = saveFileDialog.FileName;
                        CreatePdf(DEST, dd);
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
                table = new Table(new float[10]).UseAllAvailableWidth();
                //table = new Table(UnitValue.CreatePercentArray(new float[] { 1,4, 4, 3, 4, 4,4, 2, 4, 1 })).UseAllAvailableWidth();

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
                Cell ulica = new Cell().Add(new Paragraph("Ulica").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(ulica);
                Cell mesto = new Cell().Add(new Paragraph("Mesto").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(mesto);
                Cell psc = new Cell().Add(new Paragraph("PSČ").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(psc);
                Cell telK = new Cell().Add(new Paragraph("Tel.kontakt").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(telK);
                Cell poznamka = new Cell().Add(new Paragraph("Poznámka").SetTextAlignment(TextAlignment.CENTER).SetFont(font));
                table.AddHeaderCell(poznamka);






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
