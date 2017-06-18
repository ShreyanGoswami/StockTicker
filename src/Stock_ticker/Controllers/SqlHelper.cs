//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Stock_ticker.Controllers
//{
//    public class SqlHelper
//    {
//        private string mstr_ConnectionString;
//        private SqlConnection mobj_SqlConnection;
//        private SqlCommand mobj_SqlCommand;
//        private int mint_CommandTimeout = 30;

//        public enum ExpectedType
//        {

//            StringType = 0,
//            NumberType = 1,
//            DateType = 2,
//            BooleanType = 3,
//            ImageType = 4
//        }

//        public SqlHelper()
//        {
//            try
//            {

//                mstr_ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = User; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

//                mobj_SqlConnection = new SqlConnection(mstr_ConnectionString);
//                mobj_SqlCommand = new SqlCommand();
//                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
//                mobj_SqlCommand.Connection = mobj_SqlConnection;

//                //ParseConnectionString();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Error initializing data class." + Environment.NewLine + ex.Message);
//            }
//        }

//        public void Dispose()
//        {
//            try
//            {
//                //Clean Up Connection Object
//                if (mobj_SqlConnection != null)
//                {
//                    if (mobj_SqlConnection.State != ConnectionState.Closed)
//                    {
//                        mobj_SqlConnection.Close();
//                    }
//                    mobj_SqlConnection.Dispose();
//                }

//                //Clean Up Command Object
//                if (mobj_SqlCommand != null)
//                {
//                    mobj_SqlCommand.Dispose();
//                }

//            }

//            catch (Exception ex)
//            {
//                throw new Exception("Error disposing data class." + Environment.NewLine + ex.Message);
//            }

//        }

//        public void CloseConnection()
//        {
//            if (mobj_SqlConnection.State != ConnectionState.Closed) mobj_SqlConnection.Close();
//        }
//        public int GetExecuteScalarByCommand(string Command)
//        {

//            object identity = 0;
//            try
//            {
//                mobj_SqlCommand.CommandText = Command;
//                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
//                mobj_SqlCommand.CommandType = CommandType.StoredProcedure;

//                mobj_SqlConnection.Open();

//                mobj_SqlCommand.Connection = mobj_SqlConnection;
//                identity = mobj_SqlCommand.ExecuteScalar();
//                CloseConnection();
//            }
//            catch (Exception ex)
//            {
//                CloseConnection();
//                throw ex;
//            }
//            return Convert.ToInt32(identity);
//        }

//        public void GetExecuteNonQueryByCommand(string Command)
//        {
//            try
//            {
//                mobj_SqlCommand.CommandText = Command;
//                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
//                mobj_SqlCommand.CommandType = CommandType.StoredProcedure;

//                mobj_SqlConnection.Open();

//                mobj_SqlCommand.Connection = mobj_SqlConnection;
//                mobj_SqlCommand.ExecuteNonQuery();

//                CloseConnection();
//            }
//            catch (Exception ex)
//            {
//                CloseConnection();
//                throw ex;
//            }
//        }

//        public DataTable GetDatasetByCommand(string Command)
//        {
//            try
//            {
//                mobj_SqlCommand.CommandText = Command;
//                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;
//                mobj_SqlCommand.CommandType = CommandType.StoredProcedure;

//                mobj_SqlConnection.Open();

//                SqlDataAdapter adpt = new SqlDataAdapter(mobj_SqlCommand);
//                DataTable ds = new DataTable();
//                adpt.Fill(ds);
//                return ds;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                CloseConnection();
//            }
//        }

//        public static DataTable ExecuteDataTableSqlDA(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
//        {
//            System.Data.DataTable dt = new DataTable();
//            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmdText, conn);
//            da.Fill(dt);
//            return dt;
//        }

//        public SqlDataReader GetReaderBySQL(string strSQL)
//        {
//            mobj_SqlConnection.Open();
//            try
//            {
//                SqlCommand myCommand = new SqlCommand(strSQL, mobj_SqlConnection);
//                return myCommand.ExecuteReader();
//            }
//            catch (Exception ex)
//            {
//                CloseConnection();
//                throw ex;
//            }
//        }

//        public SqlDataReader GetReaderByCmd(string Command)
//        {
//            SqlDataReader objSqlDataReader = null;
//            try
//            {
//                mobj_SqlCommand.CommandText = Command;
//                mobj_SqlCommand.CommandType = CommandType.StoredProcedure;
//                mobj_SqlCommand.CommandTimeout = mint_CommandTimeout;

//                mobj_SqlConnection.Open();
//                mobj_SqlCommand.Connection = mobj_SqlConnection;

//                objSqlDataReader = mobj_SqlCommand.ExecuteReader();
//                return objSqlDataReader;
//            }
//            catch (Exception ex)
//            {
//                CloseConnection();
//                throw ex;
//            }

//        }

//        public void AddParameterToSQLCommand(string ParameterName, SqlDbType ParameterType)
//        {
//            try
//            {
//                mobj_SqlCommand.Parameters.Add(new SqlParameter(ParameterName, ParameterType));
//            }

//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        public void AddParameterToSQLCommand(string ParameterName, SqlDbType ParameterType, int ParameterSize)
//        {
//            try
//            {
//                mobj_SqlCommand.Parameters.Add(new SqlParameter(ParameterName, ParameterType, ParameterSize));
//            }

//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        public void SetSQLCommandParameterValue(string ParameterName, object Value)
//        {
//            try
//            {
//                mobj_SqlCommand.Parameters[ParameterName].Value = Value;
//            }

//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//    }
//}
