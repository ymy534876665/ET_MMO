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
                
            }
            await ETTask.CompletedTask;
        }
    }
}