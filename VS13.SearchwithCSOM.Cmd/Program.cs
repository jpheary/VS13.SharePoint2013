using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search;
using Microsoft.SharePoint.Client.Search.Query;

namespace VS13 {
    //
    class Program {
        //
        static void Main(string[] args) {
            //
            using (ClientContext context = new ClientContext(global::VS13.Properties.Settings.Default.SharePointServerUrl)) {
                //
                KeywordQuery query = new KeywordQuery(context);
                query.StartRow = 0;
                query.RowLimit = 500;
                query.EnableStemming = true;
                query.TrimDuplicates = false;
                //query.QueryText = "tiff";
                query.QueryText = "scope:\"All Sites\" AND contentclass:\"STS_ListItem_DocumentLibrary\" AND IsDocument:\"True\" AND Created:2013-08-05";
                query.SelectProperties.Add("scope");
                query.SelectProperties.Add("contentclass");
                query.SelectProperties.Add("IsDocument");
                query.SelectProperties.Add("DAV:displayname");
                query.SelectProperties.Add("DAV:href");
                query.SelectProperties.Add("TBBARCODEOWSTEXT");
                query.SelectProperties.Add("BatchDateOWSDATE");
                query.SelectProperties.Add("Created");

                ClientResult<ResultTableCollection> results = new SearchExecutor(context).ExecuteQuery(query);
                context.ExecuteQuery();
                foreach (IDictionary result in results.Value[0].ResultRows) {
                    Console.WriteLine("{0}: {1}","scope",result["scope"]);
                    Console.WriteLine("{0}: {1}","contentclass",result["contentclass"]);
                    Console.WriteLine("{0}: {1}","IsDocument",result["IsDocument"]);
                    Console.WriteLine("{0}: {1}","DAV:displayname",result["DAV:displayname"]);
                    Console.WriteLine("{0}: {1}","DAV:href",result["DAV:href"]);
                    Console.WriteLine("{0}: {1}","BOLNumOWSTEXT",result["BOLNumOWSTEXT"]);
                    Console.WriteLine("{0}: {1}","BatchDateOWSDATE",result["BatchDateOWSDATE"]);
                    Console.WriteLine("{0}: {1}","Created",result["Created"]);
                    Console.WriteLine("{0}: {1}","Title",result["Title"]);
                    Console.WriteLine("{0}: {1}","Path",result["Path"]);
                    Console.WriteLine("{0}: {1}","Description",result["Description"]);
                }
                Console.ReadLine();
            }
        }
    }
}
