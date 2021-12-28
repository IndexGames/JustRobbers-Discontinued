using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Create Item", order = 1)]
public class SO_ItemData : ScriptableObject
{
     [Header("Item Data")]

     public string itemName = "New Name";
     public string itemDescription = "Item description";
     [Tooltip("Define item prefab")] public GameObject itemPrefab = null;
     [Tooltip("Define item icon on UI")] public Texture2D itemIcon = null;
     [Tooltip("Define max stack of the item")] public int maxStack = 1;

     public bool throwable = false;
     public bool destroyOnUse = false;

}
