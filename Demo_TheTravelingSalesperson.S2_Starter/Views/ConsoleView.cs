using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_TheTravelingSalesperson
{
    /// <summary>
    /// MVC View class
    /// </summary>
    public class ConsoleView
    {
        int MAXIMUM_ATTEMPTS = 10;
        int MINIMUM_BUYSELL_AMOUNT = 1;
        int MAXIMUM_BUYSELL_AMOUNT = 100;

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor to create the console view objects
        /// </summary>
        public ConsoleView()
        {
            InitializeConsole();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize all console settings
        /// </summary>
        private void InitializeConsole()
        {
            ConsoleUtil.WindowTitle = "Sales Tracker Application.";
            ConsoleUtil.HeaderText = "The Traveling Salesperson Application";
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            ConsoleUtil.DisplayMessage("");

            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayMessage("Thank you for using the application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Created by Jason Luckhardt");
            ConsoleUtil.DisplayMessage("NMC Homework Application");
            ConsoleUtil.DisplayMessage("");

            sb.Clear();
            sb.AppendFormat("You are a traveling salesperson buying and selling widgets ");
            sb.AppendFormat("around the country. You will be prompted regarding which city ");
            sb.AppendFormat("you wish to travel to and will then be asked whether you wish to buy ");
            sb.AppendFormat("or sell widgets.");
            ConsoleUtil.DisplayMessage(sb.ToString());
            ConsoleUtil.DisplayMessage("");

            sb.Clear();
            sb.AppendFormat("Your first task will be to set up your account details.");
            ConsoleUtil.DisplayMessage(sb.ToString());

            DisplayContinuePrompt();
        }

        /// <summary>
        /// setup the new salesperson object with the initial data
        /// Note: To maintain the pattern of only the Controller changing the data this method should
        ///       return a Salesperson object with the initial data to the controller. For simplicity in 
        ///       this demo, the ConsoleView object is allowed to access the Salesperson object's properties.
        /// </summary>
        public Salesperson DisplaySetupAccount()
        {
            Salesperson salesperson = new Salesperson();

            ConsoleUtil.HeaderText = "Account Setup";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Setup your account now.");
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your first name: ");
            salesperson.FirstName = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your last name: ");
            salesperson.LastName = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayPromptMessage("Enter your account ID: ");
            salesperson.AccountID = Console.ReadLine();
            ConsoleUtil.DisplayMessage("");

            DisplayAddInventory(salesperson);

            ConsoleUtil.DisplayMessage("");

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Your account is setup");

            DisplayContinuePrompt();

            return salesperson;
        }

        /// <summary>
        /// display a closing screen when the user quits the application
        /// </summary>
        public void DisplayClosingScreen()
        {
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for using The Traveling Salesperson Application.");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// get the menu choice from the user
        /// </summary>
        public string DisplayGetUserMenuChoice()
        {
            string userMenuChoice = "0";
            int namesCount = MenuOption.MainOption.Count();
            bool usingMenu = true;

            while (usingMenu)
            {
                //
                // set up display area
                //
                ConsoleUtil.HeaderText = "Main Menu";
                ConsoleUtil.DisplayReset();
                Console.CursorVisible = false;

                //
                // display the menu
                //
                ConsoleUtil.DisplayMessage("Please type the number of your menu choice.");
                ConsoleUtil.DisplayMessage("");

                List<string> keyList = new List<string>(MenuOption.MainOption.Keys);
                for (int i = 1; i < namesCount; i++)
                {
                    Console.Write("\t" + keyList[i].ToUpper() + ". " + MenuOption.MainOption[keyList[i]] + Environment.NewLine);
                }

                //
                // get and process the user's response
                // note: ReadKey argument set to "true" disables the echoing of the key press
                //
                ConsoleKeyInfo userResponse = Console.ReadKey(true);

                if (!MenuOption.MainOption.ContainsKey(userResponse.KeyChar.ToString()))
                {
                    ConsoleUtil.DisplayMessage(
                        "It appears you have selected an incorrect choice." + Environment.NewLine +
                        "Press any key to continue or the ESC key to quit the application.");

                    userResponse = Console.ReadKey(true);
                    if (userResponse.Key == ConsoleKey.Escape)
                        usingMenu = false;
                    break;
                }

                userMenuChoice = userResponse.KeyChar.ToString();

                usingMenu = false;
            }
            Console.CursorVisible = true;

            return userMenuChoice;
        }
        /// <summary>
        /// get the next city to travel to from the user
        /// </summary>
        /// <returns>string City</returns>
        public string DisplayGetNextCity()
        {
            string nextCity = "";

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayPromptMessage("Enter the name of the next city:");
            nextCity = Console.ReadLine();

            return nextCity;
        }

        /// <summary>
        /// display a list of the cities traveled
        /// </summary>
        public void DisplayCitiesTraveled(Salesperson salesperson)
        {
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("You have traveled to the following cities.");
            ConsoleUtil.DisplayMessage("");

            foreach (string city in salesperson.CitiesVisited)
            {
                ConsoleUtil.DisplayMessage(city);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the current account information
        /// </summary>
        public void DisplayAccountInfo(Salesperson salesperson, bool header = true)
        {
            if (header)
            {
                ConsoleUtil.HeaderText = "Account Info";
                ConsoleUtil.DisplayReset();
            }

            ConsoleUtil.DisplayMessage("First Name: " + salesperson.FirstName);
            ConsoleUtil.DisplayMessage("Last Name: " + salesperson.LastName);
            ConsoleUtil.DisplayMessage("Account ID: " + salesperson.AccountID);

            DisplayContinuePrompt();
        }

        public void DisplayInventoryError()
        {
            ConsoleUtil.HeaderText = "Inventory Warning";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("You need to use the \"Add Inventory\" option before buying or selling murchandise!");

            DisplayContinuePrompt();
        }

        public void DisplayBackorderNotification( Product product, int numberOfUnitsSold)
        {
            ConsoleUtil.HeaderText = "Backordered Inventory!";
            ConsoleUtil.DisplayReset();

            int numberOfUnitsBackordered = Math.Abs(product.NumberOfUnits);
            int numberOfUnitsShipped = numberOfUnitsSold - numberOfUnitsBackordered;

            ConsoleUtil.DisplayMessage("Products Sold: " + numberOfUnitsSold);
            ConsoleUtil.DisplayMessage("Products Shipped: " + numberOfUnitsShipped);
            ConsoleUtil.DisplayMessage("Products on Backorder: " + numberOfUnitsBackordered);

            DisplayContinuePrompt();
        }

        public Product DisplayGetNumberOfUnitsToBuy( List<Product> products )
        {
            Product.ProductType productType;
            ConsoleUtil.HeaderText = "Buy Inventory";
            ConsoleUtil.DisplayReset();

            productType = ListProducts(products);

            //
            // get number of products to buy
            //
            ConsoleUtil.DisplayMessage("Buying " + UnderscoreToSpace(productType.ToString()) + " products.");
            ConsoleUtil.DisplayMessage("");

            if (!ConsoleValidator.TryGetIntegerFromUser(MINIMUM_BUYSELL_AMOUNT, MAXIMUM_BUYSELL_AMOUNT, MAXIMUM_ATTEMPTS, "products", out int numberOfUnitsToBuy))
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficulty setting the number of products to buy.");
                ConsoleUtil.DisplayMessage("By default, the number of products to sell will be set to zero.");
                numberOfUnitsToBuy = 0;
                DisplayContinuePrompt();
            }

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage(numberOfUnitsToBuy + " " + UnderscoreToSpace(productType.ToString()) + " products have been added to the inventory.");

            DisplayContinuePrompt();

            return new Product(productType, numberOfUnitsToBuy, false);
        }

        public Product DisplayGetNumberOfUnitsToSell(List<Product> products)
        {
            Product.ProductType productType;
            ConsoleUtil.HeaderText = "Sell Inventory";
            ConsoleUtil.DisplayReset();

            productType = ListProducts(products);

            //
            // Get number of products to sell.
            //
            ConsoleUtil.DisplayMessage("Selling " + UnderscoreToSpace(productType.ToString()) + " products.");
            ConsoleUtil.DisplayMessage("");

            if(!ConsoleValidator.TryGetIntegerFromUser(MINIMUM_BUYSELL_AMOUNT, MAXIMUM_BUYSELL_AMOUNT, MAXIMUM_ATTEMPTS, "products", out int numberOfUnitsToSell))
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficulty setting the number of products to sell.");
                ConsoleUtil.DisplayMessage("By default, the number of pruducts to sell will be set to zero.");
                numberOfUnitsToSell = 0;
                DisplayContinuePrompt();
            }

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage(numberOfUnitsToSell + " " + UnderscoreToSpace(productType.ToString()) + (numberOfUnitsToSell > 1 && productType.ToString().Last() != 's' ? "s" : "") + " have been subtracted from the inventory.");

            DisplayContinuePrompt();

            return new Product(productType, numberOfUnitsToSell, false);

        }

        public void DisplayInventory( List<Product> product )
        {
            ConsoleUtil.HeaderText = "Current Inventory";
            ConsoleUtil.DisplayReset();

            foreach (Product item in product)
            {
                ConsoleUtil.DisplayMessage("Product type: " + UnderscoreToSpace(item.Type.ToString()));
                ConsoleUtil.DisplayMessage("Number of units: " + item.NumberOfUnits.ToString());
                ConsoleUtil.DisplayMessage("");
            }

            DisplayContinuePrompt();
        }

        public void DisplayConfirmLoadAccountInfo(Salesperson salesperson)
        {
            ConsoleUtil.HeaderText = "Load Account";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Account information loaded.");

            DisplayAccountInfo(salesperson, false);
        }

        public void DisplayConfirmSaveAccountInfo()
        {
            ConsoleUtil.HeaderText = "Save Account";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Account information saved.");

            DisplayContinuePrompt();
        }

        public bool DisplayLoadAccountInfo(out bool maxAttemptsExceeded, Salesperson temp)
        {
            string userResponse;
            maxAttemptsExceeded = false;

            ConsoleUtil.HeaderText = "Load Account";
            ConsoleUtil.DisplayReset();

            DisplayAccountInfo(temp, false);
            ConsoleUtil.DisplayMessage("");
            userResponse = ConsoleValidator.GetYesNoFromUser(MAXIMUM_ATTEMPTS, "Load the account information?", out maxAttemptsExceeded);

            if (maxAttemptsExceeded)
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficulty. You will return to the main menu.");
                return false;
            }
            else
            {
                //
                // Note use of ternary operator.
                //
                return userResponse.ToLower() == "yes" ? true : false;
            }
        }

        public bool DisplaySaveAccountInfo(Salesperson salesperson, out bool maxAttemptsExceeded)
        {
            string userResponse;
            maxAttemptsExceeded = false;

            ConsoleUtil.HeaderText = "Save Account";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("The current account information.");
            DisplayAccountInfo(salesperson, false);

            ConsoleUtil.DisplayMessage("");
            userResponse = ConsoleValidator.GetYesNoFromUser(MAXIMUM_ATTEMPTS, "Save the account information?", out maxAttemptsExceeded);

            if (maxAttemptsExceeded)
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficulty. You will return to the main menu.");
                return false;
            }
            else
            {
                //
                // Note use of ternary operator.
                //
                return userResponse.ToLower() == "yes" ? true : false;
            }
        }

        public void DisplayUpdateAccount(Salesperson salesperson)
        {
            string userResponce = "";
            ConsoleUtil.HeaderText = "Update Setup";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Your First Name is: "+ salesperson.FirstName);
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayPromptMessage("Press enter to continue or input a new First Name:");
            userResponce = Console.ReadLine();
            if ( userResponce != "" )
                salesperson.FirstName = userResponce;
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Your Last Name is: " + salesperson.LastName);
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayPromptMessage("Press enter to continue or input a new Last Name:");
            userResponce = Console.ReadLine();
            if (userResponce != "")
                salesperson.LastName = userResponce;
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Your Account ID is: " + salesperson.AccountID);
            ConsoleUtil.DisplayMessage("");
            ConsoleUtil.DisplayPromptMessage("Press enter to continue or input a new Account ID:");
            userResponce = Console.ReadLine();
            if (userResponce != "")
                salesperson.AccountID = userResponce;
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Your account has been updated!");
            DisplayContinuePrompt();
        }

        public void DisplayLogs(Salesperson salesperson)
        {
            ConsoleUtil.HeaderText = "Logs";
            ConsoleUtil.DisplayReset();

            foreach (string log in salesperson.Logs)
            {
                ConsoleUtil.DisplayMessage(log);
            }

            DisplayContinuePrompt();
        }

        public void DisplayAddInventory(Salesperson salesperson)
        {
            Product.ProductType productType;
            string errorMes = "It appears you have selected an incorrect choice.";
            ConsoleUtil.HeaderText = "Add Inventory";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Select the product you'd like to add.");
            ConsoleUtil.DisplayMessage("");
            while (true)
            {
                ConsoleUtil.DisplayMessage("Product Types:");
                ConsoleUtil.DisplayMessage("");

                //
                // list all product types
                //
                for (int i = 1; i < Enum.GetNames(typeof(Product.ProductType)).Length; i++)
                    Console.WriteLine("\t " + i + ". " + UnderscoreToSpace(Enum.GetName(typeof(Product.ProductType), i)));

                //
                // get product type, if invalid entry ask user to choose again.
                //
                ConsoleUtil.DisplayMessage("");
                ConsoleUtil.DisplayPromptMessage("Select a product type. (1 - " + (Enum.GetNames(typeof(Product.ProductType)).Length - 1) + ") ");
                ConsoleUtil.DisplayMessage("");

                ConsoleKeyInfo userResponse = Console.ReadKey(true);

                if (Enum.TryParse<Product.ProductType>(userResponse.KeyChar.ToString(), out productType))
                {
                    if(salesperson.CurrentStock.Exists(x=>x.Type == productType))
                    {
                        errorMes = UnderscoreToSpace(productType.ToString()) + " is already in your inventory try a different item.";
                    }
                    else
                    {
                        ConsoleUtil.DisplayPromptMessage("You've selected " + UnderscoreToSpace(productType.ToString()) + ".");
                        break;
                    }
                }

                ConsoleUtil.DisplayReset();

                ConsoleUtil.DisplayMessage(
                    errorMes + Environment.NewLine +
                    "Press any key to continue or the ESC key to quit the application.");
            }
            ConsoleUtil.DisplayMessage("");

            //
            // get number of products in inventory
            //
            if (ConsoleValidator.TryGetIntegerFromUser(MINIMUM_BUYSELL_AMOUNT, MAXIMUM_BUYSELL_AMOUNT, MAXIMUM_ATTEMPTS, "products", out int numberOfUnits))
            {
                salesperson.CurrentStock.Add(new Product(productType, numberOfUnits, false));
            }
            else
            {
                ConsoleUtil.DisplayMessage("It appears you are having difficulty setting the number of products in your stock.");
                ConsoleUtil.DisplayMessage("By default, the number of products in your inventory are now set to zero.");
                salesperson.CurrentStock.Add(new Product(productType, 0, false));
            }
            salesperson.Logs.Push(DateTime.Now + " ... " + numberOfUnits + " " + productType + " added to inventory!");
        }

        public Product.ProductType ListProducts( List<Product> products )
        {
            int selection = 0;

            while (true)
            {
                ConsoleUtil.DisplayMessage("Product Types:");
                ConsoleUtil.DisplayMessage("");

                //
                // list all product types
                //
                for (int i = 0; i < products.Count; i++)
                    Console.WriteLine("\t " + (i+1) + ". " + UnderscoreToSpace(products[i].Type.ToString()) +" x"+ products[i].NumberOfUnits.ToString());

                //
                // get product type, if invalid entry ask user to choose again.
                //
                ConsoleUtil.DisplayMessage("");
                ConsoleUtil.DisplayPromptMessage("Select a product type. (1 - " + products.Count + ") ");
                ConsoleUtil.DisplayMessage("");

                ConsoleKeyInfo userResponse = Console.ReadKey(true);

                int.TryParse(userResponse.KeyChar.ToString(), out selection);
                if (selection <= products.Count && selection > 0)
                    break;

                ConsoleUtil.DisplayReset();

                ConsoleUtil.DisplayMessage(
                    "It appears you have selected an incorrect choice." + Environment.NewLine +
                    "Press any key to continue or the ESC key to quit the application.");
            }
            return products[selection - 1].Type;
        }

        /// <summary>
        /// Changes string to lowercase with first letter uppercase.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        string UppercaseFirst(string s = " ")
        {
            return s.First().ToString().ToUpper() + s.Substring(1);
        }

        string UnderscoreToSpace(string s = " ")
        {
            return s.Replace('_', ' ');
        }
        #endregion
    }
}
