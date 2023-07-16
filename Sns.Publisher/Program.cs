// See https://aka.ms/new-console-template for more information
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.SimpleNotificationService.Util;
using Sns.Publisher.Contracts;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var newCustomer = new CustomerCreated
{
    Id = Guid.NewGuid(),
    Name = "Kaoê Ferreira",
    GitHubUsername = "Gondlir",
    Email = "batman@dc.com"
};
//var credentials = new BasicAWSCredentials();
var snsClient = new AmazonSimpleNotificationServiceClient();
var topicArn = await snsClient.FindTopicAsync("customers");

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

var response = await snsClient.PublishAsync(publishRequest);

    