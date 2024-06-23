using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class InputManager : BaseInput, PlayerController.IMobileActions,
#if UNITY_EDITOR
    PlayerController.IPCActions
#endif
    {
    Vector2 displaySize;
    [Header("Counter Clock Wise From Top")] [SerializeField] Vector4 offsetFromBorder;
    Vector2 minBorder;
    Vector2 MaxBorder;
    [SerializeField] float attackThresholdDist;
    HashSet<int> trackedTouchID;
    //PlayerInput input;
    private bool isCanControl;
    public bool CanControl {
        get => isCanControl;
        set {
            isCanControl = value;
            if (!value) {
#if UNITY_EDITOR
                // TODO : Is need do something?
#endif
            }
        }
    }

    PlayerController inputActions;
#if UNITY_EDITOR
    PlayerController.PCActions pcActions;
#else
    PlayerController.MobileActions moblieActions;
#endif

#if UNITY_EDITOR
    // PC control
    public void OnAttack(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started)
            CallAttackEvent();
    }

    public void OnAttackSkill(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started)
            CallAttackSkillEvent();
    }

    public void OnDefense(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started)
            CallDefenseEvent();
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started)
            CallJumpEvent();
    }

    public void OnJumpSkill(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started)
            CallJumpSkillEvent();
    }

    public void OnCancel(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started)
            CallCancelEvent();
    }
#endif
    // Moblie control
    public void OnCancelTouch(InputAction.CallbackContext context) {
        CallCancelEvent();
    }

    public void OnTouch(InputAction.CallbackContext context) {
        var touch = context.ReadValue<TouchState>();
        TouchToAction(touch);
    }

    private void TouchToAction(TouchState touch) {
        if (CanControl) {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began) {
                if (CheckTouchArea(touch.position)) {
                    trackedTouchID.Add(touch.touchId);
                }
            }
            else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended) {
                if (trackedTouchID.Contains(touch.touchId) && CheckTouchArea(touch.position)) {
                    Vector2 judge = touch.startPosition - touch.position;
                    if (judge.magnitude > attackThresholdDist) {
                        if (judge.y < 0)
                            CallDefenseEvent();
                        else
                            CallJumpEvent();
                    }
                    else
                        CallAttackEvent();
                }
                trackedTouchID.Remove(touch.touchId);
            }
        }
    }

    private bool CheckTouchArea(Vector2 position) {
        if (position.x < minBorder.x || position.y < minBorder.y)
            return false;
        if (position.x > MaxBorder.x || position.y > MaxBorder.y)
            return false;
        return true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Vector2 dispSize = GetRenderingResolution();// new Vector2(Screen.width, Screen.height);

        float[] xRange = new float[] { offsetFromBorder.y, dispSize.x - offsetFromBorder.w };
        float[] yRange = new float[] { offsetFromBorder.z, dispSize.y - offsetFromBorder.x };

        (int, int)[] index = { (0, 0), (0, 1), (1, 1), (1, 0), (0, 0) };
        Gizmos.color = Color.yellow;
        GizmosDrawLines(xRange, yRange, index);

        xRange = new float[] { 0, dispSize.x };
        yRange = new float[] { 0, dispSize.y };
        Gizmos.color = Color.gray;
        GizmosDrawLines(xRange, yRange, index);
    }

    void GizmosDrawLines(float[] xRange, float[] yRange, (int, int)[] index) {
        for (int i = 0; i < 4; ++i) {
            Vector3 start = new Vector3(xRange[index[i].Item1], yRange[index[i].Item2], 0);
            Vector3 end = new Vector3(xRange[index[i + 1].Item1], yRange[index[i + 1].Item2], 0);
            Gizmos.DrawLine(start, end);
        }
    }

    public static Vector2 GetMainGameViewSize() {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
        return (Vector2)Res;
    }

    public static Vector2 GetRenderingResolution() {
        UnityEditor.PlayModeWindow.GetRenderingResolution(out uint width, out uint height);
        return new Vector2(width, height);
    }
#endif
    protected override void Initialize() {
        displaySize = new Vector2(Screen.width, Screen.height);
        minBorder = new Vector2(offsetFromBorder.y, offsetFromBorder.z);
        MaxBorder = new Vector2(displaySize.x - offsetFromBorder.w, displaySize.y - offsetFromBorder.x);

        inputActions = new PlayerController();
        trackedTouchID = new HashSet<int>();
        //var input = gameObject.GetComponent<PlayerInput>();
#if UNITY_EDITOR
        pcActions = inputActions.PC;
        pcActions.AddCallbacks(this);
#else
        moblieActions = inputActions.Mobile;
        moblieActions.AddCallbacks(this);
        UIManager.Instance.TryGetUI<UISkillBtn>(nameof(UISkillBtn), ui => { 
            ui.OnAttackSkill += CallAttackSkillEvent; 
            ui.OnJumpSkill += CallJumpSkillEvent;
        });
#endif
    }

    public override void ConnectInput(IControllerable target) {
        base.ConnectInput(target);
        inputActions.Enable();
    }

    public override void DisconnectInput(IControllerable target) {
        base.DisconnectInput(target);
        inputActions.Disable();
    }
}
