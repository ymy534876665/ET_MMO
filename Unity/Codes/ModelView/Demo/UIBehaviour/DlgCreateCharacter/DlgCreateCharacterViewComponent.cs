
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgCreateCharacterViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image E_Character_ImgImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Character_ImgImage == null )
     			{
		    		this.m_E_Character_ImgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Character_Img");
     			}
     			return this.m_E_Character_ImgImage;
     		}
     	}

		public UnityEngine.UI.Button E_CreateBtnButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CreateBtnButton == null )
     			{
		    		this.m_E_CreateBtnButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_CreateBtn");
     			}
     			return this.m_E_CreateBtnButton;
     		}
     	}

		public UnityEngine.UI.Image E_CreateBtnImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CreateBtnImage == null )
     			{
		    		this.m_E_CreateBtnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_CreateBtn");
     			}
     			return this.m_E_CreateBtnImage;
     		}
     	}

		public UnityEngine.UI.Button E_BackBtnButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackBtnButton == null )
     			{
		    		this.m_E_BackBtnButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"TopBar/E_BackBtn");
     			}
     			return this.m_E_BackBtnButton;
     		}
     	}

		public UnityEngine.UI.Image E_CharacterNameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CharacterNameImage == null )
     			{
		    		this.m_E_CharacterNameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_CharacterName");
     			}
     			return this.m_E_CharacterNameImage;
     		}
     	}

		public TMPro.TMP_InputField E_CharacterNameTMP_InputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CharacterNameTMP_InputField == null )
     			{
		    		this.m_E_CharacterNameTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject,"E_CharacterName");
     			}
     			return this.m_E_CharacterNameTMP_InputField;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_Character_ImgImage = null;
			this.m_E_CreateBtnButton = null;
			this.m_E_CreateBtnImage = null;
			this.m_E_BackBtnButton = null;
			this.m_E_CharacterNameImage = null;
			this.m_E_CharacterNameTMP_InputField = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_Character_ImgImage = null;
		private UnityEngine.UI.Button m_E_CreateBtnButton = null;
		private UnityEngine.UI.Image m_E_CreateBtnImage = null;
		private UnityEngine.UI.Button m_E_BackBtnButton = null;
		private UnityEngine.UI.Image m_E_CharacterNameImage = null;
		private TMPro.TMP_InputField m_E_CharacterNameTMP_InputField = null;
		public Transform uiTransform = null;
	}
}
