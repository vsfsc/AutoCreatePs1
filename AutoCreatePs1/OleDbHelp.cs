using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace AutoCreatePs1
{
    public class OleDbHelp
    {
        private static string connString;

    // Methods
    private static void AttachParameters(OleDbCommand command, OleDbParameter[] commandParameters)
    {
        if (command == null)
        {
            throw new ArgumentNullException("command");
        }
        if (commandParameters != null)
        {
            foreach (OleDbParameter p in commandParameters)
            {
                if (p != null)
                {
                    if (((p.Direction == ParameterDirection.InputOutput) || (p.Direction == ParameterDirection.Input)) && (p.Value == null))
                    {
                        p.Value = DBNull.Value;
                    }
                    command.Parameters.Add(p);
                }
            }
        }
    }

    public static DataSet ExecuteDataset(string commandText, params OleDbParameter[] commandParameters)
    {
        DataSet ds;
        using (OleDbConnection sqlConn = new OleDbConnection(ConnectionString))
        {
            using (OleDbDataAdapter myDataAdapter = new OleDbDataAdapter())
            {
                OleDbCommand sqlCommand = new OleDbCommand(commandText, sqlConn);
                AttachParameters(sqlCommand, commandParameters);
                myDataAdapter.SelectCommand = sqlCommand;
                DataSet custDS = new DataSet();
                myDataAdapter.Fill(custDS);
                ds = custDS;
            }
        }
        return ds;
    }

    public static int ExecuteNonQuery(string commandText, params OleDbParameter[] commandParameters)
    {
        using (OleDbConnection sqlConn = new OleDbConnection(ConnectionString))
        {
            OleDbCommand sqlCommand = new OleDbCommand(commandText, sqlConn);
            AttachParameters(sqlCommand, commandParameters);
            sqlConn.Open();
            return sqlCommand.ExecuteNonQuery();
        }
    }

    public static long GetMaxID(string tableName, string fieldName)
    {
        string strSql = "select max(" + fieldName + ") from " + tableName;
        using (OleDbConnection connection = new OleDbConnection(ConnectionString))
        {
            OleDbCommand myCommand = new OleDbCommand(strSql, connection);
            connection.Open();
            return (long) myCommand.ExecuteScalar();
        }
    }

    public static int ImportCity(string desString, string srcString)
    {
        OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
        string mySelectQuery = "select top 1 *  from city";
        using (OleDbConnection sqlConn = new OleDbConnection(desString))
        {
            myDataAdapter.SelectCommand = new OleDbCommand(mySelectQuery, sqlConn);
            OleDbCommandBuilder custCB = new OleDbCommandBuilder(myDataAdapter);
            DataSet custDS = new DataSet();
            myDataAdapter.Fill(custDS);
            DataSet tmp = ExecuteDataset("select * from city", new OleDbParameter[0]);
            foreach (DataRow dr in tmp.Tables[0].Rows)
            {
                custDS.Tables[0].Rows.Add(dr.ItemArray);
            }
            return myDataAdapter.Update(custDS);
        }
    }

    // Properties
    public static string ConnectionString
    {
        get
        {
            return connString;
        }
        set
        {
            connString = value;
        }
    }

    }
}
