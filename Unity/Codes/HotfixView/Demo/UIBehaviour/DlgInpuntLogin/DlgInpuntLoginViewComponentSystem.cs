
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgInpuntLoginViewComponentAwakeSystem : AwakeSystem<DlgInpuntLoginViewComponent> 
	{
		public override void Awake(DlgInpuntLoginViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgInpuntLoginViewComponentDestroySystem : DestroySystem<DlgInpuntLoginViewComponent> 
	{
		public override void Destroy(DlgInpuntLoginViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
