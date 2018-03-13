using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cars
{
    class SQL
    {
        public List<string> GetStingListByQuery(string query)
        {
            List<string> ret = new List<string>();
            using (SqlConnection db = new SqlConnection(Properties.Settings.Default.SQLConnectionString))
            {
                SqlCommand SQLquery = new SqlCommand(query, db);
                SqlDataReader Reader;
                try
                {
                    db.Open();
                    Reader = SQLquery.ExecuteReader();
                    SqlDataAdapter adapter = new SqlDataAdapter(SQLquery);
                    DataTable table = new DataTable(string.Empty);
                    db.Close();
                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                        ret.Add((string)row.ItemArray[0]);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Database error: {e}");
                    return ret;
                }
            }
            return ret;
        }
    }
}
