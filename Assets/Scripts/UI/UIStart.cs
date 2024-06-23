using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class UIStart : UIBase {
    [SerializeField] List<TextPair> texts;
    [SerializeField] Button btn;
    public override UIBase CloseUI() {
        base.CloseUI();
        gameObject.SetActive(false);
        return this;
    }
    public override UIBase DrawUI() {
        base.DrawUI();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(OnStartBtn);
        gameObject.SetActive(true);
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
    public void OnStartBtn() {
        UIManager.Instance.CloseAllUI();
        GameManager.Instance.StartGame();
    }
    public override UIBase InitUI() {
        return this;
    }
}