using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SubwayDB
{
    // 테이블
    // (열차번호, (역번호, 도착 시간, 출발시간) 리스트) 리스트
    [DataContract]
    public class TimeTable
    {
        [DataContract]
        public class TrainData
        {
            [DataContract]
            public class Entry
            {
                [DataMember]
                public string Station { get; set; }

                [DataMember]
                public TimeSpan? Arrival { get; set; }

                [DataMember]
                public TimeSpan? Departure { get; set; }
            }

            [DataMember]
            public string Name { get; set; }

            [DataMember]
            public List<Entry> Entries { get; private set; }

            public TrainData()
            {
                Entries = new List<Entry>();
            }
        }

        [DataMember]
        public List<TrainData> WeekdayTrains { get; private set; }

        [DataMember]
        public List<TrainData> SaturdayTrains { get; private set; }

        [DataMember]
        public List<TrainData> HolidayTrains { get; private set; }

        public TimeTable()
        {
            WeekdayTrains = new List<TrainData>();
            SaturdayTrains = new List<TrainData>();
            HolidayTrains = new List<TrainData>();
        }
    }
}
