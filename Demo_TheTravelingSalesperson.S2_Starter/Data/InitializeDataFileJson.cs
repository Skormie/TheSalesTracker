using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Demo_TheTravelingSalesperson
{
    public class InitializeDataFileJson
    {

        public static void WriteJsonFile( Salesperson salesperson )
        {
            StreamWriter sWriter = new StreamWriter(DataSettings.dataFilePathJson);

            using (sWriter)
            {
                sWriter.Write(JsonConvert.SerializeObject(salesperson));
            }

        }

        public static Salesperson ReadJsonFile()
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
