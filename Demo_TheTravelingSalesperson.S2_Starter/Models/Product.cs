using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_TheTravelingSalesperson
{
    public class Product
    {
        public enum ProductType
        {
            None,
            PVP_Helm,
            God_wings,
            Ahura,
            Angra
        }

        private int _numberOfUnits;

        public int NumberOfUnits
        {
            get { return _numberOfUnits; }
        }

        private bool _onBackorder;

        public bool OnBackorder
        {
            get { return _onBackorder; }
            set { _onBackorder = value; }
        }

        private ProductType _type;

        public ProductType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public void AddProducts(int unitsToAdd)
        {
            _numberOfUnits += unitsToAdd;
        }

        public void SubtractProducts(int unitsToSubtract)
        {
            if (_numberOfUnits < unitsToSubtract)
            {
                _onBackorder = true;
            }
            _numberOfUnits -= unitsToSubtract;
        }

        public Product()
        {

        }

        public Product(ProductType type, int numberOfUnits)
        {

        }

    }
}
