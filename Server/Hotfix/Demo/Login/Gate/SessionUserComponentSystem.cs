namespace ET
{

    public class SessionUserComponentAwakeSystem: AwakeSystem<SessionUserComponent, long>
    {
        public override void Awake(SessionUserComponent self, long instanceId)
        {
            self.GateUserInstanceId = instanceId;
        }
    }
    public class SessionUserComponentDestroySystem: DestroySystem<SessionUserComponent>
    {
        public override void Destroy(SessionUserComponent self)
        {
            GateUser gateUser = self.User;
            if (gateUser!=null && self.GetParent<Session>().IsDisposed)
            {
                //如果是主动断开，应该先移除sessionUserComponent,再销毁session，否则就任务是突然断开了
                //session突然断开，一段时间后没重连就下线
                gateUser.AddComponent<GateUserDisconnectComponent, long>(ConstValue.Login_SessionDisconnectTime);
            }

            self.GateUserInstanceId = 0;
        }
    }

    public static class SessionUserComponentSystem
    {
        
    }
}