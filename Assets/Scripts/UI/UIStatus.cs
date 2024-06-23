using Define;
using Stat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UI;
using UnityEngine;

public class UIStatus : UIBase {
    [SerializeField] List<TextPair> texts;
    [SerializeField] TMP_Text description;
    BaseContainer targetContainer;
    public override UIBase CloseUI() {
        base.CloseUI();
        gameObject.SetActive(false);
        if (targetContainer != null) {
            targetContainer.CharacterStat.OnChangeStat -= UpdateUI;
            targetContainer = null;
        }
        return this;
    }

    public override UIBase DrawUI() {
        base.DrawUI();
        gameObject.SetActive(true);
        return this;
    }

    public UIStatus DrawDescription(BaseContainer container) {
        StringBuilder sb = new StringBuilder();
        GetDescriptions(container.CharacterStat.InstanceStat, ref sb);
        description.text = sb.ToString();
        targetContainer = container;
        container.CharacterStat.OnChangeStat += UpdateUI;
        return this;
    }

    private void UpdateUI(ECalculationType type, BaseStat stat) {
        UpdateUI();
    }

    void GetDescriptions(InstanceStat<BaseStat> stat, ref StringBuilder sb) {
        BaseStat baseStat = stat.baseStat;
        BaseStat totalStat = stat.totalStat;

        foreach (var st in totalStat.stats) {
            sb.Append(st.Key);
            sb.Append(" : ");
            sb.Append(st.Value);
            if (baseStat.stats.TryGetValue(st.Key, out int baseSt)) {
                sb.Append("(");
                var dif = st.Value - baseSt;
                if (dif > 0)
                    sb.Append("+");
                sb.Append(dif);
                sb.Append(")");
            }
            sb.Append('\n');
        }
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
        DrawDescription(targetContainer);
        return this;
    }

}
