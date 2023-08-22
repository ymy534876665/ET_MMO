using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.GateUser))]
    public class Queue2G_EnterMapHandler : AMActorRpcHandler<Scene, Queue2G_EnterMap, G2Queue_EnterMap>
    {
        protected override async ETTask Run(Scene scene, Queue2G_EnterMap request, G2Queue_EnterMap response, Action reply)
        {
            using (await LoginHelper.GetGateUserLock(request.Account))
            {
                GateUser gateUser = scene.GetComponent<GateUserMgrComponent>().Get(request.Account);
                Log.Console($"=> 测试 账号{request.Account}排队完了。");

                if (gateUser == null || gateUser.GetComponent<MultiLoginComponent>() != null || gateUser.state == GateUserState.InGate||
                    gateUser.GetComponent<GateQueueComponent>() == null)
                {
                    response.NeedRemove = true;
                    reply();
                    return;
                }

                if (gateUser.state == GateUserState.InMap)
                {
                    reply();
                    return;
                }

                reply();
                
                gateUser.EnterMap().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}