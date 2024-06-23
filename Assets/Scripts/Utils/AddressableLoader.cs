using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLoader {
    public static void LoadAsset<T>(AssetReference assetRef, Action<T> onComplete = null) {
        if (assetRef.Asset == null) {
            var asyncOpHandle = Addressables.LoadAssetAsync<T>(assetRef);
            //Debug.Log($"{asyncOpHandle.DebugName} {asyncOpHandle.PercentComplete}");
            asyncOpHandle.Completed += (op) => {
                if (op.Status == AsyncOperationStatus.Succeeded)
                    onComplete?.Invoke(op.Result);
            };
        }
        else {
            if (assetRef.Asset is T ret)
                onComplete?.Invoke(ret);
        }
    }

    public static bool DeployAsset(AssetReference assetRef, Vector3 position, Quaternion quaternion, Action<GameObject> onComplete = null) {
        if (assetRef.Asset == null) {
            var asyncOpHandle = Addressables.InstantiateAsync(assetRef, position, quaternion);
            //Debug.Log($"{asyncOpHandle.DebugName} {asyncOpHandle.PercentComplete}");
            asyncOpHandle.Completed += (op) => {
                if (op.Status == AsyncOperationStatus.Succeeded)
                    onComplete?.Invoke(op.Result);
            };
            return true;
        }
        else {
            LoadAsset<GameObject>(assetRef);
            return false;
        }
    }
}