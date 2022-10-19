namespace ET
{
    [FriendClass(typeof(RoleInfo))]
    [FriendClass(typeof(RoleInfosComponent))]
    public static class RoleInfosComponentSystem
    {
        public static void ClearRoleInfos(this RoleInfosComponent self)
        {
            foreach (RoleInfo roleInfo in self.RoleInfos)
            {
                roleInfo?.Dispose();
            }
            self.RoleInfos.Clear();
        }

        public static void AddRoleInfo(this RoleInfosComponent self,GateRoleInfo gateRoleInfo)
        {
            RoleInfo roleInfo = self.AddChildWithId<RoleInfo>(gateRoleInfo.UnitId); // roleInfo.id = gateRoleInfo.UnitId; 
            roleInfo.Name = gateRoleInfo.Name;
            roleInfo.Level = gateRoleInfo.Level;
            self.RoleInfos.Add(roleInfo);
        }

        public static RoleInfo GetRoleInfoByIndex(this RoleInfosComponent self, int index)
        {
            if (index < 0 || index >= self.RoleInfos.Count)
            {
                return null;
            }

            return self.RoleInfos[index];
        }
    }
}