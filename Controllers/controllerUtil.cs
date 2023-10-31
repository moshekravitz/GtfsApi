using GtfsApi.Entities;
using Microsoft.VisualBasic.FileIO;

namespace Catalog.Controllers;

static class Util
{
    public static List<Shapes> GetListFromCsv(IFormFile csvFile)
    {
        if (csvFile == null || csvFile.Length <= 0)
        {
            throw new Exception("File is empty");
        }

        try
        {
            List<Shapes> records = new List<Shapes>();

            using (var streamReader = new StreamReader(csvFile.OpenReadStream()))
            using (var parser = new TextFieldParser(streamReader))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // parser.ReadLine();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    // Skip header line 
                    if (parser.LineNumber <= 1 && fields[0] == "shape_id")
                    {
                        continue;
                    }

                    //if shape id is the same as last shape id, add coords to list
                    records.Add(new Shapes
                    {
                        ShapeId = int.Parse(fields[0]),
                        ShapeStr = fields[1]
                        // Set other properties as needed
                    });
                }
            }
            return records;

            // Now, you can use 'records' to create or process the Routes objects as needed.
        }
        catch (Exception ex)
        {
            throw new Exception("Error parsing CSV file", ex);
        }
    }
}
