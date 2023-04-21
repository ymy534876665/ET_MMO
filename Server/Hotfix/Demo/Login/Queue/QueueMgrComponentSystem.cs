namespace ET
{

    public  class QueueMgrComponentAwakeSyetem : AwakeSystem<QueueMgrComponent>
    {
        public override void Awake(QueueMgrComponent self)
        {
            self.Timer_Trick = TimerComponent.Instance.NewRepeatedTimer(ConstValue.Queue_TickTime,TimerType.QueueTickTime,self);
            self.Timer_clearProtect = TimerComponent.Instance.NewRepeatedTimer(ConstValue.Queue_ClearProtect, TimerType.QueueClearProtect, self);
            self.Timer_Update = TimerComponent.Instance.NewRepeatedTimer(ConstValue.Queue_TickUpdate, TimerType.QueueUpdateTime, self);
        }
    }

    public class QueueMgrComponentDestroySystem : AwakeSystem<QueueMgrComponent>
    {
        public override void Awake(QueueMgrComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer_Trick);
            TimerComponent.Instance.Remove(ref self.Timer_clearProtect);
            TimerComponent.Instance.Remove(ref self.Timer_Update);
            
            self.online.Clear();
            self.protects.Clear();
            self.Queue.Clear();
        }
    }
    [Timer(TimerType.QueueTickTime)]
    public class QueueTickTime_TimerHandler : ATimer<QueueMgrComponent>
    {
        public override void Run(QueueMgrComponent t)
        {
            t.Tick();
        }
    }
    
    [Timer(TimerType.QueueClearProtect)]
    public class QueueClearProtect_TimerHandler : ATimer<QueueMgrComponent>
    {
        public override void Run(QueueMgrComponent t)
        {
            
        }
    }
    
    [Timer(TimerType.QueueUpdateTime)]
    public class QueueUpdateTime_TimerHandler : ATimer<QueueMgrComponent>
    {
        public override void Run(QueueMgrComponent t)
        {
            t.UpdateQueue();
        }
    }
    [FriendClassAttribute(typeof(ET.QueueMgrComponent))]
    [FriendClassAttribute(typeof(ET.QueueInfo))]
    public static class QueueMgrComponentSystem
    {
        /// <summary>
        /// 尝试进入排队，true需要排队 false不需要排队
        /// </summary>
        /// <param name="self"></param>
        /// <param name="account"></param>
        /// <param name="unitId"></param>
        /// <param name="gateActorId"></param>
        /// <returns></returns>
        public static bool TryEnqueue(this QueueMgrComponent self,string account,long unitId,long gateActorId)
        {
            //在排队保护的列表中 要进行移除
            if (self.protects.ContainsKey(unitId))
            {
                self.protects.Remove(unitId);
                if (self.Queue.ContainsKey(unitId))
                {
                    return true;
                }

                return false;
            }
            
            //已经在在线列表
            if (self.online.Contains(unitId))
            {
                return false;
            }
            
            //已经在排队的列表了 可能重复发送 继续按之前的排队
            if (self.Queue.ContainsKey(unitId))
            {
                
                return true;
            }


            self.Enqueue(account,unitId,gateActorId);
            return true;
        }
    
        public static void Enqueue(this QueueMgrComponent self,string account,long unitId,long gateActorId)
        {
            if (self.Queue.ContainsKey(unitId))
            {

                return;
            }

            QueueInfo queueInfo = self.AddChild<QueueInfo>();
            queueInfo.Account = account;
            queueInfo.UnitId = unitId;
            queueInfo.GateActorId = gateActorId;
            queueInfo.index = self.Queue.count + 1;
            self.Queue.AddLast(unitId,queueInfo);
        }

        public static int GetIndex(this QueueMgrComponent self,long unitId)
        {
            return self.Queue[unitId]?.index ?? 1;
        }

        public static void Tick(this QueueMgrComponent self)
        {
            //在线人数最大 不放人
            if (self.online.Count >= ConstValue.Queue_MaxOnline)
            {
                return;
            }
            //每次放入多少人
            for (int i = 0; i < ConstValue.Queue_TickCount; i++)
            {
                if (self.Queue.count <= 0)
                {
                    return;
                }

                QueueInfo queueInfo = self.Queue.First;
                self.EnterMap(queueInfo.UnitId).Coroutine();
            }
        }

        public static async ETTask EnterMap(this QueueMgrComponent self,long unitId)
        {
            //hashset结构 不可以添加重复的 
            if (!self.online.Add(unitId))
            {
                return;
            }

            QueueInfo queueInfo = self.Queue.Remove(unitId);

            if (queueInfo != null)
            {
                G2Queue_EnterMap msg = (G2Queue_EnterMap)await MessageHelper.CallActor(queueInfo.GateActorId, new Queue2G_EnterMap()
                {
                    Account = queueInfo.Account,
                    UnitId = queueInfo.UnitId
                });
                //是否需要移除在线状态
                if (msg.NeedRemove)
                {
                    self.online.Remove(unitId);
                }
                queueInfo.Dispose();
                
            }

            await ETTask.CompletedTask;

        }

        /// <summary>
        /// 更新排队信息
        /// </summary>
        /// <param name="self"></param>
        public static void UpdateQueue(this QueueMgrComponent self)
        {
            
        }
        
    }
}