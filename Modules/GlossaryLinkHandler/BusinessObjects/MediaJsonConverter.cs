using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace GlossaryLinkHandler
{
    /// <summary>
    /// A JsonConverter for IMedia items.
    /// </summary>
    public class MediaJsonConverter : JsonConverter<IMedia>
    {

      /// <summary>
      /// Writes the JSON representation of the object.
      /// </summary>
      /// <param name="writer">The JsonWriter to write to.</param>
      /// <param name="value">The value.</param>
      /// <param name="serializer">The calling serializer.</param>
      public override void WriteJson(JsonWriter writer, IMedia value, JsonSerializer serializer) {
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
      /// <returns>An array of IMedia items.</returns>
      public override IMedia ReadJson(
        JsonReader reader,
        Type objectType,
        IMedia existingValue,
        bool hasExistingValue,
        JsonSerializer serializer
      ) {
        var jsonObject = JObject.Load(reader);
        var media = default(IMedia);
        string type = jsonObject["Type"].Value<string>();
        switch (type != null ? type.ToLower() : type)
        {
            case "image":
                media = new GlossaryTerm.Image();
                break;
            case "video":
                media = new GlossaryTerm.Video();
                break;
        }
        serializer.Populate(jsonObject.CreateReader(), media);
        return media;
      }


    }
}