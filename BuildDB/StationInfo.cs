using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SubwayDB
{
    [DataContract]
    public class StationInfo
    {
        [DataMember(Name = "STATION_NM")]
        public string Name { get; set; }

        // 역 코드
        [DataMember(Name = "STATION_CD")]
        public string Code { get; set; }

        // 외부 코드        
        [DataMember(Name = "FR_CODE")]
        public string ExtCode { get; set; }

        // 사이버스테이션 검색용 코드 (환승역끼리는 같은 코드를 쓰고 있어서 환승역 정보를 알아낼때 쓸 수 있다)
        [DataMember(Name = "CYBER_ST_CODE")]
        public string RepresentativeCode { get; set; }

        [DataMember(Name = "LINE_NUM")]
        public string Line { get; set; }
    }

    [DataContract]
    public class StationInfos
    {
        [DataMember(Name = "DATA")]
        public List<StationInfo> Infos { get; private set; }

        public StationInfos()
        {
            Infos = new List<StationInfo>();
        }

        static StationInfos()
        {

        }
    }
}
