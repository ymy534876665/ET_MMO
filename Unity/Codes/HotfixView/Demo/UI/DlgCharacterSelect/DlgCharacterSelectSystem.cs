using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof(DlgCharacterSelect))]
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    public static class DlgCharacterSelectSystem
    {

        public static void RegisterUIEvent(this DlgCharacterSelect self)
        {
            self.View.E_CharacterListLoopHorizontalScrollRect.AddItemRefreshListener(self.OnLoopRefreshHandler);
        }

        public static void ShowWindow(this DlgCharacterSelect self, Entity contextData = null)
        {
            self.AddUIScrollItems(ref self.ScrollItemCharacters, 4);
            self.View.E_CharacterListLoopHorizontalScrollRect.SetVisible(true, 4);
        }

        public static void HideWindow(this DlgCharacterSelect self)
        {
            self.RemoveUIScrollItems(ref self.ScrollItemCharacters);
        }

        public static void OnLoopRefreshHandler(this DlgCharacterSelect self, Transform transform, int index)
        {

            Scroll_Item_Character item = self.ScrollItemCharacters[index].BindTrans(transform);
            RoleInfo roleInfo = self.ZoneScene().GetComponent<RoleInfosComponent>().GetRoleInfoByIndex(index);
            item.E_SelectFrameImage.SetVisible(false);
            if (roleInfo == null)
            {
                item.EG_AddRoleRectTransform.SetVisible(true);
                item.EG_RoleRectTransform.SetVisible(false);
                item.E_AddRoleBtnButton.AddListener(self.OnAddRoleClickHandler);
            }
            else
            {
                item.EG_AddRoleRectTransform.SetVisible(false);
                item.EG_RoleRectTransform.SetVisible(true);
                item.E_LevelTextMeshProUGUI.text = $"Lv.{roleInfo.Level}";
                item.E_NameTextMeshProUGUI.text = roleInfo.Name;
            }
        }

        public static void OnAddRoleClickHandler(this DlgCharacterSelect self)
        {
            self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_CharacterSelect);
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_CreateCharacter);
        }

    }
}
