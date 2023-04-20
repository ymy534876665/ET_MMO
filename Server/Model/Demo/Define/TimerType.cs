namespace ET
{
    public static partial class TimerType
    {
        public const int multiLogin = 2005; //顶号时上个角色保留时间

        public const int GateUserDisconnect = 2006; //顶号保留时间

        public const int QueueTickTime = 2007; //排队检测放人进游戏

        public const int QueueUpdateTime = 2008; //排队更新排名

        public const int QueueClearProtect = 2009;//定时清除掉线保护信息
    } 
}