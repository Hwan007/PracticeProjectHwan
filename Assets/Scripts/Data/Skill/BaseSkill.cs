using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill {
    public abstract class BaseSkill : ScriptableObject {
        [SerializeField] protected int key;
        [SerializeField] protected string skillName;
        [SerializeField] protected string description;
        [field: SerializeField] public float BaseCoolTime { get; protected set; }

        public abstract string GetSkillName();
        public abstract string GetDescription();
        public BaseSkill(int key, string skillName, string description) {
            this.key = key;
            this.skillName = skillName;
            this.description = description;
        }
        public abstract IEnumerator StartSkill(PlayerContainer container);
    }
}