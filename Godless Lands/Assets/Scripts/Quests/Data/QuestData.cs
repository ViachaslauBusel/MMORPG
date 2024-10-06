using Infrastructure;
using Items;
using NodeEditor;
using NodeEditor.Data;
using ObjectRegistryEditor;
using System.Collections.Generic;
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
    [SerializeField]
    private List<ItemBundleLink> _reward;

    public int ID => _id;
    public string Name => _name;
    public string Description => _description;
    public bool IsRenderableInJournal => _isRenderableInJournal;
    public IReadOnlyCollection<ItemBundleLink> Reward => _reward;



    public Texture Preview => null;


    public void Initialize(int id)
    {
        if (_id != 0) Debug.LogError($"ID:{_id} already initialized -> {id}");
        _id = id;
    }
}
