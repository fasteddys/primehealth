﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InHouseSendApproval.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("InHouseSendApproval.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://10.1.2.5/EWS/Exchange.asmx.
        /// </summary>
        internal static string ExchangeWebServicesUri {
            get {
                return ResourceManager.GetString("ExchangeWebServicesUri", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap FailedIcon {
            get {
                object obj = ResourceManager.GetObject("FailedIcon", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0.
        /// </summary>
        internal static string IsOnline {
            get {
                return ResourceManager.GetString("IsOnline", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to primehealth.local.
        /// </summary>
        internal static string MailDomain {
            get {
                return ResourceManager.GetString("MailDomain", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to P@ssw0rd@asdf.
        /// </summary>
        internal static string MailPassword {
            get {
                return ResourceManager.GetString("MailPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mohamed.Abdelalem@medgulf.com.eg.
        /// </summary>
        internal static string MailSender {
            get {
                return ResourceManager.GetString("MailSender", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mohamed.Abdelalem.
        /// </summary>
        internal static string MailUserName {
            get {
                return ResourceManager.GetString("MailUserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap SendEmail {
            get {
                object obj = ResourceManager.GetObject("SendEmail", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap SuccessIcon {
            get {
                object obj = ResourceManager.GetObject("SuccessIcon", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}