﻿using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogues.Data
{
    public interface IExecutionNode
    {
        Node Next { get; }

        void Execute();
    }
}
