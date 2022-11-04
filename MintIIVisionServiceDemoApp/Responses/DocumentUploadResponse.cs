using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MintIIVisionServiceDemoApp.Responses
{
    public class DocumentUploadResponse: BaseResponse
    {
        /// <summary>
        /// File guid of the document that was uploaded
        /// </summary>
        public string FileGuid { get; set; }
    }    

    /// <summary>
    /// Details of a single document page as part of the document view response
    /// </summary>
    public class DocumentPage
    {
        [JsonIgnore]
        public int PageId { get; set; }

        /// <summary>
        /// A value indicating the file path in blob storage
        /// </summary>
        [JsonProperty("filepath")]
        public string Filepath { get; set; }

        /// <summary>
        /// A value indicating the file path in blob storage
        /// </summary>
        [JsonProperty("thumbnailPath")]
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// The page number against the document i.e. the first page will be 1 etc.
        /// </summary>
        [JsonProperty("pageNumber")]
        public Int16 PageNumber { get; set; }


        /// <summary>
        /// A value indicating the status for the page. Valid statuses are: Saved, Classified, Extracted, Processed, Errors
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// A value indicating the primary predited class for the page
        /// </summary>
        [JsonProperty("class")]
        public string PageClass { get; set; }

        /// <summary>
        /// The id of the primary predicted class for the page
        /// </summary>
        [JsonProperty("classId")]
        public int? PageClassId { get; set; }

        /// <summary>
        /// A value indicating the confidence of the primary predited class for the page
        /// </summary>
        [JsonProperty("confidence")]
        public double PageClassConfidence { get; set; }

        /// <summary>
        /// The extracted fields from the page (if performed)
        /// </summary>
        [JsonProperty("formFields")]
        public List<DocumentExtractorField> DocumentExtractorData { get; set; }

        /// <summary>
        /// The raw output from the forms extractor
        /// </summary>
        [JsonProperty("rawFormRecognizerJson")]
        public JToken RawFormRecognizerJson { get; set; }

        /// <summary>
        /// The OCR results from the page (if performed)
        /// </summary>
        [JsonProperty("rawOcrJson")]
        public JToken RawOcrJson { get; set; }

        /// <summary>
        /// The OCR text from the page (if performed)
        /// </summary>
        [JsonProperty("ocrText")]
        public string OcrText { get; set; }

        /// <summary>
        /// The human label associated to the page
        /// </summary>
        [JsonProperty("pageLabel")]
        public string PageLabel { get; set; }

        /// <summary>
        /// If true, the page has been tagged as having classification or extraction issues
        /// </summary>
        [JsonProperty("tagIssues")]
        public bool TagIssues { get; set; }

        /// <summary>
        /// The active-learning review status for the page. Valid statuses are: Unreviewed, ToReview, Approved, Updated
        /// </summary>
        [JsonProperty("reviewStatus")]
        public string ReviewStatus { get; set; }

        /// <summary>
        /// The active-learning review status for the page fields. Valid statuses are: Unreviewed, ToReview, ValidationErrors, AmbiguousFields, Approved, Updated
        /// </summary>
        [JsonProperty("fieldReviewStatus")]
        public string FieldReviewStatus { get; set; }

        /// <summary>
        /// If true, this page is of an unknown class
        /// </summary>
        [JsonProperty("unknownClass")]
        public bool? UnknownClass { get; set; }
    }


    public class BasicDocument
    {
        /// <summary>
        /// A value indicating the file guid of document uploaded
        /// </summary>
        [JsonProperty("fileGuid")]
        public string FileGuid { get; set; }

        /// <summary>
        /// A value indicating the URL for document uploaded
        /// </summary>
        [JsonProperty("documentUrl")]
        public string DocumentUrl { get; set; }
        /// <summary>
        /// A value indicating the file name of document uploaded
        /// </summary>
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        /// <summary>
        /// A value indicating the date on which document was uploaded
        /// </summary>
        [JsonProperty("uploadedDate")]
        public DateTime UploadedDate { get; set; }

        /// <summary>
        /// A value indicating the date on which document was last modified
        /// </summary>
        [JsonProperty("modifiedDateTime")]
        public DateTime ModifiedDateTime { get; set; }

        /// <summary>
        /// A value indicating the status of upload and process of document. Valid statuses are: 
        /// PreProcessing,UploadReceived,Uploaded,FailureProcessing,FailureOnUpload,FailureOnSplit,SentForProcessing,
        /// QueuedForProcessing, Processing,ProcessCompleted,ReadyForPostProcess
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// A value indicating the class of document
        /// </summary>
        [JsonProperty("class")]
        public string Class { get; set; }
        /// <summary>
        /// A value indicating the label of document
        /// </summary>
        [JsonProperty("documentLabel")]
        public string DocumentLabel { get; set; }
        /// <summary>
        /// A value indicating the number of pages in a document
        /// </summary>
        [JsonProperty("noOfPages")]
        public int NoOfPages { get; set; }

        /// <summary>
        /// FolderId
        /// </summary>
        [JsonProperty("folderId")]
        public int? FolderId { get; set; }
        /// <summary>
        /// Folder name
        /// </summary>
        [JsonProperty("folderName")]
        public string FolderName { get; set; }
    }

    /// <summary>
    /// A Class which is response for Document Extractor API
    /// </summary>
    public class DocumentExtractorField
    {
        /// <summary>
        /// A value indicating the label name
        /// </summary>
        [JsonProperty("labelname")]
        public string LabelName { get; set; }
        /// <summary>
        /// A value indicating label value
        /// </summary>
        [JsonProperty("labelvalue")]
        public string LabelValue { get; set; }
        /// <summary>
        /// A value indicating confidence of label value
        /// </summary>
        [JsonProperty("labelconfidence")]
        public float LabelConfidence { get; set; }


    }

    /// <summary>
    /// Response returned when view a document page
    /// </summary>
    public class DocumentPageViewResponse : DocumentPage
    {
        [JsonProperty("document")]
        public BasicDocument Document { get; set; }
        /// <summary>
        /// If the tenant has been setup to perform ocr only processing on a document
        /// </summary>
        [JsonProperty("ocrOnly")]
        public bool OcrOnly { get; set; }
        // <summary>
        /// A unique identifier generated per action
        /// </summary>
        /// <example>
        /// f5a7af36-6d02-4d35-a8fa-da8d5a1bd313
        /// </example>
        [JsonProperty("transactionId")]
        public Guid TransactionId { get; set; }
        /// <summary>
        /// The total number of milliseconds elapsed while performing the action
        /// </summary>
        /// <example>
        /// 345
        /// </example>
        [JsonProperty("elapsedMilliseconds")]
        public long ElapsedMilliseconds { get; set; }
        /// <summary>
        /// A flag indicating if the client is calling an obsolete version of the service
        /// </summary>
        [JsonProperty("isObsoleteServiceVersion")]
        public bool IsObsoleteServiceVersion { get; set; }
        /// <summary>
        /// Version of the api which has been called
        /// </summary>
        [JsonProperty("apiVersion")]
        public string ApiVersion { get; set; }

    }
}
