using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.DataStructure
{
    /// <summary>
    /// 顺序表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SeqList<T>:IListDS<T>
    {
        /// <summary>
        /// 顺序表的容量
        /// </summary>
        private int maxsize;

        /// <summary>
        /// 数组，用于存储顺序表中的数据元素
        /// </summary>
        private T[] data;

        /// <summary>
        /// 指示顺序表最后一个元素的位置
        /// </summary>
        private int last;

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                data[index] = value;
            }
        }

        /// <summary>
        /// 最后一个数据元素位置属性
        /// </summary>
        public int Last
        {
            get
            {
                return last;
            }
        }

        /// <summary>
        /// 容量
        /// </summary>
        public int Maxsize
        {
            get { return maxsize; }
            set { maxsize = value; }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="size"></param>
        public SeqList(int size)
        {
            data = new T[size];
            maxsize = size;
            last = -1;
        }

        /// <summary>
        /// 求顺序表的长度
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return last + 1;
        }

        /// <summary>
        /// 清空顺序表
        /// </summary>
        public void Clear()
        {
            last = -1;
        }

        /// <summary>
        /// 判断顺序表是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (last == -1) return true;
            else return false;
        }

        /// <summary>
        /// 判断顺序表是否为满
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            if (last == maxsize - 1) return true;
            else return false;
        }

        /// <summary>
        /// 在顺序表的末尾添加新元素
        /// </summary>
        /// <param name="item"></param>
        public void Append(T item)
        {
            if(IsFull())
            {
                Console.WriteLine("List is full");
                return;
            }
            data[++last] = item;
        }

        /// <summary>
        /// 在顺序表的第i个数据元素的位置插入一个数据元素 时间复杂度O(n)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="i"></param>
        public void Insert(T item, int i)
        {
            if(IsFull())
            {
                Console.WriteLine("List is full");
                return;
            }

            if(i<1||i>last+2)
            {
                Console.WriteLine("Position is error!");
                return;
            }

            if(i==last+2)
            {
                data[last + 1] = item;
            }
            else
            {
                for(int j=last;j>=i-1;--j)
                {
                    data[j + 1] = data[j];
                }
                data[i - 1] = item;
            }
            ++last;
        }

        /// <summary>
        /// 删除顺序表的第i个数据元素 时间复杂度O(n)
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T Delete(int i)
        {
            T tmp = default(T);
            if(IsEmpty())
            {
                Console.WriteLine("List is empty");
                return tmp;
            }

            if(i<1||i>last+1)
            {
                Console.WriteLine("Position is error!");
                return tmp;
            }

            if(i==last+1)
            {
                tmp = data[last--];
            }
            else
            {
                tmp = data[i - 1];
                for (int j = i; j < last; ++j)
                {
                    data[j] = data[j + 1];
                }
            }

            --last;
            return tmp;
        }

        /// <summary>
        /// 获取顺序表的第i个元素 时间复杂度O(1)
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T GetElem(int i)
        {
            if(IsEmpty()||(i<1)||(i>last+1))
            {
                Console.WriteLine("List is empty or Position is error!");
                return default(T);
            }

            return data[i - 1];
        }

        /// <summary>
        /// 在顺序表中查找值为value的数据元素 时间复杂度O(n)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Locate(T value)
        {
            if(IsEmpty())
            {
                Console.WriteLine("List is Empty!");
                return -1;
            }

            int i = 0;
            for(i=0;i<last;++i)
            {
                if(value.Equals(data[i]))
                {
                    break;
                }
            }

            if(i>last)
            {
                return -1;
            }

            return i;
        }

        /// <summary>
        /// 顺序表倒置 时间复杂度O(n)
        /// </summary>
        public void Reverse()
        {
            T tmp = default(T);
            int len = GetLength();
            for(int i=0;i< len/2;++i)
            {
                tmp = data[i];
                data[i] = data[len - i];
                data[len - i] = tmp;
            }
        }
    }
}
