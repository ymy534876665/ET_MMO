using System;


namespace ET
{
    [FriendClassAttribute(typeof(ET.RoleInfosComponent))]
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            string url = $"http://{address}/get_realm?v={RandomHelper.RandUInt32()}";
            string result = await HttpClientHelper.Request(url);
            HTTP_GetRealmResponse httpGetRealmResponse = JsonHelper.FromJson<HTTP_GetRealmResponse>(result);
            Log.Debug($"登录测试 HTTP_GetRealmResponse{JsonHelper.ToJson(httpGetRealmResponse)}");
            int modCount = (int)((ulong)account.GetLongHashCode()  % (uint)httpGetRealmResponse.Realms.Count);
            string realmAddress = httpGetRealmResponse.Realms[modCount];
            Log.Debug($"登录测试{account} {password} realm : {realmAddress}   modCount {modCount} GetHashCode {account.GetHashCode() }");

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
            //释放掉realm网关服务器上的session连接
            zoneScene.GetComponent<SessionComponent>().Session?.Dispose(); 
            //新建Gate网关服务器的连接session
            Session gateSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(r2CLoginZone.GateAddress));
            //心跳组件
            PingComponent pingComponent = gateSession.GetComponent<PingComponent>();
            if (pingComponent == null)
            {
                pingComponent = gateSession.AddComponent<PingComponent>();
            }
            
            zoneScene.GetComponent<SessionComponent>().Session = gateSession;


            G2C_Login2Gate g2CLogin2Gate = (G2C_Login2Gate) await gateSession.Call(new C2G_Login2Gate()
            {
                GateKey = r2CLoginZone.GateKey
            });
            if (g2CLogin2Gate.Error != ErrorCode.ERR_Success)
            {
                Log.Debug($"登录测试 G2C_Login2Gate ERROR{g2CLogin2Gate.Error}");
                return g2CLogin2Gate.Error;
            }
            Log.Debug("登录Gate网关服务器成功");
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetRoleInfos(Scene zoneScene)
        {

            G2C_GetRoles g2c_GetRoles = (G2C_GetRoles)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_GetRoles()
            {
                
            });
            if (g2c_GetRoles.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"获取角色信息错误：{g2c_GetRoles.Error}");
                return g2c_GetRoles.Error;
            }
            zoneScene.GetComponent<RoleInfosComponent>().ClearRoleInfos();
            foreach (GateRoleInfo gateRoleInfo in g2c_GetRoles.Roles)
            {
                zoneScene.GetComponent<RoleInfosComponent>().AddRoleInfo(gateRoleInfo);
            }
            return ErrorCode.ERR_Success;
        }


        public static async ETTask<int> CreateRole(Scene zoneScene,string name)
        {
            G2C_CreateRole msg = (G2C_CreateRole) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_CreateRole() { Name = name});
            if (msg.Error != ErrorCode.ERR_Success) 
            {
                Log.Error($"创角角色错误 错误码{msg.Error}");
                return msg.Error;
            }
            zoneScene.GetComponent<RoleInfosComponent>().AddRoleInfo(msg.Role); 
            return ErrorCode.ERR_Success;
        }


        public static async ETTask<int> DeleteRole(Scene zoneScene, long roleId)
        {

            G2C_DeleteRole msg = (G2C_DeleteRole) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_DeleteRole()
            {
                RoleId = roleId
            });
            if (msg.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"删除角色错误：错误码{msg.Error}");
                return msg.Error;
            }
            
            zoneScene.GetComponent<RoleInfosComponent>().DeleteRoleInfoById(msg.RoleId);
            return ErrorCode.ERR_Success;
        }


        public static async ETTask<int> EnterMap(Scene zoneScene)
        {

            if (!zoneScene.GetComponent<RoleInfosComponent>().IsCurrentRoleExist())
            {
                return ErrorCode.ERR_Login_RoleNotExist;
            }
            G2C_Enter2Map msg = (G2C_Enter2Map) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_Enter2Map()
            {
                UnitId = zoneScene.GetComponent<RoleInfosComponent>().CurrentUnitId
            });
            if (msg.Error != ErrorCode.ERR_Success)
            {     
                Log.Error($"进入游戏失败：错误码{msg.Error}");
                return msg.Error;
            }

            zoneScene.GetComponent<PlayerComponent>().MyId = zoneScene.GetComponent<RoleInfosComponent>().CurrentUnitId;
            if (msg.InQueue)
            {
                //发送排队事件
                Game.EventSystem.Publish(new EventType.UpdateQueueInfo()
                {
                    Count =  msg.Count,
                    Index = msg.index,
                    ZoneScene = zoneScene
                });
                return ErrorCode.ERR_Success;
            }
            
            //等待场景切换完成
            await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_SceneChangeFinish>();
            
            
            //进入场景完成事件
            Game.EventSystem.Publish(new EventType.EnterMapFinish()
            {
                ZoneScene = zoneScene
            });
            return ErrorCode.ERR_Success;
        }

    }
}