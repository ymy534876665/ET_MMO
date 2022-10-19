﻿using System.Collections;
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
			self.View.E_Button_StartButton.AddListenerAsync(self.OnStartBtnClickHandle);
		}

		public static void ShowWindow(this DlgTapToStart self, Entity contextData = null)
		{
		}

		public static async ETTask OnStartBtnClickHandle(this DlgTapToStart self)
		{
			try
			{
				int errorCode = await LoginHelper.GetRoleInfos(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					return;
				}
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_TapToStart);
				self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_CharacterSelect);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
		 

	}
}
