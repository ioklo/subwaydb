using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace SubwayDB
{
    class ParseLine2 : WorksheetParser
    {
        public void Parse(Application app, TimeTable tt)
        {   
            var workbook = app.Workbooks.Open(new FileInfo(Path.Combine(Program.dataDir, "2호선 본선외선(13.03.01).xlsx")).FullName);
            ParseWorksheet(tt, workbook.Worksheets["평일외선(1)"], "A5:A91", "B1:IQ92");
            ParseWorksheet(tt, workbook.Worksheets["평일외선(2)"], "A5:A91", "B1:AF92");
            ParseWorksheet(tt, workbook.Worksheets["토요일외선"], "A5:A91", "B1:HG92", DayKind.Saturday);
            ParseWorksheet(tt, workbook.Worksheets["토요일외선1"], "A5:A91", "B1:X92", DayKind.Saturday);
            ParseWorksheet(tt, workbook.Worksheets["휴일외선"], "A5:A91", "B1:HF92", DayKind.Holiday);
            workbook.Close();

            
            var workbook2 = app.Workbooks.Open(new FileInfo(Path.Combine(Program.dataDir, "2호선 본선내선(13.03.01).xlsx")).FullName);
            ParseWorksheet(tt, workbook2.Worksheets["평일내선(1)"], "A5:A91", "B1:IE92");
            ParseWorksheet(tt, workbook2.Worksheets["평일내선(2)"], "A5:A91", "B1:AK92");
            ParseWorksheet(tt, workbook2.Worksheets["토요일내선"], "A5:A91", "B1:HM92", DayKind.Saturday);
            ParseWorksheet(tt, workbook2.Worksheets["토요일내선1"], "A5:A91", "B1:X92", DayKind.Saturday);
            ParseWorksheet(tt, workbook2.Worksheets["휴일내선"], "A5:A91", "B1:HD92", DayKind.Weekday);
            workbook2.Close();
        }
    }
}
