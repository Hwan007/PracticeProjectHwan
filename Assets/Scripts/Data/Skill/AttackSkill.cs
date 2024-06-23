using Define;
using Stat;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Skill {
    [CreateAssetMenu(menuName = "Skill/Attack Skill")]
    public class AttackSkill : BaseSkill {
        [field: SerializeField] public float Duration { get; protected set; }
        [field: SerializeField] public ECalculationType CalculationType { get; protected set; }
        [field: SerializeField] public CustomStat Buff {  get; protected set; }
        [field: SerializeField] public float Gravity { get; protected set; }
        [field: SerializeField] public GameObject effectPrefab { get; protected set; }
        [field: SerializeField] public Animator overrideAnimator { get; protected set; }

        public override string GetDescription() {
            return description;
        }
        public override string GetSkillName() {
            return skillName;
        }
        public override IEnumerator StartSkill(BaseContainer container) {
            float duration = Duration;
            StatModifier<BaseStat> modifier = new StatModifier<BaseStat>(CalculationType, Buff.ConvertToBaseStat());
            container.CharacterStat.ModifyStat(modifier);
            container.BattleSystem.OnAttackStart += OnAttackStart;
            container.BattleSystem.OnHitTarget += OnHitTarget;
            yield return new WaitForSeconds(duration);
            container.CharacterStat.RemoveModifyStat(modifier);
            container.BattleSystem.OnAttackStart -= OnAttackStart;
            container.BattleSystem.OnHitTarget -= OnHitTarget;
        }

        private void OnHitTarget(BaseContainer target) {
            // TODO play sound
            // TODO damage target

        }

        private void OnAttackStart() {
            // TODO play sound

        }

        public AttackSkill(int key, string skillName, string description) : base(key, skillName, description) {

        }
    }
}