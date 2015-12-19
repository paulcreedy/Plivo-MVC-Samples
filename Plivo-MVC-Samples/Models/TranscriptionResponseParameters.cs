// ***********************************************************************
// Assembly         : Plivo_MVC_Samples
// Author           : Paul
// Created          : 12-17-2015
//
// Last Modified By : Paul
// Last Modified On : 12-17-2015
// ***********************************************************************
// <copyright file="TranscriptionRequestParameters.cs" company="">
//     Copyright © Paul Creedy 2015
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Plivo_MVC_Samples.Models
{
    /// <summary>
    /// Class TranscriptionRequestParameters.
    /// </summary>
    public class TranscriptionResponseParameters
    {
        /// <summary>
        /// Gets or sets the transcribed text of the recording
        /// </summary>
        /// <value>The transcription.</value>
        public string transcription
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the credit deducted for the transcription
        /// </summary>
        /// <value>The transcription_charge.</value>
        public string transcription_charge
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the duration in seconds of the recording.
        /// </summary>
        /// <value>The duration.</value>
        public string duration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the call UUID of the call which was transcribed.
        /// </summary>
        /// <value>The call_uuid.</value>
        public string call_uuid
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rate of the transcription per minute
        /// </summary>
        /// <value>The transcription_rate.</value>
        public string transcription_rate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Recording ID of the recording which was transcribed.
        /// </summary>
        /// <value>The recording_id.</value>
        public string recording_id
        {
            get;
            set;
        }
    }
}