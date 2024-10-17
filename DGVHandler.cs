using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook
{
    internal class SqlHandler
    {
        String table_name;
        public SqlHandler(String table_name) { this.table_name = table_name; }

        //
        // Summary:
        //     Changes The Data Source Of A DataGridView Object
        //     To A Subset Of Records Based On A Query
        //
        // Parameters:
        //   query:
        //     String Containing The SQL Query
        //   data_grid:
        //     The DataGridView Object To Be Queried
        //   conn:
        //     The SQL Server Connection
        public void ChangeDataSource(String query, DataGridView data_grid, SqlConnection conn)
        {
            DataSet data_set = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            {
                adapter.Fill(data_set);
                data_grid.DataSource = data_set.Tables[0];
            }
        }

        //
        // Summary:
        //     Displays All Available Records in Table
        //
        // Parameters:
        //    data_grid:
        //      The DataGridView Object To Be Queried
        public void RefreshDataTable(DataGridView data_grid, SqlConnection connection)
        {
            this.ChangeDataSource($"SELECT * FROM {this.table_name}", data_grid, connection);
        }

        //
        // Summary:
        //     Executes An SQL Command Upon A 
        //     DataGridView Object And Refreshes Grid
        //
        // Parameters:
        //   command:
        //     String Containing The SQL Command
        //   conn:
        //     The SQL Server Connection
        //   data_grid:
        //     The DataGridView Object To Be Queried
        public void ExecuteAndRefresh(String command, SqlConnection conn, DataGridView data_grid)
        {
            new SqlCommand(command, conn).ExecuteReader().Close();
            this.RefreshDataTable(data_grid, conn);
        }

        //
        // Summary:
        //     Checks If A Person Exists In A 
        //     DataGridView Object
        //
        // Parameters:
        //   data_grid:
        //     The DataGridView Object To Be Queried
        //   conn:
        //     The SQL Server Connection
        //   inputs:
        //     Dictionary Where The Keys Are The Field Names
        //     And The Values Are The Field Values
        public bool RecordExists(DataGridView data_grid, SqlConnection conn, Dictionary<String, String> inputs)
        {
            String query = $"SELECT * FROM {table_name} WHERE ";

            for (int i = 0; i < inputs.Count; i++)
            {
                String key = inputs.ElementAt(i).Key;
                String value = inputs.ElementAt(i).Value;

                query += $"{key} = '{value}' AND ";
            }

            query = query.Trim();
            query = query.Remove(query.LastIndexOf(' '));

            try
            {
                DataSet data_set = new DataSet();
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    adapter.Fill(data_set);
                    DataRow _ = data_set.Tables[0].Rows[0];

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
