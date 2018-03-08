using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _8天玩转并行开发_3_
{
    class Program
    {
        #region 8天玩转并行开发——第三天 plinq的使用

        #endregion

        static void Main(string[] args)
        {
            #region AsParallel(并行化）
            //var dic = LoadData();

            //Stopwatch watch = new Stopwatch();
            //watch.Start();

            ////串行执行
            //var query1 = (from n in dic.Values
            //              where n.Age > 20 && n.Age < 25
            //              select n).ToList();

            //watch.Stop();
            //Console.WriteLine("串行计算耗费时间：{0}", watch.ElapsedMilliseconds);

            //watch.Restart();
            ////AsParallel 并行执行
            //var query2 = (from n in dic.Values.AsParallel()
            //              where n.Age > 20 && n.Age < 25
            //              select n).ToList();

            //watch.Stop();
            //Console.WriteLine("并行计算耗费时间：{0}", watch.ElapsedMilliseconds);
            #endregion

            #region 常用方法的使用--orderby  sum(),average()
            //var dic1 = LoadData();
            //var query3 = (from n in dic1.Values.AsParallel()
            //              where n.Age > 20 && n.Age < 25
            //              select n).ToList();

            //Console.WriteLine("默认的时间排序如下：");

            //query3.Take(10).ToList().ForEach((i) =>
            //{
            //    Console.WriteLine(i.CreateTime);
            //});

            //var query4 = (from n in dic1.Values.AsParallel()
            //              where n.Age > 20 && n.Age < 25
            //              orderby n.CreateTime descending
            //              select n).ToList();

            //Console.WriteLine("排序后的时间排序如下：");
            //query4.Take(10).ToList().ForEach((i) =>
            //{
            //    Console.WriteLine(i.CreateTime);
            //});
            #endregion

            #region 指定并行度 为了不让并行计算占用全部的硬件线程，或许可能要留一个线程做其他事情
            //var dic1 = LoadData();
            //var query5 = (from n in dic1.Values.AsParallel()
            //              .WithDegreeOfParallelism(Environment.ProcessorCount - 1)
            //              where n.Age > 20 && n.Age < 25
            //              orderby n.CreateTime descending
            //              select n).ToList();

            //query5.Take(10).ToList().ForEach((i) =>
            //{ 
            // Console.WriteLine(i.CreateTime);
            //});
            #endregion

            #region 了解ParallelEnumerable类
            //ConcurrentBag<int> bag = new ConcurrentBag<int>();
            //var list = ParallelEnumerable.Range(0, 10000);
            //list.ForAll((i) =>
            //{
            //    bag.Add(i);
            //});
            //Console.WriteLine("bag集合中元素个数有:{0}", bag.Count);
            //Console.WriteLine("list集合中元素个数总和为:{0}", list.Sum());
            //Console.WriteLine("list集合中元素最大值为:{0}", list.Max());
            //Console.WriteLine("list集合中元素第一个元素为:{0}", list.FirstOrDefault());
            #endregion

            #region plinq实现MapReduce算法
            List<Student> list = new List<Student>()
            {
                new Student(){ ID=1, Name="jack", Age=20},
                new Student(){ ID=1, Name="mary", Age=25},
                new Student(){ ID=1, Name="joe", Age=29},
                new Student(){ ID=1, Name="Aaron", Age=25},
            };

            //这里我们会对age建立一组键值对
            var map = list.AsParallel().ToLookup(i => i.Age, count => 1);

            //化简统计
            var reduce = from IGrouping<int, int> singleMap
                         in map.AsParallel()
                         select new
                         {
                             Age = singleMap.Key,
                             Count = singleMap.Count()
                         };

            ///最后遍历
            reduce.ForAll(i =>
            {
                Console.WriteLine("当前Age={0}的人数有:{1}人", i.Age, i.Count);
            });
            #endregion


            Console.ReadKey();
        }
        public static ConcurrentDictionary<int, Student> LoadData()
        {
            ConcurrentDictionary<int, Student> dic = new ConcurrentDictionary<int, Student>();

            //预加载1500w条记录
            Parallel.For(0, 15000, (i) =>
            {
                var single = new Student()
                {
                    ID = i,
                    Name = "hxc" + i,
                    Age = i % 151,
                    CreateTime = DateTime.Now.AddSeconds(i)
                };
                dic.TryAdd(i, single);
            });

            return dic;
        }
    }
   
    public class Student
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
