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
            R2C_AccountLogin r2CAccountLogin =  (R2C_AccountLogin) await session.Call( new C2R_AccountLogin()
            {
                Account = account,
                Password = password,
                LoginWay = (int)LoginWayType.Normal
            });
            if (r2CAccountLogin.Error != ErrorCode.ERR_Success) //登录请求失败
            {
                Log.Error($"登录测试错误 r2CAccountLogin.Error {r2CAccountLogin.Error}");
                return r2CAccountLogin.Error;
            }
            
            SessionComponent sessionComponent = zoneScene.GetComponent<SessionComponent>();
            if (sessionComponent == null)
            {
                sessionComponent = zoneScene.AddComponent<SessionComponent>();
            }
            
            sessionComponent.Session = session;
            return r2CAccountLogin.Error;
            
        }
    
        /// <summary>
        /// 请求区服列表
        /// </summary>
        /// <param name="zoneScen"></param>
        /// <returns></returns>
        public static async ETTask<int> GetServerList(Scene zoneScen)
        {

            R2C_GetServerList r2CGetServerList = (R2C_GetServerList) await zoneScen.GetComponent<SessionComponent>().Session.Call( new C2R_GetServerList() {});
            if (r2CGetServerList.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"获取区服列表错误 Error{r2CGetServerList.Error}");
                return r2CGetServerList.Error;
            }
            zoneScen.GetComponent<ServerInfoComponent>().ClearServerInfo();
            foreach (var serverListInfo in r2CGetServerList.serverListInfos)
            {
                zoneScen.GetComponent<ServerInfoComponent>().AddServerInfo(serverListInfo);
            }
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> LoginZone(Scene zoneScene,int zoneId)
        {
            R2C_LoginZone r2CLoginZone =(R2C_LoginZone) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2R_LoginZone()
            {
                zone = zoneId
            });
            if (r2CLoginZone.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 R2C_LoginZone Erroy {r2CLoginZone.Error}");
                return r2CLoginZone.Error;
            }
            Log.Debug($"登录测试 Gate: {r2CLoginZone.GateAddress} Key:{r2CLoginZone.GateKey}");
            return ErrorCode.ERR_Success;
        }
    }
}