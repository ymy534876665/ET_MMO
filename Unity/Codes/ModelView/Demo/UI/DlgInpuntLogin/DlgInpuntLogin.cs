namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgInpuntLogin :Entity,IAwake,IUILogic
	{

		public DlgInpuntLoginViewComponent View { get => this.Parent.GetComponent<DlgInpuntLoginViewComponent>();} 

		 

	}
}
