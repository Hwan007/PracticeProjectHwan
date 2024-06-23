using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define {
    public enum ELanguageType {
        Korean,
        English,
    }
    public enum EStatType {
        Health,
        GoldEarnMultiplier,

        NormalAtk,
        FireAtk,
        VoidAtk,
        FireTime, VoidTime,
        AtkMultiplier,
        AilmentTime, AilmentTimeMultiplier,
        AttackDist,
        CritChance, CritDamageMultiplier,
        AtkSkillChargePerCombo, AtkSkill,

        DefPower,
        DefSkillCoolTime,

        JumpPower,
        JumpMass,
        JumpSkillChargePerCombo, JumpSkill,
        AttackCoolTime,
    }

    public enum EAttackType {
        Normal,
        Fire,
        Void,
    }

    public enum EDataType {
        AudioScale,
        FontScale,
        UIScale,
        FPS,
    }

    public enum ESceneType {
        Start,
        Main,
        Stage,
        Loading,
    }

    public enum ECalculationType {
        Add,
        Subtract,
        Divide,
        Multiply,
    }
    public enum EFsmState {
        None,
        Ground,
        Jump,
        Air,
        Hit,
        Attack,
        Defense,
        AttackSkill,
        JumpSkill,
    }
    public enum EItemType {
        Weapon,
        Shield,
        Armor,
    }
}