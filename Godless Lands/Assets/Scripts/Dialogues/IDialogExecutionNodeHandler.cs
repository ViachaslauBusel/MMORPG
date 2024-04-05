﻿using Cysharp.Threading.Tasks;
using Dialogues.Data;
using Dialogues.Data.Nodes;
using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogues
{
    internal interface IDialogExecutionNodeHandler
    {
        UniTask<Node> Execute(IExecutionNode executionNode);
    }
}