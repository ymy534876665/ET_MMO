using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof(LoginGateInfo))]
    [FriendClass(typeof(AccountZoneDB))]
    [FriendClass(typeof(RoleInfoDB))]
    public class G2C_Login2GateHandler : AMRpcHandler<C2G_Login2Gate,G2C_Login2Gate>
    {
        protected override async ETTask Run(Session session, C2G_Login2Gate request, G2C_Login2Gate response, Action reply)
        {
            session.RemoveComponent<SessionAcceptTimeoutComponent>();
            Scene scene = session.DomainScene();
            
            GateSessionKeyComponent gateSessionKeyComponent = scene.GetComponent<GateSessionKeyComponent>();
            
            LoginGateInfo gateInfo = gateSessionKeyComponent.Get(request.GateKey);
            if (gateInfo == null)
            {
                response.Error = ErrorCode.ERR_Login_NoLoginGateInfo;
                reply();
                return;
            }

            string account = gateInfo.Account;
            //这里就可以判断 服务器停服，维护 封号  IP 等各种情况了
            long instanceId = session.InstanceId;

            using (await LoginHelper.GetGateUserLock(account))
            {
                if (instanceId != session.InstanceId)  //异步操作  有可能出现被定好的情况  所以要判断下唯一性
                {
                    return;
                }

                GateUserMgrComponent gateUserMgrComponent = scene.GetComponent<GateUserMgrComponent>();
                GateUser gateUser = gateUserMgrComponent.Get(account);

                if (gateUser == null)
                {
                    DBComponent db = scene.GetDirectDB();
                    List<AccountZoneDB> list =  await db.Query<AccountZoneDB>(d => d.Account == account);

                    if (list.Count <= 0) //没查询到数据库
                    {
                        //创建gate 把区服信息
                        gateUser = gateUserMgrComponent.Create(account,gateInfo.LogicZone);
                    }
                    else
                    {
                        gateUser = gateUserMgrComponent.Create(list[0]);
                    }
                    //那边写的是GetComponent<AccountZoneDB>().Id 不知道是不是写错了
                    //long id = gateUser.GetComponent<AccountZoneDB>().LoginZoneId;
                    long id = gateUser.GetComponent<AccountZoneDB>().Id;
                    List<RoleInfoDB> roleInfoDbs = await db.Query<RoleInfoDB>(d => d.AccountZoneId == id && !d.IsDeleted);
                    if (roleInfoDbs.Count > 0)
                    {
                        foreach (RoleInfoDB roleInfoDb in roleInfoDbs)
                        {
                            gateUser.GetComponent<AccountZoneDB>().AddChild(roleInfoDb);
                        }
                    }
                }
                else
                {
                    
                }

                reply();
            }
            
            
            await ETTask.CompletedTask;
        }
    }
}