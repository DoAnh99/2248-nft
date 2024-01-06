using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class AssetUser
{
    public string idAsset;
    public string nameAsset;
    public string urlImgAsset;


    // public string imageUrl;
    public AssetUser(string idAsset,string nameAsset, string urlImgAsset)
    {
        this.idAsset= idAsset;  
        this.nameAsset = nameAsset;
        this.urlImgAsset = urlImgAsset;
    }

}

[System.Serializable]
    public class Owner
    {
        public string address;
        public string referenceId;
    }
    [System.Serializable]
    public class Asset
    {
        public string id;
        public string name;
        public string description;
        public string imageUrl; 
        public Owner owner;
    }


    [System.Serializable]
    public class AssetListResponse
    {
        public List<Asset> data;
    }
public class ListAssetSale : Singleton<ListAssetSale>
{

 //   public Button Sale;


  //  public TMP_InputField textInputPrice;
  

    public List<AssetUser> ListAssetUser;
    public List<Sprite> ListImgAsset;
    // public List<Text> ListTextName;
    public GameObject gameObjectNoasset;
    public GameObject gameObjectasset1;
    public GameObject gameObjectasset2;
    public GameObject gameObjectasset3;

    public Text textNameAsset1;
    public Text textLinkImage1;
    public Image ImageItem1;

    public Text textNameAsset2;
    public Text textLinkImage2;
    public Image ImageItem2;
    
    public Text textNameAsset3;
    public Text textLinkImage3;
    public Image ImageItem3;


    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJrZXkiOiIwNTk4NGM5ZS02MTIzLTRhZWMtOGM2MC05MWM1NmE4YzEzNjgiLCJzdWIiOiI5NmZhNjlmYS0wOTlkLTRlNjctODNjNi0wYmIxZDUyYjM2ZDYiLCJpYXQiOjE3MDQyNjcwNjh9.ZiytZi2gOoFoKcLWRtbAna1FE0NPGzYk3H2G52c95y4";
    private string apiAssetsUrl = "https://api.gameshift.dev/assets";

    private void Start()
    {
        // ImageItem.SetActive(false);

        gameObjectasset1.SetActive(false);
        gameObjectasset2.SetActive(false);
        gameObjectasset3.SetActive(false);
        gameObjectNoasset.SetActive(false);
     

       // Sale.onClick.AddListener(OnButtonSaleClick);
    
        StartCoroutine(GetAssetList());
    }

    IEnumerator GetAssetList()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiAssetsUrl))
        {

            string userName = UserNFT.referenceId;
            Debug.Log(" UserName "+ userName);
            // Set headers
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("x-api-key", apiKey);
            // Send the request
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                Debug.Log("GET ASSET NOT ERROR");
                // Request successful, parse and handle the JSON response
                string jsonResponse = request.downloadHandler.text;
                AssetListResponse assetListResponse = JsonUtility.FromJson<AssetListResponse>(jsonResponse);
                // Access the list of assets
                List<Asset> assetList = assetListResponse.data;
                // Do something with the asset list
                foreach (Asset asset in assetList)
                {
                    if (asset.owner.referenceId == userName)
                    {
                        Debug.Log("Asset ID: " + asset.id);
                        Debug.Log("Asset Name: " + asset.name);
                        ListAssetUser.Add(new AssetUser(asset.id, asset.name,asset.description));//clear list
                    }
                }
            }
        }
        SetText();
    }


    public void SetText()
    {
        if (ListAssetUser.Count == 0)
        {
            gameObjectNoasset.SetActive(true);

        }

        else if (ListAssetUser.Count == 1)
        {


            gameObjectNoasset.SetActive(false);
            gameObjectasset1.SetActive(true);
            gameObjectasset2.SetActive(false);
            gameObjectasset3.SetActive(false);

            textNameAsset1.text = ListAssetUser[0].nameAsset;
            textLinkImage1.text = ListAssetUser[0].urlImgAsset;
            //  ImageItem.SetActive(true);
            //   getindexsprite(ListAssetUser[0].nameAsset);
            ImageItem1.sprite = ListImgAsset[getindexsprite(ListAssetUser[0].nameAsset)];

        }
        else if (ListAssetUser.Count == 2)
        {
            gameObjectNoasset.SetActive(false);
            gameObjectasset1.SetActive(true);
            gameObjectasset2.SetActive(true);
            gameObjectasset3.SetActive(false);

            textNameAsset1.text = ListAssetUser[0].nameAsset;
            textLinkImage1.text = ListAssetUser[0].urlImgAsset;
            ImageItem1.sprite = ListImgAsset[getindexsprite(ListAssetUser[0].nameAsset)];

            textNameAsset2.text = ListAssetUser[1].nameAsset;
            textLinkImage2.text = ListAssetUser[1].urlImgAsset;
            ImageItem2.sprite = ListImgAsset[getindexsprite(ListAssetUser[1].nameAsset)];

        }
        else if (ListAssetUser.Count == 3)
        {
            gameObjectNoasset.SetActive(false);
            gameObjectasset1.SetActive(true);
            gameObjectasset2.SetActive(true);
            gameObjectasset3.SetActive(true);

            textNameAsset1.text = ListAssetUser[0].nameAsset;
            textLinkImage1.text = ListAssetUser[0].urlImgAsset;
            ImageItem1.sprite = ListImgAsset[getindexsprite(ListAssetUser[0].nameAsset)];

            textNameAsset2.text = ListAssetUser[1].nameAsset;
            textLinkImage2.text = ListAssetUser[1].urlImgAsset;
            ImageItem2.sprite = ListImgAsset[getindexsprite(ListAssetUser[1].nameAsset)];

            textNameAsset3.text = ListAssetUser[2].nameAsset;
            textLinkImage3.text = ListAssetUser[2].urlImgAsset;
            ImageItem3.sprite = ListImgAsset[getindexsprite(ListAssetUser[2].nameAsset)];

        }
        else { }



    }
    public int getindexsprite(string nameAccess)
    {
        if (nameAccess == "Sale30")
        {
            return 0;
        }
        else if (nameAccess == "Sale50")
        {
            return 1;
        }
        else if (nameAccess == "Sale70")
        {
            return 2;
        }
        return 0;
        
    }
    public void OnButtonSaleClick()
    {
        // call sale post
    //   string price = textInputPrice.text; 
      //  PostDataRegisterUser(ListAssetUser[0].idAsset, price);
    }

    IEnumerator PostDataRegisterUser(string idAsset, string price)
    {

        // Create the JSON data to be sent in the request body
        string jsonBody = "{\"Id\":\"" + idAsset + "\",\"price\":\"" + price + "\"}";//
        Debug.Log(jsonBody);

        // Create the UnityWebRequest with POST method
        // UnityWebRequest request = UnityWebRequest.Post(apiUrl, jsonBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        // Create the UnityWebRequest with POST method
        UnityWebRequest request = new UnityWebRequest("https://api.gameshift.dev/assets/" + idAsset + "/sell", "POST");

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        // Set headers
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("x-api-key", apiKey);

        // Send the request
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("");
          
        }
        else
        {
          //  string jsonResponse = request.downloadHandler.text;
           // var responseJson = UnityEngine.JsonUtility.FromJson<ApiResponse>(jsonResponse);


          //  if (responseJson.statusCode == 409)
           // {

           // }
          //  else
         //   {
         //       Debug.Log(""successfully!");
                //Loading Scene
          //  }
        }
    }

}
