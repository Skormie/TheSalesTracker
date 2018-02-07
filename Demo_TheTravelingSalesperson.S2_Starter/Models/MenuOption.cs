using System.Collections.Generic;

namespace Demo_TheTravelingSalesperson
{
    /// <summary>
    /// all menu options
    /// </summary>
    /*public enum MenuOption
    {
        None,
        Setup_Account,
        Travel,
        Buy,
        Sell,
        Display_Inventory,
        Display_Cities,
        Display_Account_Info,
        Save_Account_Info,
        Load_Account_Info,
        Exit
    }*/

    class MenuOption
    {
        public static Dictionary<string, string> MainOption = new Dictionary<string, string>()
        {
            { "0", "None" },
            { "1", "Setup Account" },
            { "2", "Travel" },
            { "3", "Buy" },
            { "4", "Sell" },
            { "5", "Display Inventory" },
            { "6", "Display Cities" },
            { "7", "Display Account Info" },
            { "8", "Save Account Info" },
            { "9", "Load Account Info" },
            { "e", "Exit" }
        };
    }
}
