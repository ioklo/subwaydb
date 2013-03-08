using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace SubwayDB
{
    class ParseLine3 : WorksheetParser
    {
        public void Parse(Application app, TimeTable tt)
        {
            var workbook = app.Workbooks.Open(new FileInfo(Path.Combine(Program.dataDir, @"line3(130301).xls")).FullName);

            ParseWorksheet(tt, workbook.Worksheets["평일하선"], "A5:A89", "B1:HJ90");
            ParseWorksheet(tt, workbook.Worksheets["평일상선"], "A5:A89", "B1:HL90");
            ParseWorksheet(tt, workbook.Worksheets["토휴하선"], "A5:A89", "B1:GF90", DayKind.SaturdayAndHoliday);
            ParseWorksheet(tt, workbook.Worksheets["토휴상선"], "A5:A89", "B1:GG90", DayKind.SaturdayAndHoliday);

            workbook.Close();
        }
    }
}
