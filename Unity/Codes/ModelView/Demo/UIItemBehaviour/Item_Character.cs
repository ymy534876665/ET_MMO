
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class Scroll_Item_Character : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Character BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.RectTransform EG_AddRoleRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_EG_AddRoleRectTransform == null )
     				{
		    			this.m_EG_AddRoleRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_AddRole");
     				}
     				return this.m_EG_AddRoleRectTransform;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_AddRole");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_AddRoleBtnButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_AddRoleBtnButton == null )
     				{
		    			this.m_E_AddRoleBtnButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_AddRole/E_AddRoleBtn");
     				}
     				return this.m_E_AddRoleBtnButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_AddRole/E_AddRoleBtn");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_AddRoleBtnImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_AddRoleBtnImage == null )
     				{
		    			this.m_E_AddRoleBtnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_AddRole/E_AddRoleBtn");
     				}
     				return this.m_E_AddRoleBtnImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_AddRole/E_AddRoleBtn");
     			}
     		}
     	}

		public UnityEngine.RectTransform EG_RoleRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_EG_RoleRectTransform == null )
     				{
		    			this.m_EG_RoleRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_Role");
     				}
     				return this.m_EG_RoleRectTransform;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_Role");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_SelectBtnButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_SelectBtnButton == null )
     				{
		    			this.m_E_SelectBtnButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_Role/E_SelectBtn");
     				}
     				return this.m_E_SelectBtnButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_Role/E_SelectBtn");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_SelectBtnImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_SelectBtnImage == null )
     				{
		    			this.m_E_SelectBtnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_Role/E_SelectBtn");
     				}
     				return this.m_E_SelectBtnImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_Role/E_SelectBtn");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_DeleteButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_DeleteButton == null )
     				{
		    			this.m_E_DeleteButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_Role/CharacterSelectInfoTop/E_Delete");
     				}
     				return this.m_E_DeleteButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_Role/CharacterSelectInfoTop/E_Delete");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_DeleteImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_DeleteImage == null )
     				{
		    			this.m_E_DeleteImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_Role/CharacterSelectInfoTop/E_Delete");
     				}
     				return this.m_E_DeleteImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_Role/CharacterSelectInfoTop/E_Delete");
     			}
     		}
     	}

		public TMPro.TextMeshProUGUI E_LevelTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_LevelTextMeshProUGUI == null )
     				{
		    			this.m_E_LevelTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EG_Role/CaaracterInfo/E_Level");
     				}
     				return this.m_E_LevelTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EG_Role/CaaracterInfo/E_Level");
     			}
     		}
     	}

		public TMPro.TextMeshProUGUI E_NameTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_NameTextMeshProUGUI == null )
     				{
		    			this.m_E_NameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EG_Role/CaaracterInfo/E_Name");
     				}
     				return this.m_E_NameTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EG_Role/CaaracterInfo/E_Name");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_SelectFrameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_SelectFrameImage == null )
     				{
		    			this.m_E_SelectFrameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Frame/E_SelectFrame");
     				}
     				return this.m_E_SelectFrameImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Frame/E_SelectFrame");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EG_AddRoleRectTransform = null;
			this.m_E_AddRoleBtnButton = null;
			this.m_E_AddRoleBtnImage = null;
			this.m_EG_RoleRectTransform = null;
			this.m_E_SelectBtnButton = null;
			this.m_E_SelectBtnImage = null;
			this.m_E_DeleteButton = null;
			this.m_E_DeleteImage = null;
			this.m_E_LevelTextMeshProUGUI = null;
			this.m_E_NameTextMeshProUGUI = null;
			this.m_E_SelectFrameImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.RectTransform m_EG_AddRoleRectTransform = null;
		private UnityEngine.UI.Button m_E_AddRoleBtnButton = null;
		private UnityEngine.UI.Image m_E_AddRoleBtnImage = null;
		private UnityEngine.RectTransform m_EG_RoleRectTransform = null;
		private UnityEngine.UI.Button m_E_SelectBtnButton = null;
		private UnityEngine.UI.Image m_E_SelectBtnImage = null;
		private UnityEngine.UI.Button m_E_DeleteButton = null;
		private UnityEngine.UI.Image m_E_DeleteImage = null;
		private TMPro.TextMeshProUGUI m_E_LevelTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_NameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_SelectFrameImage = null;
		public Transform uiTransform = null;
	}
}
