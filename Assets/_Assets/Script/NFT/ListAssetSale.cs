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


    // public string imageUrl;
    public AssetUser(string idAsset,string nameAsset)
    {
        this.idAsset= idAsset;  
        this.nameAsset = nameAsset;
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

    public Button Sale;


    public TMP_InputField textInputPrice;
    public GameObject ImageItem;

    public List<AssetUser> ListAssetUser;
   // public List<Text> ListTextName;
    public Text textNameAsset;
    public Text textLinkImage;
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJrZXkiOiIwNTk4NGM5ZS02MTIzLTRhZWMtOGM2MC05MWM1NmE4YzEzNjgiLCJzdWIiOiI5NmZhNjlmYS0wOTlkLTRlNjctODNjNi0wYmIxZDUyYjM2ZDYiLCJpYXQiOjE3MDQyNjcwNjh9.ZiytZi2gOoFoKcLWRtbAna1FE0NPGzYk3H2G52c95y4";
    private string apiAssetsUrl = "https://api.gameshift.dev/assets";

    private void Start()
    {
        ImageItem.SetActive(false);
        Sale.onClick.AddListener(OnButtonSaleClick);
    
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
                        ListAssetUser.Add(new AssetUser(asset.id, asset.name));
                    }
                }
            }
        }
        SetText();
    }

    //  public void SetText()
    //  {
    //     for (int i = 0; i <ListAssetUser.Count; i++)
    //     {
    //        ListTextName.Add(ListAssetUser[i].nameAsset);
    //        ListTextName[i].text = ListAssetUser[i].nameAsset;
    // textNameAsset.text= ListAssetUser[0].nameAsset;
    //    }
    // }
    public void SetText()
    {
        if (ListAssetUser.Count > 0)
        { 
          textNameAsset.text = ListAssetUser[0].nameAsset;
          textLinkImage.text = "https://i.imgur.com/5QNNHEC.png";
          ImageItem.SetActive(true);
        }
    }
    public void OnButtonSaleClick()
    {
        // call sale post
       string price = textInputPrice.text; 
        PostDataRegisterUser(ListAssetUser[0].idAsset, price);
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
            Debug.Log("loi roi register Error");
          
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
