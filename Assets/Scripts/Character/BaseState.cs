using Define;
using Skill;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NoneState : BaseState {
    public NoneState(StateMachine sm, EFsmState stateType, BaseInput input, BaseState parent) : base(sm, stateType, input, parent) { }
    public override void Enter() { SubscribeInput(input); CallOnStateStartEvent(); }
    public override void Exit() { UnsubscribeInput(input); CallOnStateEndEvent(); }
}
public class GroundState : BaseState {
    public GroundState(StateMachine sm, EFsmState stateType, BaseInput input, BaseState parent) : base(sm, stateType, input, parent) { }
    public override void Update() {
        if (!CheckGround()) {
            sm.TryChangeState(EFsmState.Air);
        }
    }
}
public class JumpState : BaseState {
    float coolTime;
    public JumpState(StateMachine sm, EFsmState stateType, BaseInput input, BaseState parent) : base(sm, stateType, input, parent) {
    }
    public override void Enter() {
        SetBoolAnimator(sm.animationData.AnimatorHash[state], true);
        coolTime = sm.BattleSystem.JumpAnimationTime;
    }
    public override void Exit() {
        SetBoolAnimator(sm.animationData.AnimatorHash[state], false);
    }
    public override void Update() {
        if (coolTime > 0)
            coolTime -= Time.deltaTime;
        if (coolTime <= 0) {
            if (CheckGround())
                sm.TryChangeState(EFsmState.Ground);
            else
                sm.TryChangeState(EFsmState.Air);
        }
    }
}
public class AirState : BaseState {
    public AirState(StateMachine sm, EFsmState stateType, BaseInput input, BaseState parent) : base(sm, stateType, input, parent) {
    }
    public override void Update() {
        if (CheckGround()) {
            sm.TryChangeState(EFsmState.Ground);
        }
    }
}
public class HitState : BaseState {
    float coolTime;
    public HitState(StateMachine sm, EFsmState stateType, BaseInput input, BaseState parent) : base(sm, stateType, input, parent) {
    }
    public override void Enter() {
        base.Enter();
        coolTime = sm.BattleSystem.HitAnimationTime;
    }
    public override void Update() {
        if (coolTime > 0)
            coolTime -= Time.deltaTime;
        if (coolTime <= 0) {
            if (CheckGround())
                sm.TryChangeState(EFsmState.Ground);
            else
                sm.TryChangeState(EFsmState.Air);
        }
    }
}
public class AttackState : BaseState {
    float coolTime;
    public AttackState(StateMachine sm, EFsmState stateType, BaseInput input, BaseState parent) : base(sm, stateType, input, parent) { }
    public override void Enter() {
        base.Enter();
        coolTime = sm.CharacterStat.InstanceStat.totalStat.GetStat(EStatType.AttackCoolTime) / 1000f;
        sm.BattleSystem.canAttack = true;
    }
    public override void Exit() {
        base.Exit();
        sm.BattleSystem.canAttack = false;
    }
    public override void Update() {
        if (coolTime > 0)
            coolTime -= Time.deltaTime;
        if (coolTime <= 0) {
            if (CheckGround()) {
                if (sm.TryChangeState(EFsmState.Ground)) {
                    sm.TryChangeState(EFsmState.None);
                }
            }
            else {
                if (sm.TryChangeState(EFsmState.Air)) {
                    sm.TryChangeState(EFsmState.None);
                }
            }
        }
    }
}
public class DefenseState : BaseState {
    float coolTime;
    public DefenseState(StateMachine sm, EFsmState stateType, BaseInput input, BaseState parent) : base(sm, stateType, input, parent) { }
    public override void Enter() {
        base.Enter();
        coolTime = sm.BattleSystem.DefenseAnimationTime;
        sm.BattleSystem.canDefense = true;
    }
    public override void Exit() {
        base.Exit();
        sm.BattleSystem.canDefense = false;
    }
    public override void Update() {
        if (coolTime > 0)
            coolTime -= Time.deltaTime;
        if (coolTime <= 0) {
            if (CheckGround()) {
                if (sm.TryChangeState(EFsmState.Ground)) {
                    sm.TryChangeState(EFsmState.None);
                }
            }
            else {
                if (sm.TryChangeState(EFsmState.Air)) {
                    sm.TryChangeState(EFsmState.None);
                }
            }
        }
    }
}
public class SkillState : BaseState {
    BaseSkill skill;
    float coolTime;
    public SkillState(StateMachine sm, EFsmState stateType, BaseInput input, BaseSkill skill, BaseState parent) : base(sm, stateType, input, parent) {
        this.skill = skill;
    }
    public override void Enter() {
        base.Enter();
        coolTime = skill.BaseCoolTime;

    }
    public override void Exit() {
        base.Exit();

    }
    public override void Update() {
        if (coolTime > 0)
            coolTime -= Time.deltaTime;
        if (coolTime <= 0) {
            if (CheckGround())
                sm.TryChangeState(EFsmState.Ground);
            else
                sm.TryChangeState(EFsmState.Air);
        }
    }
}


