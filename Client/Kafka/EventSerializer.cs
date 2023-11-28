using Confluent.Kafka;
using ECommerce.Olep.Schema;
using MessagePack;

namespace Client.Kafka
{
    public class EventSerializer : ISerializer<Checkout>, IDeserializer<Checkout>
    {
        public byte[] Serialize(Checkout e, SerializationContext _)
        {
            var data = MessagePackSerializer.Serialize(e);
            //Console.WriteLine($"Serialize event: ts = {e.timestamp}, type = {e.type}, content size = {e.content.Length}, total size = {data.Length}");
            return data;
        }


        public Checkout Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext _)
        {
            if (isNull) return null;
            var e = MessagePackSerializer.Deserialize<Checkout>(data.ToArray());
            //Console.WriteLine($"Deserialize event: total size = {data.Length}, ts = {e.timestamp}, type = {e.type}, content size = {e.content.Length}");
            return e;
        }
    }
}
