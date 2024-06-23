using Stat;
using UI;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager> {
    protected override void Initialize() {
        Application.targetFrameRate = 60;
    }

    public void StartGame() {
        UIManager.Instance.TryGetUI<UIMainMenu>(nameof(UIMainMenu), ui => ui.DrawUI());
    }

    void Start() {
        UIManager.Instance.TryGetUI<UIStart>(nameof(UIStart), ui => ui.DrawUI());
        UIManager.Instance.TryGetUI<UITitle>(nameof(UITitle), ui => ui.DrawUI());
    }
}