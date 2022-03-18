using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using UnityEngine.UI;
using System.Data;
using System.IO;
using UnityEngine.SceneManagement;


public class p2 : MonoBehaviour
{
    public Text show_cn;
    public InputField search;
    public Text show_pos;

    SqlAccess sql;
    public void word()
    {
        try
        {
            sql = new SqlAccess();
            DataSet ds = sql.QuerySet("Select chinese,pos from dictionary where english='" + search.text + "'");
            DataTable table = ds.Tables[0];
            foreach (DataRow dataRow in table.Rows)
            {
                foreach (DataColumn dataColumn in table.Columns)
                {
                    show_cn.text = "中文、詞性為: " + dataRow[0].ToString() + "\n" + dataRow[1].ToString();
                }
            }
            if (table.Rows.Count == 0)
            {
                show_cn.text = "Sorry!尚未收錄此單字";
            }
        }
        catch (Exception)
        {
            show_cn.text = "Sorry!尚未收錄此單字";
        }
        sql.Close();
    }
    public void lastScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public class playerState
    {
        public string name;
        public int level;
        public Vector3 pos;
    }
    public void save()
    {
        //填寫playerState格式的資料..
        playerState myPlayer = new playerState();
        myPlayer.name = "sammaru";
        myPlayer.level = 87;
        myPlayer.pos = GameObject.Find("sammaru").transform.position;

        //將myPlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(myPlayer);

        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, "myPlayer"));
        file.Write(saveString);
        file.Close();
    }
}