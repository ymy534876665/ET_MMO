using System.Collections.Generic;

namespace ET
{

    /// <summary>
    /// 排队信息
    /// </summary>
    public class QueueInfo : Entity,IAwake,IDestroy
    {
        public long UnitId;

        public long GateActorId;

        public string Account;

        public int index;
        
        //这里可以放等级，vip之类的信息
    }
    
    
    /// <summary>
    /// 掉线保护信息
    /// </summary>
    public class ProtectInfo
    {
        public long UnitId;

        public long Time;
    }
    
    
    [ChildType(typeof(QueueInfo))]
    [ComponentOf(typeof(Scene))]
    public class QueueMgrComponent : Entity,IAwake,IDestroy
    {
        //允许在线的玩家
        // 1、HashSet中的元素是没有顺序的。
        // 2、HashSet中不允许有重复元素（元素具有唯一性）。
        // 3、与其他很多数据结构不同，HashSet不能修改元素，如果需要修改，只能删除原来的元素，再添加新的元素。
        public HashSet<long> online = new HashSet<long>();

        //排队队列
        public HashLinkedList<long, QueueInfo> Queue = new HashLinkedList<long, QueueInfo>();

        //掉线保护的玩家
        public HashLinkedList<long, ProtectInfo> protects = new HashLinkedList<long, ProtectInfo>();
        
        
        //排队检测
        public long Timer_Trick;
        //定时清除掉线保护到时间的玩家
        public long Timer_clearProtect;
        //排队更新排名
        public long Timer_Update;
    }
}