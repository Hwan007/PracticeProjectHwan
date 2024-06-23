using Stat;
using UI;
using UnityEngine;
using Utils;

public class GameManager : UnitySingleton<GameManager> {
    protected override void Initialize() {
        Application.targetFrameRate = 60;
    }

    public void StartGame() {
        //UIManager.Instance.TryGetUI<UIMainMenu>(nameof(UIMainMenu), ui => ui.DrawUI());
        StageManager.Instance.DeployPlayer(CharacterManager.Instance.GetPlayerStat(), (player) => {
            StageManager.Instance.StartStage(0);
            CameraController.SetFollow(0, player.gameObject);
        });
        //UIManager.Instance.TryGetUI<UIStageInfo>(nameof(UIStageInfo), ui => ui.DrawUI());
    }

    void Start() {
        UIManager.Instance.TryGetUI<UIStart>(nameof(UIStart), ui => ui.DrawUI());
        UIManager.Instance.TryGetUI<UITitle>(nameof(UITitle), ui => ui.DrawUI());
    }
}