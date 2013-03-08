using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace SubwayDB
{
    class ParseLine4 : WorksheetParser
    {
        string Normalize(string text)
        {
            return text.Replace(" ", "");
        }

        public void Parse(Application app, TimeTable tt)
        {            
            var workbook = app.Workbooks.Open(new FileInfo(Path.Combine(Program.dataDir,@"line4(130301).xlsx")).FullName);
            ParseWorksheet(tt, workbook.Worksheets["평일하선"], "A5:A99", "B1:IW100");
            ParseWorksheet(tt, workbook.Worksheets["평일상선"], "A5:A99", "B1:IX100");
            ParseWorksheet(tt, workbook.Worksheets["토휴일하선"], "A5:A99", "B1:HJ100", DayKind.SaturdayAndHoliday);
            ParseWorksheet(tt, workbook.Worksheets["토휴일상선"], "A5:A99", "B1:HI100", DayKind.SaturdayAndHoliday);
            workbook.Close();
        }
    }
}
