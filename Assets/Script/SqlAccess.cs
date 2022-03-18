using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using UnityEngine;
using System.IO;

namespace Assets
{
         public class SqlAccess
    {
        public static MySqlConnection mySqlConnection;//連線類物件
        private static string database = "english";
        private static string host = "152.70.110.2";
        private static string username = "english";
        private static string password = "@bcd1234";

        public SqlAccess()
        {
            OpenSql();
        }
        /// <summary>
        /// 開啟資料庫
        /// </summary>
        public static void OpenSql()
        {
            try
            {
                //string.Format是將指定的 String型別的資料中的每個格式項替換為相應物件的值的文字等效項。
                string sqlString = string.Format("Database={0};Data Source={1};User Id={2};Password={3};", database, host, username, password, "3306");
                mySqlConnection = new MySqlConnection(sqlString);
                mySqlConnection.Open();
            }
            catch (Exception)
            {
                throw ;
                //("伺服器連線失敗.....");
            }
        }
        public MySqlConnection GetMySqlConnection(){
            return  mySqlConnection;
        }
        /// <summary>
        /// 建立表
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="colName">屬性列</param>
        /// <param name="colType">屬性型別</param>
        /// <returns></returns>
        public DataSet CreateTable(string name, string[] colName, string[] colType)
        {
            if (colName.Length != colType.Length)
            {
                throw new Exception("輸入不正確：" + "columns.Length != colType.Length");
            }
            string query = "CREATE TABLE if not exists " + name + "(" + colName[0] + " " + colType[0];
            for (int i = 1; i < colName.Length; i++)
            {
                query += "," + colName[i] + " " + colType[i];
            }
            query += ")";
            return QuerySet(query);
        }
        /// <summary>
        /// 建立具有id自增的表
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="col">屬性列</param>
        /// <param name="colType">屬性列型別</param>
        /// <returns></returns>
        public DataSet CreateTableAutoID(string name, string[] col, string[] colType)
        {
            if (col.Length != colType.Length)
            {

                throw new Exception("columns.Length != colType.Length");

            }

            string query = "CREATE TABLE  if not exists " + name + " (" + col[0] + " " + colType[0] + " NOT NULL AUTO_INCREMENT";

            for (int i = 1; i < col.Length; ++i)
            {

                query += ", " + col[i] + " " + colType[i];

            }

            query += ", PRIMARY KEY (" + col[0] + ")" + ")";

            //    Debug.Log(query);

            return QuerySet(query);
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="items">需要查詢的列</param>
        /// <param name="whereColName">查詢的條件列</param>
        /// <param name="operation">條件操作符</param>
        /// <param name="value">條件的值</param>
        /// <returns></returns>
        public DataSet Select(string tableName, string[] items, string[] whereColName, string[] operation, string[] value)
        {
            if (whereColName.Length != operation.Length || operation.Length != value.Length)
            {
                throw new Exception("輸入不正確：" + "col.Length != operation.Length != values.Length");
            }
            string query = "SELECT " + items[0];
            for (int i = 1; i < items.Length; i++)
            {
                query += "," + items[i];
            }
            query += "  FROM  " + tableName + "  WHERE " + " " + whereColName[0] + operation[0] + "\"" + value[0] + "\"";
            for (int i = 1; i < whereColName.Length; i++)
            {
                query += " AND " + whereColName[i] + operation[i] + "' " + value[i] + "'";
            }
            Debug.Log(query);
            return QuerySet(query);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="cols">條件：刪除列</param>
        /// <param name="colsvalues">刪除該列屬性值所在得行</param>
        /// <returns></returns>
        public DataSet Delete(string tableName, string[] cols, string[] colsvalues)
        {
            string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];

            for (int i = 1; i < colsvalues.Length; ++i)
            {

                query += " or " + cols[i] + " = " + colsvalues[i];
            }
          //  Debug.Log(query);
            return QuerySet(query);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="cols">更新列</param>
        /// <param name="colsvalues">更新的值</param>
        /// <param name="selectkey">條件：列</param>
        /// <param name="selectvalue">條件：值</param>
        /// <returns></returns>
        public DataSet UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
        {

            string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

            for (int i = 1; i < colsvalues.Length; ++i)
            {

                query += ", " + cols[i] + " =" + colsvalues[i];
            }

            query += " WHERE " + selectkey + " = " + selectvalue + " ";

            return QuerySet(query);
        }
        
        /// <summary>
       /// 插入一條資料，包括所有，不適用自動累加ID。
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="values">插入值</param>
        /// <returns></returns>
        public DataSet InsertInto(string tableName, string[] values)
        {

            string query = "INSERT INTO " + tableName + " VALUES (" + "'" + values[0] + "'";

            for (int i = 1; i < values.Length; ++i)
            {

                query += ", " + "'" + values[i] + "'";

            }

            query += ")";

        // Debug.Log(query);
            return QuerySet(query);
        }

        
        /// <summary>
        /// 插入部分
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="col">屬性列</param>
        /// <param name="values">屬性值</param>
        /// <returns></returns>
        public DataSet InsertInto(string tableName, string[] col, string[] values)
        {

            if (col.Length != values.Length)
            {

                throw new Exception("columns.Length != colType.Length");

            }

            string query = "INSERT INTO " + tableName + " (" + col[0];
            for (int i = 1; i < col.Length; ++i)
            {

                query += ", " + col[i];

            }

            query += ") VALUES (" + "'" + values[0] + "'";
            for (int i = 1; i < values.Length; ++i)
            {

                query += ", " + "'" + values[i] + "'";

            }

            query += ")";

            //Debug.Log(query);
            return QuerySet(query);

        }
        /// <summary>
        /// 
        /// 執行Sql語句
        /// </summary>
        /// <param name="sqlString">sql語句</param>
        /// <returns></returns>
        public  DataSet QuerySet(string sqlString)
        {
            if (mySqlConnection.State == ConnectionState.Open)
            {
                DataSet ds = new DataSet();
                try
                {

                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlString,mySqlConnection);
                    mySqlDataAdapter.Fill(ds);
                }
                catch (Exception e)
                {
                    throw new Exception("SQL:" + sqlString + "/n" + e.Message.ToString());
                }
                finally
                {

                }
                return ds;
            }
            return null;
        }

        public void Close()
        {

            if (mySqlConnection != null)
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
                mySqlConnection = null;
            }

        }
    }
}
