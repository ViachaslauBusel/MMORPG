using Infrastructure;
using NodeEditor;
using NodeEditor.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/Quest Data", order = 1)]
[NodeGroup(group: "Quests")]

public class QuestData : ZenjectNodesContainer
{
    [SerializeField]
    private string m_name;
    [SerializeField]
    private string m_description;
    [SerializeField]
    private bool m_isRenderableInJournal = true;

    public string Name => m_name;
    public string Description => m_description;
    public bool IsRenderableInJournal => m_isRenderableInJournal;
}
