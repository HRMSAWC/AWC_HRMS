using System;
using System.Collections.Generic;

namespace AWC_HRMS.Models
{
    public partial class LinkGenerationTable
    {
        public int CandidateId { get; set; }
        public string? CandidateName { get; set; }
        public string? CandidateEmail { get; set; }
        public string? VerificationCode { get; set; }
        public string? CandidateContactNumber { get; set; }
        public string? Link { get; set; }
        public DateTime? LinkExpiration { get; set; }
        public string? LinkStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
