namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误
        
        // 110000以下的错误请看ErrorCore.cs
        
        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常
        public const int ERR_RealmddressError = 200002;

        public const int ERR_Login_AccountError = 200003;

        public const int ERR_Login_AccountNotExist = 200004;
        
        public const int ERR_Login_PasswordErr = 200005;
        
        public const int ERR_Login_RepeatedLogin = 200006;
        
        public const int ERR_Login_AccountNotLogin = 200007;

        public const int ERR_Login_ZoneNotExist = 200008;

        public const int ERR_Login_NoLoginGateInfo = 200009;

        public const int ERR_Login_MultiLogin = 200010;
        
        public const int ERR_Login_NoneGateUser = 200011;
        
        public const int ERR_Login_NoneAccountZoneDB = 200012;
        
        public const int ERR_Login_NoneName = 200013;
        
        public const int ERR_Login_NoneCheckName = 200014;  
        
        public const int ERR_Login_NameRepeated = 200015;
        
        public const int ERR_Login_NoRole = 200016;
        
        public const int ERR_Login_NoRoleDB= 200017;
        
        public const int ERR_Login_RoleNotExist= 200018;
        
        public const int ERR_Login_RoleInMap= 200019;
        
        

    }
}