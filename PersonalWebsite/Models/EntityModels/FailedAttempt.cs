using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalWebsite.Models
{
    public class FailedAttempt
    {
        public int FailedAttemptId { get; set; }

        public DateTime DateAttempted { get; set; }
        public string UsernameGiven { get; set; }
        public string IPAddress { get; set; }
    }
}