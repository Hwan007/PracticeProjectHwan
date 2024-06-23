using Stat;
using System;
using UnityEngine;
public class PlayerBattleSystem : BaseBattleSystem {
    [SerializeField] protected AttackCollider shieldCollider;
    [SerializeField] protected float baseAttackTime;
    [SerializeField] protected float baseDefenseTime;
    float attackTime;
    float shieldTime;
    public event Action<BaseContainer> OnParryTarget;
    public event Action<float, float> OnAttackSkillTimer;
    public event Action<float, float> OnJumpSkillTimer;

    protected float attackSkillTimer;
    protected float jumpSkillTimer;
    protected float attackTimer;
    protected float defenseTimer;

    private BaseContainer container;

    private void Update() {
        if (attackSkillTimer > 0) {
            attackSkillTimer -= Time.deltaTime;
            OnAttackSkillTimer?.Invoke(attackSkillTimer, stat.AttackSkill.BaseCoolTime);
        }
        if (jumpSkillTimer > 0) {
            jumpSkillTimer -= Time.deltaTime;
            OnJumpSkillTimer?.Invoke(jumpSkillTimer, stat.JumpSkill.BaseCoolTime);
        }
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        if (defenseTimer > 0)
            defenseTimer -= Time.deltaTime;
    }
    protected override void SubscribeOnCollider() {
        base.SubscribeOnCollider();
        if (shieldCollider != null) {
            shieldCollider.OnCollider += OnDefenseCollider;
        }
    }
    protected override void UnsubscribeOnCollider() {
        base.UnsubscribeOnCollider();
        if (shieldCollider != null) {
            shieldCollider.OnCollider -= OnDefenseCollider;
        }
    }
    void OnDefenseCollider(Collider2D collison) {
        if (canDefense && CheckLayer(collison.gameObject.layer, targetLayer)) {
            OnParryTarget?.Invoke(StageManager.Instance.TryGetContainer(collison.gameObject.GetInstanceID()));
        }
    }
    public override bool DoAttack() {
        if (attackTimer <= 0) {
            // nothing to do. animation do attack.
            attackTimer = stat.InstanceStat.totalStat.GetStat(Define.EStatType.AttackCoolTime);
            return true;
        }
        return false;
    }
    public override bool DoDefense() {
        if (defenseTimer <= 0) {
            // nothing to do. animation do defense.
            defenseTimer = stat.InstanceStat.totalStat.GetStat(Define.EStatType.DefSkillCoolTime);
            return true;
        }
        return false;
    }
    public override bool DoAttackSkill() {
        if (attackSkillTimer <= 0 && stat.AttackSkill != null) {
            StartCoroutine(stat.AttackSkill.StartSkill(container));
            attackSkillTimer = stat.AttackSkill.BaseCoolTime;
            return true;
        }
        return false;
    }
    public override bool DoJumpSkill() {
        if (jumpSkillTimer <= 0 && stat.JumpSkill != null) {
            StartCoroutine(stat.JumpSkill.StartSkill(container));
            jumpSkillTimer = stat.JumpSkill.BaseCoolTime;
            return true;
        }
        return false;
    }
    public override void Initialize(BaseContainer container, CharacterStat stat) {
        base.Initialize(container, stat);
        OnParryTarget = null;
        this.container = container;
        OnParryTarget += (target) => {
            Vector2 dir = target.transform.position - transform.position;
            target.Movement.ApplyImpact(stat.InstanceStat.totalStat.GetStat(Define.EStatType.DefPower));
            container.Movement.ApplyImpact(stat.InstanceStat.totalStat.GetStat(Define.EStatType.DefPower));
        };
    }
}