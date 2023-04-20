using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgCreateCharacter))]
	public static  class DlgCreateCharacterSystem
	{

		public static void RegisterUIEvent(this DlgCreateCharacter self)
		{
			self.View.E_CreateBtnButton.AddListenerAsync(self.OnClickCreateBtnHandler);
			self.View.E_BackBtnButton.AddListener(self.OnClickBackBtnHandler);
		}

		public static void ShowWindow(this DlgCreateCharacter self, Entity contextData = null)
		{
		}

		public static async ETTask OnClickCreateBtnHandler(this DlgCreateCharacter self)
		{
			string name = self.View.E_CharacterNameTMP_InputField.text;
			if (string.IsNullOrEmpty(name))
			{
				return;
			}

			try
			{
				int error =  await LoginHelper.CreateRole(self.ZoneScene(),name);
				if (error != ErrorCode.ERR_Success)
				{
					return;
				}
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_CreateCharacter);
				self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_CharacterSelect);
			}
			catch (Exception e)
			{
				
				Log.Error(e.ToString());
			}
			
			
			await ETTask.CompletedTask;
		}
		 
		
		public static void OnClickBackBtnHandler(this DlgCreateCharacter self)
		{
			self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_CreateCharacter);
			self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_CharacterSelect);
		}

	}
}
