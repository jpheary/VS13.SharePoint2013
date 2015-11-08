//	File:	SharePointGateway.cs
//	Author:	J. Heary
//	Date:	10/10/2014
//	Desc:	Gateway to SharePoint search web service.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VS13.rgxshpntnew;

namespace VS13 {
    //
    public enum QueryType {
        STRING,             //Indicates that the kind of query is keyword query (KQL).
        MSSQLFT,            //Indicates that the type of query is SQL Full Text Syntax query.
        FQL                 //Indicates that the type of query is FAST Query Language (FQL).
    }
    public enum ResultProvider {
        Default,            //The default result provider that is configured by using the administrative interfaces.
        SharePointSearch,   //Specifies the SharePoint Server search provider.
        FASTSearch          //Specifies the FAST Search Server for SharePoint provider.
    }

    class SharePointSearchServiceGateway {
        //Members
        private string mQueryServiceUrl = "";
        private ResultProvider mResultProvider = ResultProvider.Default;

        //Interface
        public SharePointSearchServiceGateway(string queryServiceUrl,ResultProvider rp) { 
            //Constructor
            this.mQueryServiceUrl = queryServiceUrl;
            this.mResultProvider = rp;
        }
        public string QueryServiceUrl { get { return this.mQueryServiceUrl; } }
        public string GetPortalSearchInfo() {
            //
            string info = "";
            try {
                rgxshpntnew.QueryService qs = getQueryService();
                info = qs.GetPortalSearchInfo();
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return info;
        }
        public DataSet GetSearchMetadata() {
            //
            DataSet ds = new DataSet();
            try {
                rgxshpntnew.QueryService qs = getQueryService();
                ds = qs.GetSearchMetadata();
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return ds;
        }
        public DataSet QueryEx(string queryPacket) {
            //
            DataSet result = new DataSet();
            try {
                rgxshpntnew.QueryService qs = getQueryService();
                result = qs.QueryEx(queryPacket);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return result;
        }
        public string GetSTRINGQueryPacket(string keywordQueryText, string[] properties) {
            //
            string queryPacket = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            queryPacket += "<QueryPacket Revision=\"1000\">";
            queryPacket += "<Query>";
            queryPacket += "<Context><QueryText language=\"en-US\" type=\"STRING\">" + keywordQueryText + "</QueryText></Context>";
            queryPacket += "<ResultProvider>" + this.mResultProvider.ToString() + "</ResultProvider>";
            queryPacket += "<Range><StartAt>1</StartAt><Count>20</Count></Range>";
            queryPacket += "<Properties>";
            //queryPacket += "<Property name=\"scope\" />";
            queryPacket += "<Property name=\"contentclass\" />";
            queryPacket += "<Property name=\"IsDocument\" />";
            queryPacket += "<Property name=\"DAV:displayname\" />";
            queryPacket += "<Property name=\"DAV:href\" />";
            for (int i = 0;i < properties.Length;i++) {
                queryPacket += "<Property name=\"" + properties[i] + "\" />";
            }
            queryPacket += "</Properties>";
            queryPacket += "</Query>";
            queryPacket += "</QueryPacket>";
            return queryPacket;
        }

        private QueryService getQueryService() {
            //Create query service
            QueryService qs = new QueryService();
            qs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            qs.PreAuthenticate = true;
            qs.Timeout = 30000;    //Default 60000 msec
            qs.Url = this.mQueryServiceUrl;
            return qs;
        }
    }
}
