using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace txtToPdf
{
    class makePdf
    {
        public static void toPdf(string fileName, IReadOnlyList<string> input)
        {
            /*
            //A4サイズを横向きで
            Document pdfDocument = new Document(PageSize.A4.Rotate(), 0, 0, 0, 0);

            //出力先のファイル名
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, fileStream);

            //PDFドキュメントを開く
            pdfDocument.Open();
            
            foreach(string list in input)
            {
                pdfDocument.Add(new Paragraph(list));
            }

            //PDFドキュメントを閉じる      
            pdfDocument.Close();
            */


            //A4サイズを横向きで
            Document pdfDocument = new Document(PageSize.A4.Rotate(), 0, 0, 0, 0);

            //出力先のファイル名
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, fileStream);

            //PDFドキュメントを開く
            pdfDocument.Open();

            //フォント名
            string fontName = "ＭＳ ゴシック";

            //フォントサイズ
            float fontSize = 20.0f;

            //フォントスタイル。 複合するときは、&で。
            int fontStyle = iTextSharp.text.Font.BOLD;// &iTextSharp.text.Font.ITALIC;

            //フォントカラー
            BaseColor baseColor = BaseColor.BLACK;

            //Fontフォルダを指定
            FontFactory.RegisterDirectory(Environment.SystemDirectory.Replace("system32", "fonts"));

            //フォントの設定
            iTextSharp.text.Font font =
                FontFactory.GetFont(fontName,
                BaseFont.IDENTITY_H,            //横書き
                BaseFont.NOT_EMBEDDED,          //フォントを組み込まない
                fontSize,
                fontStyle,
                baseColor);

            PdfContentByte pdfContentByte = pdfWriter.DirectContent;

            ColumnText columnText = new ColumnText(pdfContentByte);

            int rangeCounter = 0;
            //SetSimpleColumnで出力
            foreach (string list in input)
            {
                columnText.SetSimpleColumn(
                    new Phrase(list, font)
                    , 50    // X1位置
                    , 50   // Y1位置
                    , 500   // X2位置
                    , 550  - rangeCounter * fontSize   // Y2位置
                    , fontSize
                    , Element.ALIGN_LEFT    //ちなみに、SetSimpleColumnでは、ALIGN_MIDDLE（縦方向の中寄せ）は使えない
                    );

                //テキスト描画
                columnText.Go();
                rangeCounter++;
            }
            //PDFドキュメントを閉じる
            pdfDocument.Close();
        }
    }
}
