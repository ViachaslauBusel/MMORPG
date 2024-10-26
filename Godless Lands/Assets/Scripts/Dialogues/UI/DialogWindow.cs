using Dialogues;
using Dialogues.Data;
using Dialogues.Data.Nodes;
using Network.Object.Interaction;
using NodeEditor.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Windows;
using Zenject;

public class DialogWindow : Window
{
    [SerializeField]
    private Text _npcDialog;
    [SerializeField]
    private GameObject _playerDialogPrefab;
    private DiContainer _diContainer;
    private List<GameObject> _playerDialogue = new List<GameObject>();
    private bool _blockingControl = false;
    private DialogueNodeHandlerStorage _dialogueNodeHandlerStorage;
    private IInteractableObject _npc;

    [Inject]
    private void Construct(DiContainer diContainer, DialogueNodeHandlerStorage dialogueNodeHandlerStorage)
    {
        _diContainer = diContainer;
        _dialogueNodeHandlerStorage = dialogueNodeHandlerStorage;
    }

    public void OpenDialog(Node node)
    {
        if (_blockingControl) return;
        Clear();
        if (node == null)
        {
            Debug.LogError("Trying to open an empty dialog node");
            Close();
            return;
        }
        HandleNode(node);
    }

    private async void OpenNextDialogAsync(IExecutionNode executionNode)
    {
        _blockingControl = true;
        Node nextNode = null;
        if (_dialogueNodeHandlerStorage.TryGetExecutionHandler(executionNode, out IDialogExecutionNodeHandler handler))
        {
             nextNode = await handler.Execute(executionNode, _npc);
        }
        _blockingControl = false;
        HandleNode(nextNode);
    }

    private void HandleNode(Node node)
    {
        if(node == null) { return; }
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
                OpenNextDialogAsync(executionNode);
                return;
            case DialogueExitNode:
                Close();
                return;
                case ToStartNode toStartNode:
                HandleNode(toStartNode.ParentContainer.StartNode.Next);
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
            case PlayerDialogNode replicaNode:
                AddPlayerDialogue(replicaNode.Replica, replicaNode.Next);
                break;
                case IConditionNode conditionNode:
                if (_dialogueNodeHandlerStorage.TryGeConditionHandler(conditionNode, out IDialogConditionNodeHandler conditionHandler))
                {
                    Node nextNode = conditionHandler.CheckCondition(conditionNode);
                    if (nextNode != null)
                    {
                        SetPlayerReplica(nextNode);
                    }
                }
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

    public void Open(IInteractableObject npc)
    {
        _npc = npc;
        Open();
    }

    public void Clear()
    {
        _npcDialog.text = string.Empty;
        foreach(var obj in _playerDialogue) { Destroy(obj); }
        _playerDialogue.Clear();
    }
}
