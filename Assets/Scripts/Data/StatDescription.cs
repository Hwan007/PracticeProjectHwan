using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Description/Stat")]
public class StatDescription : ScriptableObject {
    public List<string> strings;
    public string GetDescription(int index) {
        return strings[index];
    }
}

[CustomEditor(typeof(StatDescription))]
public class CustomEditotStatDescription : Editor {
    StatDescription description;
    public string[] statKorDescriptions = {
            "체력",
            "골드 획득 증폭(%)",

            "일반 공격력",
            "화염 공격력",
            "공허 공격력",
            "화염 상태이상 지속 시간","공허 상태이상 지속 시간",
            "공격력 증폭",
            "상태이상 공격력 증폭(%)","상태이상 시간 증폭(%)",
            "공격 거리",
            "치명타 확률","치명타 대미지 증폭(%)",
            "공격 스킬 콤보당 충전량","공격 스킬",

            "방어력",
            "방어 재사용 대기시간",

            "추진력",
            "질량",
            "점프 스킬 콤보당 충전량","점프 스킬",
        };

    public string[] statEngDescriptions = {
            "Health",
            "Gold Earn Multiplier",

            "Normal Attack",
            "Fire Attack",
            "Void Attack",
            "Fire Ailment Duration", "Void Ailment Duration",
            "Attack Multiplier",
            "Ailment Attack Multiplier","Ailment Time Multiplier",
            "Attack Distance",
            "Critical Chance","Critical Damage Multiplier",
            "Attack Skill Charge per Combo","Attack Skill",

            "Defense Power",
            "Defense Skill Charge per Combo","Defense Skill",

            "Jump Power",
            "Mass",
            "Jump Skill Charge per Combo","Jump Skill",
        };
    private void OnEnable() {
        description = (StatDescription)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset to Korean")) {
            description.strings.Clear();
            foreach (string s in statKorDescriptions) {
                description.strings.Add(s);
            }
            EditorUtility.SetDirty(this);
        }
        if (GUILayout.Button("Reset to English")) {
            description.strings.Clear();
            foreach (string s in statEngDescriptions) {
                description.strings.Add(s);
            }
            EditorUtility.SetDirty(this);
        }
    }

}