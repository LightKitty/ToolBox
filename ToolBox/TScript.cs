using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox
{
    public static class TScript
    {
        /// <summary>
        /// 运行不抛出异常，对异常进行记录
        /// </summary>
        /// <param name="action"></param>
        public static void SafeRun(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 运行不抛出异常，对异常进行记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T SafeRun<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(T);
            }
        }

        /// <summary>
        /// 运行并忽略异常
        /// </summary>
        /// <param name="action"></param>
        public static void RunIgnoreException(Action action)
        {
            try
            {
                action();
            }
            catch { }
        }

        /// <summary>
        /// 运行并忽略异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T RunIgnoreException<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch
            {
                return default(T);
            }
        }
    }
}
