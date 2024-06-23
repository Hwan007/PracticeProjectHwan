using Skill;
using System;
using UnityEngine;

public class PlayerMovement : Movement {
    public override bool DoJump() {
        if (transform.position.y <= originPosition.y) {
            ApplyImpact(stat.InstanceStat.totalStat.GetStat(Define.EStatType.JumpPower));
            return true;
        }
        return false;
    }
}
