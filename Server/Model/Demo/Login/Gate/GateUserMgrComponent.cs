using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(GateUser))]
    public class GateUserMgrComponent : Entity,IAwake,IDestroy
    {
        /// <summary>
        /// key 账号
        /// </summary>
        public Dictionary<string, GateUser> Users = new Dictionary<string, GateUser>();
    }
}