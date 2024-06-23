using Define;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DataManager : UnitySingleton<DataManager> {
    public StatDescription StatDescription { get; protected set; }
    public ItemDescription ItemDescription { get; protected set; }
    public SkillDescription SkillDescription { get; protected set; }

    [SerializeField] AssetReference statDescAssetRef;
    [SerializeField] AssetReference itemDescAssetRef;
    [SerializeField] AssetReference skillDescAssetRef;

    public GameOption GameOpt { get; protected set; }

    protected override void Initialize() {
        AddressableLoader.LoadAsset<StatDescription>(statDescAssetRef, (data) => StatDescription = data);
        AddressableLoader.LoadAsset<ItemDescription>(itemDescAssetRef, (data) => ItemDescription = data);
        AddressableLoader.LoadAsset<SkillDescription>(skillDescAssetRef, data => SkillDescription = data);
    }
    public void LoadAsset<T>(AssetReference assetRef, Action<T> onComlete = null) {
        AddressableLoader.LoadAsset(assetRef, onComlete);
    }
    public void DeployAsset<T>(AssetReference assetRef, Vector3 position, Quaternion quaternion, Action<GameObject> onComplete = null) {
        AddressableLoader.LoadAsset<T>(assetRef, (obj) => AddressableLoader.DeployAsset(assetRef, position, quaternion, onComplete));
    }
    public void Load(EDataType type) {
        var opt = GameOpt;
        switch (type) {
            case EDataType.AudioScale:
                opt.AudioScale = PlayerPrefs.GetFloat(type.ToString());
                break;
            case EDataType.FontScale:
                opt.FontScale = PlayerPrefs.GetFloat(type.ToString());
                break;
            case EDataType.UIScale:
                opt.UIScale = PlayerPrefs.GetFloat(type.ToString());
                break;
            case EDataType.FPS:
                opt.FPS = PlayerPrefs.GetInt(type.ToString());
                break;
        }
        GameOpt = opt;
    }
    public void Save(EDataType type) {
        switch (type) {
            case EDataType.AudioScale:
                PlayerPrefs.SetFloat(type.ToString(), GameOpt.AudioScale);
                break;
            case EDataType.FontScale:
                PlayerPrefs.SetFloat(type.ToString(), GameOpt.FontScale);
                break;
            case EDataType.UIScale:
                PlayerPrefs.SetFloat(type.ToString(), GameOpt.UIScale);
                break;
            case EDataType.FPS:
                PlayerPrefs.SetInt(type.ToString(), GameOpt.FPS);
                break;
        }
    }
    public void ClearSave() {
        PlayerPrefs.DeleteAll();
    }

    public string GetStatDescription(EStatType eStatType) {
        int index = (int)eStatType;
        return StatDescription.strings[(int)eStatType];
    }
    public string GetItemName(int itemKey) {
        if (ItemDescription.TryGetDescription(itemKey, out var value))
            return value.Item1;
        return " ";
    }
    public string GetItemDescription(int itemKey) {
        if (ItemDescription.TryGetDescription(itemKey, out var value))
            return value.Item2;
        return " ";
    }
    public string GetSkillName(int key) {
        if (SkillDescription.TryGetDescription(key, out var desc)) {
            return desc.Item1;
        }
        return " ";
    }
    public string GetSkillDescription(int key) {
        if (SkillDescription.TryGetDescription(key, out var desc)) {
            return desc.Item2;
        }
        return " ";
    }
}

public struct GameOption {
    public float AudioScale;
    public float FontScale; 
    public float UIScale;
    public int FPS;
}