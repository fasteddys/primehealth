using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Client
{

    public class BaseController : Controller
    {
        public int UserID = -1;
        public  string UserName = "";
        public  string ProfilePictureURL = "";
        public string ApiURL = "";
        public string PictureLocation = "";
        public bool HasCompletedUserProfileData = false;
        public bool IsHr=false;
        public bool IsAdmin=false;
        public bool IsManager = false;
        public BaseController()
        {
            UserID = System.Web.HttpContext.Current?.Request?.Cookies["CypressUserID"]?.Value == null ? -1 : Convert.ToInt32(System.Web.HttpContext.Current?.Request?.Cookies["CypressUserID"]?.Value);
            UserName = System.Web.HttpContext.Current?.Request?.Cookies["CypressUserName"]?.Value == "" ? "" : System.Web.HttpContext.Current?.Request?.Cookies["CypressUserName"]?.Value;
            HasCompletedUserProfileData = System.Web.HttpContext.Current?.Request?.Cookies["HasCompletedUserProfileData"]?.Value == "" ? false : Convert.ToBoolean(System.Web.HttpContext.Current?.Request?.Cookies["HasCompletedUserProfileData"]?.Value);
            ProfilePictureURL = System.Web.HttpContext.Current?.Request?.Cookies["ProfilePictureURL"]?.Value == "" ? "" : System.Web.HttpContext.Current?.Request?.Cookies["ProfilePictureURL"]?.Value.Replace("%2F","/");
            ApiURL =  ConfigurationManager.AppSettings["ApiURL"];
            PictureLocation= ConfigurationManager.AppSettings["PictureLocation"];
            IsHr = System.Web.HttpContext.Current?.Request?.Cookies["IsHr"]?.Value == null ? false : Convert.ToBoolean(System.Web.HttpContext.Current?.Request?.Cookies["IsHr"]?.Value);
            IsAdmin = System.Web.HttpContext.Current?.Request?.Cookies["IsAdmin"]?.Value == null ? false : Convert.ToBoolean(System.Web.HttpContext.Current?.Request?.Cookies["IsAdmin"]?.Value);
            IsManager = System.Web.HttpContext.Current?.Request?.Cookies["IsManager"]?.Value == null ? false : Convert.ToBoolean(System.Web.HttpContext.Current?.Request?.Cookies["IsManager"]?.Value);
        }

    


    }
}