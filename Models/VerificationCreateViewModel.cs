using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Models
{
    public class VerificationCreateViewModel
    {
        public Verification NewVerification { get; set; }

        public List<Verification> Verifications { get; set; }


        public VerificationCreateViewModel()
        {
            NewVerification = new Verification();
        }
    }
}
