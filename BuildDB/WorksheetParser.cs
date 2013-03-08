using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace SubwayDB
{
    class WorksheetParser
    {
        internal enum DayKind
        {
            Weekday,
            Saturday,
            Holiday,
            SaturdayAndHoliday,
        }

        private string Normalize(string text)
        {
            return text.Replace(" ", "");
        }        

        protected void ParseWorksheet(TimeTable tt, Worksheet sheet, string stationNameRange, string timeTableRange, DayKind kind = DayKind.Weekday)
        {
            try
            {
                // 역 이름들이 적혀있는 행을 얻어온다.
                List<string> stationNames = new List<string>();
                var nameRow = sheet.Range[stationNameRange];
                var nameV2 = nameRow.Value2;
                int nameRowLen = nameV2.GetLength(0);
                for (int t = 1; t <= nameRowLen; t += 2)
                {
                    var name = Normalize(nameV2[t, 1].ToString());
                    stationNames.Add(name);
                }

                // 내가 하고 싶은건
                // B 부터 HM까지 
                var range = sheet.Range[timeTableRange];
                var v2 = range.Value2;
                // 1번부터 90번까지

                // select container
                List<TimeTable.TrainData> container = null;
                switch (kind)
                {
                    case DayKind.Weekday:
                        container = tt.WeekdayTrains;
                        break;

                    case DayKind.Saturday:
                        container = tt.SaturdayTrains;
                        break;

                    case DayKind.Holiday:
                        container = tt.HolidayTrains;
                        break;

                    case DayKind.SaturdayAndHoliday:
                        container = new List<TimeTable.TrainData>();
                        break;

                    default:
                        Debug.Assert(false);
                        break;
                }

                int nRow = v2.GetLength(0);
                int nCol = v2.GetLength(1);
                for (int j = 0; j < nCol; j++)
                {
                    TimeTable.TrainData data = new TimeTable.TrainData();
                    data.Name = v2[1, j + 1].ToString();

                    for (int i = 4; i < nRow; i += 2)
                    {
                        var arrV = v2[i + 1, j + 1];
                        var depV = v2[i + 2, j + 1];

                        if (arrV == null && depV == null) continue;

                        var entry = new TimeTable.TrainData.Entry() { Station = stationNames[(i - 4) / 2] };

                        if (arrV != null && arrV is double)
                            entry.Arrival = TimeSpan.FromDays(arrV);

                        if (depV != null && depV is double)
                            entry.Departure = TimeSpan.FromDays(depV);

                        data.Entries.Add(entry);
                    }

                    container.Add(data);
                }

                if (kind == DayKind.SaturdayAndHoliday)
                {
                    tt.SaturdayTrains.AddRange(container);
                    tt.HolidayTrains.AddRange(container);
                }
            }
            catch(Exception e)
            {
                throw new ParseException("내부 에러", e);
            }
        }
    }
}
