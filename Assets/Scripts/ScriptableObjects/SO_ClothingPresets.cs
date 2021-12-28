using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Clothing", menuName = "Create Clothing Preset", order = 1)]
public class SO_ClothingPresets : ScriptableObject
{
     public int headID = 0;
     public int shirtID = 0;
     public int pantsID = 0;

     [Header(" ========= ")]

     public Color SkinColor;
     public Color headAccessoriesColor;
     public Color shirtColor;
     public Color pantsColor;
}
