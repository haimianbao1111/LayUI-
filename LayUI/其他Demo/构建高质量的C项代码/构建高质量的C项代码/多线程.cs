using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace 构建高质量的C项代码
{
    class 多线程
    {
        //定义易失域
        //票的数量
        volatile static public int TicketCount = 10;

        //数组
        static ArrayList arrNumber = new ArrayList();
        //添加100个数值
        static void Add()
        {
            for (int i = 0; i <= 100; i++)
            {
                lock (arrNumber)
                {
                    if (!arrNumber.Contains(i))
                    {
                        arrNumber.Add(i);
                        Console.WriteLine("{0},添加样本数{1}",Thread.CurrentThread.Name,i);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            #region 多线程
            ThreadStart threadWork = new ThreadStart(Test);
            Thread thread1 = new Thread(threadWork);
            thread1.Name = "线程1";
            Thread thread2 = new Thread(threadWork);
            thread2.Name = "线程2";
            Thread thread3=new Thread (threadWork);
            thread3.Name="线程3";
            //开始执行
            thread1.Start();
            thread2.Start();
            thread3.Start();
            #endregion

            #region 简化线程的定义
            Thread thread11 = new Thread(Test);
            thread11.Name = "线程11";
            Thread thread22 = new Thread(Test);
            thread22.Name = "线程22";
            Thread thread33 = new Thread(Test);
            thread33.Name = "线程33";
            //开始执行
            thread11.Start();
            thread22.Start();
            thread33.Start();
            #endregion

            #region 易失域
            Thread thread5 = new Thread(SellTicket);
            thread5.Name = "窗口5";
            Thread thread6 = new Thread(SellTicket);
            thread6.Name = "窗口6";
            Thread thread7 = new Thread(SellTicket);
            thread7.Name = "窗口7";
            //开始执行
            thread5.Start();
            thread6.Start();
            thread7.Start();
            #endregion

            #region lock语句应用 只能操作引用类型对象
            Thread thread8 = new Thread(Add);
            thread8.Name = "线程1";
            Thread thread9 = new Thread(Add);
            thread9.Name = "线程2";
            Thread thread10 = new Thread(Add);
            thread10.Name = "线程3";
            thread8.Start();
            thread9.Start();
            thread10.Start();
            #endregion

            Console.WriteLine("=========================================================================");
            Console.WriteLine("dxper.net".Substring(3).Replace(".", "").ToUpper());
             Console.WriteLine("=========================================================================");
            Console.ReadKey();
        }
        //卖票
        static public void SellTicket()
        { 
            //票数大于0时才能继续卖票
            while (TicketCount > 0)
            {
                TicketCount--;
                Console.WriteLine("{0},卖了一张票", Thread.CurrentThread.Name);
            }
        }
        //多线程测试方法
        static public void Test()
        {
            Console.WriteLine("{0},正在执行测试方法",Thread.CurrentThread.Name);
        }
    }
}
