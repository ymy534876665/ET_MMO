using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgTapToStart))]
	public static  class DlgTapToStartSystem
	{

		public static void RegisterUIEvent(this DlgTapToStart self)
		{
			self.View.E_StartBtnButton.AddListenerAsync(self.OnStartBtnClickHandle);
		}

		public static void ShowWindow(this DlgTapToStart self, Entity contextData = null)
		{
		}

		public static async ETTask OnStartBtnClickHandle(this DlgTapToStart self)
		{
			try
			{

			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
		 

	}
}
