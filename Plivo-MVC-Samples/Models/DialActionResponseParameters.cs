// ***********************************************************************
// Assembly         : Plivo_MVC_Samples
// Author           : Paul
// Created          : 12-18-2015
//
// Last Modified By : Paul
// Last Modified On : 12-18-2015
// ***********************************************************************
// <copyright file="DialStatusResponseParameters.cs" company="">
//     Copyright © Paul Creedy 2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plivo_MVC_Samples.Models
{
    /// <summary>
    /// Data model to hold parameters that are sent to the action URL after Dial is completed.
    /// </summary>
    public class DialStatusResponseParameters
    {
        /// <summary>
        /// This indicates if the <Dial> attempt rang or not. It can be true or false.
        /// </summary>
        /// <value>The dial ring status.</value>
        public string DialRingStatus
        {
            get;
            set;
        }

        /// <summary>
        /// The standard telephony hangup cause.
        /// </summary>
        /// <value>The dial hangup cause.</value>
        public string DialHangupCause
        {
            get;
            set;
        }

        /// <summary>
        /// Status of the dial.Can be completed, busy, failed, timeout or no-answer.
        /// </summary>
        /// <value>The dial status.</value>
        public string DialStatus
        {
            get;
            set;
        }

        /// <summary>
        /// CallUUID of A leg.
        /// </summary>
        /// <value>The dial a leg UUID.</value>
        public string DialALegUUID
        {
            get;
            set;
        }

        /// <summary>
        /// CallUUID of B leg. Empty if nobody answers.
        /// </summary>
        /// <value>The dial b leg UUID.</value>
        public string DialBLegUUID
        {
            get;
            set;
        }
    }
}