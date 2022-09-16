
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgInpuntLoginViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_LoginBtnButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoginBtnButton == null )
     			{
		    		this.m_E_LoginBtnButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Popup_Login/Popup/E_LoginBtn");
     			}
     			return this.m_E_LoginBtnButton;
     		}
     	}

		public UnityEngine.UI.Image E_LoginBtnImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoginBtnImage == null )
     			{
		    		this.m_E_LoginBtnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Popup_Login/Popup/E_LoginBtn");
     			}
     			return this.m_E_LoginBtnImage;
     		}
     	}

		public UnityEngine.UI.Image E_AccountInputImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AccountInputImage == null )
     			{
		    		this.m_E_AccountInputImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Popup_Login/Popup/InputFields/E_AccountInput");
     			}
     			return this.m_E_AccountInputImage;
     		}
     	}

		public TMPro.TMP_InputField E_AccountInputTMP_InputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AccountInputTMP_InputField == null )
     			{
		    		this.m_E_AccountInputTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject,"Popup_Login/Popup/InputFields/E_AccountInput");
     			}
     			return this.m_E_AccountInputTMP_InputField;
     		}
     	}

		public UnityEngine.UI.Image E_PwdInputImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PwdInputImage == null )
     			{
		    		this.m_E_PwdInputImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Popup_Login/Popup/InputFields/E_PwdInput");
     			}
     			return this.m_E_PwdInputImage;
     		}
     	}

		public TMPro.TMP_InputField E_PwdInputTMP_InputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PwdInputTMP_InputField == null )
     			{
		    		this.m_E_PwdInputTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject,"Popup_Login/Popup/InputFields/E_PwdInput");
     			}
     			return this.m_E_PwdInputTMP_InputField;
     		}
     	}

		public UnityEngine.UI.Dropdown E_ServerAddressListDropdown
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ServerAddressListDropdown == null )
     			{
		    		this.m_E_ServerAddressListDropdown = UIFindHelper.FindDeepChild<UnityEngine.UI.Dropdown>(this.uiTransform.gameObject,"Popup_Login/Popup/E_ServerAddressList");
     			}
     			return this.m_E_ServerAddressListDropdown;
     		}
     	}

		public UnityEngine.UI.Image E_ServerAddressListImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ServerAddressListImage == null )
     			{
		    		this.m_E_ServerAddressListImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Popup_Login/Popup/E_ServerAddressList");
     			}
     			return this.m_E_ServerAddressListImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_LoginBtnButton = null;
			this.m_E_LoginBtnImage = null;
			this.m_E_AccountInputImage = null;
			this.m_E_AccountInputTMP_InputField = null;
			this.m_E_PwdInputImage = null;
			this.m_E_PwdInputTMP_InputField = null;
			this.m_E_ServerAddressListDropdown = null;
			this.m_E_ServerAddressListImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_LoginBtnButton = null;
		private UnityEngine.UI.Image m_E_LoginBtnImage = null;
		private UnityEngine.UI.Image m_E_AccountInputImage = null;
		private TMPro.TMP_InputField m_E_AccountInputTMP_InputField = null;
		private UnityEngine.UI.Image m_E_PwdInputImage = null;
		private TMPro.TMP_InputField m_E_PwdInputTMP_InputField = null;
		private UnityEngine.UI.Dropdown m_E_ServerAddressListDropdown = null;
		private UnityEngine.UI.Image m_E_ServerAddressListImage = null;
		public Transform uiTransform = null;
	}
}
