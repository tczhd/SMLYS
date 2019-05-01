using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.Infrastructure.Configuration.Email
{
    public class SendGridOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string FromEmail { get; set; }
        public string FromFullName { get; set; }
    }
}
