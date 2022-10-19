using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.AccountZoneDB))]
    [FriendClassAttribute(typeof(ET.GateUser))]
    public static class LoginHelper
    {

        /// <summary>
        /// 携程锁 
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        public static async ETTask<CoroutineLock> GetGateUserLock(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                throw new Exception("GetGateUserlock but account is Null");
            }

            return await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GateUserLock, account.GetLongHashCode());
        }

        public static ETTask<CoroutineLock> GetGateUserLock(this GateUser self)
        {
            return GetGateUserLock(self.GetComponent<AccountZoneDB>().Account);
        }

        public static async ETTask OfflineWithLock(this GateUser self, bool dispose = true)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }

            long instanceId = self.InstanceId;
            using (await self.GetGateUserLock())
            {
                if (instanceId != self.InstanceId)
                {
                    return;
                }

                await self.Offline(dispose);
            }
        }

        public static async ETTask Offline(this GateUser self, bool dispose = true)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }

            AccountZoneDB accountZoneDB = self.GetComponent<AccountZoneDB>();
            if (accountZoneDB != null)
            {
                //TODO 通知排队服务器进行角色下线 通知map服务器进行角色下线
            }

            if (dispose)
            {
                self.DomainScene().GetComponent<GateUserMgrComponent>()?.Remove(accountZoneDB?.Account);
            }
            else
            {
                self.state = GateUserState.InGate;
            }

            await ETTask.CompletedTask;
        }

        public static void OfflineSession(this GateUser self)
        {
            Log.Console($"->账号{self.GetComponent<AccountZoneDB>()?.Account}被顶号 {self.SessionInstanceId} 对外下线");
            Session session = self.Session;
            if (session != null)
            {
                //发送给原先连接的客户端一条顶号下线的消息 “您的账号下线了”
                session.Send(new A2C_Disconnect()
                {
                    Error = ErrorCode.ERR_Login_MultiLogin
                });
                session.RemoveComponent<SessionUserComponent>();//后续消息不在处理
                session.Disconnect().Coroutine();
                

            }
            self.SessionInstanceId = 0;
            
            //为了防止后续玩家一直不登录，这里加一个计时器，到时间了顶号的还不上来就对内下线
            self.RemoveComponent<GateUserDisconnectComponent>();
            self.AddComponent<GateUserDisconnectComponent, long>(ConstValue.Login_GateUserDisconnectTime);
        }


        public static async ETTask Disconnect(this Session self)
        {
            if (self == null)
                return;
            long instanceId = self.InstanceId;
            await TimerComponent.Instance.WaitAsync(1000);
            if (instanceId != self.InstanceId)
            {
                return;
            }
            self.Dispose();

        }

        public static StartSceneConfig GetGateConfig(int zone, string account)
        {
            int modeCount = (int)((ulong)account.GetLongHashCode() % (uint)StartSceneConfigCategory.Instance.Gates[zone].Count);
            StartSceneConfig gateConfig = StartSceneConfigCategory.Instance.Gates[zone][modeCount];
            return gateConfig;
        }
    }



}