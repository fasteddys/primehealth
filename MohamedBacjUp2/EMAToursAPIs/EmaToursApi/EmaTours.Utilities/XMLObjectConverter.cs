﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
//using EmaTours.Entities;

namespace EmaTours.Utilities
{
   public static class XMLObjectConverter
    {

        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {

                var z = value.GetType();

                var properties = GetProperties(value);
                StringBuilder builder = new StringBuilder();
                foreach (var p in properties)
                {
                    var Val = p.GetValue(value, null);
                    var TypeOfProbrtyvalue = Convert.GetTypeCode(Val);

                    if (TypeOfProbrtyvalue != TypeCode.Object)
                    {
                        string name = p.Name;
                        var ValueOFObj = p.GetValue(value, null);

                        builder.AppendFormat("<{1}> {0} </{1}>", p.GetValue(value, null), p.Name);

                    }
                    else
                    {
                        PropertyInfo propertyInfo = value.GetType().GetProperty(p.Name);
                        propertyInfo.SetValue(value, Convert.ChangeType(null, TypeCode.String), null);

                    }

                }
                var Final = "<" + typeof(T).Name + ">" + builder.ToString() + "</" + typeof(T).Name + ">".ToString();
                return Final;



            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

        private static PropertyInfo[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties();
        }

        //public static string Serialize(object obj)
        //{
        //    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
        //    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
        //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //    {
        //        serializer.Serialize(ms, obj);
        //        ms.Position = 0;
        //        xmlDoc.Load(ms);
        //        return xmlDoc.InnerXml;
        //    }
        //}
    }
}
