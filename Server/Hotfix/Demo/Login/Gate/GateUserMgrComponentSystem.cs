namespace ET
{

    public class GateUserMgrComponentDestroySystem : DestroySystem<GateUserMgrComponent>
    {
        public override void Destroy(GateUserMgrComponent self)
        {
            self.Users.Clear();
        }
    }
    [FriendClassAttribute(typeof(ET.GateUserMgrComponent))]
    [FriendClassAttribute(typeof(ET.AccountZoneDB))]
    public static class GateUserMgrComponentSystem
    {
        public static GateUser Get(this GateUserMgrComponent self, string account)
        {
            self.Users.TryGetValue(account, out GateUser user);
            return user;
        }

        
        public static GateUser Create(this GateUserMgrComponent self,string account,int zone)
        {
            GateUser gateUser = self.AddChild<GateUser>();

            AccountZoneDB accountZoneDB = gateUser.AddComponent<AccountZoneDB>();

            accountZoneDB.Account = account;
            accountZoneDB.LoginZoneId = zone;
            //网络通信
            gateUser.AddComponent<MailBoxComponent>();

            self.GetDirectDB().Save(accountZoneDB).Coroutine();
            
            self.Users.Add(account,gateUser);
            return gateUser;
        }

        public static GateUser Create(this  GateUserMgrComponent self,AccountZoneDB accountZoneDB)
        {
            GateUser gateUser = self.AddChild<GateUser>();

            gateUser.AddComponent(accountZoneDB);
            gateUser.AddComponent<MailBoxComponent>();
            
            self.Users.Add(accountZoneDB.Account,gateUser);
            return gateUser;
        }
    }
    
    
}