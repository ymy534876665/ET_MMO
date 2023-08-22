namespace ET
{
    [FriendClass(typeof(ET.GateUser))]
    [FriendClass(typeof(ET.GateQueueComponent))]
    public class Queue2G_UpdateInfoHandler : AMActorHandler<Scene,Queue2G_UpdateInfo>
    {
        protected override async ETTask Run(Scene scene, Queue2G_UpdateInfo message)
        {
            if (message.Account.Count != message.Index.Count)
            {
                return;
            }

            G2C_UpdateQueue g2CUpdateQueue = new G2C_UpdateQueue(){Count = message.Count};
            GateUserMgrComponent gateUserMgrComponent = scene.GetComponent<GateUserMgrComponent>();

            for (int i = 0; i < message.Account.Count; i++)
            {
                string account = message.Account[i];
                GateUser gateUser = gateUserMgrComponent.Get(account);
                if (gateUser == null || gateUser.state != GateUserState.InQueue)
                {
                    continue;
                }

                GateQueueComponent gateQueueComponent = gateUser.GetComponent<GateQueueComponent>();
                gateQueueComponent.Count = message.Count;
                gateQueueComponent.Index = message.Index[i];

                g2CUpdateQueue.Index = message.Index[i];
                gateUser.Session.Send(g2CUpdateQueue);
                
                //每帧发5个
                if ((i + 1) % 5 == 0)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                }
            }
            
            await ETTask.CompletedTask;
        }
    }
}