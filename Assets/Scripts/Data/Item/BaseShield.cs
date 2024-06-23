using Item;
using Skill;
using Stat;
using UnityEngine;

namespace Item {
    [CreateAssetMenu(menuName = "Item/Shield")]
    public class BaseShield : BaseItem {
        public BaseShield() { eItemType = Define.EItemType.Shield; }
        public override bool TryGetSkill(out BaseSkill skill) {
            skill = null;
            return false;
        }
    }
}