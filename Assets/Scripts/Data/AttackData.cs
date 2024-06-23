using Define;
using System;

namespace Stat {
    [Serializable]
    public struct AttackData {
        public EAttackType attackType;
        public int atk;
        public int time;
        public AttackData(EAttackType attackType, int atk, int time) {
            this.attackType = attackType;
            this.atk = atk;
            this.time = time;
        }
    }
}
