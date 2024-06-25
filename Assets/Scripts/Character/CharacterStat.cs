using Define;
using Item;
using Skill;
using Stat;
using System;
using System.Collections.Generic;

namespace Character {
    public class StatModifier<T> {
        public ECalculationType calcType;
        public T stat;
        public StatModifier(ECalculationType calcType, T stat) {
            this.calcType = calcType;
            this.stat = stat;
        }
    }
    public class InstanceStat<T> where T : new() {
        public T baseStat;
        public T totalStat;
        public List<StatModifier<T>> modifiedStats;

        public InstanceStat() {
            baseStat = new T();
            totalStat = new T();
            modifiedStats = new List<StatModifier<T>>();
        }
    }

    public class CharacterStat {
        public event Action<ECalculationType, BaseStat> OnChangeStat;
        protected InstanceStat<BaseStat> instanceStat;
        public InstanceStat<BaseStat> InstanceStat {
            get {
                if (instanceStat == null)
                    instanceStat = new InstanceStat<BaseStat>();
                return instanceStat;
            }
        }
        protected Dictionary<EItemType, BaseItem> equippedItem;
        public JumpSkill JumpSkill {
            get {
                if (equippedItem.TryGetValue(EItemType.Armor, out var item))
                    if (item.TryGetSkill(out var skill))
                        return skill as JumpSkill;
                return null;
            }
        }
        public AttackSkill AttackSkill {
            get {
                if (equippedItem.TryGetValue(EItemType.Weapon, out var item))
                    if (item.TryGetSkill(out var skill))
                        return skill as AttackSkill;
                return null;
            }
        }
        public CharacterStat() {
            instanceStat = new InstanceStat<BaseStat>();
            equippedItem = new Dictionary<EItemType, BaseItem>();
        }
        protected static void RecalculateStat(InstanceStat<BaseStat> instStat) {
            instStat.totalStat.Reset();
            instStat.totalStat.Add(instStat.baseStat);
            foreach (var statMod in instStat.modifiedStats) {
                switch (statMod.calcType) {
                    case ECalculationType.Add:
                        instStat.totalStat.Add(statMod.stat);
                        break;
                    case ECalculationType.Subtract:
                        instStat.totalStat.Subtract(statMod.stat);
                        break;
                    case ECalculationType.Divide:
                        instStat.totalStat.Divide(statMod.stat);
                        break;
                    case ECalculationType.Multiply:
                        instStat.totalStat.Multiply(statMod.stat);
                        break;
                }
            }
        }
        protected static void ModifyStat(StatModifier<BaseStat> statMod, InstanceStat<BaseStat> instStat) {
            switch (statMod.calcType) {
                case ECalculationType.Add:
                    instStat.totalStat.Add(statMod.stat);
                    break;
                case ECalculationType.Subtract:
                    instStat.totalStat.Subtract(statMod.stat);
                    break;
                case ECalculationType.Divide:
                    instStat.totalStat.Divide(statMod.stat);
                    break;
                case ECalculationType.Multiply:
                    instStat.totalStat.Multiply(statMod.stat);
                    break;
                default:
                    return;
            }

            instStat.modifiedStats.Add(statMod);
        }
        protected static void RemoveModifyStat(StatModifier<BaseStat> statMod, InstanceStat<BaseStat> instStat) {
            switch (statMod.calcType) {
                case ECalculationType.Add:
                    instStat.totalStat.Subtract(statMod.stat);
                    break;
                case ECalculationType.Subtract:
                    instStat.totalStat.Add(statMod.stat);
                    break;
                case ECalculationType.Divide:
                    instStat.totalStat.Multiply(statMod.stat);
                    break;
                case ECalculationType.Multiply:
                    instStat.totalStat.Divide(statMod.stat);
                    break;
                default:
                    return;
            }

            instStat.modifiedStats.Remove(statMod);
        }
        protected static void RemoveStat(BaseStat stat, InstanceStat<BaseStat> instStat) {
            int index = instStat.modifiedStats.FindIndex(s => ReferenceEquals(s.stat, stat));
            instStat.modifiedStats.RemoveAt(index);
            instStat.totalStat.Subtract(stat);
        }
        public void ChangeItemTo(BaseItem fromItem = null, BaseItem toItem = null) {
            if (fromItem != null) {
                var from = fromItem.GetStat();
                RemoveStat(from, InstanceStat);
                OnChangeStat?.Invoke(ECalculationType.Subtract, from);
                equippedItem.Remove(fromItem.GetItemType());
            }
            if (toItem != null) {
                var to = toItem.GetStat();
                ModifyStat(new StatModifier<BaseStat>(ECalculationType.Add, to), InstanceStat);
                OnChangeStat?.Invoke(ECalculationType.Add, to);
                equippedItem.Add(toItem.GetItemType(), toItem);
            }
        }
        public void ModifyStat(StatModifier<BaseStat> statMod) {
            ModifyStat(statMod, InstanceStat);
            OnChangeStat?.Invoke(statMod.calcType, statMod.stat);
        }
        public void RemoveModifyStat(StatModifier<BaseStat> statMod) {
            RemoveModifyStat(statMod, InstanceStat);
            OnChangeStat?.Invoke(statMod.calcType, statMod.stat);
        }
        public void ResetStatTo(CharacterStat toStat) {
            InstanceStat.modifiedStats.Clear();
            InstanceStat.baseStat.ResetTo(toStat.instanceStat.baseStat);
            foreach (var mod in toStat.InstanceStat.modifiedStats) {
                ModifyStat(new StatModifier<BaseStat>(mod.calcType, mod.stat));
            }
            RecalculateStat(InstanceStat);
        }
        public void ResetStatTo(BaseStat toStat) {
            InstanceStat.modifiedStats.Clear();
            InstanceStat.baseStat.ResetTo(toStat);
            RecalculateStat(InstanceStat);
        }
    }
}