namespace ET
{
    public class ServerInfo : Entity,IAwake
    {
        public int ServerZone;  //区服ID

        public string Name; //区服名字

        public int Status; //状态
    }

    public enum ServerStatus
    {
        Active = 0,
        Close = 1,
    }
}