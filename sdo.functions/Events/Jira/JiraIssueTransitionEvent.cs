using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sdo.functions;

namespace sdo.functions.Events.Jira
{
    internal class JiraIssueTransitionEvent : EventBase
    {
        public JiraIssue Issue { get; set; }
    }
}
