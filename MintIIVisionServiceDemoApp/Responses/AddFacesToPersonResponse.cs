using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MintIIVisionServiceDemoApp.Responses
{
    public class AddFacesToPersonResponse: BaseResponse
    {
        /// <summary>
        /// The unique face id generated
        /// </summary>
        public string FaceId { get; set; }
        /// <summary>
        /// The person registration percentage
        /// </summary>
        public decimal RegistrationPercentage { get; set; }
        /// <summary>
        /// which head poses are still missing for this person
        /// </summary>
        public HeadPoseTypes[] Missingheadposes { get; set; }
        /// <summary>
        /// The head pose information for the added face
        /// </summary>
        public Headpose AddedHeadPose { get; set; }
        /// <summary>
        /// The blur information for the added face
        /// </summary>
        public Blur AddedFaceBlur { get; set; }
        /// <summary>
        /// Data indicating if the added face is a match to the person image in home affairs
        /// </summary>
        public AddFaceMatchConfidence HomeAffairsMatch { get; set; }
        /// <summary>
        /// Data indicating if the added face is a match to the any identity documents linked to the person
        /// </summary>
        public List<AddFaceIdentityDocumentMatchConfidence> IdentityDocumentMatch { get; set; }
        /// <summary>
        /// Data indicating if the added face is a match to the person
        /// </summary>
        public AddFaceMatchConfidence PersonMatch { get; set; }
        /// <summary>
        /// A list of warning generated when matching the face to home affairs, person documents
        /// and previously added person faces
        /// </summary>
        public List<Warning> FaceMatchWarnings = new List<Warning>();
 
    }

    public class Warning
    {
        /// <summary>
        /// The code of the warning
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// The warning message
        /// </summary>
        public string Message { get; set; }
    }

    public class AddFaceMatchConfidence
    {
        /// <summary>
        /// A flag indicating if the faces are identical
        /// </summary>
        public bool IsIdentical { get; set; }
        /// <summary>
        /// How confident we are that the two faces are for the same person
        /// </summary>        
        /// <example>
        /// 0.64
        /// </example>
        public decimal Confidence { get; set; }
        /// <summary>
        /// The confidence as a percentage
        /// </summary>
        /// <example>
        /// 64
        /// </example>
        public decimal ConfidencePercentage { get { return Confidence * 100; } }
    }
    /// <summary>
    /// The confidence when matching the new person face to the person identity document
    /// </summary>
    public class AddFaceIdentityDocumentMatchConfidence : AddFaceMatchConfidence
    {
        /// <summary>
        /// A description of the document type
        /// Options: IDBook, DriversFront, DriversBack, IDBack, IDFront, Passport, NotFound
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public IdDocumentTypes DocumentType { get; set; }        
        public string DocumentUrl { get; set; }

    }
    /// <summary>
    /// Face is blurry or not
    /// </summary>
    public class Blur
    {
        /// <summary>
        /// The level of blur
        /// </summary>
        /// <example>
        /// Low, Medium or High
        /// </example>
        public string BlurLevel { get; set; }
        /// <summary>
        /// Number indicating how blurry the image is, the larger the blurier
        /// </summary>
        /// <example>
        /// 0.52
        /// </example>
        public decimal Value { get; set; }
    }
    public class Headpose
    {
        /// <summary>        
        /// </summary>
        public decimal Roll { get; set; }
        /// <summary>        
        /// </summary>
        public decimal Yaw { get; set; }
        /// <summary>        
        /// </summary>
        public decimal Pitch { get; set; }
        /// <summary>
        /// A description of the head pose
        /// </summary>
        /// <example>
        /// Right, Left, Down, Up, Straight, TiltLeft or TiltRight
        /// </example>
        [JsonConverter(typeof(StringEnumConverter))]
        public HeadPoseTypes HeadPoseDescription { get; set; }
    }

    /// <summary>
    /// Type of headposes
    /// </summary>
    public enum HeadPoseTypes
    {
        /// <summary>
        /// Looking right
        /// </summary>
        Right,
        /// <summary>
        /// Looking left
        /// </summary>
        Left,
        /// <summary>
        /// Looking down
        /// </summary>
        Down,
        /// <summary>
        /// Looking up
        /// </summary>
        Up,
        /// <summary>
        /// Looking straight ahead
        /// </summary>
        Straight,
        /// <summary>
        /// Head is tilted left
        /// </summary>
        TiltLeft,
        /// <summary>
        /// Head is tiled right
        /// </summary>
        TiltRight,
        /// <summary>
        /// Head pose could not be found
        /// </summary>
        NotFound,
        /// <summary>
        /// Head is tiled right
        /// </summary>
        [Obsolete]
        SlideLeft,
        /// <summary>
        /// Head pose could not be found
        /// </summary>
        [Obsolete]
        SlideRight,
    }

    /// <summary>
    /// A list of document types
    /// </summary>
    public enum IdDocumentTypes
    {
        /// <summary>
        /// South African Id Book
        /// </summary>
        IDBook,
        /// <summary>
        /// South African drivers license front
        /// </summary>
        DriversFront,
        /// <summary>
        /// South African drivers license back
        /// </summary>
        DriversBack,
        /// <summary>
        /// South African Id card back
        /// </summary>
        IDBack,
        /// <summary>
        /// South African Id card front
        /// </summary>
        IDFront,
        /// <summary>
        /// South African passport
        /// </summary>
        Passport,
        /// <summary>
        /// The document type cannot be found
        /// </summary>
        NotFound
    }
}
