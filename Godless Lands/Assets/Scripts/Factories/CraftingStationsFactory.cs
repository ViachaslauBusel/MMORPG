using Protocol.Data.Workbenches;
using UnityEngine;
using Zenject;

namespace Factories
{
    internal class CraftingStationsFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject smelterPref;
        [SerializeField]
        private GameObject grindstonePref;
        [SerializeField]
        private GameObject workbenchPref;
        [SerializeField]
        private GameObject tanneryPref;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject CreateStone(WorkbenchType workbenchType, Transform transform, Vector3 position)
        {
            var workbenchPrefab = workbenchType switch
            {
                WorkbenchType.Smelter => smelterPref,
                WorkbenchType.Grindstone => grindstonePref,
                WorkbenchType.Workbench => workbenchPref,
                WorkbenchType.Tannery => tanneryPref,
                _ => null
            };

            if (workbenchPrefab == null)
            {
                Debug.LogError($"Stone with id {workbenchType} not found");
                return null;
            }

            return _diContainer.InstantiatePrefab(workbenchPrefab, position, Quaternion.identity, transform);
        }
    }
}
