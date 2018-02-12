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
        private static Salesperson InitializeSalesperson()
        {
            Salesperson salesperson = new Salesperson()
            {
                FirstName = "Bonzo",
                LastName = "Regan",
                AccountID = "banana103",
                CitiesVisited = new List<string>()
                {
                    "Detroit",
                    "Grand Rapids",
                    "Ann Arbor"
                }
            };

            salesperson.CurrentStock.Add(new Product(Product.ProductType.PVP_Helm, 20, false));

            return salesperson;
        }

        public static void SeedDataFile()
        {
            JsonServices jsonService = new JsonServices(DataSettings.dataFilePathJson);

            jsonService.WriteJsonFile(InitializeSalesperson());
        }
    }
}
