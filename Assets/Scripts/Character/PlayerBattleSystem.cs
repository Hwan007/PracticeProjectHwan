using Stat;
using System;
using UnityEngine;
public class PlayerBattleSystem : BaseBattleSystem {
    [SerializeField] protected AttackCollider shieldCollider;
    [field: SerializeField] public float HitAnimationTime { get; protected set; }
    [field: SerializeField] public float JumpAnimationTime { get; protected set; }
    [field: SerializeField] public float DefenseAnimationTime { get; protected set; }

    public bool canDefense { get; set; }
    public bool canAttack { get; set; }

    public event Action<BaseContainer> OnHitTarget;
    public event Action<MonsterContainer> OnParryTarget;
    public event Action<float, float> OnAttackSkillTimer;
    public event Action<float, float> OnJumpSkillTimer;
    public event Action OnAttackStart;

    protected float attackSkillTimer;
    protected float jumpSkillTimer;
    protected float attackTimer;
    protected float defenseTimer;

    private PlayerContainer container;
    protected CharacterStat stat;
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
    void OnDefenseCollider(Collider2D collider) {
        if (canDefense && CheckLayer(collider.gameObject.layer, targetLayer)) {
            OnParryTarget?.Invoke(StageManager.Instance.TryGetContainer(collider.gameObject.GetInstanceID()));
        }
    }
    public bool DoAttack() {
        if (attackTimer <= 0) {
            attackTimer = stat.InstanceStat.totalStat.GetStat(Define.EStatType.AttackCoolTime) / 1000f;
            return true;
        }
        return false;
    }
    public bool DoDefense() {
        if (defenseTimer <= 0) {
            defenseTimer = stat.InstanceStat.totalStat.GetStat(Define.EStatType.DefSkillCoolTime) / 1000f;
            return true;
        }
        return false;
    }
    public bool DoAttackSkill() {
        if (attackSkillTimer <= 0 && stat.AttackSkill != null) {
            StartCoroutine(stat.AttackSkill.StartSkill(container));
            attackSkillTimer = stat.AttackSkill.BaseCoolTime;
            return true;
        }
        return false;
    }
    public bool DoJumpSkill() {
        if (jumpSkillTimer <= 0 && stat.JumpSkill != null) {
            StartCoroutine(stat.JumpSkill.StartSkill(container));
            jumpSkillTimer = stat.JumpSkill.BaseCoolTime;
            return true;
        }
        return false;
    }
    public void Initialize(PlayerContainer container, CharacterStat stat) {
        this.stat = stat;
        SubscribeOnCollider();
        OnHitTarget = null;
        OnHitTarget += (target) => {
            foreach (var atk in stat.InstanceStat.totalStat.GetAttackData())
                target.HealthSystem.ChangeHealth(atk);
        };
        OnParryTarget = null;
        this.container = container;
        OnParryTarget += (target) => {
            target.Movement.ApplyImpact(stat.InstanceStat.totalStat.GetStat(Define.EStatType.DefPower));
            container.Movement.ApplyImpact(-stat.InstanceStat.totalStat.GetStat(Define.EStatType.DefPower));
        };
    }

    protected override void OnAttackCollider(Collider2D collider) {
        if (canAttack && CheckLayer(collider.gameObject.layer, targetLayer)) {
            var target = StageManager.Instance.TryGetContainer(collider.gameObject.GetInstanceID());
            OnHitTarget?.Invoke(target);
        }
    }
}