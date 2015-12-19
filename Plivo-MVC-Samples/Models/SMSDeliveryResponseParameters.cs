using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plivo_MVC_Samples.Models
{
    public class SMSDeliveryResponseParameters
    {
        public string UUID
        {
            get;
            set;
        }

        public string From
        {
            get;
            set;
        }

        public string To
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string ParentMessageUUID
        {
            get;
            set;
        }

        public string PartInfo
        {
            get;
            set;       
        }
    }
}