using UnityEngine;
using Stat;

public abstract class BaseContainer : MonoBehaviour{
    public CharacterStat CharacterStat { get; protected set; }
    public HealthSystem HealthSystem { get; protected set; }
    public abstract void Initialize(CharacterStat toStat);
    public abstract void Initialize(BaseStat toStat);
}