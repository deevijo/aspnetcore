using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewManagement.Models
{
    public class Candidate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime InterviewDate { get; set; }
        public string Number { get; set; }
        public string Skills { get; set; }
    }
}
