using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox
{
    /// <summary>
    /// 路径处理
    /// </summary>
    public static class TPath
    {
        /// <summary>
        /// 获取路径下所有文件以及子文件夹中文件  
        /// </summary>
        /// <param name="path">绝对路径</param>
        /// <param name="recursive">是否包含子目录</param> 
        /// <returns></returns>
        public static List<string> GetFilePaths(string path, bool recursive = false)
        {
            return GetFilePaths(path, new List<string>(), recursive);
        }

        /// <summary>  
        /// 获取路径下所有文件以及子文件夹中文件  
        /// </summary>  
        /// <param name="path">全路径根目录</param>  
        /// <param name="fileList">存放所有文件的全路径</param> 
        /// <param name="recursive">是否包含子目录</param> 
        /// <returns></returns>  
        private static List<string> GetFilePaths(string path, List<string> fileList, bool recursive = false)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles();
            foreach (FileInfo f in fil)
            {
                //int size = Convert.ToInt32(f.Length);  
                //long size = f.Length;
                fileList.Add(f.FullName);//添加文件路径到列表中  
            }

            if(recursive)
            {
                DirectoryInfo[] dii = dir.GetDirectories();
                //获取子文件夹内的文件列表，递归遍历  
                foreach (DirectoryInfo d in dii)
                {
                    GetFilePaths(d.FullName, fileList);
                }
            }

            return fileList;
        }
    }
}
