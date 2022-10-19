using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.AccountZoneDB))]
    [FriendClassAttribute(typeof(ET.RoleInfoDB))]
    public class C2G_CreateRoleHandler : AMRpcHandler<C2G_CreateRole, G2C_CreateRole>
    {
        protected override async ETTask Run(Session session, C2G_CreateRole request, G2C_CreateRole response, Action reply)
        {
            GateUser gateUser = session.GetComponent<SessionUserComponent>()?.User;
            if (gateUser == null)
            {
                response.Error = ErrorCode.ERR_Login_NoneGateUser;
                reply();
                return;
            }

            AccountZoneDB accountZoneDB = gateUser.GetComponent<AccountZoneDB>();
            if (accountZoneDB == null)
            {
                response.Error = ErrorCode.ERR_Login_NoneAccountZoneDB;
                reply();
                return;
            }

            string name = request.Name;
            if (string.IsNullOrEmpty(name))
            {
                response.Error = ErrorCode.ERR_Login_NoneName;
                reply();
                return;
            }
            
            long instanceId = accountZoneDB.InstanceId;
            using (await gateUser.GetGateUserLock())
            {
                if (instanceId != accountZoneDB.InstanceId)
                {
                    response.Error = ErrorCode.ERR_Login_NoneAccountZoneDB;
                    reply();
                    return;
                }

                long unitId = IdGenerater.Instance.GenerateUnitId(accountZoneDB.LoginZoneId);
                //判断重名
               // G2Name_CheckName
               Name2G_CheckName msg =  (Name2G_CheckName) await MessageHelper.CallActor(accountZoneDB.LoginZoneId,SceneType.Name,new G2Name_CheckName()
               {
                   Name = name,
                   UnitId = unitId
               });
               if (msg.Error != ErrorCode.ERR_Success)
               {
                   response.Error = msg.Error;
                   reply();
                   return;
               }
                
                RoleInfoDB roleInfoDB = accountZoneDB.AddChildWithId<RoleInfoDB>(unitId);
                roleInfoDB.Account = accountZoneDB.Account;
                roleInfoDB.AccountZoneId = accountZoneDB.Id;
                roleInfoDB.IsDeleted = false;
                roleInfoDB.LoginZone = accountZoneDB.LoginZoneId;
                roleInfoDB.Name = request.Name;
                roleInfoDB.level = 1;

                await session.GetDirectDB().Save(roleInfoDB);

                response.Role = roleInfoDB.ToMessage();
            }

            reply();

            await ETTask.CompletedTask;
        }
    }
}