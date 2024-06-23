using Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stat {
    [Serializable]
    public struct StatPair {
        public EStatType type;
        public int value;
        public StatPair(EStatType type, int value) {
            this.type = type;
            this.value = value;
        }
    }
    public class BaseStat {
        public Dictionary<EStatType, int> stats { get; protected set; }
        public BaseStat(EStatType[] statTypes) {
            if (statTypes != null) {
                stats = new Dictionary<EStatType, int>(statTypes.Length);
                foreach (EStatType statType in statTypes) {
                    stats.Add(statType, 0);
                }
            }
        }
        public BaseStat() {
            stats = new Dictionary<EStatType, int>();
        }
        public virtual AttackData[] GetAttackData() {
            List<AttackData> attackDatas = new List<AttackData>();
            if (stats.TryGetValue(EStatType.NormalAtk, out int normal)) {
                attackDatas.Add(new AttackData(EAttackType.Normal, normal,0));
            }
            if (stats.TryGetValue(EStatType.FireAtk, out int fire)) {
                attackDatas.Add(new AttackData(EAttackType.Normal, fire, stats[EStatType.FireTime]));
            }
            if (stats.TryGetValue(EStatType.VoidAtk, out int voi)) {
                attackDatas.Add(new AttackData(EAttackType.Normal, voi, stats[EStatType.VoidTime]));
            }
            return attackDatas.ToArray();
        }
        public virtual EStatType[] GetStatTypes() {
            return stats.Keys.ToArray();
        }
        public virtual int GetStat(EStatType type) {
            if (stats.ContainsKey(type)) {
                return stats[type];
            }
            return 0;
        }
        public virtual int Add(StatPair add) {
            if (stats.ContainsKey(add.type)) {
                stats[add.type] += add.value;
            }
            else {
                stats.Add(add.type, add.value);
            }
            return stats[add.type];
        }
        public virtual int Subtract(StatPair sub) {
            int ret = 0;
            if (stats.ContainsKey(sub.type)) {
                stats[sub.type] -= sub.value;
                if (stats[sub.type] < 0) {
                    stats.Remove(sub.type);
                }
                else
                    ret = stats[sub.type];
            }
            return ret;
        }
        public virtual int Multiply(StatPair mul) {
            if (stats.ContainsKey(mul.type)) {
                stats[mul.type] *= mul.value;
                return stats[mul.type];
            }
            return 0;
        }
        public virtual int Divide(StatPair div) {
            if (stats.ContainsKey(div.type)) {
                stats[div.type] /= div.value;
                return stats[div.type];
            }
            return 0;
        }
        public virtual void Reset() {
            var keys = GetStatTypes();
            foreach (var key in keys) {
                stats[key] = 0;
            }
        }
        public virtual void ResetTo(StatPair toValue) {
            if (stats.ContainsKey(toValue.type))
                stats[toValue.type] = toValue.value;
        }
        public virtual BaseStat Add(BaseStat add) {
            foreach (var statType in add.GetStatTypes()) {
                Add(new StatPair(statType, add.GetStat(statType)));
            }
            return this;
        }
        public virtual BaseStat Subtract(BaseStat sub) {
            foreach (var statType in sub.GetStatTypes()) {
                Subtract(new StatPair(statType, sub.GetStat(statType)));
            }
            return this;
        }
        public virtual BaseStat Divide(BaseStat div) {
            foreach (var statType in div.GetStatTypes()) {
                Divide(new StatPair(statType, div.GetStat(statType)));
            }
            return this;
        }
        public virtual BaseStat Multiply(BaseStat mult) {
            foreach (var statType in mult.GetStatTypes()) {
                Multiply(new StatPair(statType, mult.GetStat(statType)));
            }
            return this;
        }
        public virtual BaseStat ResetTo(BaseStat to) {
            stats.Clear();
            foreach (var statType in to.GetStatTypes()) {
                Add(new StatPair(statType, to.GetStat(statType)));
            }
            return this;
        }
        public string GetDescription() {
            StringBuilder sb = new StringBuilder();
            foreach (var st in stats) {
                sb.Append(st.Key);
                sb.Append(" : ");
                sb.Append(st.Value);
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
    [Serializable]
    public class CustomStat {
        public List<StatPair> statList = new List<StatPair>() {
            new StatPair(EStatType.Health, 10),
            new StatPair(EStatType.NormalAtk, 10),
            new StatPair(EStatType.JumpMass, 1),
        };
        Dictionary<EStatType, int> statMap;
        EStatType[] types;
        BaseStat convertBaseStat;
        public virtual EStatType[] GetStatTypes() {
            if (types == null || types.Length != statList.Count) {
                types = new EStatType[statList.Count];
                for (int i = 0; i < statList.Count; ++i) {
                    types[i] = statList[i].type;
                }
            }
            return types;
        }
        public virtual BaseStat ConvertToBaseStat() {
            if (convertBaseStat == null) {
                convertBaseStat = new BaseStat(GetStatTypes());
                foreach (StatPair pair in statList) {
                    convertBaseStat.ResetTo(pair);
                }
            }
            return convertBaseStat;
        }
    }
}
