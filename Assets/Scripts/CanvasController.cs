using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    Canvas cvsComp;
    CanvasScaler canvasScaler;
    GraphicRaycaster graphicRaycaster;

    private void Start() {
        gameObject.TryGetComponent<Canvas>(out cvsComp);
        gameObject.TryGetComponent<CanvasScaler>(out canvasScaler);

        bool isRaycast = gameObject.TryGetComponent<GraphicRaycaster>(out graphicRaycaster);
        UIManager.Instance.RegisterCanvas(this,
            new CanvasOption(cvsComp.renderMode, isRaycast));
    }

    public Canvas GetCanvasComponent() {
        return cvsComp;
    }
}
