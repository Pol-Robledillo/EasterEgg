using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasterEgg
{
    public static class Game
    {
        public static bool ValidateOption(int option, int min, int max)
        {
            return option < min || option > max;
        }
        public static bool ExitGame(int option)
        {
            return option == 2;
        }
        public static bool ExitGameMenu(int option)
        {
            return option == 3;
        }
    }
}
