using System.Collections.Generic;
using UnityEngine;
using Define;
using Character;
using Character2D;

public class StateMachine : IControllerable {
    protected Dictionary<EFsmState, IState> states;
    #region ÇÊµå
    public PlayerContainer Container { get; protected set; }
    public CharacterStat CharacterStat { get => Container.CharacterStat; }
    public HealthSystem HealthSystem { get => Container.HealthSystem; }
    public PlayerPlatformerMovement Movement { get => Container.Movement; }
    public PlayerBattleSystem BattleSystem { get => Container.BattleSystem; }
    public BaseSpriteController SpriteController { get => Container.SpriteController; }
    public AnimationData animationData { get; protected set; }
    public Animator animator { get; protected set; }
    public BaseInput input { get; protected set; }

    public IState currentState { get; protected set; }
    public EFsmState currentStateType { get; protected set; }
    #endregion

    public StateMachine(PlayerContainer container, AnimationData animationData, Animator animator, BaseInput input) {
        states = new Dictionary<EFsmState, IState>();
        Container = container;
        this.animationData = animationData;
        this.animator = animator;
        foreach (var data in animationData.AnimatorHash) {
            switch (data.Key) {
                case EFsmState.None:
                    TryAddState(EFsmState.Ground, new NoneState(this, data.Key, input, null));
                    TryChangeState(EFsmState.Ground);
                    break;
                case EFsmState.Ground:
                    TryAddState(data.Key, new GroundState(this, data.Key, input, null));
                    TryChangeState(EFsmState.Ground);
                    break;
                case EFsmState.Jump:
                    TryAddState(data.Key, new JumpState(this, data.Key, input, null));
                    break;
                case EFsmState.Air:
                    TryAddState(data.Key, new AirState(this, data.Key, input, null));
                    break;
                case EFsmState.Hit:
                    TryAddState(data.Key, new HitState(this, data.Key, input, null));
                    break;
                case EFsmState.Attack:
                    TryAddState(data.Key, new AttackState(this, data.Key, input, null));
                    break;
                case EFsmState.Defense:
                    TryAddState(data.Key, new DefenseState(this, data.Key, input, null));
                    break;
                case EFsmState.AttackSkill:
                    TryAddState(data.Key, new SkillState(this, data.Key, input, container.CharacterStat.AttackSkill, null));
                    break;
                case EFsmState.JumpSkill:
                    TryAddState(data.Key, new SkillState(this, data.Key, input, container.CharacterStat.JumpSkill, null));
                    break;
            }
        }

        this.input = input;
    }
    public bool ContainsState(EFsmState state) {
        return states.ContainsKey(state);
    }

    public bool TryChangeState(EFsmState state) {
        if (states.TryGetValue(state, out IState nextState)) {
            currentState?.Exit();
            currentState = nextState;
            currentStateType = state;
            currentState?.Enter();
            return true;
        }
        return false;
    }

    public bool TryAddState(EFsmState type, IState state) {
        return states.TryAdd(type, state);
    }

    public bool TryRemoveState(EFsmState type) {
        return states.Remove(type);
    }

    public virtual void Update() {
        currentState?.Update();
    }

    public virtual void FixedUpdate() {
        currentState?.PhysicsUpdate();
    }

    public void StopStateMachine() {
        currentState?.Exit();
        currentState = null;
        currentStateType = EFsmState.None;
    }

    public T TryGetState<T>(EFsmState type) where T : BaseState {
        if (states.TryGetValue(type, out var state))
            return state as T;
        return null;
    }

    public void SubscribeInput(BaseInput input) {
        // TODO
    }

    public void UnsubscribeInput(BaseInput input) {
        // TODO
    }
}
