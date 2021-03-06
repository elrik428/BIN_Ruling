﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;  
using System.Data.SqlClient;

namespace routingrules_loadbalances
{
    public class Connection_Query
    {
        StringBuilder errorMessages = new StringBuilder();
        SqlConnection zacrptConn;
        // Connection to DB
         public void connString(string connDatabseSrc, string connDatabsName, string query_ToRun)
         {
         SqlConnectionStringBuilder sqConString = new SqlConnectionStringBuilder();
         sqConString.DataSource = connDatabseSrc; 
         sqConString.IntegratedSecurity = true;
         sqConString.InitialCatalog = connDatabsName;
         sqConString.ConnectTimeout = 0;

         zacrptConn = new SqlConnection(sqConString.ConnectionString);
         try
         {
             zacrptConn.Open();
         }
         catch (SqlException ex)
         {
             for (int i = 0; i < ex.Errors.Count; i++ )
             { 
                 errorMessages.Append("Index #" + i + "\n" +
                         "Message: " + ex.Errors[i].Message + "\n" +
                         "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                         "Source: " + ex.Errors[i].Source + "\n" +
                         "Procedure: " + ex.Errors[i].Procedure + "\n");
             }
         }
              Console.WriteLine(errorMessages.ToString());
         }
         
        // Create and run SQL query
        public void sqlRuncommand(string query_ToRun)
        {
         SqlCommand sqlCommnd = new SqlCommand(query_ToRun, zacrptConn);
         sqlCommnd.Connection = zacrptConn;
         sqlCommnd.CommandTimeout = 0;
         //sqlCommnd.CommandText = query_ToRun;
                   
          try
          {
              sqlCommnd.ExecuteNonQuery();
          }
          catch(SqlException ex)
          {
                for (int i = 0; i < ex.Errors.Count; i++ )
                {
                  errorMessages.Append("Message");
                }
           Console.WriteLine(errorMessages.ToString());
          }
        }

        // Create and run SQL query -- COUNT
        public  int sqlRunCount(string query_ToRun2)
        {
            int returnval=0;
            SqlCommand sqlCount = new SqlCommand();
            sqlCount.Connection = zacrptConn;
            sqlCount.CommandTimeout = 0;
            sqlCount.CommandText = query_ToRun2;

            try
            {
                //Int32 count = (Int32)sqlCount.ExecuteScalar();
                returnval = Convert.ToInt32(sqlCount.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Message");
                }
                Console.WriteLine(errorMessages.ToString());
            }
            return returnval;    
        }
                
        internal int sqlRunCount()
        {
            throw new NotImplementedException();
        }
    }
    
}

