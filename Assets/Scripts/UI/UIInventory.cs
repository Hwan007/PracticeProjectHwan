using Define;
using Item;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase {
    [SerializeField] List<TextPair> texts;
    [SerializeField] UIInventorySlot slotPrefab;
    [SerializeField] RectTransform slotRoot;
    [SerializeField] TMP_Text description;
    [SerializeField] Toggle weapon;
    [SerializeField] Toggle shield;
    [SerializeField] Toggle armor;
    [SerializeField] Button equip;
    [SerializeField] Button destroy;
    CustomPool<UIInventorySlot> slots;
    BaseItem selectedItem;
    EItemType selectedType;
    public override UIBase CloseUI() {
        base.CloseUI();
        gameObject.SetActive(false);
        selectedItem = null;
        return this;
    }

    public override UIBase DrawUI() {
        base.DrawUI();
        gameObject.SetActive(true);
        selectedItem = null;
        UpdateUI();
        return this;
    }

    public UIInventory DrawItemsByType(EItemType type) {
        selectedType = type;
        DrawItems(CharacterManager.Instance.GetOwnedItems());
        return this;
    }

    public UIInventory DrawItems(BaseItem[] items) {
        foreach (BaseItem item in items) {
            DrawItem(item).transform.SetAsFirstSibling();
        }
        return this;
    }

    UIInventorySlot DrawItem(BaseItem item) {
        var slot = slots.Get();
        slot.DrawUI();
        slot.DrawItem(item);
        slot.ActOnClick += DrawDescription;
        slot.ActOnClick += (data) => selectedItem = data;
        return slot;
    }

    public void DrawDescription(BaseItem item) {
        Debug.Log("TODO get description from DataManager");
        var desc = item.GetStat().GetDescription();
        description.text = desc;
    }

    public override UIBase InitUI() {
        slots = new CustomPool<UIInventorySlot>(slotPrefab, slotRoot, null, null, null, null, 5, true);
        selectedType = EItemType.Weapon;
        weapon.isOn = true;
        weapon.onValueChanged.AddListener(DisplayWeaponSlot);
        shield.onValueChanged.AddListener(DisplayShieldSlot);
        armor.onValueChanged.AddListener(DisplayArmorSlot);
        return this;
    }
    void DisplayWeaponSlot(bool onoff) {
        if (onoff) {
            DrawItemsByType(EItemType.Weapon);
        }
    }
    void DisplayShieldSlot(bool onoff) {
        if (onoff) {
            DrawItemsByType(EItemType.Shield);
        }
    }
    void DisplayArmorSlot(bool onoff) {
        if (onoff) {
            DrawItemsByType(EItemType.Armor);
        }
    }

    public override void SetFontScale(float fontScale) {
        foreach (var text in texts) {
            text.text.fontSize = text.fontSize * fontScale;
        }
    }

    public override UIBase UpdateUI() {
        DrawItemsByType(selectedType);
        return this;
    }

    void OnEquipBtn() {
        if (selectedItem != null) {
            CharacterManager.Instance.EquipItem(selectedItem);
            UpdateUI();
        }
    }

    void OnDestroyButton() {
        if (selectedItem != null) {
            CharacterManager.Instance.DestoryItem(selectedItem);
            UpdateUI();
        }
    }
}
