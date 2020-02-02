using ConsoleApp12.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12.Implementations
{
    internal class WorkitemWrapper
    {
        public WorkitemWrapper(IWorkItem workItem, bool required)
        {
            Required = required;
            WorkItem = workItem;
        }

        public IWorkItem WorkItem { get; }

        public bool Required { get; }
    }
}
