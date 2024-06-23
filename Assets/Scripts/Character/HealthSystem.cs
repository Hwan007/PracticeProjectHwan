using System;
using System.Collections;
using Define;
using Stat;
using UnityEngine;

public class HealthSystem {
    int total;
    int current;
    public event Action<BaseContainer> OnDead;
    public event Action<int, int> OnHealthChange;
    BaseContainer container;
    CharacterStat stat;
    Coroutine fireCoroutine;

    public HealthSystem(CharacterStat stat) {
        this.stat = stat;
        total = stat.InstanceStat.totalStat.GetStat(EStatType.Health);
        current = total;
        stat.OnChangeStat += OnChangeStat;
    }
    public int ChangeHealth(AttackData atk) {
        int final = current;
        switch (atk.attackType) {
            case EAttackType.Normal:
                ChangeHealth(-atk.atk);
                final = current;
                break;
            case EAttackType.Fire:
                fireCoroutine ??= container.StartCoroutine(DotDamage(atk.atk, atk.time));
                final -= atk.atk * atk.time;
                break;
            case EAttackType.Void:
                ChangeHealth(-atk.atk);
                final = current;
                break;
        }
        return final;
    }
    public int ChangeHealth(int point) {
        current = Mathf.Clamp(current + point, 0, total);
        OnHealthChange?.Invoke(current, total);
        if (current <= 0)
            OnDead?.Invoke(container);
        return current;
    }
    void OnChangeStat(ECalculationType type, BaseStat point) {
        int before = total;
        total = stat.InstanceStat.totalStat.GetStat(EStatType.Health);
        current = (current * total) / (before);
    }
    IEnumerator DotDamage(int dmgPerSec, float duration) {
        int count = 0;
        float passed = 0;
        while (duration > passed) {
            passed += Time.deltaTime;
            while (passed > count) {
                ChangeHealth(-dmgPerSec);
                ++count;
            }
            yield return null;
        }
    }
}