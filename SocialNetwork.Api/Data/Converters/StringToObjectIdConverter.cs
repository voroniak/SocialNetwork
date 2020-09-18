//using MongoDB.Bson;
//using MongoDB.Bson.IO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.Json;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;

//namespace SocialNetwork.Api.Data.Converters
//{
//    public class StringToObjectIdConverter : JsonConverter
//    {
//        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//        {
//            serializer.Serialize( value.ToString(), writer);

//        }

//        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//        {
//            throw new NotImplementedException();
//        }

//        public override bool CanConvert(Type objectType)
//        {
//            return typeof(ObjectId).IsAssignableFrom(objectType);
//            //return true;
//        }
//    }
//}
