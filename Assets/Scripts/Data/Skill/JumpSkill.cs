using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

namespace Skill {
    [CreateAssetMenu(menuName = "Skill/Jump Skill")]
    public class JumpSkill : BaseSkill {
        [field: SerializeField] public float Duration { get; protected set; }
        [field: SerializeField] public float Speed { get; protected set; }
        [field: SerializeField] public float DamageMultiplier { get; protected set; }
        [field: SerializeField] public Animator overrideAnimator { get; protected set; }

        public JumpSkill(int key, string skillName, string description) : base(key, skillName, description) {
        }
        public override string GetDescription() {
            return description;
        }
        public override string GetSkillName() {
            return skillName;
        }
        public override IEnumerator StartSkill(PlayerContainer container) {
            float duration = Duration;
            container.Movement.ChangeDownForce(0);
            container.Movement.ApplyImpact(Speed);
            container.BattleSystem.OnHitTarget += OnHitTarget;
            yield return new WaitForSeconds(duration);
            container.Movement.ChangeDownForce(0.5f);
            container.BattleSystem.OnHitTarget -= OnHitTarget;
        }

        private void OnHitTarget(BaseContainer target) {
            // TODO play sound
            // TODO damage target

        }
    }
}