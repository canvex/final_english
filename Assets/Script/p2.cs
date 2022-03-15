using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using UnityEngine.UI;
using System.Data;
using UnityEngine.SceneManagement;

public class p2 : MonoBehaviour
{
    public Text show_cn;
    public InputField search;
    public Text show_pos;

    SqlAccess sql;
    public void word()
    {
        sql = new SqlAccess();
        DataSet ds = sql.QuerySet("Select chinese,pos from dictionary where english='"+search.text+"'");
        DataTable table = ds.Tables[0];
        foreach (DataRow dataRow in table.Rows)
        {
            foreach (DataColumn dataColumn in table.Columns)
            {
                show_cn.text = "中文、詞性為: " + dataRow[0].ToString()+"\n"+dataRow[1].ToString();
            }
        }
        if (table.Rows.Count == 0)
            {
                show_cn.text = "Sorry!尚未收錄此單字";
                show_pos.text= "Sorry!尚未收錄此單字";
            }     
        sql.Close();
    }    
    public void lastScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}