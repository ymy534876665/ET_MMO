
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgTapToStartViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_Button_StartButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Button_StartButton == null )
     			{
		    		this.m_E_Button_StartButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"TapToStart/E_Button_Start");
     			}
     			return this.m_E_Button_StartButton;
     		}
     	}

		public UnityEngine.UI.Image E_Button_StartImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Button_StartImage == null )
     			{
		    		this.m_E_Button_StartImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"TapToStart/E_Button_Start");
     			}
     			return this.m_E_Button_StartImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_Button_StartButton = null;
			this.m_E_Button_StartImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_Button_StartButton = null;
		private UnityEngine.UI.Image m_E_Button_StartImage = null;
		public Transform uiTransform = null;
	}
}
