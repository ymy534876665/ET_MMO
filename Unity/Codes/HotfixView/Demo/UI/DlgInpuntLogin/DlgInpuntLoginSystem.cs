using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgInpuntLogin))]
	public static  class DlgInpuntLoginSystem
	{

		public static void RegisterUIEvent(this DlgInpuntLogin self)
		{
			self.View.E_LoginBtnButton.AddListenerAsync(self.OnClickLoginBtn);
		}

		private static async ETTask OnClickLoginBtn(this DlgInpuntLogin self)
		{
			string account = self.View.E_AccountInputTMP_InputField.text;
			string pwd = self.View.E_PwdInputTMP_InputField.text;
			string address = self.View.E_ServerAddressListDropdown.options[1].text;
			Log.Debug(string.Format("开始登录服务器：ip port:{0},account:{1},pwd:{2}",address,account,pwd));
			await LoginHelper.Login(self.ZoneScene(),address,account,pwd);
			PlayerPrefs.SetString("Account",account);
			PlayerPrefs.SetString("Password",pwd);
		}

		public static void ShowWindow(this DlgInpuntLogin self, Entity contextData = null)
		{
			string[] addressArr = File.ReadAllLines(@"../Excel/ServerAddress.txt");
			self.View.E_ServerAddressListDropdown.options.Clear();
			foreach (string s in addressArr)
			{
				//self.View.E_ServerAddressListDropdown.options
				Dropdown.OptionData optionData = new Dropdown.OptionData();
				optionData.text = s;
				self.View.E_ServerAddressListDropdown.options.Add(optionData);
			}

			string account = PlayerPrefs.GetString("Account", string.Empty);
			string pwd = PlayerPrefs.GetString("Password", string.Empty);
			self.View.E_AccountInputTMP_InputField.text = account;
			self.View.E_PwdInputTMP_InputField.text = pwd;

		}

		 

	}
}
