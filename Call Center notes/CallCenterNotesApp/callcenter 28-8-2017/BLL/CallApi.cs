using CallCenterNotesApp.DLL;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientSample
{


    public class CallApi
    {
        static HttpClient client = new HttpClient();



        public static async Task<EmailApprovalRequest> EmailTracking(EmailApprovalRequest request, bool isclosed)
        {
            try
            {
                using (PhNetworkEntities db = new PhNetworkEntities())
                {
                    string path = db.EmailApprovalsConfigurations.Where(p=>p.ConfigurationKey== "TrackingApiUrl").FirstOrDefault().ConfigurationValue.ToString()+ "api/EmailTracking/AutoGeneratedEmailTracking" + "?AutoGeneratedEmailId=" + request.AutoGeneratedEmailID + "&CallCenterMailType=" + request.TicketTypeID + "&IsClosed=" + isclosed;

                    string Result = await client.GetStringAsync(path);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}