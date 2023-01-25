using System;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Configuration;
using MOON.Entities.Dashboard;
using System.Collections.Generic;

namespace MOON.DAO.Common
{
    public class DbConnection
    {

        /// <summary>
        /// Defines the conStr.
        /// </summary>
        public static string conStr = String.Empty;

        /// <summary>
        /// Defines the conn.
        /// </summary>
        public static SqlConnection conn = new SqlConnection();

        /// <summary>
        /// Defines the sqlCmd.
        /// </summary>
        public static SqlCommand sqlCmd = new SqlCommand();

        /// <summary>
        /// Defines the adapter.
        /// </summary>
        public static SqlDataAdapter adapter = new SqlDataAdapter();

        /// <summary>
        /// The GetConnection.
        /// </summary>
        private static string GetConnection(string id = "DefaultConnection")
        {
            string connectionString = ConfigurationManager.ConnectionStrings[id].ConnectionString;

            return connectionString;
        }

        /// <summary>
        /// The GetPrivateProfileString.
        /// </summary>
        /// <param name="Section">The Section<see cref="string"/>.</param>
        /// <param name="Key">The Key<see cref="string"/>.</param>
        /// <param name="Default">The Default<see cref="string"/>.</param>
        /// <param name="RetVal">The RetVal<see cref="StringBuilder"/>.</param>
        /// <param name="Size">The Size<see cref="int"/>.</param>
        /// <param name="FilePath">The FilePath<see cref="string"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        internal static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        /// <summary>
        /// The GetIniString.
        /// </summary>
        /// <param name="Section">The Section<see cref="String"/>.</param>
        /// <param name="Keyname">The Keyname<see cref="String"/>.</param>
        /// <param name="IniFile">The IniFile<see cref="String"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetIniString(String Section, String Keyname, String IniFile)
        {
            StringBuilder iniString = new StringBuilder(255);
            GetPrivateProfileString(Section, Keyname, "", iniString, iniString.Capacity, IniFile);
            return iniString.ToString();
        }

        /// <summary>
        /// The ExecuteDataTable.
        /// </summary>
        /// <param name="TypeOfCommand">The TypeOfCommand<see cref="CommandType"/>.</param>
        /// <param name="CmdText">The CmdText<see cref="string"/>.</param>
        /// <returns>The <see cref="DataTable"/>.</returns>
        public DataTable ExecuteDataTable(
                                   CommandType TypeOfCommand,
                                   string CmdText
                               )
        {
            DataSet dtSet = new DataSet();
            conStr = GetConnection();
            using (SqlConnection sqlConn = new SqlConnection(conStr))
            {
                try
                {
                    if (sqlConn.State != ConnectionState.Open)
                    {
                        sqlConn.Open();
                    }
                    sqlCmd.Connection = sqlConn;
                    sqlCmd.CommandText = CmdText;
                    sqlCmd.CommandType = TypeOfCommand;
                    sqlCmd.CommandTimeout = 200;

                    adapter = new SqlDataAdapter(CmdText, conn);
                    adapter.SelectCommand = sqlCmd;
                    dtSet.Reset();
                    adapter.Fill(dtSet);
                    sqlCmd.Connection.Close();
                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                }
                finally
                {
                    sqlConn.Close();
                }
            }
            return dtSet.Tables[0];
        }

