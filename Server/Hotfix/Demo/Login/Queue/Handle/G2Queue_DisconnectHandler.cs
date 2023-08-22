namespace ET
{
    public class G2Queue_DisconnectHandler : AMActorHandler<Scene,G2Queue_Disconnect>
    {
        protected override async ETTask Run(Scene scene, G2Queue_Disconnect message)
        {
            scene.GetComponent<QueueMgrComponent>().Disconnect(message.UnitId,message.Protect);
            await ETTask.CompletedTask;
        }
    }
}