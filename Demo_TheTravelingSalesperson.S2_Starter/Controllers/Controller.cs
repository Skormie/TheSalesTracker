using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_TheTravelingSalesperson
{
    /// <summary>
    /// MVC Controller class
    /// </summary>
    public class Controller
    {
        #region FIELDS

        private bool _usingApplication;

        //
        // declare ConsoleView and Salesperson objects for the Controller to use
        // Note: There is no need for a Salesperson or ConsoleView property given only the Controller 
        //       will access the ConsoleView object and will pass the Salesperson object to the ConsoleView.
        //
        private ConsoleView _consoleView;
        private Salesperson _salesperson;

        private JsonServices _jsonService = new JsonServices(DataSettings.dataFilePathJson);

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            InitializeController();

            //
            // instantiate a Salesperson object
            //
            _salesperson = new Salesperson();

            //
            // instantiate a ConsoleView object
            //
            _consoleView = new ConsoleView();

            //
            // begins running the application UI
            //
            ManageApplicationLoop();
        }

        #endregion
        
        #region METHODS

        /// <summary>
        /// initialize the controller 
        /// </summary>
        private void InitializeController()
        {
            _usingApplication = true;
        }

        /// <summary>
        /// method to manage the application setup and control loop
        /// </summary>
        private void ManageApplicationLoop()
        {
            //MenuOption userMenuChoice;

            string userMenuChoice;

            _consoleView.DisplayWelcomeScreen();

            InitializeDataFileJson.SeedDataFile();

            //
            // application loop
            //
            while (_usingApplication)
            {

                //
                // get a menu choice from the ConsoleView object
                //
                userMenuChoice = _consoleView.DisplayGetUserMenuChoice();

                //
                // choose an action based on the user's menu choice
                //

                switch (MenuOption.MainOption[userMenuChoice])
                {
                    case "Setup Account":
                        DisplaySetupAccount();
                        break;
                    case "Travel":
                        Travel();
                        break;
                    case "Buy":
                        Buy();
                        break;
                    case "Sell":
                        Sell();
                        break;
                    case "Display Inventory":
                        DisplayInventory();
                        break;
                    case "Display Cities":
                        DisplayCities();
                        break;
                    case "Display Account Info":
                        DisplayAccountInfo();
                        break;
                    case "Save Account Info":
                        SaveAccountInfo();
                        break;
                    case "Load Account Info":
                        ReadAccountInfo();
                        break;
                    case "Add Inventory":
                        DisplayAddInventory();
                        break;
                    case "Update Account":
                        DisplayUpdateAccount();
                        break;
                    case "Display Logs":
                        DisplayLogs();
                        break;
                    case "Exit":
                        _usingApplication = false;
                        break;
                    default:
                        break;
                }
            }

            _consoleView.DisplayClosingScreen();

            //
            // close the application
            //
            Environment.Exit(1);
        }

        private void DisplaySetupAccount()
        {
            //
            // setup initial salesperson account
            //
            _salesperson = _consoleView.DisplaySetupAccount();
            _salesperson.Logs.Push(DateTime.Now + " ... Account Created!");
        }

        private void DisplayLogs()
        {
            _consoleView.DisplayLogs(_salesperson);
        }

        private void DisplayAddInventory()
        {
            _consoleView.DisplayAddInventory(_salesperson);
        }

        private void DisplayUpdateAccount()
        {
            _consoleView.DisplayUpdateAccount(_salesperson);
            _salesperson.Logs.Push(DateTime.Now + " ... Account Information Updated!");
        }

        /// <summary>
        /// add the next city location to the list of cities
        /// </summary>
        private void Travel()
        {
            string nextCity = _consoleView.DisplayGetNextCity();

            //
            // do not add empty strings to list for city names
            //
            if (nextCity != "")
            {
                _salesperson.CitiesVisited.Add(nextCity);
            }
        }

        private void Buy()
        {
            if (_salesperson.CurrentStock.Count() <= 0)
            {
                _consoleView.DisplayInventoryError();
                return;
            }
            Product selectedProduct = _consoleView.DisplayGetNumberOfUnitsToBuy( _salesperson.CurrentStock );
            _salesperson.CurrentStock.Find(x => x.Type == selectedProduct.Type).AddProducts(selectedProduct.NumberOfUnits);
            _salesperson.Logs.Push(DateTime.Now + " ... Purchased "+ selectedProduct.NumberOfUnits+" "+ selectedProduct.Type + "!");
        }

        private void Sell()
        {
            if (_salesperson.CurrentStock.Count() <= 0)
            {
                _consoleView.DisplayInventoryError();
                return;
            }
            Product selectedProduct = _consoleView.DisplayGetNumberOfUnitsToSell(_salesperson.CurrentStock);
            Product targetProduct = _salesperson.CurrentStock.Find(x => x.Type == selectedProduct.Type);
            targetProduct.SubtractProducts(selectedProduct.NumberOfUnits);

            if (targetProduct.OnBackorder)
            {
                _consoleView.DisplayBackorderNotification(targetProduct, selectedProduct.NumberOfUnits);
                _salesperson.Logs.Push(DateTime.Now + " ... Backordered " + selectedProduct.NumberOfUnits + " " + selectedProduct.Type + "!");
            } else
            {
                _salesperson.Logs.Push(DateTime.Now + " ... Sold " + selectedProduct.NumberOfUnits + " " + selectedProduct.Type + "!");
            }
        }

        private void DisplayInventory()
        {
            _consoleView.DisplayInventory(_salesperson.CurrentStock);
        }

        /// <summary>
        /// display all cities traveled to
        /// </summary>
        private void DisplayCities()
        {
            _consoleView.DisplayCitiesTraveled(_salesperson);
        }

        /// <summary>
        /// display account information
        /// </summary>
        private void DisplayAccountInfo()
        {
            _consoleView.DisplayAccountInfo(_salesperson);
        }

        private void SaveAccountInfo()
        {
            bool active;
            active = _consoleView.DisplaySaveAccountInfo(_salesperson, out bool attempted);
            if (active)
            {
                _consoleView.DisplayConfirmSaveAccountInfo();
                _jsonService.WriteJsonFile(_salesperson);
                _salesperson.Logs.Push(DateTime.Now + " ... Account Information Saved!");
            }
        }

        private void ReadAccountInfo()
        {
            bool active;
            active = _consoleView.DisplayLoadAccountInfo(out bool attempted, _jsonService.ReadJsonFile());
            if (active)
            {
                _salesperson = _jsonService.ReadJsonFile();
                _consoleView.DisplayConfirmLoadAccountInfo(_salesperson);
                _salesperson.Logs.Push(DateTime.Now + " ... Account Information Loaded!");
            }
        }

        #endregion
    }
}
