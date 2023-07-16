using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Sns.Publisher.Contracts;
using System.Text.Json;

namespace Sns.Publisher
{
    public sealed class EventSnsPublisher
    {
        private const string _topicTitle = "customers";
        private static AmazonSimpleNotificationServiceConfig _config = new AmazonSimpleNotificationServiceConfig
        {
            RegionEndpoint = RegionEndpoint.USEast1
        };
        private static AmazonSimpleNotificationServiceClient _awsSnS = new AmazonSimpleNotificationServiceClient(_config);

        public async static Task MainAsync()
        {
            Console.WriteLine("Iniciando a publicação da mensagem !");
            await PublishMessageInSnsQueueAsync();
        }
        #region Private Methods
        private async static Task PublishMessageInSnsQueueAsync()
        {
            try
            {
                #region New Customer Model Object
                var newCustomer = new CustomerCreated
                {
                    Id = Guid.NewGuid(),
                    Name = "Kaoê Ferreira",
                    GitHubUsername = "Gondlir",
                    Email = "batman@dc.com"
                };
                #endregion
                var topicArn = await _awsSnS.FindTopicAsync(_topicTitle)!;
                var publishRequest = new PublishRequest
                {
                    TopicArn = topicArn.TopicArn,
                    Message = JsonSerializer.Serialize(newCustomer),
                    MessageAttributes = new Dictionary<string, MessageAttributeValue>
                    {
                      {
                        "MessageType", new MessageAttributeValue
                        {
                            DataType = "String",
                            StringValue = nameof(CustomerCreated)
                        }
                      }
                    }
                };
                var response = await _awsSnS.PublishAsync(publishRequest);
            }
            catch (Exception ex)
            {
                // treat errors 
                throw;
            }
            
        }
        #endregion
    }
}
