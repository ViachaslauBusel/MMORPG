using Animation;
using Assets.Scripts.Physics.Dynamic;
using DynamicsObjects.TransformHandlers;
using Nickname;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Animation;
using Protocol.Data.Replicated.Skins;
using Protocol.Data.Replicated.Transform;
using Skins;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Services.Replication
{
    public class DataHandlerStorage
    {
        private Dictionary<Type, Type> m_handlers = new Dictionary<Type, Type>();
        private DiContainer m_container;

        [Inject]
        private void Construct(DiContainer container)
        {
            m_container = container;
        }

        public DataHandlerStorage() 
        {
            m_handlers.Add(typeof(TransformData), typeof(TransfromDataHandler));
            m_handlers.Add(typeof(CharacterSkinData), typeof(CharacterViewDataHandler));
            m_handlers.Add(typeof(DynamicObjectData), typeof(DynamicObjectDataHandler));
            m_handlers.Add(typeof(TransformEvents), typeof(TransformEventsHandler));
            m_handlers.Add(typeof(AnimationPlaybackBuffer), typeof(AnimationPlaybackBufferHandler));
            m_handlers.Add(typeof(UnitName), typeof(UnitNameHandler));
            m_handlers.Add(typeof(AnimationStateData), typeof(AnimationStateDataHandler));
        }

        public INetworkDataHandler CreateDataHandler(GameObject gameObject, IReplicationData data)
        {
            Type dataType = data.GetType();

            if(!m_handlers.ContainsKey(dataType)) 
            {
                Debug.LogError($"[{dataType}]handler not found for specified data");
                return null;
            }

            Type handlerType = m_handlers[dataType];
            return (INetworkDataHandler)m_container.InstantiateComponent(handlerType, gameObject);
        }
    }
}