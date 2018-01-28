using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarTracker.Web.Configuration
{
    public class BuildInformation
    {
        public long BuildDate { get; set; }
        public string GitBranch { get; set; }
        public int GitRevision { get; set; }
        public string GitShortHash { get; set; }
        public string GitLongHash { get; set; }
        public int BuildNumber { get; set; }
    }
}
