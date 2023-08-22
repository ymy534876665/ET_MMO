
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgQueueViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_ExitBtnButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExitBtnButton == null )
     			{
		    		this.m_E_ExitBtnButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_ExitBtn");
     			}
     			return this.m_E_ExitBtnButton;
     		}
     	}

		public UnityEngine.UI.Image E_ExitBtnImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExitBtnImage == null )
     			{
		    		this.m_E_ExitBtnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_ExitBtn");
     			}
     			return this.m_E_ExitBtnImage;
     		}
     	}

		public TMPro.TextMeshProUGUI E_QueueDesTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_QueueDesTextMeshProUGUI == null )
     			{
		    		this.m_E_QueueDesTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_QueueDes");
     			}
     			return this.m_E_QueueDesTextMeshProUGUI;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ExitBtnButton = null;
			this.m_E_ExitBtnImage = null;
			this.m_E_QueueDesTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_ExitBtnButton = null;
		private UnityEngine.UI.Image m_E_ExitBtnImage = null;
		private TMPro.TextMeshProUGUI m_E_QueueDesTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
