using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebManager : MonoBehaviour
{
    string PicturePath = @"file://D:\user-data\下載\12361.jpg";

    public RawImage myRaw;

    public Texture2D m_uploadImage;
    string m_info="";


    private void Awake()
{
        StartCoroutine(ReadPic());
}

IEnumerator ReadPic()
{
	WWW www = new WWW(PicturePath);
	yield return www;
    if (www.error != null)
    {
    	m_info = www.error;
        yield return null;
    }
    m_uploadImage = www.texture;
}
/// <summary>
    /// 服务器下载图片
    /// </summary>
    /// <returns></returns>
    IEnumerator DownLoadPic()
    {
        WWW www = new WWW("http://english.tk888.me/upload/xxx.png");
        yield return www;
        if (www.error != null)
        {
            Debug.LogError(www.error);
            yield return null;
        }
        myRaw.texture = www.texture;
    }

    /// <summary>
    /// 图片上传服务器
    /// </summary>
    /// <returns></returns>
    IEnumerator IRequestPic()
    {
        WWWForm form = new WWWForm();
        form.AddField("folder","upload/");
        form.AddBinaryData("Pic", m_uploadImage.EncodeToPNG(),"xxx.png","image/png");
        WWW www = new WWW("http://english.tk888.me/index.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError(www.error);
            yield return null;
        }
        if (www.isDone)
        {
            Debug.LogError("上传成功");
            StartCoroutine(DownLoadPic());
        }
        Debug.LogError(www.text);
    }
 private void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width * 0.5f - 100,Screen.height * 0.5f -100,500,200),"");

        GUI.Label(new Rect(10,10,400,30),m_info);

        if (GUI.Button(new Rect(10, 110, 150, 30), "上传 Image"))
        {
            StartCoroutine(IRequestPic());
        }

        if (GUI.Button(new Rect(10, 140, 150, 30), "下载 Image"))
        {
            StartCoroutine(DownLoadPic());
        }
        GUI.EndGroup();
    }

}