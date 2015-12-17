namespace Plivo_MVC_Samples.Models
{
    /// <summary>
    /// Class RecordRequestParameters.
    /// </summary>
    public class RecordRequestParameters
    {
        /// <summary>
        /// Gets or sets the Complete path to the recorded file URL.
        /// </summary>
        /// <value>The record URL.</value>
        public string RecordUrl
        {
            get;
            set;
        }

        /// <summary>
        /// If set, Gets or sets the digits pressed to stop the record.
        /// </summary>
        /// <value>The digits.</value>
        public string Digits
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Duration of recording in seconds.
        /// </summary>
        /// <value>The duration of the recording.</value>
        public string RecordingDuration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Duration of recording in milliseconds
        /// </summary>
        /// <value>The recording duration ms.</value>
        public string RecordingDurationMs
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the recording started (epoch time UTC) in milliseconds.
        /// </summary>
        /// <value>The recording start ms.</value>
        public string RecordingStartMs
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the recording ended (epoch time UTC) in milliseconds.
        /// </summary>
        /// <value>The recording end ms.</value>
        public string RecordingEndMs
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Recording ID of the file.
        /// </summary>
        /// <value>The recording identifier.</value>
        public string RecordingID
        {
            get;
            set;
        }
    }
}