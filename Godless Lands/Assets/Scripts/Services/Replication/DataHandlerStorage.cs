using Animation;
using Assets.Scripts.Physics.Dynamic;
using DynamicsObjects.TransformHandlers;
using NetworkObjectVisualization;
using NetworkObjectVisualization.Characters;
using Nickname;
using ObjectInteraction;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Animation;
using Protocol.Data.Replicated.ObjectInteraction;
using Protocol.Data.Replicated.Skins;
using Protocol.Data.Replicated.Transform;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Services.Replication
{
    public class DataHandlerStorage
    {
        private Dictionary<Type, Type> _handlers = new Dictionary<Type, Type>();
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        public DataHandlerStorage() 
        {
            _handlers.Add(typeof(TransformData), typeof(TransfromDataHandler));
            _handlers.Add(typeof(CharacterSkinData), typeof(CharacterViewDataHandler));
            _handlers.Add(typeof(DynamicObjectData), typeof(DynamicObjectDataHandler));
            _handlers.Add(typeof(TransformEvents), typeof(TransformEventsHandler));
            _handlers.Add(typeof(AnimationPlaybackBuffer), typeof(AnimationPlaybackBufferHandler));
            _handlers.Add(typeof(UnitName), typeof(UnitNameHandler));
            _handlers.Add(typeof(AnimationStateData), typeof(AnimationStateDataHandler));
            _handlers.Add(typeof(MonsterSkinData), typeof(MonsterViewDataHandler));
            _handlers.Add(typeof(InteractionObjectData), typeof(InteractionObjectDataHandler));
        }

        public INetworkDataHandler CreateDataHandler(GameObject gameObject, IReplicationData data)
        {
            Type dataType = data.GetType();

            if(_handlers.ContainsKey(dataType) == false) 
            {
                Debug.LogError($"[{dataType}]handler not found for specified data");
                return null;
            }

            Type handlerType = _handlers[dataType];
            return (INetworkDataHandler)_container.InstantiateComponent(handlerType, gameObject);
        }
    }
}