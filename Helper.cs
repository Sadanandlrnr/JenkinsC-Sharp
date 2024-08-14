using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Single_Click
{
    internal static class Helper
    {

        public static Dictionary<string, string> ConvertToDictionary(DataGridView dataGridView)
        {
            var dictionary = new Dictionary<string, string>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Ensure the row is not a new row (the last empty row in DataGridView)
                if (!row.IsNewRow)
                {
                    var key = row.Cells[0].Value?.ToString();
                    var value = row.Cells[1].Value?.ToString();

                    // Only add non-null keys to the dictionary
                    if (!string.IsNullOrEmpty(key))
                    {
                        dictionary[key] = value; // Add or update the dictionary
                    }
                }
            }

            return dictionary;
        }
    }
}
