using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace ToolBox
{
    /// <summary>
    /// zip压缩解压
    /// </summary>
    public static class TZip
    {
        /// <summary>
        /// zip解压
        /// </summary>
        /// <param name="zipPath">zip文件路径</param>
        /// <param name="outPath">解压路径</param>
        /// <returns>是否解压成功</returns>
        public static bool UnZip(string zipPath, string outPath)
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (!outPath.EndsWith("\\") && !outPath.EndsWith("/")) outPath += "\\";
            if (!Directory.Exists(outPath)) Directory.CreateDirectory(outPath);
            using (ZipStorer zip = ZipStorer.Open(zipPath, FileAccess.Read))
            {
                List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir(); //获取zip文件内所有文件路径
                foreach (ZipStorer.ZipFileEntry entry in dir)
                {
                    if (!zip.ExtractFile(entry, outPath + entry.FilenameInZip)) return false; //存在没有解压成功的
                }
                zip.Close();
            }
            return true;
        }

        /// <summary>
        /// zip压缩
        /// </summary>
        /// <param name="paths">文件绝对路径</param>
        /// <param name="zipPath">zip文件绝对路径</param>
        /// <returns>是否压缩成功</returns>
        public static bool CreateZip(List<string> paths, string zipPath)
        {
            using (ZipStorer zip = ZipStorer.Create(zipPath, string.Empty))
            {
                List<Tuple<string,string>> exPathWithRels = new List<Tuple<string, string>>(); //绝对路径 zip中的相对路径
                foreach(string path in paths)
                { //参数中所有路径
                    if(File.Exists(path))
                    { //文件
                        zip.AddFile(ZipStorer.Compression.Deflate, path, Path.GetFileName(path), string.Empty);
                    }
                    else if(Directory.Exists(path))
                    { //文件夹
                        int pathLength = Path.GetDirectoryName(path).Length; //获取目录长度
                        List<string> exPaths = TPath.GetFilePaths(path); //获取文件夹中所有文件 包含子目录中文件
                        foreach(string exPath in exPaths)
                        {
                            exPathWithRels.Add(Tuple.Create(exPath, exPath.Remove(0, pathLength)));
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("路径非法：" + path);
                    }
                }
                foreach (var path in exPathWithRels)
                { 
                    if (File.Exists(path.Item1))
                    { //文件夹中文件路径
                        zip.AddFile(ZipStorer.Compression.Deflate, path.Item1, path.Item2, string.Empty);
                    }
                    else
                    {
                        throw new InvalidOperationException("路径非法：" + path);
                    }
                }
            }
            return true;
        }
    }
}
