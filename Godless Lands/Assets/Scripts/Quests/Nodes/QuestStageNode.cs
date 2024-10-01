using NodeEditor;
using NodeEditor.Data;
using Protocol.Data.Quests;
using Protocol.Data.Quests.Nodes;
using Quests.Export;
using UnityEngine;

[NodeGroup("Quests")]
public class QuestStageNode : Node, IExportableNode
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

    public QuestSNode ToServerData()
    {
        return new QuestStageSNode(ID, NextNodeId);
    }
}
