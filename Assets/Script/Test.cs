using Assets;
using UnityEngine;
using System.Collections;
using System.Data;
using  UnityEngine.UI;
using System.Linq;
using System;
using System.Threading;

public class Test : MonoBehaviour
{
    public Text text;
	
	void Start () {
        SqlAccess sql = new SqlAccess();
		sql.CreateTableAutoID("user",new string[] {"id","name","password"},new string[] {"int","text","text"});
       // sql.CreateTableAutoID("jl", new string[] { "id", "name", "qq", "email", "blog" }, new string[] { "int", "text", "text", "text", "text" });
        //sql.InsertInto("jl", new string[] { "name", "qq", "email", "blog" }, new string[] { "jianglei", "289187120", "[email protected]", "jianglei.com" });
        //  sql.InsertInto("jl", new string[] { "name", "qq", "email", "blog" }, new string[] { "lizhih", "34546546", "[email protected]", "lizhih.com" });
	   // sql.Delete("jl", new string[] {"id"}, new string[] {"2"});
       	DataSet ds=sql.QuerySet("Select *  from user");
		
		text = GameObject.Find("TestText").GetComponent<Text>();
	    if (ds!=null)
	    {
	        DataTable table = ds.Tables[0];
			// 找值
			// string name = table.Rows[0][0].ToString();
			// Debug.Log(name);

			// 印出 datatable 
			// string res = string.Join(Environment.NewLine, 
     		// table.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));
			// Debug.Log(res);
	        foreach (DataRow dataRow in table.Rows)
	        {
	            foreach (DataColumn dataColumn in table.Columns)
	            {
	                Debug.Log(dataRow[dataColumn]);
					
					
	                text.text += "  "+dataRow[dataColumn].ToString();
	            }
	        }
	    }
		
        sql.Close();
	}
	
	
	void Update () {
	
	}
}