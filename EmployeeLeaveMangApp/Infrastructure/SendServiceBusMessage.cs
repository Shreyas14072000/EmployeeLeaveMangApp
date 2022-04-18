using Azure.Messaging.ServiceBus;
using EmployeeLeaveMangApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeLeaveMangApp.Infrastructure
{
    public class SendServiceBusMessage
    {
        private readonly ILogger _logger;

        public IConfiguration _configuration;

        public ServiceBusClient _client;
        public ServiceBusSender _clientSender;

        public SendServiceBusMessage(IConfiguration _configuration, ILogger<SendServiceBusMessage> logger)
        {
            _logger = logger;
            var _serviceBusConnectionString = _configuration["ServiceBusConnectionString"];
            string queueName = _configuration["serviceBusQueueName"];
            _client = new ServiceBusClient(_serviceBusConnectionString);
            _clientSender = _client.CreateSender(queueName);
        }

        public async Task sendServiceBusMessage(ServiceBusMessageData serviceBusMessage)
        {
            var messagePayload = JsonSerializer.Serialize(serviceBusMessage);
            ServiceBusMessage message = new ServiceBusMessage(messagePayload);
            try
            {
                await _clientSender.SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
