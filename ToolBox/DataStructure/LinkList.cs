using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.DataStructure
{
    /// <summary>
    /// 单链表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkList<T>:IListDS<T>
    {
        private Node<T> head; //单链表的头引用

        /// <summary>
        /// 头引用属性
        /// </summary>
        public Node<T> Head
        {
            get { return head; }
            set { head = value; }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public LinkList()
        {
            head = null;
        }

        /// <summary>
        /// 求单链表的长度 时间复杂度O(n)
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            Node<T> p = head;

            int len = 0;
            while(p!=null)
            {
                ++len;
                p = p.Next;
            }
            return len;
        }

        /// <summary>
        /// 清空单链表
        /// </summary>
        public void Clear()
        {
            head = null;
        }

        /// <summary>
        /// 判断单链表是否为空
        /// </summary>
        /// <returns></returns>
        public  bool IsEmpty()
        {
            if (head == null) return true;
            else return false;
        }

        /// <summary>
        /// 在单链表的末尾添加新元素 时间复杂度O(n)
        /// </summary>
        /// <param name="item"></param>
        public void Append(T item)
        {
            Node<T> q = new Node<T>(item);
            Node<T> p = new Node<T>();

            if (head == null)
            {
                head = q;
                return;
            }

            p = head;
            while(p.Next!=null)
            {
                p = p.Next;
            }

            p.Next = q;
        }

        /// <summary>
        /// 在单链表的第i个结点的位置前插入一个值为item的节点 时间复杂度O(n)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="i"></param>
        public void Insert(T item, int i)
        {
            if(IsEmpty()||i<1)
            {
                Console.WriteLine("List is empty or Position is error!");
                return;
            }

            if(i==1)
            {
                Node<T> q = new Node<T>(item);
                q.Next = head;
                head = q;
                return;
            }

            Node<T> p = head;
            Node<T> r = new Node<T>();
            int j = 1;

            while(p.Next!=null&&j<i)
            {
                r = p;
                p = p.Next;
                ++j;
            }

            if(j==i)
            {
                Node<T> q = new Node<T>(item);
                q.Next = p;
                r.Next = q;
            }
        }

        /// <summary>
        /// 删除单链表的第i个节点
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T Delete(int i)
        {
            if(IsEmpty()||i<0)
            {
                Console.WriteLine("Link is empty or Position is error!");
                return default(T);
            }

            Node<T> q = new Node<T>();
            if(i==1)
            {
                q = head;
                head = head.Next;
                return q.Data;
            }

            Node<T> p = head;
            int j = 1;

            while(p.Next!=null&&j<i)
            {
                ++j;
                q = p;
                p = p.Next;
            }

            if(j==i)
            {
                q.Next = p.Next;
                return p.Data;
            }
            else
            {
                Console.WriteLine("The ith node is not exist!");
                return default(T);
            }
        }

        /// <summary>
        /// 获得单链表的第i个数据元素
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T GetElem(int i)
        {
            if (IsEmpty())
            {
                Console.WriteLine("List is empty!");
                return default(T);
            }

            Node<T> p = new Node<T>();
            p = head;
            int j = 1;

            while(p.Next!=null&&j<i)
            {
                ++j;
                p = p.Next;
            }

            if(j==i)
            {
                return p.Data;
            }
            else
            {
                Console.WriteLine("The ith node is not exist!");
                return default(T);
            }
        }

        /// <summary>
        /// 在单链表中查找值为value的节点
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

            Node<T> p = new Node<T>();
            p = head;
            int i = 1;
            while(!p.Data.Equals(value)&&p.Next!=null)
            {
                p = p.Next;
                ++i;
            }

            return i;
        }

        /// <summary>
        /// 单链表倒置
        /// </summary>
        public void Reverse()
        {
            if(IsEmpty())
            {
                Console.WriteLine("List is Empty!");
                return;
            }

            Node<T> next  = head.Next;
            while (next!=null)
            {
                Node<T> temp = next;
                next = next.Next;
                temp.Next = head;
                head = temp;
            }
        }
    }
}
