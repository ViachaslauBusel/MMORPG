﻿using System;
using UnityEngine;
using Zenject;

namespace Network.Object.Dynamic
{
    internal class DynamicObjectControllersFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_playerController;
        [SerializeField]
        private GameObject m_aiController;
        private DiContainer m_container;

        [Inject]
        private void Construct(DiContainer container)
        {
            m_container = container;
        }

        public GameObject CreateController(ControllerType controllerType, Transform parent) => controllerType switch
        {
            ControllerType.Player => m_container.InstantiatePrefab(m_playerController, parent),
            ControllerType.AI => m_container.InstantiatePrefab(m_aiController, parent),
            _ => throw new ArgumentException()
        };
    }

    public enum ControllerType
    {
        Player,
        AI,
    }
}
