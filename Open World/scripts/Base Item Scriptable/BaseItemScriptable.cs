using System;
using System.Collections;
using UnityEngine;

public enum ItemTags { All, Aid, Ammo, Clothing, Collectable, Food, Weapon }
public enum ItemRarity { Common, Uncommom, Rare, Epic, Legandary }

[CreateAssetMenu(fileName = "Base Item", menuName = "ScriptableObjects/Item/BaseItem", order = 0)]
public class BaseItemScriptable : ScriptableObject
{
    public string itemName;
    public ItemRarity Rarity;
    [HideInInspector] public ItemTags ItemTag;
    public Color RarityColor;
    public float itemWeight;
    public float itemprice;
    [SerializeField] GameObject Model;
 

    public void GetRarity(out ItemRarity rarity, out Color rarityColor)
    {
        if (Rarity == ItemRarity.Uncommom)
        {
            rarity = ItemRarity.Common;
            rarityColor = MyColors.Base.Green;
            return;
        }
        if (Rarity == ItemRarity.Rare)
        {
            rarity = ItemRarity.Rare;
            rarityColor = MyColors.Base.Blue;
            return;

        }
        if (Rarity == ItemRarity.Epic)
        {
            rarity = ItemRarity.Epic;
            rarityColor = MyColors.Base.Purple;
            return;

        }
        if (Rarity == ItemRarity.Legandary)
        {
            rarity = ItemRarity.Legandary;
            rarityColor = MyColors.Base.Orange;
            return;

        }
        // is common
        rarity = ItemRarity.Common;
        rarityColor = Color.white;
    }
}



[CreateAssetMenu(fileName = "Base Dialogue", menuName = "ScriptableObjects/Dialogue/BaseDialogue", order = 0)]
public partial class BaseDialogueScriptable : ScriptableObject
{
}
