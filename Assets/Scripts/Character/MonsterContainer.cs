using System;
using Stat;
using UnityEngine;
using Character;
using Character2D;

public class MonsterContainer : BaseContainer, IMovable {
    public PlatformerMovement Movement { get; private set; }

    public float GetGravity() {
        throw new NotImplementedException();
    }

    public float GetLimitVelocity() {
        throw new NotImplementedException();
    }

    public float GetMass() {
        throw new NotImplementedException();
    }

    public float GetPower() {
        throw new NotImplementedException();
    }

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
        Movement = Movement != null ? Movement : gameObject.GetComponent<PlatformerMovement>();
    }
    private void InitComponents(CharacterStat stat) {
        HealthSystem ??= new HealthSystem(stat);
        Movement.Initialize(this);
    }
}
