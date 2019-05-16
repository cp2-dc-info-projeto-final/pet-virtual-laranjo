using System.IO;
using System.Xml.Serialization;

public static class ferramentas
{
    public static string Serializar<T>(this T classe_){
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringWriter escritor = new StringWriter();
        xml.Serialize(escritor, classe_);
        return escritor.ToString();
    }

    public static T Desserializar<T>(this string string_){
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringReader leitor = new StringReader(string_);
        return (T)xml.Deserialize(leitor);
    }
}
