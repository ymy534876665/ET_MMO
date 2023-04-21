using System;
using MongoDB.Driver.Linq;

namespace ET
{
    [FriendClassAttribute(typeof(ET.QueueMgrComponent))]
    public class G2Queue_EnQueueHandler : AMActorRpcHandler<Scene,G2Queue_EnQueue,Queue2G_EnQueue>
    {
        protected override async ETTask Run(Scene scene, G2Queue_EnQueue request, Queue2G_EnQueue response, Action reply)
        {
            QueueMgrComponent queueMgrComponent = scene.GetComponent<QueueMgrComponent>();
    
            if (queueMgrComponent.TryEnqueue(request.Account,request.UnitId,request.GateActorId))
            {
                response.NeedQueue = true;
                response.Count = queueMgrComponent.Queue.count;
                response.Index = queueMgrComponent.GetIndex(request.UnitId);
            }

            reply();
            await ETTask.CompletedTask;
        }
        
        
    }
}