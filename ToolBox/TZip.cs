using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace ToolBox
{
    public static class TZip
    {
        public static bool UnZip(string zipFile, string path)
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (!path.EndsWith("\\") && !path.EndsWith("/")) path += "\\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            using (ZipStorer zip = ZipStorer.Open(zipFile, FileAccess.Read))
            {
                List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir(); //获取zip文件内所有文件路径
                foreach (ZipStorer.ZipFileEntry entry in dir)
                {
                    if (!zip.ExtractFile(entry, path + entry.FilenameInZip)) return false; //存在没有解压成功的
                }
                zip.Close();
            }
            return true;
        }
    }
}
