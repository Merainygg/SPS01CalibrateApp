using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MySql.Data.MySqlClient;

namespace SPS01CalibrateApp
{
    public class MySqlLocal:IDisposable
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        private MySqlConnection Connection { get; set; }
        public bool IsConnected { get; set; }

        public void Connect()
        {
            // 连接数据库
            var connectionString = "SERVER=" + Server + ";" + "DATABASE=" + Database + ";" + "UID=" + User + ";" + "PASSWORD=" + Password + ";";
            try
            {
                Connection = new MySqlConnection(connectionString);
                Connection.Open();
            }
            catch (Exception){
                throw new Exception("连接数据库失败");
            }
        }

        public void CreateTable(string sql)
        {
            // 创建表
            var cmd = new MySqlCommand(sql, Connection);
            cmd.ExecuteNonQuery();
        }

        public void Insert(string sql)
        {
            // 插入数据
            var cmd = new MySqlCommand(sql, Connection);
            cmd.ExecuteNonQuery();
        }

        public void Update(string sql)
        {
            // 更新数据
            var cmd = new MySqlCommand(sql, Connection);
            cmd.ExecuteNonQuery();
        }

        public void Delete(string sql)
        {
            // 删除数据
            var cmd = new MySqlCommand(sql, Connection);
            cmd.ExecuteNonQuery();
        }

        public List<string>[] Select(string sql)
        {
            // 查询数据
            var cmd = new MySqlCommand(sql, Connection);
            var dataReader = cmd.ExecuteReader();
            if (dataReader == null)
            {
                return null;
            }
            var result = new List<string>[dataReader.FieldCount];
            for (var i = 0; i < dataReader.FieldCount; i++)
            {
                result[i] = new List<string>();
            }
            while (dataReader.Read())
            {
                for (var i = 0; i < dataReader.FieldCount; i++)
                {
                    result[i].Add(dataReader[i] + "");
                }
            }
            dataReader.Close();
            var resultTrans = new List<string>[result[0].Count];
            for (var i = 0; i < result[0].Count; i++)
            {
                resultTrans[i] = new List<string>();
                foreach (var t in result)
                {
                    resultTrans[i].Add(t[i]);
                }
            }
            
            return resultTrans;
        }

        public void Dispose()
        {
            // 释放资源
            Connection.Close();
            //throw new NotImplementedException();
        }
    }

    public class SqlLocal
    {
        public string Database { get; set; }
        
        private SQLiteConnection Connection { get; set; }

        public void Connect()
        {
            var connectionString = "Data Source=" + Database + ";Version=3;";
            Console.WriteLine(connectionString);
            Connection = new SQLiteConnection(connectionString);
            Connection.Open();
        }

        public void CreateTable(string sql)
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public void Insert(string sql)
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public void Update(string sql)
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public List<string>[] Select(string sql)
        {
            var cmd = new SQLiteCommand(sql, Connection);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            var dataReader = cmd.ExecuteReader();
            if (dataReader == null)
            {
                return null;
            }
            var result = new List<string>[dataReader.FieldCount];
            for (var i = 0; i < dataReader.FieldCount; i++)
            {
                result[i] = new List<string>();
            }
            while (dataReader.Read())
            {
                for (var i = 0; i < dataReader.FieldCount; i++)
                {
                    try
                    {
                        result[i].Add(dataReader[i] + "");

                    }
                    catch (Exception e)
                    {
                        // Console.WriteLine(e);
                        // throw;
                        result[i].Add("");
                    }
                }
            }
            dataReader.Close();
            
            var resultTrans = new List<string>[result[0].Count];
            for (var i = 0; i < result[0].Count; i++)
            {
                resultTrans[i] = new List<string>();
                foreach (var t in result)
                {
                    resultTrans[i].Add(t[i]);
                }
            }
            return resultTrans;
        }

        public void Delete(string sql)
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
        
        public void Drop(string sql)
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            Connection.Close();
        }
        
    }
}
