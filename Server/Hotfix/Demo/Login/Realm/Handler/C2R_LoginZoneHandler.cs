using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.AccountDB))]
    [FriendClassAttribute(typeof(ET.RealmAccountComponent))]
    public class C2R_LoginZoneHandler : AMRpcHandler<C2R_LoginZone, R2C_LoginZone>
    {
        protected async override ETTask Run(Session session, C2R_LoginZone request, R2C_LoginZone response, Action reply)
        {
            RealmAccountComponent realmAccountComponent = session.GetComponent<RealmAccountComponent>();
            if (realmAccountComponent == null)
            {
                response.Error = ErrorCode.ERR_Login_AccountNotLogin;
                reply();
                return;
            }

            if (!StartZoneConfigCategory.Instance.Contain(request.zone))
            {
                response.Error = ErrorCode.ERR_Login_ZoneNotExist;
                reply();
                return;
            }

            string account = realmAccountComponent.Info.Account;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginZone,account.GetHashCode()))
            {
                StartSceneConfig startSceneConfig = LoginHelper.GetGateConfig(request.zone,account);

                G2R_GatGateKey g2RGatGateKey = (G2R_GatGateKey) await MessageHelper.CallActor(startSceneConfig.InstanceId,new R2G_GatGateKey()
                {
                    info = new LoginGateInfo()
                    {
                        Account = account,
                        LogicZone = request.zone,
                    }
                });
                if (g2RGatGateKey.Error != ErrorCode.ERR_Success)
                {
                    response.Error = g2RGatGateKey.Error;
                    reply();
                    return;
                }
                
                
                response.GateAddress = startSceneConfig.InnerIPOutPort.ToString();
                response.GateKey = g2RGatGateKey.GateKey;
                reply();
                
                session?.Disconnect().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}