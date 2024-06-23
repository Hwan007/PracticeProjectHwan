using Define;
using Skill;
using Stat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item {
    public abstract class BaseItem : ScriptableObject {
        [SerializeField] protected int itemKey;
        [field: SerializeField] public CustomStat data { get; protected set; }

        protected string itemName;
        protected string itemDescription;
        protected int level;
        protected EItemType eItemType;

        public virtual string GetItemName() {
            return itemName;
        }
        public virtual string GetDescription() {
            return itemDescription;
        }
        public abstract bool TryGetSkill(out BaseSkill skill);
        public virtual BaseStat GetStat() {
            return data.ConvertToBaseStat();
        }
        public virtual EItemType GetItemType() { return eItemType; }
    }
}