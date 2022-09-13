using helloserve.SePush.Models;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace helloserve.SePush.Converters
{
    internal class TimeStringSlotConverter : JsonConverterFactory
    {
        public TimeStringSlotConverter() { }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(ScheduleDaySlots);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return new SlotConverter();
        }
    }

    internal class SlotConverter : JsonConverter<ScheduleDaySlots>
    {
        public override ScheduleDaySlots Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            ScheduleDaySlots result = new ScheduleDaySlots();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.StartArray)
                    continue;

                if (reader.TokenType == JsonTokenType.EndArray)
                    break;

                if (reader.TokenType == JsonTokenType.String)
                {
                    string value = Encoding.UTF8.GetString(reader.ValueSpan.ToArray());
                    var slot = ParseSlot(value);
                    result.Add(slot);
                }
                else
                {
                    throw new FormatException($"Expected a string token type, but found '{reader.TokenType}' at position {reader.TokenStartIndex}");
                }
            }

            return result;
        }

        Regex pattern = new Regex(@"(?<starthours>\d{1,2}):(?<startminutes>\d{1,2})-(?<endhours>\d{1,2}):(?<endminutes>\d{1,2})");

        private Slot ParseSlot(string value)
        {            
            Match match = pattern.Match(value);
            if (!match.Success)
                throw new FormatException($"Expected a time string, but found '{value}'");

            return new Slot()
            {
                Start = new TimeSpan(int.Parse(match.Groups["starthours"].Value), int.Parse(match.Groups["startminutes"].Value), 0),
                End = new TimeSpan(int.Parse(match.Groups["endhours"].Value), int.Parse(match.Groups["endminutes"].Value), 0)
            };
        }

        public override void Write(Utf8JsonWriter writer, ScheduleDaySlots value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var slot in value)
            {
                string start = FormattableString.Invariant($"{slot.Start.Hours:0#}:{slot.Start.Minutes:0#}");
                string end = FormattableString.Invariant($"{slot.End.Hours:0#}:{slot.End.Minutes:0#}");
                writer.WriteStringValue($"{start}-{end}");
            }
            writer.WriteEndArray();
        }
    }
}
