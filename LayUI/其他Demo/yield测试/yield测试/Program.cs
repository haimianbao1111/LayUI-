using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yield测试
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = Power(2, 8, "");
            Console.WriteLine("Begin to iterate the collection.");
            foreach (int i  in Power(2, 8, ""))
            {
                Console.Write("{0} ", i);
            }
            Console.ReadKey();
        }
        public static IEnumerable<int> Power(int number, int exponent, string s)
        {
            int result = 1;
            if (string.IsNullOrEmpty(s))
            {
                //throw new Exception("这是一个异常");
                Console.WriteLine("Begin to invoke GetItems() method");
            }

            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }
        #region 改造前


        //public static IEnumerable<int> Power(int number, int exponent, string s)
        //{
        //    int result = 1;
        //    //接口不能实例化，我们这儿new一个实现了IEnumerable接口的List
        //    IEnumerable<int> example = new List<int>();
        //    for (int i = 0; i < exponent; i++)
        //    {
        //        result = result * number;
        //        (example as List<int>).Add(result);
        //    }
        //    return example;
        //}
        #endregion
    }
}
