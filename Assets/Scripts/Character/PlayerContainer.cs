using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat;
using Skill;
using UnityEngine.WSA;
using UnityEngine.UIElements;




#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerContainer : BaseContainer, IControllerable {
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
        Movement = Movement != null ? Movement : gameObject.GetComponent<PlayerMovement>();
        SpriteController = SpriteController != null ? SpriteController : gameObject.GetComponent<SpriteController>();
        BattleSystem = BattleSystem != null ? BattleSystem : gameObject.GetComponent<PlayerBattleSystem>();
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

    public void SubscribeInput(BaseInput input) {
        foreach (var sm in StateMachines) { sm.SubscribeInput(input); }
    }

    public void UnsubscribeInput(BaseInput input) {
        foreach (var sm in StateMachines) { sm.UnsubscribeInput(input); }
    }
}