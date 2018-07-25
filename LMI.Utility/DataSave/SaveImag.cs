using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


namespace LMI.Utility
{
    /// <summary>
    /// Save Image 
    /// </summary>
    public class SaveImag
    {
        /// <summary>
        /// Save Halcon Format Image
        /// </summary>
        /// <param name="image">Halcon Image</param>
        /// <param name="format">Halcon Image Format</param>
        /// <param name="Folder">Folder Path</param>
        /// <param name="SN">Image Name</param>
        //public static void SaveImage(HImage image, string format, string Folder,string SN)
        //{
        //    var time    = DateTime.Now;
        //    var strDir  = "D:\\Image";
        //    var sb      = new StringBuilder();

        //    try
        //    {
        //        sb.AppendFormat("{0}\\{1}\\{2}\\", strDir, Folder, time.ToString("yyyy-MM-dd"));

        //        if (!Directory.Exists(sb.ToString()))
        //        {
        //            Directory.CreateDirectory(sb.ToString());
        //        }
        //        sb.AppendFormat("{0}", SN + time.ToString("-MMdd_HH_mm_ss"));
        //        image.WriteImage(format, 0, sb.ToString());
        //        Trace.TraceInformation("{0} :SaveImage finish", Folder);
        //    }
        //    catch(Exception ex)
        //    {
        //        Trace.TraceError("{0}-SaveImage fail, ex:{1}", Folder, ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Save Cognex Format Image
        ///// </summary>
        ///// <param name="image"></param>
        ///// <param name="format"></param>
        ///// <param name="Folder"></param>
        ///// <param name="SN"></param>
        //public static void SaveCogImage(ICogImage image, string format, string Folder, string SN)
        //{
        //    var time = DateTime.Now;
        //    var strDir = "D:\\Image";
        //    var sb = new StringBuilder();
        //    try
        //    {
        //        sb.AppendFormat("{0}\\{1}\\{2}\\", strDir, Folder, time.ToString("yyyy-MM-dd"));

        //        if (!Directory.Exists(sb.ToString()))
        //        {
        //            Directory.CreateDirectory(sb.ToString());
        //        }
        //        sb.AppendFormat("{0}", SN + time.ToString("-MMdd_HH_mm_ss") + "." + format);
        //        CogImageFileBMP cogImageFileBmp = new CogImageFileBMP();
        //        cogImageFileBmp.Open(sb.ToString(), CogImageFileModeConstants.Write);
        //        cogImageFileBmp.Append(image);
        //        cogImageFileBmp.Close();
        //        Trace.TraceInformation("{0} :SaveImage finish", Folder);
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.TraceError("{0}-SaveImage fail, ex:{1}", Folder, ex.Message);
        //    }
        //}

        /// <summary>
        /// Save Bitmap
        /// </summary>
        /// <param name="image"></param>
        /// <param name="Folder"></param>
        /// <param name="SN"></param>
        //public static void SaveBitmap(Bitmap image, string Folder, string SN)
        //{
        //    var time = DateTime.Now;
        //    var strDir = "D:\\Image";
        //    var sb = new StringBuilder();

        //    try
        //    {
        //        sb.AppendFormat("{0}\\{1}\\{2}\\", strDir, Folder, time.ToString("yyyy-MM-dd"));

        //        if (!Directory.Exists(sb.ToString()))
        //        {
        //            Directory.CreateDirectory(sb.ToString());
        //        }
        //        sb.AppendFormat("{0}", SN + time.ToString("-MMdd_HH_mm_ss")+ ".jpg");
        //        image.Save(sb.ToString(), ImageFormat.Jpeg);
        //        Trace.TraceInformation("{0} :SaveImage finish", Folder);
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.TraceError("{0}-SaveImage fail, ex:{1}", Folder, ex.Message);
        //    }
        //}
    }
}
