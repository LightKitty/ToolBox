using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.DataStructure
{
    public class Node<T>
    {
        private T data; //数据域
        private Node<T> next; //引用域

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="val"></param>
        /// <param name="p"></param>
        public Node(T val, Node<T> p)
        {
            data = val;
            next = p;
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="p"></param>
        public Node(Node<T> p)
        {
            next = p;
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="val"></param>
        public Node(T val)
        {
            data = val;
            next = null;
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public Node()
        {
            data = default(T);
            next = null;
        }

        /// <summary>
        /// 数据域属性
        /// </summary>
        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// 引用域属性
        /// </summary>
        public Node<T> Next
        {
            get { return next; }
            set { next = value; }
        }
    }
}
