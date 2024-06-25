using Define;
using Item;
using Stat;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Character;

public class CharacterManager : UnitySingleton<CharacterManager> {
    [SerializeField] CustomStat playerBaseStat;
    CharacterStat playerStat;
    List<BaseItem> ownedItems;
    Dictionary<EItemType, BaseItem> equipItems;

    protected override void Initialize() {
        playerStat = new CharacterStat();
        ownedItems = new List<BaseItem>();
        equipItems = new Dictionary<EItemType, BaseItem>();
        playerStat.ResetStatTo(playerBaseStat.ConvertToBaseStat());
    }

    public CharacterStat GetPlayerStat() {
        return playerStat;
    }

    public CharacterStat EquipItem(BaseItem item) {
        if (equipItems.TryGetValue(item.GetItemType(), out var equipped)) {
            playerStat.ChangeItemTo(equipped, item);
            equipItems[item.GetItemType()] = item;
        }
        else {
            playerStat.ChangeItemTo(null, item);
            equipItems.Add(item.GetItemType(), item);
        }
        return playerStat;
    }

    public CharacterStat UnequipItem(BaseItem item) {
        playerStat.ChangeItemTo(item, null);
        equipItems.Remove(item.GetItemType());
        return playerStat;
    }

    public void DestoryItem(BaseItem item) {
        if (equipItems.TryGetValue(item.GetItemType(), out var equipped)) {
            if (equipped != null) {
                if (equipped == item) {
                    UnequipItem(item);
                }
            }
            ownedItems.Remove(item);
        }
    }

    internal BaseItem[] GetOwnedItems() {
        return ownedItems.ToArray();
    }
}