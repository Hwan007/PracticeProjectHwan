using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI {
    public class UIManager : UnitySingleton<UIManager> {
        [SerializeField] List<AssetReference> uiAssetRef;
        Dictionary<string, int> typeToAssetRefIndex;
        LinkedList<UIBase> openedUiList;
        Dictionary<string, UIBase> uiDict;
        private Dictionary<CanvasOption, CanvasController> canvas;
        protected override void Initialize() {
            openedUiList = new LinkedList<UIBase>();
            uiDict = new Dictionary<string, UIBase>();
            typeToAssetRefIndex = new Dictionary<string, int>();
        }
        private void Start() {
            LoadUI<GameObject>();
        }
        void LoadUI<T>(Action<T> onComplete = null) {
            for (int i = 0; i < uiAssetRef.Count; ++i) {
                int index = i;
                DataManager.Instance.LoadAsset<GameObject>(uiAssetRef[i], obj => {
                    if (obj.TryGetComponent<T>(out T ui)) {
                        typeToAssetRefIndex.Add(ui.GetType().Name, index);
                        onComplete?.Invoke(ui);
                    }
                });
            }
        }
        void DeployUI<T>(AssetReference assetRef, Action<T> onComplete = null) where T : UIBase {
            DataManager.Instance.DeployAsset<GameObject>(assetRef, Vector3.zero, Quaternion.identity, obj => {
                if (obj.TryGetComponent<T>(out T ui)) {
                    ui.ActOnDraw += () => openedUiList.AddLast(ui);
                    ui.ActOnClose += () => openedUiList.Remove(ui);
                    uiDict.Add(ui.GetType().Name, ui);
                    ui.InitUI();
                    if (obj.TryGetComponent<RectTransform>(out RectTransform uiRectTransform)) {
                        uiRectTransform.SetParent(canvas[ui.GetCanvasOption()].transform);
                        uiRectTransform.localEulerAngles = Vector3.zero;
                        uiRectTransform.localPosition = Vector3.zero;
                        uiRectTransform.localScale = Vector3.one;
                        uiRectTransform.sizeDelta = Vector2.zero;
                    }
                    onComplete?.Invoke(ui);
                }
            });
        }
        public T TryGetUI<T>(string typeName, Action<T> onLoadComplete = null) where T : UIBase {
            if (uiDict.TryGetValue(typeName, out var ui)) {
                onLoadComplete?.Invoke(ui as T);
                return ui as T;
            }
            else {
                if (!typeToAssetRefIndex.ContainsKey(typeName))
                    LoadUI<T>((ui) => DeployUI<T>(uiAssetRef[typeToAssetRefIndex[typeName]], onLoadComplete));
                else {
                    DeployUI<T>(uiAssetRef[typeToAssetRefIndex[typeName]], onLoadComplete);
                }
                return null;
            }
        }
        public void RegisterCanvas(CanvasController cv, CanvasOption opt) {
            if (canvas == null) {
                canvas = new Dictionary<CanvasOption, CanvasController>();
            }
            if (!canvas.TryAdd(opt, cv)) {
                GameObject.Destroy(cv);
            }
        }
        public UIBase TryGetTopUI() {
            if (openedUiList.Count > 0)
                return openedUiList.Last.Value;
            return null;
        }
        public void UpdateSize(float fontScale, float sizeScale) {
            foreach (UIBase ui in openedUiList) {
                ui.SetFontScale(fontScale);
                ui.SetScale(sizeScale);
            }
        }
        public Canvas TryGetCanvas(CanvasOption opt) {
            if (canvas == null)
                return null;
            if (canvas.TryGetValue(opt, out var ui))
                return ui.GetCanvasComponent();
            return null;
        }
        public void CloseAllUI(int last = 0) {
            while(openedUiList.Count > last) {
                openedUiList.Last.Value.CloseUI();
            }
        }
        public void UpdateUI() {
            foreach (var ui in openedUiList) {
                ui.UpdateUI();
            }
        }
    }
    [Serializable]
    public struct CanvasOption {
        public RenderMode renderMode;
        public bool isRaycaster;
        public CanvasOption(RenderMode mode = RenderMode.ScreenSpaceCamera, bool isRaycast = true) {
            renderMode = mode;
            isRaycaster = isRaycast;
        }
    }
}