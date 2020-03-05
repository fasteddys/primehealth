using HRMS.Utilities;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Helpers
{
  public  class DirectoryIdentity : IIdentity
    {
        #region Internal Fields
        internal string name;
        private string path = "LDAP://DC=Primehealth,DC=local";
        private bool auth;
        #endregion

        #region Constructors
        public DirectoryIdentity(string userName, string password) : this(null, userName, password) { }

        public DirectoryIdentity(string path, string userName, string password)
        {
            DirectoryEntry de = new DirectoryEntry(path, userName, password);
            try
            {
                object o = de.NativeObject;
                DirectorySearcher ds = new DirectorySearcher(de);
                if (userName.Contains("\\"))
                    userName = userName.Substring(userName.IndexOf("\\") + 1);
                ds.Filter = "samaccountname=" + userName;
                ds.PropertiesToLoad.Add("cn");
                ds.PropertiesToLoad.Add("department");

                SearchResult sr = ds.FindOne();
                if (sr == null) throw new Exception();
                this.name = userName;
                this.path = sr.Path;
                auth = true;
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                auth = false;
            }
        }
        #endregion



        #region Properties and Events
        public string AuthenticationType
        {
            get { return null; }
        }

        public string GivenName
        {
            get { return Name.Substring(0, Name.IndexOf(' ')); }
        }

        public bool IsAuthenticated
        {
            get { return auth; }
        }

        public string Name
        {
            get
            {
                int i = path.IndexOf('=') + 1, j = path.IndexOf(',');
                return path.Substring(i, j - i);
            }
        }
        #endregion

    }
}
