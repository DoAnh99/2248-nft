using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winscript : Singleton<winscript>
{
    // Start is called before the first frame update
    public Image imageassetwin;
    public List<Sprite> ListSpriteAssetWin; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAssetWin(int indx)
    {
        if (ListSpriteAssetWin.Count > 0 && indx < ListSpriteAssetWin.Count)
        {
            imageassetwin.sprite = ListSpriteAssetWin[indx];

        }
        else
        {
          
        }
    }
}
