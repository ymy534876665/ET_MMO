﻿namespace ET
{
    /// <summary>
    /// 顶号和多次登录标识  GateUser
    /// </summary>
    [ComponentOf(typeof(GateUser))]
    public class MultiLoginComponent : Entity,IAwake,IDestroy
    {
        public long Timer_Over;
    }
}