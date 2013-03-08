using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace SubwayDB
{
    class Program
    {
        internal static string outputDir = @"..\Output";
        internal static string dataDir = @"..\Data";

        internal static string stationInfoFile = Path.Combine(dataDir, @"역명지하철역검색기능.json");
        internal static string timeTableFile = Path.Combine(outputDir, @"TimeTable.json");

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("지하철 데이터 파싱을 시작합니다..");
                Parse();                
                Console.WriteLine(" - 파싱 완료");

                Console.WriteLine();

                Console.WriteLine("만들어진 데이터가 제대로 된 것인지 검사를 시작합니다...");
                if (!Validator.Check())
                    Console.WriteLine(" - 잘못된 데이터입니다");
                else
                    Console.WriteLine(" - 확인 완료");
            }
            catch (ParseException pe)
            {
                Console.WriteLine(" - 파싱 실패:" + pe.Message);
                return;
            }
            
        }

        private static void Parse()
        {
            Microsoft.Office.Interop.Excel.Application app = null;

            try
            {
                app = new Excel.Application();
                app.Visible = false;

                TimeTable tt = new TimeTable();

                var parser2 = new ParseLine2();
                parser2.Parse(app, tt);               
                
                var parser3 = new ParseLine3();
                parser3.Parse(app, tt);                    

                var parser4 = new ParseLine4();
                parser4.Parse(app, tt);

                // 저장
                SaveTimeTable(tt);
            }
            finally
            {
                if (app != null)
                {
                    app.UserControl = true;
                    app.Quit();

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        internal static StationInfos LoadStationInfos()
        {
            DataContractJsonSerializer stationLoader = new DataContractJsonSerializer(typeof(StationInfos));

            StationInfos ssi = null;
            using (var stream = new FileStream(stationInfoFile, FileMode.Open))
            {
                ssi = (StationInfos)stationLoader.ReadObject(stream);
            }
            return ssi;
        }

        internal static void SaveTimeTable(TimeTable tt)
        {
            var setting = new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = true };
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(TimeTable), setting);

            // outputDir이 항상 만들어지도록
            Directory.CreateDirectory(outputDir);
            
            using (var stream = new FileStream(timeTableFile, FileMode.Create))
            {
                serializer.WriteObject(stream, tt);
            }
        }

        internal static TimeTable LoadTimeTable()
        {
            var setting = new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = true };
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(TimeTable), setting);

            TimeTable tt = null;
            using (var stream = new FileStream(timeTableFile, FileMode.Open))
            {
                tt = (TimeTable)serializer.ReadObject(stream);
            }

            return tt;
        }
    }
}
