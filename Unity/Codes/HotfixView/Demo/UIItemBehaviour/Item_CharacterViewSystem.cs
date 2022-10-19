
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_CharacterDestroySystem : DestroySystem<Scroll_Item_Character> 
	{
		public override void Destroy( Scroll_Item_Character self )
		{
			self.DestroyWidget();
		}
	}
}
