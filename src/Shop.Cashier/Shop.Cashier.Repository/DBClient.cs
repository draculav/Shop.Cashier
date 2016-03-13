using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
using System;
using System.Data;

namespace Shop.Cashier
{
    public class DBClient
    {
        public void TestTest()
        {
            DBClient client = new DBClient();
            client.CreateDB();
            string result = client.Test();

            Assert.IsNotNull(result);
        }

        public string GetConnection()
        {
            FbConnectionStringBuilder connBuilder = new FbConnectionStringBuilder();
            connBuilder.UserID = "aaa";//设置一个值，嵌入式版本并不验证用户名。
            connBuilder.ServerType = FbServerType.Embedded;//设置数据库类型为 嵌入式；
            connBuilder.Database = @"DB/test.pdb";//数据库文件的目录；
            connBuilder.Charset = "GB_2312";
            return connBuilder.ConnectionString;
        }

        public string Test()
        {
            string result = "";
            string connStr = GetConnection();
            using (FbConnection conn = new FbConnection(connStr))
            {
                conn.Open();

                string str = "SELECT * FROM TABLE1";
                FbCommand command = new FbCommand(str, conn);

                FbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result += reader.GetInt32(0) + ", " + reader.GetString(1) + ";";
                }

                conn.Close();
            }
            return result;
        }

        public void CreateDB()
        {
            string connStr = GetConnection();
            FbConnection.CreateDatabase(connStr);

            using (FbConnection conn = new FbConnection(connStr))
            {
                conn.Open();

                TryExecuteSql(conn, (c) =>
                {
                    string create = "CREATE TABLE TABLE1 (ID INTEGER, FIRSTNAME  VARCHAR(50), LASTNAME VARCHAR(50));";
                    ExecuteSql(create, c);
                });

                FbTransaction transaction = conn.BeginTransaction();
                TryExecuteSql(conn, transaction, (c, t) =>
                {
                    string insert = "INSERT INTO TABLE1 (ID, FIRSTNAME, LASTNAME) VALUES (1, 'John', 'Doe');";
                    ExecuteSql(insert, c, t);
                    insert = "INSERT INTO TABLE1 (ID, FIRSTNAME, LASTNAME) VALUES (2, 'Jane', 'Doe');";
                    ExecuteSql(insert, c, t);
                });

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

        }

        public void ExecuteScript(string scriptPath)
        {
            FbScript script = new FbScript(scriptPath);
            script.Parse();

            // execute the SQL script  
            string connStr = GetConnection();
            using (FbConnection c = new FbConnection(connStr))
            {
                FbBatchExecution fbe = new FbBatchExecution(c);
                foreach (string cmd in script.Results)
                {
                    fbe.SqlStatements.Add(cmd);
                }
                fbe.Execute();
            }
        }

        public void ExecuteSql(string sql, FbConnection conn)
        {
            FbCommand cmd = new FbCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        public void ExecuteSql(string sql, FbConnection conn, FbTransaction transaction)
        {
            FbCommand cmd = new FbCommand(sql, conn, transaction);
            cmd.ExecuteNonQuery();
        }

        public void TryExecuteSql(FbConnection conn, Action<FbConnection> action)
        {
            try
            {
                action(conn);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                //异常直接关闭
                conn.Close();
            }
        }

        public void TryExecuteSql(FbConnection conn, FbTransaction transaction, Action<FbConnection, FbTransaction> action)
        {
            try
            {
                action(conn, transaction);
                transaction.Commit();
            }
            catch (Exception ee)
            {
                transaction.Rollback();
                conn.Close();
                Console.WriteLine(ee.Message);
            }
        }

        public void TryExecuteSql(Action<FbConnection> action)
        {
            string connStr = GetConnection();
            FbConnection conn = new FbConnection(connStr);
            try
            {
                conn.Open();
                action(conn);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}
