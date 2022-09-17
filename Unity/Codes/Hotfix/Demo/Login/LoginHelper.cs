using System;


namespace ET
{
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            string url = $"http://{address}/get_realm?v={RandomHelper.RandUInt32()}";
            string result = await HttpClientHelper.Request(url);
            HTTP_GetRealmResponse httpGetRealmResponse = JsonHelper.FromJson<HTTP_GetRealmResponse>(result);
            Log.Debug($"登录测试 HTTP_GetRealmResponse{JsonHelper.ToJson(httpGetRealmResponse)}");
            int modCount = Math.Abs(account.GetHashCode())  % httpGetRealmResponse.Realms.Count;
            string realmAddress = httpGetRealmResponse.Realms[modCount];
            Log.Debug($"登录测试{account} {password} realm : {realmAddress}");

            Session session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(realmAddress));
            // C2R_AccountLogin msg = new C2R_AccountLogin();
            // msg.Account = account;
            // msg.Password = password;
            // msg.LoginWay = (int)LoginWayType.Normal;
            R2C_AccountLogin r2CAccountLogin =  (R2C_AccountLogin) await session.Call( new C2R_AccountLogin()
            {
                Account = account,
                Password = password,
                LoginWay = (int)LoginWayType.Normal,
            });
            // if (r2CAccountLogin.Error != ErrorCode.ERR_Success) //登录请求失败
            // {
            //     Log.Error($"登录测试错误 r2CAccountLogin.Error {r2CAccountLogin.Error}");
            //     return r2CAccountLogin.Error;
            // }
            //
            // SessionComponent sessionComponent = zoneScene.GetComponent<SessionComponent>();
            // if (sessionComponent == null)
            // {
            //     sessionComponent = zoneScene.AddComponent<SessionComponent>();
            // }
            //
            // sessionComponent.Session = session;
            return r2CAccountLogin.Error;
        } 
    }
}