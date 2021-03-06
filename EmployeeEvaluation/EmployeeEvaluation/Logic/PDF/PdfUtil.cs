﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using EmployeeEvaluation.Models;
using iTextSharp.text.pdf.draw;

namespace EmployeeEvaluation.Logic.PDF
{
    public class PdfUtil
    {
        Font small;
        Font normal;
        Font bold;
        Font big;
        Font bigBold;

        public PdfUtil()
        {
            string calibripath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\calibri.ttf";
            BaseFont calibri = BaseFont.CreateFont(calibripath, BaseFont.CP1250, BaseFont.EMBEDDED);
            normal = new Font(calibri, 8f, Font.NORMAL, BaseColor.BLACK);
            bold = new Font(calibri, 8f, Font.BOLD, BaseColor.BLACK);
            big = new Font(calibri, 12f, Font.NORMAL, BaseColor.BLACK);
            bigBold = new Font(calibri, 12f, Font.BOLD, BaseColor.BLACK);
            small = new Font(calibri, 6f, Font.NORMAL, BaseColor.BLACK);
        }

        private void header(Document doc, Employee employee, string team, string position)
        {
            PdfPTable tbl1 = new PdfPTable(new float[] { 0.3f, 0.7f } );
            PdfPTable tbl2 = new PdfPTable(1);
            tbl1.DefaultCell.Border = Rectangle.NO_BORDER;
            tbl2.DefaultCell.Border = Rectangle.NO_BORDER;

            doc.Add(addParagraphHeader("ARKUSZ OCENY OKRESOWEJ PRACOWNIKA", bigBold));

            Chunk linebreak = new Chunk(new LineSeparator(0.5f, 100f, GrayColor.GRAY, Element.ALIGN_CENTER, -1));
            doc.Add(linebreak);

            doc.Add(addParagragh("Dane dotyczące ocenianego pracownika", bold, 58));

            tbl1.AddCell(new Phrase("Nazwisko i imię:", normal));
            tbl1.AddCell(new Phrase(employee.LastName + " " + employee.FirstName, normal));
            tbl1.AddCell(new Phrase("Jednostka organizacyjna:", normal));
            tbl1.AddCell(new Phrase(team, normal));
            tbl1.AddCell(new Phrase("Zajmowane stanowisko:", normal));
            tbl1.AddCell(new Phrase(position, normal));

            doc.Add(tbl1);


            Chunk linebreak2 = new Chunk(new LineSeparator(0.5f, 100f, GrayColor.GRAY, Element.ALIGN_CENTER, -1));
            doc.Add(linebreak2);

            doc.Add(addParagragh("Skala oceny:", bold, 58));
            tbl2.AddCell(new Phrase("5 - ocena zdecydowanie powyżej oczekiwań, pracownik wyraźnie wyróżnia się spośród innych, przewyższa oczekiwania", normal));
            tbl2.AddCell(new Phrase("4 - ocena powyżej oczekiwań, pracownik osiąga dobre rezultaty pracy, całkowicie spełnia oczekiwania", normal));
            tbl2.AddCell(new Phrase("3 - ocena zgodna z oczekiwaniami, pracownik spełnia oczekiwania na poziomie zadowalającym", normal));
            tbl2.AddCell(new Phrase("2 - ocena poniżej oczekiwań, pracownik spełnia niektóre oczekiwania", normal));
            tbl2.AddCell(new Phrase("1 - ocena zdecydowanie poniżej oczekiwań, pracownik nie spełnia oczekiwań", normal));
            doc.Add(tbl2);

            doc.Add(linebreak2);
        }

        private void surveyHeader(Document doc, BrowseSurvey browseSurvey)
        {
            doc.Add(addParagragh("Dane dotyczące ankiety", bold, 58));

            PdfPTable tbl = new PdfPTable(new float[] { 0.3f, 0.7f });
            tbl.DefaultCell.Border = Rectangle.NO_BORDER;

            tbl.AddCell(new Phrase("Ankieta:", normal));
            tbl.AddCell(new Phrase(browseSurvey.Survey.Name, normal));
            tbl.AddCell(new Phrase("Data wypełnienia:", normal));
            tbl.AddCell(new Phrase(browseSurvey.Survey.CompliteEmployeeDate.ToShortDateString(), normal));
            tbl.AddCell(new Phrase("Termin wypełnienia:", normal));
            tbl.AddCell(new Phrase(browseSurvey.Survey.SurveyDadline.ToShortDateString(), normal));

            doc.Add(tbl);


            Chunk linebreak2 = new Chunk(new LineSeparator(0.5f, 100f, GrayColor.GRAY, Element.ALIGN_CENTER, -1));
            doc.Add(linebreak2);
        }

