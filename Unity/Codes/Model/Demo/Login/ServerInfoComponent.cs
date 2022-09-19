using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(ServerInfo))]
    public class ServerInfoComponent : Entity,IAwake
    {
        public List<ServerInfo> ServerInfosList = new List<ServerInfo>();
    }
}