using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Demo_TheTravelingSalesperson
{
    class JsonServices
    {
        string _dataFilePath;

        public JsonServices( string dataFilePath )
        {
            _dataFilePath = dataFilePath;
        }

        public void WriteJsonFile(Salesperson salesperson)
        {
            StreamWriter sWriter;

            try
            {
                sWriter = new StreamWriter(DataSettings.dataFilePathJson);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("You need more permissions to run the application. Try running it as an admin.");
                Console.WriteLine(ex);
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                File.Create(DataSettings.dataFilePathJson);
                sWriter = new StreamWriter(DataSettings.dataFilePathJson);
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("The path is too long.");
                throw;
            }

            using (sWriter)
            {
                sWriter.Write(JsonConvert.SerializeObject(salesperson, Formatting.Indented));
            }
        }

        public Salesperson ReadJsonFile()
        {
            Salesperson salesPerson = new Salesperson();
            StreamReader sReader = new StreamReader(DataSettings.dataFilePathJson);

            using (sReader)
            {
                salesPerson = JsonConvert.DeserializeObject<Salesperson>(sReader.ReadToEnd());
            }

            return salesPerson;
        }
    }
}
