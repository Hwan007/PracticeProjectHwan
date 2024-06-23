using System;
using UnityEngine;

public class MonsterBattleSystem : BaseBattleSystem {
    public event Action<BaseContainer> OnHitTarget;
    protected CharacterStat stat;

    protected override void OnAttackCollider(Collider2D collider) {
        if (CheckLayer(collider.gameObject.layer, targetLayer)) {
            var target = StageManager.Instance.player;
            OnHitTarget?.Invoke(target);
        }
    }
    public void Initialize(MonsterContainer container, CharacterStat stat) {
        this.stat = stat;
        SubscribeOnCollider();
        OnHitTarget = null;
        OnHitTarget += (target) => {
            foreach (var atk in stat.InstanceStat.totalStat.GetAttackData())
                target.HealthSystem.ChangeHealth(atk);
        };
    }
}
