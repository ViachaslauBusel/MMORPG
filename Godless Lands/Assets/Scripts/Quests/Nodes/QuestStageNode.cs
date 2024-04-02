using NodeEditor;
using NodeEditor.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeGroup("Quests")]
public class QuestStageNode : Node
{
    [Port("out")]
    private Node m_nextNode;

    [SerializeField]
    private string m_name;
    [SerializeField, Multiline]
    private string m_description;

    public Node Next => m_nextNode;
    public string Name => m_name;
    public string Description => m_description;
}
