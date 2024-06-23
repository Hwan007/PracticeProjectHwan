using Item;
using Skill;
using Stat;
using UnityEngine;

namespace Item {
    [CreateAssetMenu(menuName = "Item/Armor")]
    public class BaseArmor : BaseItem {
        [field: SerializeField] public JumpSkill Skill { get; protected set; }
        public BaseArmor() { eItemType = Define.EItemType.Armor; }
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