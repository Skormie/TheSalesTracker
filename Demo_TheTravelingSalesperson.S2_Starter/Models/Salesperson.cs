using System.Collections.Generic;

namespace Demo_TheTravelingSalesperson
{
    /// <summary>
    /// Salesperson MVC Model class
    /// </summary>
    public class Salesperson
    {
        #region FIELDS

        private string _firstName;
        private string _lastName;
        private string _accountID;
        private List<string> _citiesVisited;
        private List<Product> _currentStock;
        private bool _active;
        private int _age;
        private Stack<string> _logs;

        #endregion

        #region PROPERTIES

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string AccountID
        {
            get { return _accountID; }
            set { _accountID = value; }
        }
      
        public List<string> CitiesVisited
        {
            get { return _citiesVisited; }
            set { _citiesVisited = value; }
        }
        public Stack<string> Logs
        {
            get { return _logs; }
            set { _logs = value; }
        }

        public List<Product> CurrentStock
        {
            get { return _currentStock; }
            set { _currentStock = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Salesperson()
        {
            _citiesVisited = new List<string>();
            _currentStock = new List<Product>();
            _logs = new Stack<string>();
        }

        public Salesperson(string firstName, string lastName, string acountID, int age = 0, bool active = true)
        {
            _firstName = firstName;
            _lastName = lastName;
            _accountID = acountID;
            _active = active;
            _age = age;

            _logs = new Stack<string>();
            _citiesVisited = new List<string>();
            _currentStock = new List<Product>();
        }

        #endregion
        
        #region METHODS



        #endregion
    }
}
