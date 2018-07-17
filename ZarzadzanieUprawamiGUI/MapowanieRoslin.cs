using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZarzadzanieUprawamiGUI
{
    class MapowanieRoslin
    {

        public string nazwa { get; private set; }
        public int idRoslina { get; private set; }
        public MapowanieRoslin(string name, int value)
        {
            nazwa = name;
            idRoslina = value;
        }

        public static  List<MapowanieRoslin> possibleChoices = new List<MapowanieRoslin>
    {    };

        public static List<MapowanieRoslin> GetChoices()
        {
            return possibleChoices;
        }
    }
}
