﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqMessageTracking
{
    public class SingleMessagePublisher : BulkMessagePublisher
    {
        public async Task<IMessageState<T>> SendAsync<T>(string exchange,
            string routingKey,
            T message)
        {
            var messageTracker = await SendMessagesAsync<T>(exchange, routingKey, new List<T>() { message }, 1, TimeSpan.FromTicks(0));

            return messageTracker.GetMessageStates().First();
        }

        public async Task<IMessageState<T>> SendAsyncWithRetry<T>(string exchange,
            string routingKey,
            T message,
            byte retryLimit,
            short retryPeriodMs)
        {
            var messageTracker = await SendBatchWithRetryAsync<T>(exchange, routingKey, new List<T>() { message }, retryLimit, retryPeriodMs, 1, TimeSpan.FromTicks(0));

            return messageTracker.GetMessageStates().First();
        }
    }
}
