using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Defective.JSON;

public class APIHelper : Singleton<APIHelper>
{

    [System.Serializable]
    public class AssetDetails
    {
        public string description;
        public string imageUrl;
        public string name;
        public Attribute[] attributes;
    }

    [System.Serializable]
    public class Attribute
    {
        public string displayType;
        public string traitType;
        public string value;
    }

    [System.Serializable]
    public class CreateAssetRequest
    {
        public AssetDetails details;
        public string destinationUserReferenceId;
    }


    public string description = "newBlockUnLock";
    public string imageUrl = "https://i.imgur.com/5QNNHEC.png";
    public string nameAsset = "Block2048";
    public string destinationUserReferenceId = UserNFT.referenceId;
    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJrZXkiOiIwNTk4NGM5ZS02MTIzLTRhZWMtOGM2MC05MWM1NmE4YzEzNjgiLCJzdWIiOiI5NmZhNjlmYS0wOTlkLTRlNjctODNjNi0wYmIxZDUyYjM2ZDYiLCJpYXQiOjE3MDQyNjcwNjh9.ZiytZi2gOoFoKcLWRtbAna1FE0NPGzYk3H2G52c95y4";
    private string apiAssetsUrl = "https://api.gameshift.dev/assets";

  
    IEnumerator PostDataCreateAssetUser(string description, string imageUrl, string nameAsset, string destinationUserReferenceId)
    {

        string jsonBody = "";

        JSONObject jsonData = new();

        JSONObject jsonDetails = new();

        jsonDetails.AddField("description", description);
        jsonDetails.AddField("imageUrl", imageUrl);
        jsonDetails.AddField("name", nameAsset);

        JSONObject jsonAttributes = new();

        JSONObject jsonAttributeElement = new();
        jsonAttributeElement.AddField("displayType", "Formatted Attribute Name");
        jsonAttributeElement.AddField("traitType", "attribute-name");
        jsonAttributeElement.AddField("value", "attribute-value");

        jsonAttributes.Add(jsonAttributeElement);

        jsonDetails.AddField("attributes", jsonAttributes);

        jsonData.AddField("details", jsonDetails);

        jsonData.AddField("destinationUserReferenceId", destinationUserReferenceId);

        jsonBody = jsonData.ToString();

        Debug.Log(jsonBody);

        // Create the UnityWebRequest with POST method
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        UnityWebRequest request = new UnityWebRequest(apiAssetsUrl, "POST");
      
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
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // Parse the JSON response
            Debug.Log("Create Access Success");
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Response: " + jsonResponse);
        }
    }
    public void CreateAssetUser(string description, string imageUrl, string nameAsset, string destinationUserReferenceId)
    {
        Debug.Log("#1: "+destinationUserReferenceId);
        Debug.Log("#2: "+UserNFT.referenceId);
        StartCoroutine(PostDataCreateAssetUser(description,imageUrl,nameAsset,destinationUserReferenceId));
    }



}
