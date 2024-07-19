using Network.Replication;
using Protocol.Data.Replicated;
using UnityEngine;
using Zenject;

namespace Network.Object.Dynamic.Handler
{
    internal class DynamicObjectDataHandler : MonoBehaviour, INetworkDataHandler
    {
        private SessionManagmentService m_sessionManagmentService;
        private DynamicObjectControllersFactory m_controllersFactory;
        private NetworkComponentsProvider m_componentsProvider;
        private bool m_remoteControl = false;

        [Inject]
        private void Construct(SessionManagmentService sessionManagmentService, DynamicObjectControllersFactory controllersFactory) 
        {
            m_sessionManagmentService = sessionManagmentService;
            m_controllersFactory = controllersFactory;
        }

        private void Awake()
        {
            m_componentsProvider = GetComponent<NetworkComponentsProvider>();
        }

        private void Start()
        {
            m_remoteControl = m_sessionManagmentService.CharacterObjectID != m_componentsProvider.NetworkGameObjectID;

            m_controllersFactory.CreateController(m_remoteControl ? ControllerType.AI : ControllerType.Player, transform);
        }

        public void UpdateData(IReplicationData updatedData)
        {
           
        }
    }
}
