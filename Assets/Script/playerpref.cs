using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class playerpref : MonoBehaviour
{
    public InputField test;
    public Text Text;
    UnityWebRequest w_Texture;
    UnityWebRequest default_avatar_Texture;//取預設大頭貼
    UnityWebRequest avatar_Texture;//取用戶的大頭貼
    public RawImage image;
    public RawImage avatar;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Start());
        }
    }

    IEnumerator Start()
    {
        //==============取用戶大頭貼==============
        int nInt = PlayerPrefs.GetInt("ID");
        avatar_Texture = UnityWebRequestTexture.GetTexture("http://english.tk888.me/avatar/" + nInt.ToString() + ".png");
        default_avatar_Texture = UnityWebRequestTexture.GetTexture("http://english.tk888.me/avatar/default.png");

        yield return avatar_Texture.SendWebRequest();
        yield return default_avatar_Texture.SendWebRequest();

        if (avatar_Texture.isNetworkError || avatar_Texture.isHttpError)
        {
            avatar.texture = DownloadHandlerTexture.GetContent(default_avatar_Texture);
            avatar.SetNativeSize();
            //Debug.Log(avatar_Texture.error);
        }
        else
        {
            avatar.texture = DownloadHandlerTexture.GetContent(avatar_Texture);
            avatar.SetNativeSize();
        }
        //==============取活動貼圖，可以更改(目前無用處)==============
        w_Texture = UnityWebRequestTexture.GetTexture("http://english.tk888.me/baha.png");
        yield return w_Texture.SendWebRequest();

        if (w_Texture.isNetworkError || w_Texture.isHttpError)
        {
            Debug.Log(w_Texture.error);
        }
        else
        {
            image.texture = DownloadHandlerTexture.GetContent(w_Texture);
            image.SetNativeSize();
        }
        // // Load();
        // //StartCoroutine(Download_File());
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
    public IEnumerator profile()
    {
        int nInt = PlayerPrefs.GetInt("ID");
        default_avatar_Texture = UnityWebRequestTexture.GetTexture("http://english.tk888.me/avatar/default.png");
        avatar_Texture = UnityWebRequestTexture.GetTexture("http://english.tk888.me/avatar/" + nInt.ToString() + ".png");
        yield return avatar_Texture.SendWebRequest();
        if (avatar_Texture.error == "404")
        {
            avatar.texture = DownloadHandlerTexture.GetContent(default_avatar_Texture);
            avatar.SetNativeSize();
            //Debug.Log(avatar_Texture.error);
        }
        else
        {
            avatar.texture = DownloadHandlerTexture.GetContent(avatar_Texture);
            avatar.SetNativeSize();
        }
    }

}