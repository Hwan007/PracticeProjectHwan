using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using Stat;

[CreateAssetMenu(menuName = "Stage Info")]
public class StageInfo : ScriptableObject {
    public float limitTime;
    public List<AssetReference> assets;
    public List<StageInfoPair> stageInfos;
}

[Serializable]
public struct StageInfoPair {
    public int index;
    public float fromBeforeMonster;
    public CustomStat stat;
}
