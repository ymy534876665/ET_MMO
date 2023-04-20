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
            throw new System.NotImplementedException();
        }
    }
    
    [Timer(TimerType.QueueClearProtect)]
    public class QueueClearProtect_TimerHandler : ATimer<QueueMgrComponent>
    {
        public override void Run(QueueMgrComponent t)
        {
            throw new System.NotImplementedException();
        }
    }
    
    [Timer(TimerType.QueueUpdateTime)]
    public class QueueUpdateTime_TimerHandler : ATimer<QueueMgrComponent>
    {
        public override void Run(QueueMgrComponent t)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public static class QueueMgrComponentSystem
    {
        
    }
}