        /// <summary>
        /// The ExecuteScalar.
        /// </summary>
        /// <param name="TypeOfCommand">The TypeOfCommand<see cref="CommandType"/>.</param>
        /// <param name="CmdText">The CmdText<see cref="string"/>.</param>
        /// <param name="NpgSqlParams">The NpgSqlParams<see cref="NpgsqlParameter[]"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object ExecuteScalar(
                                   CommandType TypeOfCommand,
                                   string CmdText,
                                   SqlParameter[] SqlParams
                               )
        {
            //Create temp object.
            object objTemp = null;
            conStr = GetConnection();
            using (SqlConnection sqlConn = new SqlConnection(conStr))
            {
                try
                {
                    sqlCmd.Connection = sqlConn;
                    sqlCmd.CommandType = TypeOfCommand;
                    sqlCmd.CommandText = CmdText;
                    sqlCmd.CommandTimeout = 200;

                    if (SqlParams != null)
                    {
                        foreach (SqlParameter SqlParam in SqlParams)
                        {
                            SqlParam.Value = (SqlParam.Value == null || SqlParam.Value.ToString() == String.Empty ?
                            DBNull.Value : (object)SqlParam.Value);
                            sqlCmd.Parameters.Add(SqlParam);
                        }
                    }
                    if (sqlConn.State != ConnectionState.Open)
                    {
                        sqlCmd.Connection.Open();
                    }
                    //Execute and sets object returned.
                    objTemp = sqlCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                }
                finally
                {
                    sqlCmd.Connection.Close();
                    sqlCmd.Parameters.Clear();
                }
            }
            // Return from function
            return objTemp;
        }

