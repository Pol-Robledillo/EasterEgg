namespace EasterEgg
{
    public static class Helper
    {
        public static bool ValidateOption(int option, int min, int max)
        {
            return option >= min || option <= max;
        }

        public static bool ContinuePlaying(int option)
        {
            return option == 1;
        }
    }
}