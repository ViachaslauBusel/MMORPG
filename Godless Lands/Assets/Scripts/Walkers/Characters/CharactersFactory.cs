using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharactersFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject m_humanMalePrefab;
    private DiContainer m_container;

    [Inject]
    private void Construct(DiContainer container)
    {
        m_container = container;
    }

    public GameObject CreateHumanMale(Transform parent, Vector3 position)
    {
        Debug.Log($"spawn in {position}");
        return m_container.InstantiatePrefab(m_humanMalePrefab, position, Quaternion.identity, parent);
    }
}