        /// <summary>
        /// The ExecuteNonQuery.
        /// </summary>
        /// <param name="TypeOfCommand">The TypeOfCommand<see cref="CommandType"/>.</param>
        /// <param name="CmdText">The CmdText<see cref="String"/>.</param>
        /// <param name="NpgSqlParams">The NpgSqlParams<see cref="NpgsqlParameter[]"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public bool ExecuteNonQuery(
                                CommandType TypeOfCommand,
                                String CmdText,
                                SqlParameter[] SqlParams
                                )
        {
            int affectedRow = 0;
            conStr = GetConnection();
            using (SqlConnection sqlConn = new SqlConnection(conStr))
            {
                try
                {
                    if (sqlConn.State != ConnectionState.Open)
                    {
                        sqlConn.Open();
                    }
                    sqlCmd.Connection = sqlConn;
                    sqlCmd.CommandText = CmdText;
                    sqlCmd.CommandType = TypeOfCommand;
                    sqlCmd.CommandTimeout = 200;
                    if (SqlParams != null)
                    {
                        foreach (SqlParameter SqlParam in SqlParams)
                        {
                            SqlParam.Value = (SqlParam.Value == null || SqlParam.Value.ToString() == String.Empty ?
                            DBNull.Value : (object)SqlParam.Value);
                            sqlCmd.Parameters.Add(SqlParam);
                        }
                    }
                    affectedRow = sqlCmd.ExecuteNonQuery();
                    if (sqlConn.State != ConnectionState.Open)
                    {
                        sqlCmd.Connection.Open();
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                }
                finally
                {
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Connection.Close();
                }
            }
            if (affectedRow > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The ExecuteDataSet.
        /// </summary>
        /// <param name="TypeOfCommand">The TypeOfCommand<see cref="CommandType"/>.</param>
        /// <param name="CmdText">The CmdText<see cref="String"/>.</param>
        /// <returns>The <see cref="DataSet"/>.</returns>
        public DataSet ExecuteDataSet(
                                        CommandType TypeOfCommand,
                                        String CmdText
                                    )
        {
            conStr = GetConnection();
            DataSet dsSet = new DataSet();
            using (SqlConnection sqlConn = new SqlConnection(conStr))
            {
                try
                {
                    // Execute the command
                    sqlCmd.Connection = sqlConn;
                    sqlCmd.CommandText = CmdText;
                    sqlCmd.CommandType = TypeOfCommand;
                    sqlCmd.CommandTimeout = 200;
                    if (sqlConn.State != ConnectionState.Open)
                    {
                        sqlCmd.Connection.Open();
                    }
                    adapter.SelectCommand = sqlCmd;
                    adapter.Fill(dsSet);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                }
                finally
                {
                    sqlCmd.Connection.Close();
                }
            }
            // Return from function
            return dsSet;
        }

        /// <summary>
        /// ExecuteQuery
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public bool ExecuteQuery(string strQuery, string conStr)
        {
            bool bstatus = false;
            try
            {
                SqlConnection m_sqlConnection = new SqlConnection(conStr);
                int snReturnValue = 0;
                try
                {
                    if (m_sqlConnection.State != ConnectionState.Open)
                        m_sqlConnection.Open();
                    if (m_sqlConnection.State == ConnectionState.Open)
                    {
                        SqlCommand objSqlCommand = new SqlCommand(strQuery, m_sqlConnection);

                        snReturnValue = objSqlCommand.ExecuteNonQuery();
                        if (snReturnValue == -1)
                            bstatus = false;
                        else
                            bstatus = true;
                    }
                    if (m_sqlConnection.State == ConnectionState.Open)
                        m_sqlConnection.Close();

                    if (m_sqlConnection != null)
                        m_sqlConnection.Dispose();
                }
                catch (Exception ex)
                {
                    bstatus = false;
                    if (m_sqlConnection.State == ConnectionState.Open)
                        m_sqlConnection.Close();

                    if (m_sqlConnection != null)
                        m_sqlConnection.Dispose();
                }
            }
            catch (Exception ex)
            { }
            return bstatus;
        }

        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="TypeOfCommand"></param>
        /// <param name="CmdText"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(
                                        CommandType TypeOfCommand,
                                        String CmdText,
                                        string conStr
                                    )
        {
            DataSet dsSet = new DataSet();
            using (SqlConnection sqlConn = new SqlConnection(conStr))
            {
                try
                {
                    sqlCmd.Connection = sqlConn;
                    sqlCmd.CommandText = CmdText;
                    sqlCmd.CommandType = TypeOfCommand;
                    sqlCmd.CommandTimeout = 200;
                    if (sqlConn.State != ConnectionState.Open)
                    {
                        sqlCmd.Connection.Open();
                    }
                    adapter.SelectCommand = sqlCmd;
                    adapter.Fill(dsSet);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                }
                finally
                {
                    sqlCmd.Connection.Close();
                }
            }
            // Return from function
            return dsSet;
        }



       
        public List<CategoryEntity> GetAllCategory(CommandType TypeOfCommand,
                                   string CmdText)
        {
            List<CategoryEntity> category = new List<CategoryEntity>();
            conStr = GetConnection();
            SqlConnection sqlConn = new SqlConnection(conStr);
            try
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();
                }

                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = CmdText;
                sqlCmd.CommandType = TypeOfCommand;
                sqlCmd.CommandTimeout = 200;

                //sqlConn.Open();
                SqlCommand cmd = new SqlCommand(CmdText, conn);
                cmd.Connection = sqlConn;
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    category.Add(new CategoryEntity()
                    {
                        CategoryId = Convert.ToInt32(rdr["CategoryId"]),
                        Name = rdr["Name"].ToString(),
                        Slug = rdr["Slug"].ToString(),
                    }); ;
                }
                rdr.Close();
                sqlConn.Close();
                cmd.Connection = sqlConn;
                return category;
            }

            catch (Exception)
            {
                return null;
            }
        }


        public List<ArticleEntity> GetAllArticle(CommandType TypeOfCommand,
                                  string CmdText)
        {
            List<ArticleEntity> article = new List<ArticleEntity>();
            conStr = GetConnection();
            SqlConnection sqlConn = new SqlConnection(conStr);
            try
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();
                }

                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = CmdText;
                sqlCmd.CommandType = TypeOfCommand;
                sqlCmd.CommandTimeout = 200;

                //sqlConn.Open();
                SqlCommand cmd = new SqlCommand(CmdText, conn);
                cmd.Connection = sqlConn;
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    article.Add(new ArticleEntity()
                    {
                        ArticleId = Convert.ToInt32(rdr["ArticleId"]),
                        UserId = Convert.ToInt32(rdr["UserId"]),
                        CategroyId = Convert.ToInt32(rdr["CategoryId"]),
                        Title = rdr["Title"].ToString(),
                        Slug = rdr["Slug"].ToString(),
                        Description = rdr["Description"].ToString(),
                        Excerpt = rdr["Excerpt"].ToString(),
                        Thumbnail = rdr["Thumbnail"].ToString(),
                        Profile = rdr["Profile"].ToString(),
                        UserName = rdr["Username"].ToString(),
                        Status = rdr["Status"].ToString(),
                        CategoryName = rdr["Name"].ToString(),
                    }); ;
                }
                rdr.Close();
                sqlConn.Close();
                cmd.Connection = sqlConn;
                return article;
            }

            catch (Exception)
            {
                return null;
            }
        }



        public List<ArticleEntity> GetAllTrending(CommandType TypeOfCommand,
                                 string CmdText)
        {
            List<ArticleEntity> article = new List<ArticleEntity>();
            conStr = GetConnection();
            SqlConnection sqlConn = new SqlConnection(conStr);
            try
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();
                }

                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = CmdText;
                sqlCmd.CommandType = TypeOfCommand;
                sqlCmd.CommandTimeout = 200;

                //sqlConn.Open();
                SqlCommand cmd = new SqlCommand(CmdText, conn);
                cmd.Connection = sqlConn;
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    article.Add(new ArticleEntity()
                    {
                        ArticleId = Convert.ToInt32(rdr["ArticleId"]),
                        UserId = Convert.ToInt32(rdr["UserId"]),
                        CategroyId = Convert.ToInt32(rdr["CategoryId"]),
                        Title = rdr["Title"].ToString(),
                        Slug = rdr["Slug"].ToString(),
                        Description = rdr["Description"].ToString(),
                        Excerpt = rdr["Excerpt"].ToString(),
                        Thumbnail = rdr["Thumbnail"].ToString(),
                        Status = rdr["Status"].ToString(),
                    }); ;
                }
                rdr.Close();
                sqlConn.Close();
                cmd.Connection = sqlConn;
                return article;
            }

            catch (Exception)
            {
                return null;
            }
        }




        public List<PhotoEntity> GetAllImage(CommandType TypeOfCommand,
                                  string CmdText)
        {
            List<PhotoEntity> photo = new List<PhotoEntity>();
            conStr = GetConnection();
            SqlConnection sqlConn = new SqlConnection(conStr);
            try
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();
                }

                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = CmdText;
                sqlCmd.CommandType = TypeOfCommand;
                sqlCmd.CommandTimeout = 200;

                //sqlConn.Open();
                SqlCommand cmd = new SqlCommand(CmdText, conn);
                cmd.Connection = sqlConn;
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    photo.Add(new PhotoEntity()
                    {
                        PhotoId = Convert.ToInt32(rdr["PhotoId"]),
                        ArticleId = Convert.ToInt32(rdr["ArticleId"]),
                        PhotoImage = rdr["PhotoName"].ToString(),
                    });
                }
                rdr.Close();
                sqlConn.Close();
                cmd.Connection = sqlConn;
                return photo;
            }

            catch (Exception)
            {
                return null;
            }
        }


        public List<PhotoEntity> GetImageBySlug(CommandType TypeOfCommand,
                                 string CmdText)
        {
            List<PhotoEntity> photo = new List<PhotoEntity>();
            conStr = GetConnection();
            SqlConnection sqlConn = new SqlConnection(conStr);
            try
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();
                }

                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = CmdText;
                sqlCmd.CommandType = TypeOfCommand;
                sqlCmd.CommandTimeout = 200;

                //sqlConn.Open();
                SqlCommand cmd = new SqlCommand(CmdText, conn);
                cmd.Connection = sqlConn;
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    photo.Add(new PhotoEntity()
                    {
                        PhotoImage = rdr["PhotoName"].ToString(),
                    });
                }
                rdr.Close();
                sqlConn.Close();
                cmd.Connection = sqlConn;
                return photo;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}
