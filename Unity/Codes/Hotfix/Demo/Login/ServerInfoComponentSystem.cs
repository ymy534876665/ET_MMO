namespace ET
{
    [FriendClass(typeof(ServerListInfo))]
    [FriendClass(typeof(ServerInfo))]
    [FriendClass(typeof(ServerInfoComponent))]
    public static class ServerInfoComponentSystem
    {
        public static void ClearServerInfo(this ServerInfoComponent self)
        {
            foreach (ServerInfo serverInfo in self.ServerInfosList)
            {
                serverInfo?.Dispose();
            }
            self.ServerInfosList.Clear();
        }

        public static void AddServerInfo(this ServerInfoComponent self,ServerListInfo serverListInfo)
        {
            ServerInfo serverInfo = self.AddChild<ServerInfo>();
            serverInfo.ServerZone = serverListInfo.Zone;
            serverInfo.Name = serverListInfo.Name;
            serverInfo.Status = serverListInfo.Status;
            self.ServerInfosList.Add(serverInfo);
        }
    }
}