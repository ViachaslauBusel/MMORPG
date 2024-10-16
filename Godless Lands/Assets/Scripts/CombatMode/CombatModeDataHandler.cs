﻿using Network.Object.Visualization;
using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.MSG.Game.CombatMode;
using UnityEngine;

namespace CombatMode
{
    public class CombatModeDataHandler : MonoBehaviour, INetworkDataHandler
    {
        private IVisualRepresentation _visualRepresentation;
        private Animator _animator;
        private bool _combatModeActive = false;

        public bool CombatModeActive => _combatModeActive;

        private void Awake()
        {
            _visualRepresentation = GetComponent<IVisualRepresentation>();

            _visualRepresentation.OnVisualObjectUpdated += OnVisualObjectUpdated;
        }

        private void OnVisualObjectUpdated(GameObject @object)
        {
           _animator = @object.GetComponent<Animator>();

            UpdateAnimatorArmedState();
        }

        public void UpdateData(IReplicationData updatedData)
        {
           CombotModeData data = (CombotModeData)updatedData;

            _combatModeActive = data.CombatMode;

            UpdateAnimatorArmedState();
        }


        private void UpdateAnimatorArmedState()
        {
            _animator?.SetBool("armed", _combatModeActive);
        }
    }
}
