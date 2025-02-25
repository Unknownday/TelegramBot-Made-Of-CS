using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp5.MessageInfo
{
    public class Info
    {


        public class Update
        {
            [JsonPropertyName("update_id")]
            public long UpdateId { get; set; }

            [JsonPropertyName("message")]
            public Message Message { get; set; }
        }

        public class Message
        {
            [JsonPropertyName("message_id")]
            public long MessageId { get; set; }

            [JsonPropertyName("from")]
            public User From { get; set; }

            [JsonPropertyName("date")]
            public long Date { get; set; }

            [JsonPropertyName("chat")]
            public Chat Chat { get; set; }

            [JsonPropertyName("text")]
            public string Text { get; set; }
        }

        public class User
        {
            [JsonPropertyName("id")]
            public long Id { get; set; }

            [JsonPropertyName("is_bot")]
            public bool IsBot { get; set; }

            [JsonPropertyName("first_name")]
            public string FirstName { get; set; }

            [JsonPropertyName("username")]
            public string Username { get; set; }

            [JsonPropertyName("language_code")]
            public string LanguageCode { get; set; }
        }

        public class Chat
        {
            [JsonPropertyName("id")]
            public long Id { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("username")]
            public string Username { get; set; }

            [JsonPropertyName("first_name")]
            public string FirstName { get; set; }
        }
    }

}
