using Stat;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AddressableAssets;
using Character;
using Character2D;

public class StageManager : UnitySingleton<StageManager> {
    #region field
    [SerializeField] AssetReference playerAssetRef;
    public PlayerContainer player { get; private set; }
    Dictionary<int, MonsterContainer> fieldContainers;
    StageInfo stageInfo;
    Coroutine stageCoroutine;
    CustomPool<MonsterContainer> pool;
    [SerializeField] List<AssetReference> stageInfos;
    public event Action<float, float> stageProgress;
    #endregion
    #region public method
    protected override void Initialize() {
        fieldContainers = new Dictionary<int, MonsterContainer>();
    }
    public void DeployPlayer(CharacterStat toStat, Action<PlayerContainer> onDeploySuccess = null) {
        if (player == null) {
            DataManager.Instance.DeployAsset<GameObject>(playerAssetRef, new Vector3(0, 1, 0), Quaternion.identity, obj => {
                player = obj.GetComponent<PlayerContainer>();
                InitPlayer(toStat);
                onDeploySuccess?.Invoke(player);
            });
        }
        else {
            InitPlayer(toStat);
            onDeploySuccess?.Invoke(player);
        }
    }
    public void StartStage(int level) {
        if (level < stageInfos.Count) {
            DataManager.Instance.LoadAsset<StageInfo>(stageInfos[level], info => StartStage(info));
        }
    }
    void StartStage(StageInfo stageInfo) {
        this.stageInfo = stageInfo;
        foreach (var mon in stageInfo.assets) {
            DataManager.Instance.LoadAsset<GameObject>(mon,
                obj => {
                    if (obj.TryGetComponent<MonsterContainer>(out var comp))
                        pool = CustomPool<MonsterContainer>.MakePool(comp, null, OnCreateMonster, OnGetMonster, OnReleaseMonster, OnDestroyMonster, 10, true);
                });
        }
        stageCoroutine = StartCoroutine(OnStage(stageInfo));
    }
    public void ClearStage() {
        StopCoroutine(stageCoroutine);
        stageCoroutine = null;
        ReleaseMonster();
    }
    public MonsterContainer TryGetContainer(int id) {
        if (fieldContainers.TryGetValue(id, out var container))
            return container;
        return null;
    }
    #endregion
    #region private mathod
    private void OnCreateMonster(MonsterContainer container, CustomPool<MonsterContainer> pool) {
        fieldContainers.Add(container.gameObject.GetInstanceID(), container);
        container.gameObject.SetActive(false);
    }
    private void OnGetMonster(MonsterContainer container) {
        container.gameObject.SetActive(true);
    }
    private void OnReleaseMonster(MonsterContainer container) {
        container.gameObject.SetActive(false);
    }
    private void OnDestroyMonster(MonsterContainer container) {
        fieldContainers.Remove(container.GetInstanceID());
    }
    private void ReleaseMonster() {
        pool.ReleaseAll();
    }
    IEnumerator OnStage(StageInfo stageInfo) {
        float passedTime = 0;
        float countTime = 0;
        int index = 0;
        while (passedTime < stageInfo.limitTime) {
            passedTime += Time.deltaTime;
            countTime += Time.deltaTime;
            if (countTime >= stageInfo.stageInfos[index].fromBeforeMonster) {
                DeployMonster(stageInfo.assets[stageInfo.stageInfos[index].index].AssetGUID, stageInfo.stageInfos[index].stat.ConvertToBaseStat());
                countTime = 0;
            }
            stageProgress?.Invoke(passedTime, stageInfo.limitTime);
            yield return null;
        }
    }
    void DeployMonster(string guid, BaseStat toStat) {
        var mon = pool.Get();
        mon.transform.position = new Vector2(0, 30);
        mon.Initialize(toStat);
        mon.HealthSystem.OnDead += data => pool.Release(mon);
    }

    void InitPlayer(CharacterStat toStat) {
        player.Initialize(toStat);
        InputManager.Instance.ConnectInput(player);
        player.HealthSystem.OnDead += (data) => InputManager.Instance.DisconnectInput(player);
    }
    #endregion
}