using Skill;
using Stat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item {
    [CreateAssetMenu(menuName = "Item/Weapon")]
    public class BaseWeapon : BaseItem{
        [field: SerializeField] public AttackSkill Skill { get; protected set; }
        public BaseWeapon() { eItemType = Define.EItemType.Weapon; }
        public override bool TryGetSkill(out BaseSkill skill) {
            if (Skill != null) {
                skill = Skill;
                return true;
            }
            skill = null;
            return false;
        }
    }
}