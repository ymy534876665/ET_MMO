using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClassAttribute(typeof(ET.AccountZoneDB))]
    [FriendClassAttribute(typeof(ET.RoleInfoDB))]
    [FriendClassAttribute(typeof(ET.GateUser))]
    [FriendClassAttribute(typeof(ET.GateQueueComponent))]
    public class C2G_Enter2MapHandler : AMRpcHandler<C2G_Enter2Map, G2C_Enter2Map>
    {
        protected override async ETTask Run(Session session, C2G_Enter2Map request, G2C_Enter2Map response, Action reply)
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



            long instanceId = accountZoneDB.InstanceId;
            long unitId = request.UnitId;
            string account = accountZoneDB.Account;

            using (await gateUser.GetGateUserLock())
            {
                if (instanceId != accountZoneDB.InstanceId)
                {
                    response.Error = ErrorCode.ERR_Login_NoneAccountZoneDB;
                    reply();
                    return;
                }
                //处于顶号状态
                if (gateUser.GetComponent<MultiLoginComponent>() != null)
                {
                    if (accountZoneDB.LastRoleId != unitId)
                    {
                        await gateUser.Offline(false);
                    }
                    //等上面下线后再移除顶号状态,防止这时候刚好排队服排到了上一个号
                    gateUser.RemoveComponent<MultiLoginComponent>();

                    if (gateUser.state == GateUserState.InQueue)
                    {
                        GateQueueComponent gateQueueComponent = gateUser.GetComponent<GateQueueComponent>();
                        response.InQueue = true;
                        response.index = gateQueueComponent.Index;
                        response.Count = gateQueueComponent.Count;
                        reply();
                        return;
                    }

                    if (gateUser.state == GateUserState.InMap)
                    {
                        reply();
                        gateUser.EnterMap().Coroutine();
                        return;
                    }
                }
                
                

                RoleInfoDB targetRoleInfoDB = accountZoneDB.GetChild<RoleInfoDB>(request.UnitId);

                if (targetRoleInfoDB == null || targetRoleInfoDB.IsDeleted)
                {
                    response.Error = ErrorCode.ERR_Login_NoRoleDB;
                    reply();
                    return;
                }

                //正常选角进入的流程
                accountZoneDB.LastRoleId = request.UnitId;

                //给排队服发消息 
                Queue2G_EnQueue msg = (Queue2G_EnQueue)await MessageHelper.CallActor(accountZoneDB.LoginZoneId, SceneType.Queue, new G2Queue_EnQueue()
                {
                    UnitId = request.UnitId,
                    Account = account,
                    GateActorId = session.DomainScene().InstanceId
                });

                if (msg.Error != ErrorCode.ERR_Success)
                {
                    response.Error = msg.Error;
                    reply();
                    return;
                }

                response.InQueue = msg.NeedQueue;
                //需要排队
                if (msg.NeedQueue)
                {
                    gateUser.state = GateUserState.InQueue;

                    GateQueueComponent gateQueueComponent = gateUser.GetComponent<GateQueueComponent>();
                    if (gateQueueComponent == null)
                    {
                        gateQueueComponent = gateUser.AddComponent<GateQueueComponent>();
                    }

                    gateQueueComponent.Count = msg.Count;
                    gateQueueComponent.Index = msg.Index;
                    gateQueueComponent.UnitId = unitId;
                    response.index = msg.Index;
                    response.Count = msg.Count;
                    Log.Console($"->测试账号：{account} 需要排队，排队人数{msg.Count},当前排队序列{msg.Index}");
                }

                reply();
                DBComponent dbComponent = gateUser.GetDirectDB();
                await dbComponent.Save(accountZoneDB);

                if (!msg.NeedQueue)
                {
                    Log.Console($"->测试账号：{account},不排队直接进入游戏");
                    //游戏直接进入游戏
                    gateUser.EnterMap().Coroutine();
                }

            }
            await ETTask.CompletedTask;
        }
    }
}