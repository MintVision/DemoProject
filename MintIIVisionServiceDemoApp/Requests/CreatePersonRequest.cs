using System;
using System.Collections.Generic;
using System.Text;

namespace MintIIVisionServiceDemoApp.Requests
{
    public class CreatePersonRequest
    {
        public string SaIdNumberOrUniqueNumber { get; set; }
        public PersonProperties[] Properties { get; set; }
        public string AcceptedTermsAndConditionsText { get; set; }
        public string Name { get; set; }
        public string NotificationEmailAddress { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TelephoneNumber { get; set; }
    }

    public class PersonProperties
    {
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string FriendlyDescription { get; set; }
        public string Type { get; set; }
        public string ParsedValue { get; set; }
        public bool IsHidden { get; set; }
        public bool IsEncrypted { get; set; }
    }
}
