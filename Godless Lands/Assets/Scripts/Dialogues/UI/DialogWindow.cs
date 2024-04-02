using Dialogues.Data;
using Dialogues.Data.Nodes;
using NodeEditor.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DialogWindow : MonoBehaviour
{
    [SerializeField]
    private Text _npcDialog;
    [SerializeField]
    private GameObject _playerDialogPrefab;
    private Canvas _canvas;
    private DiContainer _diContainer;
    private List<GameObject> _playerDialogue = new List<GameObject>();

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer; 
    }

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        Close();
    }

    public void OpenDialog(Node node)
    {
        Clear();
        if (node == null)
        {
            Debug.LogError("Trying to open an empty dialog node");
            Close();
            return;
        }
        switch (node)
        {
            case DialogNode dialogNode:
                //SetNpcReplica(dialogNode.NpcReplica);
                _npcDialog.text = dialogNode.NpcReplica;
                foreach (var playerReplica in dialogNode.PlayerReplica)
                {
                    SetPlayerReplica(playerReplica);
                }
                break;
            case IExecutionNode executionNode:
                executionNode.Execute();
                OpenDialog(executionNode.Next);
                return;
            case DialogueExitNode:
                Close();
                return;
            default:
                Debug.LogError($"Invalid node type:{node.GetType()}");
                break;
        }

        _canvas.enabled = true;
    }

    private void SetNpcReplica(Node node)
    {
        switch(node) 
        {
            case ReplicaNode replicaNode:
                _npcDialog.text = replicaNode.Replica;
                break;
            default:
                Debug.LogError($"Invalid node type:{node.GetType()}");
                break;
        }
    }

    private void SetPlayerReplica(Node node)
    {
        switch (node)
        {
            case ReplicaNode replicaNode:
                AddPlayerDialogue(replicaNode.Replica, replicaNode.Next);
                break;
                case IConditionNode conditionNode:
                if (conditionNode.CheckCondition())
                { SetPlayerReplica(conditionNode.Next); }
                break;
                case ToStartNode:
                if(node.ParentContainer.StartNode.Next is DialogNode dialogNode)
                {
                    foreach(var playerReplica in dialogNode.PlayerReplica)
                    {
                        SetPlayerReplica(playerReplica);
                    }
                }
                break;
            default:
                Debug.LogError($"Invalid node type:{node.GetType()}");
                break;
        }
    }

    private void AddPlayerDialogue(string dialogue, Node nextNode)
    {
        var singleDialogue = _diContainer.InstantiatePrefab(_playerDialogPrefab, _playerDialogPrefab.transform.parent).GetComponent<PlayerSingleDialog>();
        singleDialogue.gameObject.SetActive(true);
        _playerDialogue.Add(singleDialogue.gameObject);

        singleDialogue.SetDialog(dialogue, nextNode);
    }

    public void Close() 
    {
        _canvas.enabled = false;
    }

    public void Clear()
    {
        _npcDialog.text = string.Empty;
        foreach(var obj in _playerDialogue) { Destroy(obj); }
        _playerDialogue.Clear();
    }
}