        private void surveyBody(Document doc, BrowseSurvey browseSurvey)
        {
            int employeeSum = 0;
            int managerSum = 0;
            int cnt = 0;
            decimal employeeAvg = 0;
            decimal managerAvg = 0;
            int employeeTotalSum = 0;
            int managerTotalSum = 0;
            int cntTotal = 0;
            decimal employeeTotalAvg = 0;
            decimal managerTotalAvg = 0;

            foreach (var surveyPart in browseSurvey.Survey.SurveyParts)
            {
                PdfPTable tbl = new PdfPTable(new float[] { 0.8f, 0.1f, 0.1f });

                tbl.AddCell(new Phrase(browseSurvey.SurveyTemplate.SurveyPartTemplates.Where(t => t.Id == surveyPart.SurveyPartTemplateId).ToList()[0].Name, bold));
                tbl.AddCell(new Phrase("Samoocena pracownika", bold));
                tbl.AddCell(new Phrase("Ocena menadżera", bold));

                employeeSum = 0;
                managerSum = 0;
                employeeAvg = 0;
                managerAvg = 0;
                cnt = 0;

                foreach (var surveyQuestion in surveyPart.SurveyQuestions)
                {

                    int questionType = browseSurvey.SurveyTemplate
                                    .SurveyPartTemplates
                                    .Where(t => t.Id == surveyPart.SurveyPartTemplateId)
                                    .ToList()[0]
                                    .SurveyQuestionTemplates
                                    .Where(q => q.Id == surveyQuestion.SurveyQuestionTemplateId)
                                    .ToList()[0]
                                    .QuestionType;

                    tbl.AddCell(new Phrase(browseSurvey.SurveyTemplate
                                                .SurveyPartTemplates
                                                .Where(t => t.Id == surveyPart.SurveyPartTemplateId)
                                                .ToList()[0]
                                                .SurveyQuestionTemplates
                                                .Where(q => q.Id == surveyQuestion.SurveyQuestionTemplateId)
                                                .ToList()[0]
                                                .Name, normal));
                    if (questionType == 1)
                    {
                        tbl.AddCell(new Phrase(surveyQuestion.EmployeeScore.ToString(), normal));
                        tbl.AddCell(new Phrase(surveyQuestion.ManagerScore.ToString(), normal));

                        employeeSum += surveyQuestion.EmployeeScore;
                        managerSum += surveyQuestion.ManagerScore;
                        cnt += 1;
                        employeeTotalSum += surveyQuestion.EmployeeScore;
                        managerTotalSum += surveyQuestion.ManagerScore;
                        cntTotal += 1;
                    }
                    else
                    {
                        var cell = new PdfPCell(new Phrase(browseSurvey.SurveyTemplate
                                                  .SurveyPartTemplates
                                                  .Where(t => t.Id == surveyPart.SurveyPartTemplateId)
                                                  .ToList()[0]
                                                  .SurveyQuestionTemplates
                                                  .Where(q => q.Id == surveyQuestion.SurveyQuestionTemplateId)
                                                  .ToList()[0]
                                                  .Definition, normal));
                        cell.Colspan = 3;
                        tbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Komentarz pracownika: " + surveyQuestion.EmployeeComment, normal));
                        cell.Colspan = 3;
                        tbl.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Komentarz menadżera: " + surveyQuestion.ManagerComment, normal));
                        cell.Colspan = 3;
                        tbl.AddCell(cell);
                    }
                }

                if (cnt > 0)
                {
                    employeeAvg = decimal.Round((decimal)employeeSum / (decimal)cnt, 2);
                    managerAvg = decimal.Round((decimal)managerSum / (decimal)cnt, 2);
                }

                var cell1 = new PdfPCell(new Phrase("Podsumowanie sekcji", bold));
                tbl.AddCell(cell1);

                tbl.AddCell(new Phrase(employeeSum.ToString(), normal));
                tbl.AddCell(new Phrase(managerSum.ToString(), normal));

                cell1 = new PdfPCell(new Phrase("Średnia sekcji", bold));
                tbl.AddCell(cell1);

                tbl.AddCell(new Phrase(employeeAvg.ToString(), normal));
                tbl.AddCell(new Phrase(managerAvg.ToString(), normal));

                doc.Add(tbl);

                doc.Add(Chunk.NEWLINE);
            }

            if(cntTotal > 0)
            {
                employeeTotalAvg = decimal.Round((decimal)employeeTotalSum / (decimal)cntTotal, 2);
                managerTotalAvg = decimal.Round((decimal)managerTotalSum / (decimal)cntTotal, 2);
            }

            PdfPTable tblf = new PdfPTable(new float[] { 0.8f, 0.1f, 0.1f });

            tblf.AddCell(new Phrase("Podsumowanie ankiety", bold));
            tblf.AddCell(new Phrase(employeeTotalSum.ToString(), normal));
            tblf.AddCell(new Phrase(managerTotalSum.ToString(), normal));

            tblf.AddCell(new Phrase("Średnia ankiety", bold));
            tblf.AddCell(new Phrase(employeeTotalAvg.ToString(), normal));
            tblf.AddCell(new Phrase(managerTotalAvg.ToString(), normal));

            doc.Add(tblf);
        }

