namespace ET
{
    [FriendClassAttribute(typeof(ET.RoleInfoDB))]
    public static class RoleInfoDBSystem
    {
        public static GateRoleInfo ToMessage(this RoleInfoDB self)
        {
            GateRoleInfo gateUser = new GateRoleInfo() { Name = self.Name, Level = self.level, UnitId = self.Id };
            return gateUser;
        }
    }
}