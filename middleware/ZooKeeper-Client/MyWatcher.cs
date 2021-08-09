using org.apache.zookeeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooKeeper_Client
{
    public class MyWatcher : Watcher
    {

        private ZooKeeper ZooClient;
        public MyWatcher(ZooKeeper zooKeeper)
        {
            ZooClient = zooKeeper;
        }
        public override async Task process(WatchedEvent events)
        {
            if (ZooClient == null) return;

            var children = await ZooClient.getChildrenAsync("/servers", new MyWatcher(ZooClient));

            List<string> list = new List<string>();
            foreach (var item in children.Children)
            {
                var dataResult = await ZooClient.getDataAsync($"/servers/{item}", false);
                list.Add(Encoding.UTF8.GetString(dataResult.Data));
            }

            for (int i = 0; i < list.Count; i++)
            {

                if (i == list.Count - 1)
                {
                    Console.WriteLine(list[i]);
                }
                else
                {

                    Console.Write(list[i] + "  ");
                }
            }

        }
    }
}
