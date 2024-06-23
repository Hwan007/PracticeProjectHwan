using Stat;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AddressableAssets;

public class StageManager : UnitySingleton<StageManager> {
    #region field
    [SerializeField] AssetReference playerAssetRef;
    public PlayerContainer player {get; private set;}
    Dictionary<int, BaseContainer> fieldContainers;
    StageInfo stageInfo;
    Coroutine stageCoroutine;
    Dictionary<string, CustomPool<BaseContainer>> pool;
    [SerializeField] List<AssetReference> stageInfos;
    public event Action<float, float> stageProgress;
    #endregion
    #region public method
    protected override void Initialize() {
        fieldContainers = new Dictionary<int, BaseContainer>();
        pool = new Dictionary<string, CustomPool<BaseContainer>>();
    }
    public void DeployPlayer(CharacterStat toStat, Action<PlayerContainer> onDeploySuccess = null) {
        if (player == null) {
            DataManager.Instance.DeployAsset<GameObject>(playerAssetRef, Vector3.zero, Quaternion.identity, obj => {
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
            DataManager.Instance.LoadAsset<BaseContainer>(mon,
                obj => {
                    pool.Add(mon.AssetGUID,
                        CustomPool<BaseContainer>.MakePool(obj, null, OnCreateMonster, OnGetMonster, OnReleaseMonster, OnDestroyMonster, 10, true)
                        );
                });
        }
        stageCoroutine = StartCoroutine(OnStage(stageInfo));
    }
    public void ClearStage() {
        StopCoroutine(stageCoroutine);
        stageCoroutine = null;
        ReleaseMonster();
    }
    public BaseContainer TryGetContainer(int id) {
        if (fieldContainers.TryGetValue(id, out var container))
            return container;
        return null;
    }
    #endregion
    #region private mathod
    private void OnCreateMonster(BaseContainer container) {
        fieldContainers.Add(container.gameObject.GetInstanceID(), container);
    }
    private void OnGetMonster(BaseContainer container) {
        container.gameObject.SetActive(true);
    }
    private void OnReleaseMonster(BaseContainer container) {
        container.gameObject.SetActive(false);
    }
    private void OnDestroyMonster(BaseContainer container) {
        fieldContainers.Remove(container.GetInstanceID());
    }
    private void ReleaseMonster() {
        foreach (var mons in pool.Values) {
            mons.ReleaseAll();
        }
    }
    IEnumerator OnStage(StageInfo stageInfo) {
        float passedTime = 0;
        float countTime = 0;
        int index = 0;
        while (passedTime < stageInfo.limitTime) {
            passedTime += Time.deltaTime;
            countTime += Time.deltaTime;
            if (countTime >= stageInfo.stageInfos[index].fromBeforeMonster) {
                DeployMonster(stageInfo.assets[stageInfo.stageInfos[index].index].AssetGUID, stageInfo.stageInfos[index].stat);
                countTime = 0;
            }
            stageProgress?.Invoke(passedTime, stageInfo.limitTime);
            yield return null;
        }
    }
    void DeployMonster(string guid, BaseStat toStat) {
        if (pool.TryGetValue(guid, out var mons)) {
            var mon = mons.Get();
            mon.Initialize(toStat);
        }
    }
    void InitPlayer(CharacterStat toStat) {
        player.Initialize(toStat);
        InputManager.Instance.ConnectInput(player);
        fieldContainers.Add(player.gameObject.GetInstanceID(), player);
        player.HealthSystem.OnDead += (data) => InputManager.Instance.DisconnectInput(player);
    }
    #endregion
}