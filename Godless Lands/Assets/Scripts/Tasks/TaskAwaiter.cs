using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    internal class TaskAwaiter
    {
        private bool _isCompleted = false;
        private bool _result = false;

        public bool IsCompleted => _isCompleted;
        public bool Result => _result;

        public void SetResult(bool result)
        {
            _result = result;
            _isCompleted = true;
        }
    }
}
