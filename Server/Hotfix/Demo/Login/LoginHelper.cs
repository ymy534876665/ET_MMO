using System;

namespace ET
{
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

            return await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GateUserLock,account.GetHashCode());
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

        public static StartSceneConfig GetGateConfig(int zone,string account)
        {
            int modeCount = Math.Abs(account.GetHashCode() % StartSceneConfigCategory.Instance.Gates[zone].Count);
            StartSceneConfig gateConfig = StartSceneConfigCategory.Instance.Gates[zone][modeCount];
            return gateConfig;
        }
    }
    
    

}