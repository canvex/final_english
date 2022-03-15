using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using UnityEngine.UI;
using System.Data;
using UnityEngine.SceneManagement;

public class User : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public InputField email;
    public Text loginMsg;
    public Text info;
    SqlAccess sql;

    public bool isSpace(InputField inputField){
        if(inputField.text.Length !=0){
            return true;
        }
            return false;
    }
     public void register(){
        sql= new SqlAccess();
        if(isSpace(username)&&isSpace(password)&&isSpace(email)){
            DataSet ds=sql.InsertInto("user",new string[]{"username","password","email"},new string[]{username.text,password.text,email.text});
            loginMsg.text="註冊成功";
        }else{
            loginMsg.text="帳密不能為空值";
        }
        sql.Close();
    }

    public void Login()
    {
        sql = new SqlAccess();
        if (username.text != null && password.text != null)
        {
            DataSet ds = sql.QuerySet("Select id from user where username ='" + username.text + "' and password ='" + password.text + "'");

            DataTable table = ds.Tables[0];
            foreach (DataRow dataRow in table.Rows)
            {
                foreach (DataColumn dataColumn in table.Columns)
                {
                    Debug.Log("登入ID:" + dataRow[dataColumn]);
                    info.text = "你的ID為: " + dataRow[dataColumn];
                }
            }
            if (table.Rows.Count > 0)
            {
                loginMsg.text = "歡迎" + username.text + "登入";
            }
            else
            {
                loginMsg.text = "帳號或密碼錯誤";
            }
        }
        sql.Close();
    }

    public void nextScene()
    {
        SceneManager.LoadScene("2");
    }

    public void lastScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void btn()
    {
        loginMsg.text = "一切準備就緒!!";

    }


}
