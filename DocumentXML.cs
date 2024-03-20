using System.Xml;
using System.Xml.Linq;
using EasterEgg;

namespace EasterEgg
{
    public class DocumentXML
    {
        public static void ReadMainNode(string fileName)
        {
            const string NotFound = "No se encontraron elementos 'Personaje' en el archivo XML.";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);

                XmlNodeList personajesNodes = doc.SelectNodes("/personajes/Personaje");
                if (personajesNodes != null)
                {
                    foreach (XmlNode personajeNode in personajesNodes)
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
            const string Done = "Se ha añadido un nuevo personaje al archivo XML.";
            try
            {
                XDocument doc;

                // Verificar si el archivo XML existe
                if (File.Exists("Personajes.xml"))
                {
                    // Si el archivo XML existe, cargarlo
                    doc = XDocument.Load("Personajes.xml");

                    // Verificar si el documento tiene un elemento raíz
                    if (doc.Root == null)
                    {
                        // Si no tiene un elemento raíz, crear uno
                        doc.Add(new XElement("personajes"));
                    }
                }
                else
                {
                    // Si el archivo XML no existe, crear un nuevo documento con un elemento raíz
                    doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("personajes"));
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
                doc.Save("Personajes.xml");

                Console.WriteLine(Done);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el XML: " + ex.Message);
            }
        }
        public static void ModifyTag(string name, string tag, int newValue)
        {
            const string NotFound = "No se encontró el tag a modificar.";
            const string Done = "Se ha modificado el tag correctamente.";
            try
            {
                XDocument doc = XDocument.Load("Personajes.xml");
                XElement personaje = doc.Descendants("Personaje").FirstOrDefault(p => p.Element("Nombre").Value == name);
                if (personaje != null)
                {
                    XElement element = personaje.Element(tag);
                    if (element != null)
                    {
                        element.Value = Convert.ToString(newValue);
                        doc.Save("Personajes.xml");
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
                Console.WriteLine("Error al modificar el tag: " + ex.Message);
            }
        }
        public static Character GetXMLCharacter(string name)
        {
            const string NotFound = "No se encontró el personaje.";
            try
            {
                XDocument doc = XDocument.Load("Personajes.xml");
                XElement personaje = doc.Descendants("Personaje").FirstOrDefault(p => p.Element("Nombre").Value == name);
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
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el personaje: " + ex.Message);
                return null;
            }
        }
    }
}
