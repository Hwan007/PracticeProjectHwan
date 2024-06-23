using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CreateAssetMenu(menuName = "Description/Item")]
public class ItemDescription : ScriptableObject {
    public Dictionary<int, (string, string)> strings;
    public List<int> keys;
    public List<string> itemName;
    public List<string> description;

    public bool TryGetDescription(int key, out (string, string) value) {
        if (strings == null) {
            strings = new Dictionary<int, (string, string)>();
            for (int i = 0; i < keys.Count; ++i) {
                strings.Add(keys[i], (itemName[i], description[i]));
            }
        }
        if (strings.ContainsKey(key)) {
            value = strings[key];
            return true;
        }
        else {
            value = ("","");
            return false;
        }
    }
}

[CustomEditor(typeof(ItemDescription))]
public class CustomEditotItemDescription : Editor {
    ItemDescription description;

    private void OnEnable() {
        description = (ItemDescription)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

    }
}