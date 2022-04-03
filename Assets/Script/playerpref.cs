using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class playerpref : MonoBehaviour
{
    public InputField test;
    public Text Text;
    UnityWebRequest w_Texture;
    public RawImage image;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.A)){
            StartCoroutine(Start());
        }
    }
    IEnumerator Start()
    {
        w_Texture=UnityWebRequestTexture.GetTexture("http://english.tk888.me/baha.png");
        yield return w_Texture.SendWebRequest();

        if(w_Texture.isNetworkError || w_Texture.isHttpError){
            Debug.Log(w_Texture.error);
        }
        else{
            image.texture=DownloadHandlerTexture.GetContent(w_Texture);
            image.SetNativeSize();
        }
        // Load();
        //StartCoroutine(Download_File());
    }

    IEnumerator Download_File()
    {
        string url = "https://i.imgur.com/heCxlGL.jpg"; // 欲下載圖片的網路位址
        string savePath = "d:/unity.jpg"; // 圖片下載後的儲存路徑

        var uwr = new UnityWebRequest(url);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var dh = new DownloadHandlerFile(savePath);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError) { Debug.Log(uwr.error); }
        else { Debug.Log("檔案已下載到:" + savePath); }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("ID", 1);
        PlayerPrefs.SetString("username", test.text);
    }

    public void Load()
    {
        int nInt = PlayerPrefs.GetInt("ID");
        string sString = PlayerPrefs.GetString("username");

        Text.text = "ID:" + nInt.ToString() + "\n用戶名:" + sString.ToString();
        Debug.Log("nInt: " + nInt.ToString() + ", sString: " + sString);
    }
    public void delete()
    {
        PlayerPrefs.DeleteAll();
    }
}