using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameShift : Singleton<GameShift>
{

    public TMP_InputField textInputName;
    public TMP_InputField textInputEmail;
    public Button btnLogin;
    public Button btnRegister;
    public Text ObjErrRegister;
   
    /// ////////////////////

    private string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJrZXkiOiIwNTk4NGM5ZS02MTIzLTRhZWMtOGM2MC05MWM1NmE4YzEzNjgiLCJzdWIiOiI5NmZhNjlmYS0wOTlkLTRlNjctODNjNi0wYmIxZDUyYjM2ZDYiLCJpYXQiOjE3MDQyNjcwNjh9.ZiytZi2gOoFoKcLWRtbAna1FE0NPGzYk3H2G52c95y4";
    private string apiRegisterUrl = "https://api.gameshift.dev/users";// API URL  REGISTER
    private string apiLoginUrl = "https://api.gameshift.dev/users/";


    public string idUser;
    public string EmailUser;
    void Start()
    {
        ObjErrRegister.gameObject.SetActive(false);
        btnLogin.onClick.AddListener(OnButtonLoginClick);
        btnRegister.onClick.AddListener(OnButtonRegisterClick);
       
    }
    

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PostDataRegisterUser(string referenceId, string email)
    {       
         // Create the JSON data to be sent in the request body
         string jsonBody = "{\"referenceId\":\"" + referenceId + "\",\"email\":\"" + email + "\"}";

        // Create the UnityWebRequest with POST method
        // UnityWebRequest request = UnityWebRequest.Post(apiUrl, jsonBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        // Create the UnityWebRequest with POST method
        UnityWebRequest request = new UnityWebRequest(apiRegisterUrl, "POST");
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
                ObjErrRegister.gameObject.SetActive(true);
                DOVirtual.DelayedCall(2.0f, () =>
                {
                   ObjErrRegister.gameObject.SetActive(false);

                });
            }
            else
            {


            // if 404  hien thong bao loi reload scene


            //else// debug and go to game luoon  load scene
            // Debug.Log("User" + EmailUser +  " registered successfully!");

            string jsonResponse = request.downloadHandler.text;
            var responseJson = UnityEngine.JsonUtility.FromJson<ApiResponse>(jsonResponse);


            if (responseJson.statusCode == 409)
            {
                //      ReloadAssets scene
                Debug.Log("register Error");
                ObjErrRegister.text = responseJson.message;
                ObjErrRegister.gameObject.SetActive(true);


            }
            else
            {
                Debug.Log("User" + EmailUser + " registered successfully!");
                //Loading Scene
            }
        }
    }

    IEnumerator PostDataLoginUser(string referenceId)
    {
        UnityWebRequest request = new UnityWebRequest(apiLoginUrl + referenceId, "GET");
     
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
                ObjErrRegister.text = "Login error";
            ObjErrRegister.gameObject.SetActive(true);
        }
        else
        {


            // if 404  hien thong bao loi reload scene


            //else// debug and go to game luoon  load scene
            // Debug.Log("User" + EmailUser +  " registered successfully!");

            string jsonResponse = request.downloadHandler.text;
            var responseJson = UnityEngine.JsonUtility.FromJson<ApiResponse>(jsonResponse);


            if (responseJson.statusCode == 409)
            {
                //      ReloadAssets scene
                Debug.Log("Login Error");
                ObjErrRegister.text = responseJson.message;
                ObjErrRegister.gameObject.SetActive(true);


            }
            else
            {
                UserNFT.referenceId = responseJson.referenceId;
                UserNFT.email = responseJson.email;
                UserNFT.address = responseJson.address;
                Debug.Log("login successfully!");
                //Loading Scene
                SceneManager.LoadScene("Loading");
            }
        }
    }

    void OnButtonLoginClick()
    {
        ObjErrRegister.gameObject.SetActive(false);
        // goi get
        StartCoroutine(PostDataLoginUser(textInputName.text));
    }

    void OnButtonRegisterClick()
    {
        ObjErrRegister.gameObject.SetActive(false);
        //goi post  
        StartCoroutine(PostDataRegisterUser(textInputName.text, textInputEmail.text));
    }



    [System.Serializable]
    public class ApiResponse
    {
        public int statusCode;
        public string message;
        public string referenceId;
        public string address;
        public string email;
    }
}