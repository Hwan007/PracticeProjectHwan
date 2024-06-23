using System;
using Stat;
using UnityEngine;

public class MonsterContainer : BaseContainer {
    public Movement Movement { get; private set; }

    public override void Initialize(CharacterStat toStat) {
        GetComponents();
        CharacterStat.ResetStatTo(toStat);
        InitComponents(CharacterStat);
    }

    public override void Initialize(BaseStat toStat) {
        GetComponents();
        CharacterStat.ResetStatTo(toStat);
        InitComponents(CharacterStat);
    }

    private void GetComponents() {
        CharacterStat ??= new CharacterStat();
        Movement = Movement != null ? Movement : gameObject.GetComponent<Movement>();
    }
    private void InitComponents(CharacterStat stat) {
        HealthSystem ??= new HealthSystem(stat);
        Movement.Initialize(stat);
    }
}
