namespace AirportTicketBookingSystem.Utilities;

public static class CSVFileReader
{
    /// <summary>
    /// Reads a CSV file and return list of dictionaries 
    /// that contains celss of each row in the file.
    /// 
    /// first row must be the headings of columns.
    /// and all rows must have same number of columns.
    /// </summary>
    public static List<Dictionary<string, string>> ReadDataRows(string filepath)
    {
        var rows = new List<Dictionary<string, string>>();

        using (var reader = new StreamReader(filepath))
        {
            var headerLine = reader.ReadLine();
            if (string.IsNullOrEmpty(headerLine))
            {
                throw new InvalidOperationException("CSV file is empty or missing a header row.");
            }

            var fields = headerLine.Split(',');

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;

                var values = line.Split(',');

                if (values.Length != fields.Length)
                {
                    throw new InvalidOperationException("Row contains a different number of columns than the header.");
                }

                var row = new Dictionary<string, string>();
                for (int column = 0; column < fields.Length; column++)
                {
                    row[fields[column]] = values[column];
                }

                rows.Add(row);
            }
        }

        return rows;
    }
}
