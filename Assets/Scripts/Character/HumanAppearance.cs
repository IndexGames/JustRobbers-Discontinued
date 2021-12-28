using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAppearance : MonoBehaviour
{
     //Clothing
     public SkinnedMeshRenderer body;
     public List<Color> skinColors = new List<Color>();

     [Header("===================")]
     public List<GameObject> shirts = new List<GameObject>();
     public List<Color> shirtColor = new List<Color>();
     [Header("===================")]
     public List<GameObject> pants = new List<GameObject>();
     public List<Color> pantsColor = new List<Color>();
     [Header("===================")]
     public List<GameObject> head = new List<GameObject>();
     public List<Color> headColor = new List<Color>();

     public SO_ClothingPresets customPreset;
     // Start is called before the first frame update
     void Start()
    {
          ResetClothes();

          
    }
     // Update is called once per frame
     void Update()
    {
          if (customPreset)
          {
               LoadClothesPreset(customPreset);
          }
     }

     private void ResetClothes()
     {
          for(int i = 0; i < shirts.Count; i++)
          {
               shirts[i].SetActive(false);
          }

          for (int i = 0; i < pants.Count; i++)
          {
               pants[i].SetActive(false);
          }

          for (int i = 0; i < head.Count; i++)
          {
               head[i].SetActive(false);
          }
     }

     
     public void LoadClothesPreset(SO_ClothingPresets preset)
     {
          if(shirts[preset.shirtID] != null)
          {
               shirts[preset.shirtID].SetActive(true);
               shirts[preset.shirtID].GetComponent<SkinnedMeshRenderer>().material.color = preset.shirtColor;
          }

          if (pants[preset.pantsID] != null)
          {
               pants[preset.pantsID].SetActive(true);
               pants[preset.pantsID].GetComponent<SkinnedMeshRenderer>().material.color = preset.pantsColor;
          }

          if (head[preset.headID] != null)
          {
               head[preset.headID].SetActive(true);
               head[preset.headID].GetComponent<SkinnedMeshRenderer>().material.color = preset.headAccessoriesColor;
          }

          body.material.color = preset.SkinColor;
     }
}