        private void surveyFooter(Document doc, BrowseSurvey browseSurvey)
        {
            Chunk linebreak = new Chunk(new LineSeparator(0.5f, 100f, GrayColor.GRAY, Element.ALIGN_CENTER, -1));
            doc.Add(linebreak);

            doc.Add(addParagragh("Przyznaję okresową ocenę pracownika:", normal, 58));
            doc.Add(addParagragh(browseSurvey.Survey.HRSummary, normal, 58));

            PdfPTable tbl1 = new PdfPTable(new float[] { 0.33f, 0.33f, 0.34f });
            tbl1.DefaultCell.Border = Rectangle.NO_BORDER;
            PdfPTable tbl2 = new PdfPTable(new float[] { 0.33f, 0.33f, 0.34f });
            tbl2.DefaultCell.Border = Rectangle.NO_BORDER;

            doc.Add(Chunk.NEWLINE);

            var cell = new PdfPCell(new Phrase("........................", normal));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tbl1.AddCell(cell);

            cell = new PdfPCell(new Phrase(".....................", normal));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tbl1.AddCell(cell);

            cell = new PdfPCell(new Phrase("...................................", normal));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tbl1.AddCell(cell);

            cell = new PdfPCell(new Phrase("(miejscowość)", normal));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tbl1.AddCell(cell);

            cell = new PdfPCell(new Phrase("(data)", normal));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tbl1.AddCell(cell);

            cell = new PdfPCell(new Phrase("(podpis oceniającego)", normal));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tbl1.AddCell(cell);

            doc.Add(tbl1);
            
            doc.Add(Chunk.NEWLINE);
            doc.Add(linebreak);

            doc.Add(addParagragh("Zapoznałem(-am) się z oceną", normal, 58));

            doc.Add(Chunk.NEWLINE);

            cell = new PdfPCell(new Phrase("........................", normal));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tbl2.AddCell(cell);

            cell = new PdfPCell(new Phrase(".....................", normal));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tbl2.AddCell(cell);

            cell = new PdfPCell(new Phrase("...................................", normal));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tbl2.AddCell(cell);

            cell = new PdfPCell(new Phrase("(miejscowość)", normal));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tbl2.AddCell(cell);

            cell = new PdfPCell(new Phrase("(data)", normal));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tbl2.AddCell(cell);

            cell = new PdfPCell(new Phrase("(podpis ocenianego)", normal));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tbl2.AddCell(cell);

            doc.Add(tbl2);
        }

        public byte[] CreatePdf(BrowseSurvey browseSurvey, Employee employee, Team team, Position position)
        {
            Document doc = new Document(PageSize.A4, 36, 36, 36, 55);
            MemoryStream ms = new MemoryStream();
            PdfWriter pw = PdfWriter.GetInstance(doc, ms);
            doc.Open();
            pw.PageEvent = new Footer();

            header(doc, employee, team.Name, position.Name);
            surveyHeader(doc, browseSurvey);
            surveyBody(doc, browseSurvey);
            surveyFooter(doc, browseSurvey);

            doc.Close();
            byte[] byteArray = ms.ToArray();
            ms.Flush();
            ms.Close();
            ms.Dispose();

            return byteArray;
        }

        private Paragraph addSlimParagragh(string paragraphText, Font font, float before)
        {
            Paragraph p = new Paragraph(paragraphText, font);
            p.SpacingBefore = 0f;
            p.FirstLineIndent = before;
            p.SpacingAfter = 0f;
            p.Alignment = Element.ALIGN_JUSTIFIED;
            return p;
        }
        private Paragraph addParagragh(string paragraphText, Font font, float before)
        {
            Paragraph p = new Paragraph(paragraphText, font);
            p.SpacingBefore = 10f;
            p.FirstLineIndent = before;
            p.SpacingAfter = 10f;
            p.Alignment = Element.ALIGN_JUSTIFIED;
            return p;
        }
        private Paragraph addParagraphHeader(string headingText, Font font)
        {
            Paragraph p = new Paragraph(headingText, font);
            p.SpacingBefore = 10f;
            p.SpacingAfter = 1f;
            p.Alignment = Element.ALIGN_CENTER;
            return p;
        }
    }
}