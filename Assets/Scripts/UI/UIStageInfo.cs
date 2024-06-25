using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Character;

public class UIStageInfo : UIBase {
    [SerializeField] List<TextPair> texts;
    [SerializeField] Slider stageProgress;
    [SerializeField] Slider monsterHealth;
    [SerializeField] TMP_Text monsterHealthText;
    BaseContainer targetMonster;
    StageManager stageManager;

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

    public void ActivateStageProgress(StageManager manager) {
        stageProgress.gameObject.SetActive(true);
        manager.stageProgress += UpdateStage;
        stageManager = manager;
    }

    public void UpdateStage(float current, float full) {
        stageProgress.value = current / full;
    }

    public void DeactivateStageProgress() {
        stageProgress.gameObject.SetActive(false);
        stageManager.stageProgress -= UpdateStage;
        stageManager = null;
    }

    public void ActivateMonsterHealth(BaseContainer monster) {
        monsterHealth.gameObject.SetActive(true);
        targetMonster = monster;
        monster.HealthSystem.OnHealthChange += UpdateMonsterHealth;
    }

    public void UpdateMonsterHealth(int current, int full) {
        monsterHealth.value = current / full;
        monsterHealthText.text = $"{current:N0} / {full:N0}";
    }

    public void DeactivateMonsterHealth() {
        monsterHealth.gameObject.SetActive(false);
        if (targetMonster != null) {
            targetMonster.HealthSystem.OnHealthChange -= UpdateMonsterHealth;
            targetMonster = null;
        }
    }
}
