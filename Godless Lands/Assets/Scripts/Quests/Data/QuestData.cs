using Infrastructure;
using NodeEditor;
using NodeEditor.Data;
using ObjectRegistryEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/Quest Data", order = 1)]
[NodeGroup(group: "Quests")]

public class QuestData : NodesContainer, IDataObject
{
    [SerializeField, HideInInspector]
    private int _id;
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _description;
    [SerializeField]
    private bool _isRenderableInJournal = true;

    public int ID => _id;
    public string Name => _name;
    public string Description => _description;
    public bool IsRenderableInJournal => _isRenderableInJournal;

    

    public Texture Preview => null;


    public void Initialize(int id)
    {
        if (_id != 0) Debug.LogError($"ID:{_id} already initialized -> {id}");
        _id = id;
    }
}
