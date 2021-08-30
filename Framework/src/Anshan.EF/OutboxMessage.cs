using System;

namespace Anshan.EF
{
    public class OutboxMessage
    {
        private OutboxMessage()
        {
        }

        internal OutboxMessage(DateTime occurredOn, string type, string data)
        {
            Id = Guid.NewGuid();
            OccurredOn = occurredOn;
            Type = type;
            Data = data;
        }

        /// <summary>
        ///     Id of message.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        ///     Occurred on.
        /// </summary>
        public DateTime OccurredOn { get; }

        /// <summary>
        ///     Full name of message type.
        /// </summary>
        public string Type { get; }

        /// <summary>
        ///     Message data - serialzed to JSON.
        /// </summary>
        public string Data { get; }
    }
}