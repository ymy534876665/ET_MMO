﻿namespace ET
{
    /// <summary>
    /// 账号区服数据库
    /// </summary>
    [ComponentOf(typeof(GateUser))]
    [ChildType(typeof(RoleInfoDB))]
    public class AccountZoneDB : Entity,IAwake,IDestroy
    {
        public string Account;

        public int LoginZoneId;
    }
}