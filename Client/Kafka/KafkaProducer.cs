using Confluent.Kafka;
using ECommerce.Olep.Schema;
using Utilities;

namespace Client.Kafka;
public class KafkaProducer
{

    private readonly string outputTopic;
    private readonly IProducer<Null, Checkout> producer;

    public static KafkaProducer Build()
    {
        var config = new ProducerConfig { BootstrapServers = Constants.kafkaService };
        var kafkaBuilder = new ProducerBuilder<Null, Checkout>(config).SetValueSerializer(new EventSerializer());

        return new KafkaProducer(kafkaBuilder.Build(), "checkout");
    }

    private KafkaProducer(IProducer<Null, Checkout> producer, string outputTopic)
    {
        this.producer = producer;
        this.outputTopic = outputTopic;
    }

    async Task Append(Checkout e)
    {
        // output the event to kafka (external service)
        // here we use the .NET kafka client implemented by Confluent
        await producer.ProduceAsync(outputTopic, new Message<Null, Checkout>
        {
            Timestamp = new Timestamp(Timestamp.UnixTimeEpoch, TimestampType.CreateTime),
            Value = e
        });
    }

}

