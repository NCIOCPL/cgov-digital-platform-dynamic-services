using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace GlossaryLinkHandler
{
  /// <summary>
  /// A JsonConverter for IMedia items.
  /// </summary>
  public class RelatedResourceJsonConverter : JsonConverter<IRelatedResource>
  {

    /// <summary>
    /// Writes the JSON representation of the object.
    /// </summary>
    /// <param name="writer">The JsonWriter to write to.</param>
    /// <param name="value">The value.</param>
    /// <param name="serializer">The calling serializer.</param>
    public override void WriteJson(JsonWriter writer, IRelatedResource value, JsonSerializer serializer)
    {
        JToken t = JToken.FromObject(value);
        t.WriteTo(writer);
    }

    /// <summary>
    /// Reads the JSON representation of the object.
    /// </summary>
    /// <param name="reader">The JsonReader to read from.</param>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="existingValue">The existing value of object being read.</param>
    /// <param name="hasExistingValue"></param>
    /// <param name="serializer">The calling serializer.</param>
    /// <returns>An array of IRelatedResource items.</returns>
    public override IRelatedResource ReadJson(
      JsonReader reader,
      Type objectType,
      IRelatedResource existingValue,
      bool hasExistingValue,
      JsonSerializer serializer
    )
    {
      var jsonObject = JObject.Load(reader);
      var resource = default(IRelatedResource);
      string type = jsonObject["Type"].Value<string>();
      switch (type != null ? type.ToLower() : type)
      {
        case "drugsummary":
        case "summary":
        case "external":
          resource = new GlossaryTerm.LinkResource();
          break;
        case "glossaryterm":
          resource = new GlossaryTerm.GlossaryResource();
          break;
      }
      serializer.Populate(jsonObject.CreateReader(), resource);
      return resource;
    }


  }
}