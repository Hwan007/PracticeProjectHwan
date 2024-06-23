using System.Collections.Generic;
using UI;
using UnityEngine;

public class UITitle : UIBase {
    [SerializeField] List<TextPair> texts;
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
}
