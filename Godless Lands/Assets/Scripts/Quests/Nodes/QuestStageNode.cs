using NodeEditor;
using NodeEditor.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeGroup("Quests")]
public class QuestStageNode : Node
{
    [Port("out")]
    private Node _nextNode;

    [SerializeField]
    private string _name;
    [SerializeField, Multiline]
    private string _description;


    public Node NextNode => _nextNode;
    public int NextNodeId => _nextNode != null ? _nextNode.ID : 0;
    public string Name => _name;
    public string Description => _description;
}
