﻿using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.GateUserMgrComponent))]
    public class G2R_GatGateKeyHandler : AMActorRpcHandler<Scene, R2G_GatGateKey, G2R_GatGateKey>
    {
        protected override async ETTask Run(Scene scene, R2G_GatGateKey request, G2R_GatGateKey response, Action reply)
        {
            GateUserMgrComponent gateUserMgrComponent = scene.GetComponent<GateUserMgrComponent>();

            gateUserMgrComponent.Users.TryGetValue(request.info.Account, out GateUser gateUser);
            if (gateUser != null)//顶号的逻辑
            {
                //TODO 顶号的逻辑
            }

            GateSessionKeyComponent gateSessionKeyComponent = scene.GetComponent<GateSessionKeyComponent>();

            long key = RandomHelper.RandInt64();
            
            gateSessionKeyComponent.Add(key,request.info);

            response.GateKey = key;
            reply();
            
            await ETTask.CompletedTask;
        }
    }
}