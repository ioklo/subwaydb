using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SubwayDB
{
    class Validator
    {
        public static bool Check()
        {
            var sis = Program.LoadStationInfos();
            var tt = Program.LoadTimeTable();
            
            // 데이터 로딩
            DataContractJsonSerializer stationLoader = new DataContractJsonSerializer(typeof(TimeTable));

            // 1. 모든 데이터가 stationInfo 상에 있는지
            if (!CheckValidStationCode(sis, tt))
                return false;

            return true;
        }

        private static bool CheckValidStationCode(StationInfos sis, TimeTable tt)
        {
            var stationCodeDic = sis.Infos.ToDictionary(si => si.Code);

            var trainsList = new List<List<TimeTable.TrainData>>() { tt.WeekdayTrains, tt.SaturdayTrains, tt.HolidayTrains };
            foreach (var trains in trainsList)
                foreach (var data in trains)
                    foreach (var entry in data.Entries)
                    {
                        if (!stationCodeDic.ContainsKey(entry.Station))
                            return false;
                    }

            return true;
        }
    }
}
