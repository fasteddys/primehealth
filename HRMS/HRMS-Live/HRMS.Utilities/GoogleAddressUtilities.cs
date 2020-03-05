using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HRMS.Utilities
{
   public static class GoogleAddressUtilities
    {
        public static string GetAddressFromLocation(double latitude, double longitude,string language)
        {
            string ReverseGeoCodingUriFormat = "https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false&language="+ language + "&key=";
            var requestUri = string.Format(ReverseGeoCodingUriFormat, latitude, longitude);
            string uri = requestUri + ConfigurationManager.AppSettings["GetGoogleMapAPIKey"];
            try
            {
                WebRequest request = WebRequest.Create(requestUri);
                WebResponse response = request.GetResponse();
                XDocument xdoc = XDocument.Load(response.GetResponseStream());

                XElement GeocodeResponse = xdoc.Element("GeocodeResponse");
                XElement status = GeocodeResponse.Element("status");
                string statusString = status.Value;
                if (statusString == "OK")
                {
                    XElement result = GeocodeResponse.Element("result");
                    XElement formatted_address = result.Element("formatted_address");
                    string formatted_addressString = formatted_address.Value;

                    return formatted_addressString;
                }
                else // no address
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
