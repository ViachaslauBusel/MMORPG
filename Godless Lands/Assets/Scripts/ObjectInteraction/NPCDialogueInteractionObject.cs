﻿using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ObjectInteraction
{
    public class NPCDialogueInteractionObject : MonoBehaviour, IInteractableObject
    {
        [SerializeField] 
        private DialogData _dialogData;
        private NetworkComponentsProvider _networkComponentsProvider;
        private InteractableObjectsRegistry _interactableObjectsRegistry;
        private DialogWindow _dialogWindow;

        public int NetworkGameObjectID => _networkComponentsProvider.NetworkGameObjectID;

        public Vector3 Position => transform.position;

        [Inject]
        private void Construct(InteractableObjectsRegistry interactableObjectsRegistry, DialogWindow dialogWindow)
        {
            _interactableObjectsRegistry = interactableObjectsRegistry;
            _dialogWindow = dialogWindow;
        }

        private void Awake()
        {
            _networkComponentsProvider = GetComponentInParent<NetworkComponentsProvider>();
            _interactableObjectsRegistry.RegisterInteractableObject(_networkComponentsProvider.NetworkGameObjectID, this);
        }

        public void HandleInteraction()
        {
            _dialogWindow.OpenDialog(_dialogData.StartNode.Next);
        }

        private void OnDestroy()
        {
            _interactableObjectsRegistry.UnregisterInteractableObject(_networkComponentsProvider.NetworkGameObjectID);
        }
    }
}
