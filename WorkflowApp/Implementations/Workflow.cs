﻿using WorkflowApp.Interfaces;
using WorkflowApp.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WorkflowApp.Implementations
{
    internal class Workflow: IWorkflow
    {
        private readonly Runner _taskRunner;
        private readonly List<Node> _currentNodes;
        private Node _head;
        private Timer _timer;

        public Workflow(Runner taskRunner)
        {
            _taskRunner = taskRunner;
            _currentNodes = new List<Node>();
        }

        public void SetStart(IWorkItem workItem)
        {
            if (_head != null)
                _head = new Node(new WorkitemWrapper(workItem, false));
        }

        public void AddContinuation(IWorkItem workItem, IWorkItem[] continuation, bool needWait)
        {
            var node = _head.SearchNode(workItem);
            if (node == null)
                throw new ArgumentOutOfRangeException($"workitem doest not exit");

            foreach (var i in continuation)
                node.AddNext(new WorkitemWrapper(i, needWait));
        }

        public void Start()
        {
            if (_timer == null)
            {
                Execute(_currentNodes.First().Value);
                _timer = new Timer(s => Run(), null, 0, 1);
            }
        }

        public bool Completed()
        {
            return !_currentNodes.Any();
        }

        private void Run()
        {
            if (Completed())
            {
                _timer.Dispose();
                return;
            }

            foreach(var node in _currentNodes.ToArray())
            {
                var next = node.Next;
                if (_taskRunner.Competed(node.Value.WorkItem))
                {
                    _currentNodes.Remove(node);
                    _currentNodes.AddRange(node.Next);
                    Execute(node.Next.Select(n => n.Value).ToArray());
                }
                Execute(node.Next.Where(n => !n.Value.WaitPrev).Select(n => n.Value).ToArray());
            }
        }

        private void Execute(params WorkitemWrapper[] workItems)
        {
            foreach (var item in workItems)
                if (!item.Started)
                {
                    item.Started = true;
                    _taskRunner.Enqueue(item.WorkItem);
                }
        }
    }
}