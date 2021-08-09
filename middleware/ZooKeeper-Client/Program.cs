using org.apache.zookeeper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static org.apache.zookeeper.ZooDefs;

namespace ZooKeeper_Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ZooKeeper zk=null;
            zk = new ZooKeeper("192.168.1.207:2181,192.168.1.208:2181,192.168.1.209:2181", 5000,new MyWatcher(null));


            //var result = await zk.createAsync("/a", Encoding.UTF8.GetBytes("some"), Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT_SEQUENTIAL);
            var children = await zk.getChildrenAsync("/servers", new MyWatcher(zk));
            List<string> list = new List<string>();
            foreach (var item in children.Children)
            {
                var dataResult= await zk.getDataAsync($"/servers/{item}", false);
                list.Add(Encoding.UTF8.GetString(dataResult.Data));
            }
            
            for (int i = 0; i < list.Count; i++)
            {
              
                if(i== list.Count-1)
                {
                    Console.WriteLine(list[i]);
                }else
                {
                  
                   Console.Write(list[i]+"  ");
                }
            }
       
       
            //Console.WriteLine("-------------------------");

            //var state= await zk.existsAsync("/demon", false);

            Console.ReadKey();
        }
    }
}
