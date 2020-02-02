using ConsoleApp12.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12.Implementations
{
    public class Workflow: IWorkflow
    {
        private readonly Runner _taskRunner; 
        private readonly 

        public Workflow(Runner taskRunner)
        {
            _taskRunner = taskRunner;
        }

        public void Start()
        {

        }

        public void Add(IWorkItem workItem)
        {

        }
    }
}