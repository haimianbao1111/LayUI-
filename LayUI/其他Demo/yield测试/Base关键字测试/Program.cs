using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base关键字测试
{
    class Program :A
    {
        public Program():base()
        {
            Console.WriteLine("Build B");
        }
        static void Main(string[] args)
        {
            Program b = new Program();
            Console.ReadLine();
        }
    }
    public class A
    {
        public A()
        {
            Console.WriteLine("Build A");
        }
    }
}
