using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plivo_MVC_Samples.Models
{
    public class CallAnswerResponseParameters
    {
        public string TotalCost
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the direction. Indicates the direction of the call. The direction will be outbound.
        /// </summary>
        /// <value>The direction.</value>
        public string Direction
        {
            get;
            set;
        }

        public string BillDuration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets from number. This is the phone number you specified as the caller ID.
        /// </summary>
        /// <value>From.</value>
        public string From
        {
            get;
            set;

        }

        public string CallerName
        {
            get;
            set;
        }

        public string HangupCause
        {
            get;
            set;
        }

        public string BillRate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets To number. This is the phone number you specified as the caller ID.
        /// </summary>
        /// <value>To.</value>
        public string To
        {
            get;
            set;
        }

        public string AnswerTime
        {
            get;
            set;
        }

        public string StartTime
        {
            get;
            set;
        }

        public string Duration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the call UUID. A unique identifier for this call.
        /// </summary>
        /// <value>The call UUID.</value>
        public string CallUUID
        {
            get;
            set;
        }
        public string EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the call status. Indicates the status of the call. The value is set to either completed ,busy, failed, timeout or no-answer.
        /// </summary>
        /// <value>The call status.</value>
        public string CallStatus
        {
            get;
            set;
        }

        public string Event
        {
            get;
            set;
        }
    }
}