using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UI {
    public abstract class UIBase : MonoBehaviour {
        public Vector3 originScale = Vector3.one;
        public abstract void SetFontScale(float fontScale);
        public event Action ActOnClose;
        public event Action ActOnDraw;
        [SerializeField] CanvasOption canvasOption;
        public virtual CanvasOption GetCanvasOption() {
            return canvasOption;
        }
        public virtual void SetScale(float scale) {
            transform.localScale = originScale * scale;
        }
        public virtual UIBase CloseUI() {
            ActOnClose?.Invoke();
            return this;
        }
        public abstract UIBase UpdateUI();
        public virtual UIBase DrawUI() {
            ActOnDraw?.Invoke();
            return this;
        }
        public abstract UIBase InitUI();
    }
}

public struct TextPair {
    public TMP_Text text;
    public float fontSize;
}