using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic.PDF
{
    public partial class Footer : PdfPageEventHelper
    {
        PdfContentByte cb;
        PdfTemplate template;
        DateTime PrintTime = DateTime.Now;
        BaseFont bf = null;
        int cnt = 0;

        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            if (cnt == 0)
            {
                string calibripath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\calibri.ttf";
                BaseFont calibri = BaseFont.CreateFont(calibripath, BaseFont.CP1250, BaseFont.EMBEDDED);
                PrintTime = DateTime.Now;
                bf = calibri;
                cb = writer.DirectContent;
                template = cb.CreateTemplate(50, 50);
            }

            int pageN = writer.PageNumber;
            String text = "Strona " + pageN + " z ";
            float len = bf.GetWidthPoint(text, 6);
            Rectangle pageSize = doc.PageSize;
            cb.SetRGBColorFill(100, 100, 100);
            cb.BeginText();
            cb.SetFontAndSize(bf, 6);
            cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(25));
            cb.ShowText(text);
            cb.EndText();
            cb.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(25));

            cb.BeginText();
            cb.SetFontAndSize(bf, 6);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT,
            "Wydrukowano " + PrintTime.ToShortDateString(),
            pageSize.GetRight(40),
            pageSize.GetBottom(25), 0);
            cb.EndText();

            cnt++;
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            template.BeginText();
            template.SetFontAndSize(bf, 6);
            template.SetTextMatrix(0, 0);
            template.ShowText("" + (writer.PageNumber));
            template.EndText();
        }


    }
}