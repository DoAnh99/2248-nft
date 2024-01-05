using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowList : MonoBehaviour
{
   public List<Text> ListTextName;
     void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetText()
    {
        for (int i = 0; i < ListAssetSale.Instance.ListAssetUser.Count; i++)
        {
      //      ListTextName.Add(ListAssetSale.Instance.ListAssetUser.nameAsset);
        }
    }
}