public abstract class BaseState : IState, IControllerable {
    protected StateMachine sm;
    public EFsmState state { get; protected set; }
    protected BaseInput input;
    public BaseState Parent { get; protected set; }
    public LinkedList<BaseState> childs { get; protected set; }
    public event Action OnStateStart;
    public event Action OnStateEnd;
    public BaseState(StateMachine sm, EFsmState stateType, BaseInput input, BaseState parent) {
        this.sm = sm;
        this.state = stateType;
        this.Parent = parent;
        this.input = input;
        parent?.childs.AddLast(this);
    }
    public virtual void Enter() { SubscribeInput(input); SetBoolAnimator(sm.animationData.AnimatorHash[state], true); OnStateStart?.Invoke(); }
    public virtual void Exit() { UnsubscribeInput(input); SetBoolAnimator(sm.animationData.AnimatorHash[state], false); OnStateEnd?.Invoke(); }
    public virtual void Update() { }
    public virtual void PhysicsUpdate() { }
    public virtual void SubscribeInput(BaseInput input) {
        input.JumpEvent += ActOnJump;
        input.JumpSkillEvent += ActOnJumpSkill;
        input.AttackEvent += ActOnAttack;
        input.AttackSkillEvent += ActOnAttackSkill;
        input.DefenseEvent += ActOnDefense;
    }
    public virtual void UnsubscribeInput(BaseInput input) {
        input.JumpEvent -= ActOnJump;
        input.JumpSkillEvent -= ActOnJumpSkill;
        input.AttackEvent -= ActOnAttack;
        input.AttackSkillEvent -= ActOnAttackSkill;
        input.DefenseEvent -= ActOnDefense;
    }
    public virtual void ActOnJump() {
        if (sm.ContainsState(EFsmState.Jump) && sm.Movement.DoJump()) {
            sm.TryChangeState(EFsmState.Jump);
        }
    }
    public virtual void ActOnJumpSkill() {
        if (sm.ContainsState(EFsmState.JumpSkill) && sm.BattleSystem.DoJumpSkill()) {
            sm.TryChangeState(EFsmState.JumpSkill);
        }
    }
    public virtual void ActOnAttack() {
        if (sm.ContainsState(EFsmState.Attack) && sm.BattleSystem.DoAttack()) {
            sm.TryChangeState(EFsmState.Attack);
        }
    }
    public virtual void ActOnAttackSkill() {
        if (sm.ContainsState(EFsmState.AttackSkill) && sm.BattleSystem.DoAttackSkill()) {
            sm.TryChangeState(EFsmState.AttackSkill);
        }
    }
    public virtual void ActOnDefense() {
        if (sm.ContainsState(EFsmState.Defense) && sm.BattleSystem.DoDefense()) {
            sm.TryChangeState(EFsmState.Defense);
        }
    }
    protected virtual bool CheckGround() {
        var value = sm.Container.gameObject.transform.position.y;
        if (value > 0)
            return false;
        return true;
    }
    protected virtual void SetIntAnimator(int hash, int value) {
        sm.animator.SetInteger(hash, value);
    }
    protected virtual void SetBoolAnimator(int hash, bool onoff) {
        sm.animator.SetBool(hash, onoff);
        sm.animator.Play(0, 0, 0);
    }
    protected virtual void SetFloatAnimator(int hash, float value) {
        sm.animator.SetFloat(hash, value);
    }
    protected void CallOnStateStartEvent() { OnStateStart?.Invoke(); }
    protected void CallOnStateEndEvent() { OnStateEnd?.Invoke(); }
}

public interface IState {
    public event Action OnStateStart;
    public event Action OnStateEnd;
    void Enter();
    void Exit();
    void Update();
    void PhysicsUpdate();
    void ActOnJump();
    void ActOnAttack();
    void ActOnDefense();
}