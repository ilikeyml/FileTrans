

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;

namespace LMI.Utility
{
    public class SerializeTool<T>
    {
        /// <summary>
        /// Deserialize byte data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T SoapFormatterDeserialize(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                SoapFormatter formatter = new SoapFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        ///  Serialize object data to byte data
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SoapFormatterSerialize(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new SoapFormatter().Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Deserialize from XML Serializer
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T XmlSerializerDeserialize(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// Serialize XML Serializer Content
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] XmlSerializerSerialize(T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new XmlSerializer(typeof(T)).Serialize((Stream)stream, obj);
                return stream.ToArray();
            }
        }
    }
}
