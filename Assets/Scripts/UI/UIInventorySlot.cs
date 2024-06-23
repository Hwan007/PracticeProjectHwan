using Item;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : UIBase {
    [SerializeField] List<TextPair> texts;
    [SerializeField] Image itemIcon;
    [SerializeField] Toggle toggle;
    BaseItem item;
    public event Action<BaseItem> ActOnClick;
    public override UIBase CloseUI() {
        base.CloseUI();
        gameObject.SetActive(false);
        return this;
    }

    public override UIBase DrawUI() {
        base.DrawUI();
        gameObject.SetActive(true);
        return this;
    }

    public UIInventorySlot DrawItem(BaseItem item) {
        Debug.Log("TODO get image from DataManager");
        // itemIcon
        return this;
    }

    public override UIBase InitUI() {
        toggle.onValueChanged.AddListener(OnClick);
        return this;
    }

    public override void SetFontScale(float fontScale) {
        foreach (var text in texts) {
            text.text.fontSize = text.fontSize * fontScale;
        }
    }

    public override UIBase UpdateUI() {
        return this;
    }

    void OnClick(bool onoff) {
        if (onoff) {
            ActOnClick?.Invoke(item);
        }
    }
}