using UnityEngine;
using Stat;

public abstract class BaseContainer : MonoBehaviour {
    public CharacterStat CharacterStat { get; protected set; }
    public HealthSystem HealthSystem { get; protected set; }
    public Movement Movement { get; protected set; }
    public BaseBattleSystem BattleSystem { get; protected set; }
    public BaseSpriteController SpriteController { get; protected set; }
    public StateMachine[] StateMachines { get; protected set; }
    [SerializeField] public float HitCoolTime { get; protected set; }
    [SerializeField] public float JumpCoolTime { get; protected set; }

    public abstract void Initialize(CharacterStat toStat);
    public abstract void Initialize(BaseStat toStat);
}