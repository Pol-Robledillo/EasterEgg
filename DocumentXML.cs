using System.Xml;
using System.Xml.Linq;

namespace EasterEgg
{
    public class DocumentXML
    {
        public const string FilePath = @"..\..\..\Personajes.xml";
        public static void ReadMainNode(string fileName)
        {
            const string NotFound = "No 'Character' elements were found in the XML file";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);

                XmlNodeList PersonajesNodes = doc.SelectNodes("/Personajes/Personaje");

                if (PersonajesNodes != null)
                {
                    foreach (XmlNode personajeNode in PersonajesNodes)
                    {
                        ReadChilds(personajeNode);
                    }
                }

                else
                {
                    Console.WriteLine(NotFound);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void ReadChilds(XmlNode personajeNode)
        {
            XmlNodeList attributes = personajeNode.ChildNodes;
            foreach (XmlNode attribute in attributes)
            {
                if (attribute.NodeType == XmlNodeType.Element)
                {
                    Console.WriteLine(attribute.Name + ": " + attribute.InnerText);
                }
            }
            Console.WriteLine();
        }

        public static void CreateXML(Character character)
        {
            const string Done = "A new character has been added to the XML file.";
            const string Error = "Error when creating the XML: {0}";
            try
            {
                XDocument doc;

                if (File.Exists(FilePath)) // Verificar si el archivo XML existe
                {
                    doc = XDocument.Load(FilePath); // Si el archivo XML existe, cargarlo

                    if (doc.Root == null) // Verificar si el documento tiene un elemento raíz
                    {
                        doc.Add(new XElement("Personajes")); // Si no tiene un elemento raíz, crear uno
                    }
                }
                else
                {
                    // Si el archivo XML no existe, crear un nuevo documento con un elemento raíz
                    doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Personajes"));
                }

                // Crear el nuevo elemento Personaje y agregarlo al documento
                XElement nuevoPersonaje = new XElement("Personaje",
                    new XElement("Name", character.Name),
                    new XElement("Level", character.Level),
                    new XElement("Health", character.Health),
                    new XElement("Attack", character.Attack),
                    new XElement("Defense", character.Defense));

                doc.Root.Add(nuevoPersonaje);

                // Guardar el documento actualizado
                doc.Save(FilePath);
                Console.WriteLine();
                Console.WriteLine(Done);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Error, ex.Message);
            }
        }

        public static void ModifyTag(string name, string tag, int newValue)
        {
            const string NotFound = "Tag not found.";
            const string Done = "Tag updated successfully.";
            const string Error = "Error when updating the tag: {0}";

            try
            {
                XDocument doc = XDocument.Load(FilePath);
                XElement personaje = doc.Descendants("Personaje").FirstOrDefault(p => p.Element("Name").Value == name);
                if (personaje != null)
                {
                    XElement element = personaje.Element(tag);
                    if (element != null)
                    {
                        element.Value = Convert.ToString(newValue);
                        doc.Save(FilePath);
                        Console.WriteLine();
                        Console.WriteLine(Done);
                    }
                    else
                    {
                        Console.WriteLine(NotFound);
                    }
                }
                else
                {
                    Console.WriteLine(NotFound);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Error, ex.Message);
            }
        }

        public static Character? GetXMLCharacter(string name)
        {
            const string Error = "Error when searching for the character: {0}";
            const string NotFound = "Character not found.";

            try
            {
                XDocument doc = XDocument.Load(FilePath);
                XElement personaje = doc.Descendants("Personaje").FirstOrDefault(p => p.Element("Name").Value == name);

                if (personaje != null)
                {
                    string nombre = personaje.Element("Name").Value;
                    int nivel = Convert.ToInt32(personaje.Element("Level").Value);
                    int salud = Convert.ToInt32(personaje.Element("Health").Value);
                    int ataque = Convert.ToInt32(personaje.Element("Attack").Value);
                    int defensa = Convert.ToInt32(personaje.Element("Defense").Value);
                    return new Character(nombre, nivel, salud, ataque, defensa);
                }
                else
                {
                    Console.WriteLine(NotFound);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Error, ex.Message);
            }

            return null;
        }

        public static void LevelUp(string name)
        {
            const string Error = "Error when searching for the character: {0}";
            const string NotFound = "Character not found.";

            try
            {
                XDocument doc = XDocument.Load(FilePath);
                XElement personaje = doc.Descendants("Personaje").FirstOrDefault(p => p.Element("Name").Value == name);

                if (personaje != null)
                {
                    personaje.Element("Level").Value += 1;
                    doc.Save(FilePath);
                }
                else
                {
                    Console.WriteLine(NotFound);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(Error, ex.Message);
            }
        }
    }
}