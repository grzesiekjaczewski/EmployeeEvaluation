using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using EmployeeEvaluation.Models;
using iTextSharp.text.pdf.draw;
//using System.Drawing;

namespace EmployeeEvaluation.Logic.PDF
{
    public class PdfUtil
    {
        Font normal;

        public PdfUtil()
        {
            string calibripath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\calibri.ttf";
            BaseFont calibri = BaseFont.CreateFont(calibripath, BaseFont.CP1250, BaseFont.EMBEDDED);
            normal = new Font(calibri, 10f, Font.NORMAL, BaseColor.BLACK);
        }

        public byte[] CreatePdf(BrowseSurvey browseSurvey, Employee employee, Team team, Position position)
        {
            Rectangle pagesize = new Rectangle(20, 20, PageSize.A4.Width, PageSize.A4.Height);
            Document doc = new Document(pagesize, 10, 10, 50, 10);
            MemoryStream ms = new MemoryStream();
            PdfWriter pw = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            doc.Add(addParagraphHeader("ARKUSZ OCENY OKRESOWEJ PRACOWNIKA"));
            doc.Add(addParagragh("Dane osobowe pracownika:", true));
            doc.Add(addParagragh("Nazwisko i imię:         " + employee.LastName + " " + employee.FirstName));
            doc.Add(addParagragh("Jednostka organizacyjna: " + team.Name));
            doc.Add(addParagragh("Zajmowane stanowisko:    " + position.Name));

            Chunk linebreak = new Chunk(new LineSeparator(4f, 100f, GrayColor.GRAY, Element.ALIGN_CENTER, -1));
            doc.Add(linebreak);

            //string calibri = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\calibri.ttf";
            //BaseFont sylfaen = BaseFont.CreateFont(calibri, BaseFont.CP1250, BaseFont.EMBEDDED);
            //Font normal = new Font(sylfaen, 10f, Font.NORMAL, BaseColor.BLACK);

           

            //cell = PhraseCell(new Phrase("Najważniejsze zdarzenia", newfntbldh2), PdfPCell.ALIGN_CENTER);

            PdfPTable tbl = new PdfPTable(4);
            tbl.DefaultCell.Border = Rectangle.NO_BORDER;

            tbl.AddCell("Sr No");
            tbl.AddCell("Name");
            tbl.AddCell("Address");
            tbl.AddCell("Phone");
            
            for (int i = 1; i < 6; i++)
            {
                tbl.AddCell(i.ToString());
                tbl.AddCell("Name " + i.ToString());
                tbl.AddCell("Address " + i.ToString());

                tbl.AddCell(new Phrase("ąśćźĄŻĘĄŁÓŃaaa" + i.ToString(), normal));
            }
            doc.Add(tbl);

            doc.Close();
            byte[] byteArray = ms.ToArray();
            ms.Flush();
            ms.Close();
            ms.Dispose();

            return byteArray;
        }

        private Paragraph addParagragh(string ParagraphText, bool bold = false)
        {
            //string calibripath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\calibri.ttf";
            ////BaseFont sylfaen = BaseFont.CreateFont(sylfaenpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            //BaseFont calibri = BaseFont.CreateFont(calibripath, BaseFont.CP1250, BaseFont.EMBEDDED);
            //Font normal = new Font(calibri, 10f, Font.NORMAL, BaseColor.BLACK);

            Paragraph p = new Paragraph(ParagraphText, normal);
            p.SpacingBefore = 10f;
            p.FirstLineIndent = 10f;
            p.SpacingAfter = 15f;

            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.Font.Size = 13f;
            p.Font.SetColor(0, 0, 0);
            if (bold) p.Font.SetStyle("bold");

            return p;
        }
        private Chunk addParagraphHeader(string headingText)
        {
            Chunk ch = new Chunk(headingText);
            ch.Font.Size = 16f;
            ch.Font.SetStyle("bold");
            ch.Font.SetColor(0, 0, 0);
            return ch;
        }
    }
}