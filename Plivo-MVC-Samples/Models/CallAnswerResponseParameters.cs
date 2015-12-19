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