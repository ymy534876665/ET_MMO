using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClassAttribute(typeof(ET.AccountDB))]
    [FriendClassAttribute(typeof(ET.RealmAccountComponent))]
    public class C2R_AccountLoginHandler : AMRpcHandler<C2R_AccountLogin, R2C_AccountLogin>
    {
        protected override async ETTask Run(Session session, C2R_AccountLogin request, R2C_AccountLogin response, Action reply)
        {
            session.RemoveComponent<SessionAcceptTimeoutComponent>();
            int hashCode = request.Account.GetHashCode();
            int modCount = Math.Abs(hashCode  % StartSceneConfigCategory.Instance.Realms.Count);
            if (session.DomainScene().InstanceId != StartSceneConfigCategory.Instance.Realms[modCount].InstanceId)
            {
                response.Error = ErrorCode.ERR_RealmddressError;
                reply();
                session?.Disconnect().Coroutine();
                return;
            }

            RealmAccountComponent realmAccountComponent = session.GetComponent<RealmAccountComponent>();
            if (realmAccountComponent != null) //防止重复登录
            {
                response.Error = ErrorCode.ERR_Login_RepeatedLogin;
                reply();
                return;
            }

            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_Login_AccountError;
                reply();
                return;
            }

            string account = request.Account;

            //携程锁  防止同一时间相投的账号进行数据库读写操作
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.AccountLogin,account.GetHashCode())) 
            {
                AccountDB accountDB = null;
                List<AccountDB> list = await session.GetDirectDB().Query<AccountDB>(db => db.Account == account);

                if (list.Count > 0)
                {
                    accountDB = list[0];
                }

                if (Game.Options.Develop == 0) //发布模模式  启动项 --Develop=0
                {
                    if (accountDB == null) //为查询到
                    {
                        response.Error = ErrorCode.ERR_Login_AccountNotExist;
                        reply();
                        return;
                    }

                    if (accountDB.Password != request.Password)
                    {
                        response.Error = ErrorCode.ERR_Login_PasswordErr;
                        reply();
                        return;
                    }
                }
                else
                {
                    if (accountDB == null) //为查询到
                    {
                        accountDB = session.AddChild<AccountDB>();
                        accountDB.Account = account;
                        accountDB.Password = request.Password;
                        await session.GetDirectDB().Save(accountDB);
                    }
                }

                realmAccountComponent = session.AddComponent<RealmAccountComponent>();
                realmAccountComponent.Info = accountDB;
                realmAccountComponent.AddChild(accountDB);
            }
      

            reply(); //直接调用 返回成功的错误码
            await ETTask.CompletedTask;
        }
    }
}