using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS13 {
    class Program {
        static void Main(string[] args) {
            //
            try {
                //Create a gateway
                SharePointSearchServiceGateway gateway = new SharePointSearchServiceGateway(global::VS13.Properties.Settings.Default.SharePointQueryService,ResultProvider.Default);

                //
                //Console.WriteLine("Portal Search Info -------------------------");
                //Console.WriteLine(gateway.GetPortalSearchInfo());
                //Console.WriteLine("");

                Console.WriteLine("Search Metadata -------------------------");
                DataSet ds = gateway.GetSearchMetadata();

                Console.WriteLine(gateway.GetSearchMetadata().GetXml());
                Console.ReadLine();

                Console.WriteLine("QueryEx -------------------------");
                string[] properties = new string[] { "BOLNumOWSTEXT","BatchDateOWSDATE","Created" };
                string queryPacket = gateway.GetSTRINGQueryPacket("88888888*",properties);
                Console.WriteLine(gateway.QueryEx(queryPacket).GetXml());
                Console.ReadLine();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); Console.ReadLine(); }
        }
    }
}
