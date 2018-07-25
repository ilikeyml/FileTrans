using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace LMI.Utility
{
    /// <summary>
    /// Enumerate File Operation Status
    /// </summary>
    public enum FileOperationResult
    {
        OK,
        NOTEXIST,
        WRITEERROR
    }

    /// <summary>
    /// Construct SerilizeFileTool Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SerializeFileTool<T>
    {
        /// <summary>
        /// Get Content of XML file
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <returns>Content</returns>
        public T GetConfig(string fileName)
        {
            T local = default(T);
            byte[] bStr = null;
            if (this.ReadBytes(fileName, out bStr) == FileOperationResult.OK)
            {
                local = SerializeTool<T>.XmlSerializerDeserialize(bStr);
            }
            return local;
        }


        /// <summary>
        /// Read content from file
        /// </summary>
        /// <param name="path">File Path</param>
        /// <param name="bStr">byte array of file content</param>
        /// <returns></returns>
        private FileOperationResult ReadBytes(string path, out byte[] bStr)
        {
            bStr = null;
            if (!File.Exists(path))
            {
                return FileOperationResult.NOTEXIST;
            }
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                {
                    bStr = new byte[stream.Length];
                    stream.Read(bStr, 0, bStr.Length);
                }
            }
            catch
            {
                return FileOperationResult.WRITEERROR;
            }
            return FileOperationResult.OK;
        }


        /// <summary>
        /// Set content to the XML file 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="config"></param>
        public void SetConfig(string fileName, T config)
        {
            WriteBytes(fileName, FileMode.Create, SerializeTool<T>.XmlSerializerSerialize(config));
        }


        /// <summary>
        /// Write bytes content to the file
        /// </summary>
        /// <param name="path">file path</param>
        /// <param name="filemode">FileOperationResult</param>
        /// <param name="bStr">Byte array</param>
        /// <returns></returns>
        private FileOperationResult WriteBytes(string path, FileMode filemode, byte[] bStr)
        {
            try
            {
                using (FileStream stream = new FileStream(path, filemode, FileAccess.ReadWrite))
                {
                    stream.Write(bStr, 0, bStr.Length);
                }
            }
            catch (Exception)
            {
                return FileOperationResult.WRITEERROR;
            }
            return FileOperationResult.OK;
        }


        /// <summary>
        /// write string to the file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filemode"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private FileOperationResult WriteString(string path, FileMode filemode, string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return this.WriteBytes(path, filemode, bytes);
        }
    }
}
