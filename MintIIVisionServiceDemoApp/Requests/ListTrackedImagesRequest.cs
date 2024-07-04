
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VisionAiServiceDemoApp
{
    public class ListTrackedImagesRequest
    {       
        /// <summary>
        /// A value indicating Device parameter for the search method
        /// </summary>
        [JsonProperty("device")]
        public string SourceDevice { get; set; }

        /// <summary>
        /// If the view type is Reviewed then this is the start date of when the image was reviewed
        /// Otherwise this is the start date of when the image was created
        /// </summary>
        [JsonProperty("start")]
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// If the view type is Reviewed then this is the end date of when the image was reviewed
        /// Otherwise this is the end date of when the image was created
        /// </summary>
        [JsonProperty("end")]
        public DateTimeOffset? EndDate { get; set; }
        
        /// <summary>
        /// The classifier\detector class to use when filtering the tracked image list
        /// This can be the class display name or tag name
        /// </summary>
        [JsonProperty("class")]
        public Guid? ClassGuid { get; set; }

        /// <summary>
        /// The classifier\detector to use when filtering the tracked image list        
        /// </summary>
        [JsonProperty("aiModel")]
        public Guid? AiModelGuid { get; set; }

        /// <summary>
        /// A value indicating the review state of the image
        /// Will be ignored for view types: Unclassified, ToReview, Reviewed
        /// </summary>
        [JsonProperty("state")]
        public String? ReviewState { get; set; }
        /// <summary>
        /// The maximum number of rows to return
        /// </summary>
        [JsonProperty("max")]
        public int MaxRows { get; set; } = 50;
        /// <summary>
        /// The page to start the query from
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; set; } = 0;
       

        /// <summary>
        /// The camera id where the image has been captured
        /// </summary>
        [JsonProperty("camera")]
        public Guid? CameraGuid { get; set; }
        /// <summary>
        /// The region of interest name where the image has been captured
        /// </summary>
        [JsonProperty("roi")]
        public Guid? RoiGuid { get; set; }
        /// <summary>
        /// The region of interest name where the image has been captured
        /// </summary>
        [JsonProperty("tracker")]
        public Guid? TrackerGuid { get; set; }

        /// <summary>
        /// The view type to use when querying the tracked images
        /// This will default to AllImages if not provided
        /// </summary>
        public string ViewType { get; set; }
        
    }
}
