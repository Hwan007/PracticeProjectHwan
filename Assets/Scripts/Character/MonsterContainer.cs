using System;
using Stat;
using UnityEngine;

public class MonsterContainer : BaseContainer {
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
        SpriteController = SpriteController != null ? SpriteController : gameObject.GetComponent<SpriteController>();
        BattleSystem = BattleSystem != null ? BattleSystem : gameObject.GetComponent<BaseBattleSystem>();
    }
    private void InitComponents(CharacterStat stat) {
        HealthSystem ??= new HealthSystem(stat);
        Movement.Initialize(stat);
        BattleSystem.Initialize(this, stat);
        SpriteController.Initialize(this);
        StateMachines = new StateMachine[SpriteController.aniData.Length];
        for (int i = 0; i < StateMachines.Length; ++i) {
            StateMachines[i] = new StateMachine(this, SpriteController.aniData[i], SpriteController.animator[i], InputManager.Instance);
        }
    }
}
