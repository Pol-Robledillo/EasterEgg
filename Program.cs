namespace EasterEgg
{
    class Program
    {
        static void Main()
        {
            const int OptionOne = 1, OptionTwo = 2, OptionThree = 3, StartingLevel = 0;
            const string HealthTag = "Health", AttackTag = "Attack", DefenseTag = "Defense";
            const string MsgGoodbye = "Thank you for playing.";
            const string MsgTitle = "Character fight game \n" +
                                    "1. Start \n" +
                                    "2. Exit \n" +
                                    "Your option: ";

            const string MsgCharacterCreation = "What do you want to do? \n" +
                                                "1. Create new character \n" +
                                                "2. Update existing character \n" +
                                                "3. Continue to fight \n" +
                                                "4. Exit \n" +
                                                "Your option: ";

            const string MsgChooseStat = "Choose the stat to update: \n" +
                                         "1. Health \n" +
                                         "2. Attack \n" +
                                         "3. Defense \n" +
                                         "Your option: ";

            const string MsgInputName = "Enter the name of the character: ";
            const string MsgInputNewValue = "Enter the new value: ";
            const string MsgInputHealth = "Enter the health of the character: ";
            const string MsgInputAttack = "Enter the attack of the character: ";
            const string MsgInputDefense = "Enter the defense of the character: ";
            const string MsgInputPlayer1Name = "Enter the name of the first character to fight: ";
            const string MsgInputPlayer2Name = "Enter the name of the second character to fight: ";
            const string MsgCharacterDead = "{0} died and {1} won the fight.";

            bool continuePlaying;
            string name, firstFighterName, secondFighterName;
            int option, health, attack, defense;

            do
            {
                //Start menu
                do
                {
                    Console.Write(MsgTitle);
                    option = Convert.ToInt32(Console.ReadLine());

                } while (!Helper.ValidateOption(option, OptionOne, OptionTwo));

                Console.WriteLine();

                if (Helper.ContinuePlaying(option))
                {
                    continuePlaying = true;

                    while (continuePlaying)
                    {
                        //Character creation menu
                        do
                        {
                            Console.Write(MsgCharacterCreation);
                            option = Convert.ToInt32(Console.ReadLine());

                        } while (!Helper.ValidateOption(option, OptionOne, OptionThree));

                        Console.WriteLine();

                        switch (option)
                        {
                            //Character creation
                            case 1:
                                Console.Write(MsgInputName);
                                name = Console.ReadLine() ?? "";

                                Console.Write(MsgInputHealth);
                                health = Convert.ToInt32(Console.ReadLine());

                                Console.Write(MsgInputAttack);
                                attack = Convert.ToInt32(Console.ReadLine());

                                Console.Write(MsgInputDefense);
                                defense = Convert.ToInt32(Console.ReadLine());

                                Character character = new Character(name, StartingLevel, health, attack, defense);
                                DocumentXML.CreateXML(character);

                                break;

                            //Character update
                            case 2:
                                Console.Write(MsgInputName);
                                name = Console.ReadLine() ?? "";
                                do
                                {
                                    Console.Write(MsgChooseStat);
                                    option = Convert.ToInt32(Console.ReadLine());

                                } while (!Helper.ValidateOption(option, OptionOne, OptionThree));

                                Console.WriteLine();

                                Console.Write(MsgInputNewValue);
                                int newValue = Convert.ToInt32(Console.ReadLine());

                                switch (option)
                                {
                                    case 1:
                                        DocumentXML.ModifyTag(name, HealthTag, newValue);
                                        break;
                                    case 2:
                                        DocumentXML.ModifyTag(name, AttackTag, newValue);
                                        break;
                                    case 3:
                                        DocumentXML.ModifyTag(name, DefenseTag, newValue);
                                        break;
                                }

                                break;

                            // Character fight
                            case 3:
                                do
                                {
                                    Console.Write(MsgInputPlayer1Name);
                                    firstFighterName = Console.ReadLine() ?? "";

                                } while (firstFighterName == "");

                                Console.WriteLine();

                                do
                                {
                                    Console.Write(MsgInputPlayer2Name);
                                    secondFighterName = Console.ReadLine() ?? "";

                                } while (secondFighterName == "");


                                // Leer el archivo XML y guardar en las variables de firstCharacter y secondCharacter los personajes instanciados de la clase Character.
                                Character firstCharacter = DocumentXML.GetXMLCharacter(firstFighterName);
                                Character secondCharacter = DocumentXML.GetXMLCharacter(secondFighterName);

                                while (!firstCharacter.IsDead() && !secondCharacter.IsDead())
                                {
                                    secondCharacter.TakeDamage(firstCharacter.Attack);

                                    if (!secondCharacter.IsDead())
                                    {
                                        firstCharacter.TakeDamage(secondCharacter.Attack);
                                    }
                                }

                                Console.WriteLine();

                                if (firstCharacter.IsDead())
                                {
                                    Console.WriteLine(MsgCharacterDead, firstCharacter.Name, secondCharacter.Name);
                                    DocumentXML.LevelUp(secondCharacter.Name);
                                }
                                else
                                {
                                    Console.WriteLine(MsgCharacterDead, secondCharacter.Name, firstCharacter.Name);
                                    DocumentXML.LevelUp(firstCharacter.Name);
                                }

                                break;

                            case 4:
                                Console.WriteLine(MsgGoodbye);
                                continuePlaying = false;
                                break;

                        }

                        Console.WriteLine();
                    }
                }

                else
                {
                    Console.WriteLine(MsgGoodbye);
                }

            } while (Helper.ContinuePlaying(option));
        }
    }
}