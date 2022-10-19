
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgCharacterSelectViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_Button_EnterButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Button_EnterButton == null )
     			{
		    		this.m_E_Button_EnterButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Button_Enter");
     			}
     			return this.m_E_Button_EnterButton;
     		}
     	}

		public UnityEngine.UI.Image E_Button_EnterImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Button_EnterImage == null )
     			{
		    		this.m_E_Button_EnterImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Button_Enter");
     			}
     			return this.m_E_Button_EnterImage;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect E_CharacterListLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CharacterListLoopHorizontalScrollRect == null )
     			{
		    		this.m_E_CharacterListLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"E_CharacterList");
     			}
     			return this.m_E_CharacterListLoopHorizontalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_Button_EnterButton = null;
			this.m_E_Button_EnterImage = null;
			this.m_E_CharacterListLoopHorizontalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_Button_EnterButton = null;
		private UnityEngine.UI.Image m_E_Button_EnterImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_E_CharacterListLoopHorizontalScrollRect = null;
		public Transform uiTransform = null;
	}
}
