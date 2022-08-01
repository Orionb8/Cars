using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestProject.Data.Converters {
    public class DateTimeConverter : JsonConverter<DateTime> {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var dateTime = DateTime
                .ParseExact(reader.GetString(), "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffZ", null);

            return dateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) {
            writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffZ"));
        }
    }
}
