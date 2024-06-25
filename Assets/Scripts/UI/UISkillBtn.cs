using Skill;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Character;
public class UISkillBtn : UIBase {
    [SerializeField] List<TextPair> texts;
    [SerializeField] Button JumpSkill;
    [SerializeField] Button AttackSkill;

    public override UIBase CloseUI() {
        base.CloseUI();
        gameObject.SetActive(false);
        if (StageManager.Instance.player.BattleSystem is PlayerBattleSystem pb) {
            pb.OnAttackSkillTimer -= UpdateAttacSkillTimer;
            pb.OnJumpSkillTimer -= UpdateJumpSkillTimer;
        }
        return this;
    }
    public override UIBase DrawUI() {
        base.DrawUI();
        gameObject.SetActive(true);
        if (StageManager.Instance.player.BattleSystem is PlayerBattleSystem pb) {
            pb.OnAttackSkillTimer += UpdateAttacSkillTimer;
            pb.OnJumpSkillTimer += UpdateJumpSkillTimer;
        }
        return this;
    }
    private void UpdateJumpSkillTimer(float current, float full) {
        JumpSkill.image.fillAmount = (full - current) / full;
    }
    private void UpdateAttacSkillTimer(float current, float full) {
        AttackSkill.image.fillAmount = (full - current) / full;
    }
    public override UIBase InitUI() {
        JumpSkill.image.fillMethod = Image.FillMethod.Radial360;
        AttackSkill.image.fillMethod = Image.FillMethod.Radial360;
        JumpSkill.onClick.AddListener(OnJumpSkillBtn);
        AttackSkill.onClick.AddListener(OnAttackSkillBtn);
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
    void OnJumpSkillBtn() {
        InputManager.Instance.CallJumpSkillEvent();
    }
    void OnAttackSkillBtn() {
        InputManager.Instance.CallAttackSkillEvent();
    }
}