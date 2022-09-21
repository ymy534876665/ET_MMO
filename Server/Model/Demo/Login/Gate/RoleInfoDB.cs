namespace ET
{
    /// <summary>
    /// 角色数据库
    /// </summary>
    public class RoleInfoDB : Entity,IAwake,IDestroy
    {
        public string Account;

        public long AccountZoneId;

        public bool IsDeleted;

        public string Name;

        public int level;
    }
}