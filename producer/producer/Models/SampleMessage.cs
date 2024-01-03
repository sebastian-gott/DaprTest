using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace producer.Models
{
    public class SampleMessage
    {
        public Guid CorrelationId { get; set; }
        public Guid MessageId { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
        public string Sentiment { get; set; }
        public DateTime PreviousAppTimestamp { get; set; }
    }
}
