using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DispatchingMVCVer1.Models
{
    public class ReportModels
    {

        public class WorldModel
        {

            private string m_NumOfBocklets;
            private string m_ClaimsType;
            private string m_StartClaims;
            private string m_EndClaims;
            private string m_ProviderName;
            private string m_ClosedDate;


            public string NumOfBocklets
            {
                get { return m_NumOfBocklets; }

                set { value = m_NumOfBocklets; }
            }

            public string ClaimsType
            {
                get { return m_ClaimsType; }

                set { value = m_ClaimsType; }
            }

            public string Year
            {
                get { return m_StartClaims; }

                set { value = m_StartClaims; }
            }

            public string EndClaims
            {
                get { return m_EndClaims; }

                set { value = m_EndClaims; }
            }
            public string ClosedDate
            {
                get { return m_ClosedDate; }

                set { value = m_ClosedDate; }
            }
            public string ProviderName
            {
                get { return m_ProviderName; }

                set { value = m_ProviderName; }
            }
            public WorldModel() { }

            public WorldModel(string numofbocklets, string claimstype, string startclaims, string endclaims,string closeddate , string providername )
            {
                m_NumOfBocklets = numofbocklets;
                m_StartClaims = startclaims;
                m_ClaimsType = claimstype;
                m_EndClaims = endclaims;
                m_ProviderName = providername;
                m_ClosedDate = closeddate;
            }
        }
    }
}