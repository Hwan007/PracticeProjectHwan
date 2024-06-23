using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Description/Skill")]
public class SkillDescription : ScriptableObject {
    public Dictionary<int, (string, string)> strings;
    public List<int> keys;
    public List<string> skillName;
    public List<string> description;

    public bool TryGetDescription(int key, out (string, string) value) {
        if (strings == null) {
            strings = new Dictionary<int, (string, string)>();
            for (int i = 0; i < keys.Count; ++i) {
                strings.Add(keys[i], (skillName[i], description[i]));
            }
        }
        if (strings.ContainsKey(key)) {
            value = strings[key];
            return true;
        }
        else {
            value = ("", "");
            return false;
        }
    }
}

[CustomEditor(typeof(SkillDescription))]
public class CustomEditotSkillDescription : Editor {
    SkillDescription description;

    private void OnEnable() {
        description = (SkillDescription)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
    }
}