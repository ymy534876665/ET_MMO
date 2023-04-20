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

        public static void DeleteRoleInfoById(this RoleInfosComponent self,long roleId)
        {
            int index =  self.RoleInfos.FindIndex((roleInfo) => { return roleInfo.Id == roleId; });
            
            self.RoleInfos.RemoveAt(index);
        }

        public static bool IsCurrentRoleExist(this RoleInfosComponent self)
        {
            if (self.CurrentUnitId == 0)
            {
                return false;
            }

            RoleInfo info = self.RoleInfos.Find(d => { return d.Id == self.CurrentUnitId;});
            if (info == null)
            {
                return false;
            }
            return true;
        }
    }
}