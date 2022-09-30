
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgTapToStartViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_StartBtnButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartBtnButton == null )
     			{
		    		this.m_E_StartBtnButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"TapToStart/E_StartBtn");
     			}
     			return this.m_E_StartBtnButton;
     		}
     	}

		public UnityEngine.UI.Image E_StartBtnImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartBtnImage == null )
     			{
		    		this.m_E_StartBtnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"TapToStart/E_StartBtn");
     			}
     			return this.m_E_StartBtnImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_StartBtnButton = null;
			this.m_E_StartBtnImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_StartBtnButton = null;
		private UnityEngine.UI.Image m_E_StartBtnImage = null;
		public Transform uiTransform = null;
	}
}
