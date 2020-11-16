using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    private static ItemsManager Instance { get; set; }
    [SerializeField] ItemsContainer items;

    private void Awake()
    {
        Instance = this;
    }

    public static Item Create(int id) => Instance.items.GetDuplicateItem(id); 
}
