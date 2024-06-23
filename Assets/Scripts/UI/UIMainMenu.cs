using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIBase {
    [SerializeField] List<TextPair> texts;
    [SerializeField] Button statusBtn;
    [SerializeField] Button inventoryBtn;
    [SerializeField] Button startBtn;
    public event Action OnStatus;
    public event Action OnInventory;
    public event Action OnStart;
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

    public override UIBase InitUI() {
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

    public void OnStatusBtn() {
        UIManager.Instance.CloseAllUI(1);
        OnStatus?.Invoke();
        UIManager.Instance.TryGetUI<UIStatus>(nameof(UIStatus), ui => ui.DrawUI());
    }
    public void OnStartBtn() {
        UIManager.Instance.CloseAllUI(1);
        OnStart?.Invoke();
        
    }
    public void OnInventoryBtn() {
        UIManager.Instance.CloseAllUI(1);
        OnInventory?.Invoke();
        UIManager.Instance.TryGetUI<UIInventory>(nameof(UIInventory), ui => ui.DrawUI());
    }
}
