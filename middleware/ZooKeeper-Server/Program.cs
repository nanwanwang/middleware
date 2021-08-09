using org.apache.zookeeper;
using System;
using System.Text;
using static org.apache.zookeeper.ZooDefs;

namespace ZooKeeper_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var zk = new ZooKeeper("192.168.1.207:2181,192.168.1.208:2181,192.168.1.209:2181", 2000, new MyWatcher());

            var result =zk.createAsync($"/servers/{args[0]}", Encoding.UTF8.GetBytes(args[0]), Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL_SEQUENTIAL);
            Console.WriteLine($"{args[0]} 上线...");

            Console.ReadLine();
        }
    }
}
