using Network.Object.Visualization;
using OpenWorld;
using UnityEngine;
using Zenject;

namespace OpenWorldLegacy
{
    public class MapActivator : MonoBehaviour
    {
        private IVisualRepresentation m_skinObjectHolder;
        private MapLoader m_mapLoader;

        [Inject]
        private void Construct(MapLoader mapLoader)
        {
            m_mapLoader = mapLoader;
        }

        private void Awake()
        {
            m_skinObjectHolder = GetComponentInParent<IVisualRepresentation>();
        }

        private void Start()
        {
            m_skinObjectHolder.OnVisualObjectUpdated += SetMapTrackingTransform;
            SetMapTrackingTransform(m_skinObjectHolder.VisualObject);
        }

        private void SetMapTrackingTransform(GameObject @object)
        {
            if(@object != null)
            {
                m_mapLoader.SetTarget(@object.transform);
            }
        }
    }
}
