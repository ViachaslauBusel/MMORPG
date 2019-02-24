using Items;
using Recipes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Machines
{
    public interface Machine
    {
        void Refresh();
        void SelectRecipe(Recipe select);
        void Open();
        void PutComponent(int index, Item item, int count);
        void PutFuel(int index, Item item, int count);
        void Hide();
    }
}
