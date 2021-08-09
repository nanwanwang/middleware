using org.apache.zookeeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooKeeper_Server
{
    public class MyWatcher:Watcher
    {
        public override async Task process(WatchedEvent events)
        {
            //if (ZooClient == null) return;

            //var children = await ZooClient.getChildrenAsync("/", new MyWatcher(ZooClient));
            //foreach (var item in children.Children)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine("---------------------------------------------");
            await Task.CompletedTask;
        }
    }
}
