using Stat;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseBattleSystem : MonoBehaviour {
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected AttackCollider attackCollider;
    
    public event Action<BaseContainer> OnHitTarget;
    public event Action OnAttackStart;

    protected CharacterStat stat;

    protected bool canDefense;
    protected bool canAttack;
    public void CanDefense(bool onoff) {
        canDefense = onoff;
    }
    public void CanAttack(bool onoff) {
        canAttack = onoff;
    }
    protected virtual void SubscribeOnCollider() {
        if (attackCollider != null) {
            attackCollider.OnCollider += OnAttackCollide;
        }
    }
    protected virtual void UnsubscribeOnCollider() {
        if (attackCollider != null) {
            attackCollider.OnCollider -= OnAttackCollide;
        }
    }
    protected static bool CheckLayer(int layerValue, LayerMask target) {
        return (1 << layerValue) == (1 << layerValue & target.value);
    }
    protected virtual void OnAttackCollide(Collider2D collison) {
        if (CheckLayer(collison.gameObject.layer, targetLayer)) {
            var target = StageManager.Instance.TryGetContainer(collison.gameObject.GetInstanceID());
            OnHitTarget?.Invoke(target);
        }
    }
    public virtual void Initialize(BaseContainer container, CharacterStat stat) {
        this.stat = stat;
        SubscribeOnCollider();
        OnHitTarget = null;
        OnHitTarget += (target) => {
            foreach (var atk in stat.InstanceStat.totalStat.GetAttackData())
                target.HealthSystem.ChangeHealth(atk);
        };
    }
    public virtual bool DoAttack() {
        return false;
    }
    public virtual bool DoDefense() {
        return false;
    }
    public virtual bool DoAttackSkill() {
        return false;
    }
    public virtual bool DoJumpSkill() {
        return false;
    }
}
