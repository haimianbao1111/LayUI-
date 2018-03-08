using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace _8天玩转并行开发_2_
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Task简单使用
            ////第一种方式开启 实例化Task
            //var task1 = new Task(() =>
            //{
            //    Run1();
            //});
            ////第二种方式开启 从工厂中创建
            //var task2 = Task.Factory.StartNew(() =>
            //{
            //    Run2();
            //});
            //var task3 = Task.Factory.StartNew(() =>
            //{
            //    Run3();
            //});

            //Console.WriteLine("调用start之前****************************\n");

            ////调用start之前的“任务状态”
            //Console.WriteLine("task1的状态:{0}", task1.Status);
            //Console.WriteLine("task2的状态:{0}", task2.Status);

            //task1.Start();

            //Console.WriteLine("\n调用start之后****************************");

            ////调用start之前的“任务状态”
            //Console.WriteLine("\ntask1的状态:{0}", task1.Status);
            //Console.WriteLine("task2的状态:{0}", task2.Status);

            ////主线程等待任务执行完
            //Task.WaitAll(task1, task2, task3);

            //Console.WriteLine("\n任务执行完后的状态****************************");

            ////调用start之前的“任务状态”
            //Console.WriteLine("\ntask1的状态:{0}", task1.Status);
            //Console.WriteLine("task2的状态:{0}", task2.Status);
            #endregion

            #region 取消任务
            //var cts = new CancellationTokenSource();
            //var ct = cts.Token;

            //Task task4 = new Task(() => { Run1(ct); }, ct);
            //Task task5 = new Task(Run2);

            //try
            //{
            //    task4.Start();
            //    task5.Start();
            //    Thread.Sleep(1000);
            //    cts.Cancel();

            //    Task.WaitAll(task4, task5);
            //}
            //catch (AggregateException ex)
            //{
            //    foreach (var e in ex.InnerExceptions)
            //    {
            //        Console.WriteLine("\nhi,我是OperationCanceledException：{0}\n", e.Message);
            //    }

            //    //task4是否取消
            //    Console.WriteLine("task1是不是被取消了？ {0}", task4.IsCanceled);
            //    Console.WriteLine("task2是不是被取消了？ {0}", task5.IsCanceled);
            //}

            #endregion

            #region 获取任务的返回值：Task<TResult>

            //var t1 = Task.Factory.StartNew<List<string>>(() => { return Run4(); });
            //t1.Wait();
            //var t2 = Task.Factory.StartNew(() =>
            //{
            //    Console.WriteLine("t1集合中返回的个数：" + string.Join(",", t1.Result));
            //});
            #endregion

            #region  获取任务的返回值：ContinueWith

            //var t3 = Task.Factory.StartNew<List<string>>(() => { return Run4(); });

            //var t4 = t3.ContinueWith((i) =>
            //{
            //    Console.WriteLine("t3集合中返回的个数：" + string.Join(",", i.Result));
            //});
            #endregion

            #region ContinueWith结合WaitAll
            ConcurrentStack<int> stack = new ConcurrentStack<int>();

            //t1先串行
            var t1 = Task.Factory.StartNew(() =>
            {
                stack.Push(1);
                stack.Push(2);
            });

            //t2,t3并行执行
            var t2 = t1.ContinueWith(t =>
            {
                int result;

                stack.TryPop(out result);
            });

            //t2,t3并行执行
            var t3 = t1.ContinueWith(t =>
            {
                int result;

                stack.TryPop(out result);
            });

            //等待t2和t3执行完
            Task.WaitAll(t2, t3);


            //t4串行执行
            var t4 = Task.Factory.StartNew(() =>
            {
                stack.Push(1);
                stack.Push(2);
            });

            //t5,t6并行执行
            var t5 = t4.ContinueWith(t =>
            {
                int result;

                stack.TryPeek(out result);
            });

            //t5,t6并行执行
            var t6 = t4.ContinueWith(t =>
            {
                int result;

                //只弹出，不移除
                stack.TryPeek(out result);
            });

            //临界区：等待t5，t6执行完
            Task.WaitAll(t5, t6);

            //t7串行执行
            var t7 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("当前集合元素个数：" + stack.Count);
                foreach (var item in stack.ToArray())
                {
                    Console.WriteLine(item);
                }
            });
            #endregion

            #region TaskFactory ContinueWhenAll 简单使用
            //TaskFactory taskFactory = new TaskFactory();
            //Task[] tasks = new Task[]
            //                   {
            //                      taskFactory.StartNew(() => { Console.WriteLine("taskFactory并行--t1"); }),
            //                      taskFactory.StartNew(() => { s2(); }),
            //                      taskFactory.StartNew(() => { Console.WriteLine("taskFactory并行--t3"); }),
            //                      taskFactory.StartNew(() => { Console.WriteLine("taskFactory并行--t4"); }),
            //                      taskFactory.StartNew(() => { Console.WriteLine("taskFactory并行--t5"); })
            //                   };
            //taskFactory.ContinueWhenAll(tasks, TasksEnded, CancellationToken.None);
            //foreach (var t in tasks)
            //{
            //    Console.WriteLine(string.Format("tasks-id:{0},Status:{1},IsCompleted:{2}", t.Id, t.Status, t.IsCompleted));
            //}
            #endregion


            Console.ReadKey();

        }
        static void Run1()
        {
            Thread.Sleep(1000);
            Console.WriteLine("\n我是任务1");
        }

        static void Run2()
        {
            Thread.Sleep(1000);
            Console.WriteLine("我是任务2");
        }
        static void Run3()
        {
            Thread.Sleep(1000);
            Console.WriteLine("我是任务3");
        }
        static List<string> Run4()
        {
            return new List<string> { "1", "4", "8" };
        }

        static void TasksEnded(Task[] tasks)
        {
            Console.WriteLine("所有任务已完成！");
        }
        static void s2()
        {
            Console.WriteLine("taskFactory并行--t2，等待20000");
            Thread.Sleep(20000);
        }

        static void Run1(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            Console.WriteLine("我是任务1");

            Thread.Sleep(2000);

            ct.ThrowIfCancellationRequested();

            Console.WriteLine("我是任务1的第二部分信息");
        }
    }
}
