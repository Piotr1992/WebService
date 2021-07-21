using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Helpers
{
    public class DBHelper
    {
        public static DataTable RunSqlQuery(string query, string tableName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable(tableName);

                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }

        public static DataTable RunSqlQueryParam(string query, string tableName, string paramName, int paraValue)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue(paramName, paraValue);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(tableName);

                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }
        public static DataTable RunSqlProcParam(string procName, string tableName, string paramName, int paraValue)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(paramName, paraValue);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(tableName);

                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }
        public static DataTable RunSqlQueryParam(string query, string tableName, List<QueryParam> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    BuildParameters(cmd, parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(tableName);

                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }
        public static DataTable RunSqlProcParam(string procName, string tableName, List<QueryParam> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    BuildParameters(cmd, parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(tableName);

                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }
        public static DataTable RunSqlProcParam(string procName, string tableName, List<SqlParameter> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = conn, CommandText = procName, CommandType = CommandType.StoredProcedure
                    };

                    BuildParameters(cmd, parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(tableName);

                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }
        public static object RunScalarSqlProcParam(string procName, List<QueryParam> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    BuildParameters(cmd, parameters);

                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }
        public static bool ExecSqlProcParam(string procName, string tableName, List<QueryParam> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    BuildParameters(cmd, parameters);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return false;
            }
        }

        private static void BuildParameters(SqlCommand cmd, List<QueryParam> parameters)
        {
            if (parameters != null)
            {
                foreach (QueryParam par in parameters)
                {
                    cmd.Parameters.AddWithValue(par.Name, par.Value);
                }
            }
        }
        private static void BuildParameters(SqlCommand cmd, List<SqlParameter> parameters)
        {
            if (parameters != null)
            {
                foreach (SqlParameter par in parameters)
                {
                    cmd.Parameters.Add(par);
                }
            }
        }
        public static DataTable RunSqlProc(string procName, string tableName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procName;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(tableName);
                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }

        public static object RunScalarSqlQueryParam(string query, List<QueryParam> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    BuildParameters(cmd, parameters);
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return null;
            }
        }
        public static string WebNumer(int kntid, string numer)
        {
            try
            {
                if (kntid != 0 && !string.IsNullOrEmpty(numer))
                {
                    List<QueryParam> lstPar = new List<QueryParam>();
                    lstPar.Add(new QueryParam("@kntid", kntid));
                    lstPar.Add(new QueryParam("@numer", numer));

                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                    {
                        conn.Open();
                        string query = @"select count(*) from cdn.kntkarty where knt_gidnumer=@kntid and knt_gidnumer<>0";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        BuildParameters(cmd, lstPar);
                        object ob = cmd.ExecuteScalar();
                        if (Convert.ToInt32(ob) > 0)
                        {
                            cmd = new SqlCommand(query, conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "b2b_NumerKontrahentaWeb";
                            lstPar = new List<QueryParam>();
                            lstPar.Add(new QueryParam("@kntid", kntid));
                            lstPar.Add(new QueryParam("@numer", numer));
                            BuildParameters(cmd, lstPar);
                            cmd.ExecuteScalar();
                            Logger.Logger.LogDebug(string.Format("dodano atrybut kontrahenta {0}", kntid));
                            return ResponseSerializer.Serialize("ok"); //atrybut dodany lub zaktualizowany
                        }
                        else
                        {
                            return ResponseSerializer.Serialize("błędny kontrahent");
                        }
                    }
                }
                else
                {
                    return ResponseSerializer.Serialize("błędne parametry");
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return ResponseSerializer.Serialize((int)ErrorType.unknown);
            }
        }

        public static string VerifyUser(string login, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
                {

                    List<QueryParam> lstPar = new List<QueryParam>();
                    lstPar.Add(new QueryParam("@KnS_HasloOsoby", Coder.Decrypt(password)));
                    lstPar.Add(new QueryParam("@KnS_EMail", login));
                    string query = @"select count(*) from [dbo].[b2b_operatorzy]() where KnS_EMail=@KnS_EMail;select *
FROM [dbo].[b2b_operatorzy] () where KnS_EMail=@KnS_EMail and KnS_HasloOsoby=@KnS_HasloOsoby COLLATE SQL_Latin1_General_CP1_CS_AS";
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);

                        BuildParameters(cmd, lstPar);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
                        {
                            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                            {
                                ds.Tables[1].TableName = "Operator";
                                ds.Tables[1].Columns.Remove("KnS_HasloOsoby");
                                return DataTableHelper.GetJson(ds.Tables[1]);
                            }
                            else
                            {
                                return ResponseSerializer.Serialize((int)ErrorType.incorrectPassword);
                            }
                        }
                        else
                        {
                            return ResponseSerializer.Serialize((int)ErrorType.incorrectLogin);
                        }
                    }
                }
                else
                {
                    return ResponseSerializer.Serialize((int)ErrorType.incorrectParameters);
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return ResponseSerializer.Serialize((int)ErrorType.unknown);
            }
        }

        public static string SetNewUserPassword(string login,string new_password)
        {
            try
            {
                if (!string.IsNullOrEmpty(login))
                {
                    List<QueryParam> lstPar = new List<QueryParam>();
                    lstPar.Add(new QueryParam("@KnS_EMail", login));

                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString))
                    {
                        conn.Open();
                        string query = @"select count(*) from [dbo].[b2b_operatorzy]() where KnS_EMail=@KnS_EMail";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        BuildParameters(cmd, lstPar);
                        object ob = cmd.ExecuteScalar();
                        if (Convert.ToInt32(ob) > 0)
                        {
                            string password = string.IsNullOrEmpty(new_password)? PasswordGenerator.CreateRandomPassword(8): new_password;
                            cmd = new SqlCommand(query, conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "b2b_UpdatePassword";
                            lstPar = new List<QueryParam>();
                            lstPar.Add(new QueryParam("@KnS_EMail", login));
                            lstPar.Add(new QueryParam("@KnS_HasloOsoby", password));
                            BuildParameters(cmd, lstPar);
                            cmd.ExecuteScalar();
                            return ResponseSerializer.Serialize(password);
                        }
                        else
                        {
                            return ResponseSerializer.Serialize((int)ErrorType.incorrectLogin);
                        }
                    }
                }
                else
                {
                    return ResponseSerializer.Serialize((int)ErrorType.incorrectParameters);
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.LogException(ex);
                return ResponseSerializer.Serialize((int)ErrorType.unknown);
            }
        }
        public static string GetNumerDokumentuTRN(int gidnumer)
        {
            string query = @"select 
            CDN.NumerDokumentu ( CDN.DokMapTypDokumentu (ZaN_GIDTyp,ZaN_ZamTyp, ZaN_Rodzaj),0,0,ZaN_ZamNumer,ZaN_ZamRok,ZaN_ZamSeria,zan_zammiesiac)
                from cdn.ZamNag where ZaN_GIDNumer=@ZaN_GIDNumer";
            List<QueryParam> lst = new List<QueryParam>();
            lst.Add(new QueryParam("@ZaN_GIDNumer", gidnumer));
            return Convert.ToString(RunScalarSqlQueryParam(query, lst));
        }
        public static string GetSeriaMagazynu(int gidnumer)
        {
            string query = @"select [dbo].[bmp_b2b_SeriaMagazynu] (@gidnumer)";
            List<QueryParam> lst = new List<QueryParam>();
            lst.Add(new QueryParam("@gidnumer", gidnumer));
            return Convert.ToString(RunScalarSqlQueryParam(query, lst));
        }
        public static int GetPayerNumberFromPaymentForm(int formgid)
        {   
            int r;
            try
            {
                string query =  @"select knt_gidnumer from dbo.bmp_b2b_platniFormyPlatnosci where formaPlGid=@formgid";
                List<QueryParam> lst = new List<QueryParam>();
                lst.Add(new QueryParam("@formgid", formgid));
                r = Convert.ToInt32(RunScalarSqlQueryParam(query, lst));
            }
            catch
            {
                r=0;
            }
            return r;
        }
    }
}