using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClassAttribute(typeof(ET.CheckNameLog))]
    public class G2Name_CheckNameHandler : AMActorRpcHandler<Scene,G2Name_CheckName,Name2G_CheckName>
    {
        protected override async ETTask Run(Scene scene, G2Name_CheckName request, Name2G_CheckName response, Action reply)
        {

            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.ERR_Login_NoneCheckName;
                reply();
                return;
            }

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CheckName,request.Name.GetLongHashCode()))
            {
                DBComponent db = scene.GetDirectDB();
                List<CheckNameLog> list = await db.Query<CheckNameLog>(d=> d.Name == request.Name);
                if (list.Count > 0 )//查询到了名字  重名了
                {
                    response.Error = ErrorCode.ERR_Login_NameRepeated;
                    reply();
                    return;
                }

                using (CheckNameLog checkNameLog = scene.GetComponent<TempComponent>().AddChild<CheckNameLog>())
                {
                    checkNameLog.Name = request.Name;
                    checkNameLog.CreateTime = TimeHelper.ServerNow();
                    checkNameLog.UnitId = request.UnitId;
                    await db.Save(checkNameLog);
                }
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}