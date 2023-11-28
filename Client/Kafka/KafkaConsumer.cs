using Confluent.Kafka;
using ECommerce.Olep.Schema;
using Utilities;

namespace Client.Kafka;

public class KafkaConsumer
{
    private readonly string topic;
    private readonly IConsumer<Null, Checkout> consumer;

    public static KafkaConsumer Build()
    {
        var config = new ConsumerConfig { 
            BootstrapServers = Constants.kafkaService,
            // Disable auto-committing of offsets.
            EnableAutoCommit = false
        };
        var kafkaBuilder = new ConsumerBuilder<Null, Checkout>(config).SetValueDeserializer(new EventSerializer());
        return new KafkaConsumer(kafkaBuilder.Build(), "checkout");
    }


    public KafkaConsumer(IConsumer<Null, Checkout> consumer, string topic)
    {
        this.consumer = consumer;
        this.topic = topic;
    }

    public void SubscribeAndConsume(CancellationToken cancellationToken)
    {
        this.consumer.Subscribe(topic);

        // TODO implement the consume loop
        while (!cancellationToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume();

            // handle consumed message.
            // ...
        }

    }

}

