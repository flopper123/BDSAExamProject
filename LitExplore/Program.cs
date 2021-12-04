using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using LitExplore.Persistence.Entities;

namespace LitExplore
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var v = new Vertex(new Publication{Author = "J. K. Rowling", References = new List<Reference>{}});
            //var pup = v.GetData();
            

            NewMethod(out string root, out string filename, out string path, out PdfReader doc2, out string udd);
        }

        private static void NewMethod(out string root, out string filename, out string path, out PdfReader? doc2,
            out string? udd)
        {
            root = Directory.GetCurrentDirectory();
            filename = "MadsProject.pdf";
            path = root + "/" + filename;
            doc2 = new PdfReader(path);

            udd = PdfTextExtractor.GetTextFromPage(doc2, 4);

            Console.WriteLine(udd);


            var Chapter = "References";

            var ReferenceNumber = @"(?<refnumber>\[[0-9]+\])";

            var Author = @"\G[A-z]+";

            //var regex = new Regex(Reference);

            var works_on_current_ref =
                @"(?<wholeref>(?<ref>\[[0-9]\]).(?<ath>[A-z]\. [A-z]+\.) (?<title>[A-z]+\W[A-z]*'*\w \w* \w* \w* \w*\.) [A-z]*\s*\w*\, (?<year>\d*\.))";

            var letter = "e";

            var match = Regex.Match(udd,works_on_current_ref);
            
            
        }
    }
}
