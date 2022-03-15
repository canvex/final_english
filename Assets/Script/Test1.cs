using System.Collections;
using System.Collections.Generic;
using Assets;
using MySql.Data.MySqlClient;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SqlAccess sql = new SqlAccess();
        MySqlConnection coon = SqlAccess.mySqlConnection;
        string sqlsyntax = "select * from user";//sql语句
         MySqlCommand cmd = new MySqlCommand(sqlsyntax, coon);//创建一条新的指令
            //string insertaql = "insert into gamerinfo values('1313333','5840','Join');";

            MySqlDataReader reader = cmd.ExecuteReader();//读取指令

            while (reader.Read())
            {
                Debug.Log("账号:" + reader[0].ToString() + "密码:" + reader[1].ToString() + "玩家昵称" + reader[2].ToString());
                //我们可以把读取的数据看成一个数组 其中的索引也从0开始计数
            }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
