using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
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
