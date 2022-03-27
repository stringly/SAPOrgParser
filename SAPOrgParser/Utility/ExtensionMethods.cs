using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SAPOrgParser.Utility
{
    public static class ExtensionMethods
    {
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
        public static string ToJSONString<T>(this T input)
        {            
            return JsonSerializer.Serialize(input);
        }
        public static XElement ToXElement<T>(this T input)
        {
            return XElement.Parse(input.ToXmlString());
        }
        public static IEnumerable<XElement> ToXElements<T>(this IEnumerable<T> input)
        {
            foreach (var item in input)
                yield return input.ToXElement();
        }
        public static IEnumerable<string> ToXmlString<T>(this IEnumerable<T> input)
        {
            foreach (var item in input)
                yield return item.ToXmlString();
        }
        public static string ToXmlString<T>(this T input)
        {
            using (var writer = new StringWriter())
            {
                input.ToXml(writer);
                return writer.ToString();
            }
        }
        public static void ToXml<T>(this T objectToSerialize, Stream stream)
        {
            new XmlSerializer(typeof(T)).Serialize(stream, objectToSerialize);
        }

        public static void ToXml<T>(this T objectToSerialize, StringWriter writer)
        {
            new XmlSerializer(typeof(T)).Serialize(writer, objectToSerialize);
        }
    }
}
