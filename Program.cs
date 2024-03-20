using System.Collections;
using System.Reflection.Metadata;
namespace EasterEgg
{
    class Program
    {
        static void Main()
        {
            string name, firstFighter, secondFighter;
            int option, hp, atk, def;
            const string MsgTitle = "Easter Game \n" +
                                    "1. Start \n" +
                                    "2. Exit";
            const string MsgCharacterCreation = "What do you want to do? \n" +
                                                "1. Create new character \n" +
                                                "2. Update existing character \n" +
                                                "3. Continue to fight";
            const string MsgInputName = "Enter the name of the character: ";
            const string MsgChooseStat = "Choose the stat to update: \n" +
                                         "1. Health \n" +
                                         "2. Attack \n" +
                                         "3. Defense";
            const string MsgInputNewValue = "Enter the new value: ";
            const string MsgInputHP = "Enter the health of the character: ";
            const string MsgInputAttack = "Enter the attack of the character: ";
            const string MsgInputDefense = "Enter the defense of the character: ";
            const string MsgInputPlayer1Name = "Enter the name of the first character to fight: ";
            const string MsgInputPlayer2Name = "Enter the name of the second character to fight: ";
            do
            {
                //Start menu
                do
                {
                    Console.WriteLine(MsgTitle);
                    option = Convert.ToInt32(Console.ReadLine());
                } while (!Game.ValidateOption(option));
                if (option == 1)
                {
                    do
                    {
                        //Character creation menu
                        do
                        {
                            Console.WriteLine(MsgCharacterCreation);
                            option = Convert.ToInt32(Console.ReadLine());
                        } while (!Game.ValidateOption(option));
                        switch (option)
                        {
                            case 1:
                                //Character creation
                                Console.WriteLine(MsgInputName);
                                name = Console.ReadLine();
                                Console.WriteLine(MsgInputHP);
                                hp = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(MsgInputAttack);
                                atk = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(MsgInputDefense);
                                def = Convert.ToInt32(Console.ReadLine());
                                Character character = new Character(name, 1, hp, atk, def);
                                DocumentXML.CreateXML(character);
                                break;
                            case 2:
                                //Character update
                                Console.WriteLine(MsgInputName);
                                name = Console.ReadLine();
                                do
                                {
                                    Console.WriteLine(MsgChooseStat);
                                    option = Convert.ToInt32(Console.ReadLine());
                                } while (!Game.ExitGameMenu(option));
                                Console.WriteLine(MsgInputNewValue);
                                switch (option)
                                {
                                    case 1:
                                        DocumentXML.ModifyTag(name, "HP", Convert.ToInt32(Console.ReadLine()));
                                        break;
                                    case 2:
                                        DocumentXML.ModifyTag(name, "Ataque", Convert.ToInt32(Console.ReadLine()));
                                        break;
                                    case 3:
                                        DocumentXML.ModifyTag(name, "HP", Convert.ToInt32(Console.ReadLine()));
                                        break;
                                }
                                break;
                        }
                    } while (!Game.ExitGameMenu(option));
                    //Fight
                    do
                    {
                        Console.Write("Introduce el nombre del primer peleador: ");
                        firstFighter = Console.ReadLine() ?? "";
                    } while (firstFighter == "");

                    Console.WriteLine();

                    do
                    {
                        Console.Write("Introduce el nombre del primer peleador: ");
                        secondFighter = Console.ReadLine() ?? "";
                    } while (secondFighter == "");


                    // Leer el archivo XML y guardar en las variables de firstCharacter y secondCharacter los personajes instanciados de la clase Character.
                    Character firstCharacter = DocumentXML.GetXMLCharacter(firstFighter);
                    Character secondCharacter = DocumentXML.GetXMLCharacter(secondFighter);

                    while (!firstCharacter.IsDead() && !secondCharacter.IsDead())
                    {
                        secondCharacter.TakeDamage(firstCharacter.Attack);

                        if (!secondCharacter.IsDead())
                        {
                            firstCharacter.TakeDamage(secondCharacter.Attack);
                        }
                    }

                    if (firstCharacter.IsDead())
                    {
                        Console.WriteLine("Primer personaje muerto.");
                    }
                    else
                    {
                        Console.WriteLine("Segundo personaje muerto.");
                    }
                }
            } while (!Game.ExitGame(option));
        }
    }
}