using System;
using UnityEngine;

public abstract class BaseInput : UnitySingleton<BaseInput> {
    public event Action JumpEvent;
    public event Action AttackEvent;
    public event Action DefenseEvent;
    public event Action CancelEvent;
    public event Action AttackSkillEvent;
    public event Action JumpSkillEvent;
    protected IControllerable controlObj;

    public virtual void CallAttackSkillEvent() {
        AttackSkillEvent?.Invoke();
    }
    public virtual void CallJumpSkillEvent() {
        JumpSkillEvent?.Invoke();
    }
    protected virtual void CallJumpEvent() {
        JumpEvent?.Invoke();
    }

    protected virtual void CallAttackEvent() {
        AttackEvent?.Invoke();
    }

    protected virtual void CallDefenseEvent() {
        DefenseEvent?.Invoke();
    }

    protected virtual void CallCancelEvent() {
        CancelEvent?.Invoke();
    }

    public virtual void ConnectInput(IControllerable target) {
        if (controlObj != null)
            DisconnectInput(controlObj);
        target.SubscribeInput(this);
        controlObj = target;
    }
    public virtual void DisconnectInput(IControllerable target) {
        target.UnsubscribeInput(this);
    }
}

public interface IControllerable {
    public void SubscribeInput(BaseInput input);
    public void UnsubscribeInput(BaseInput input);
}