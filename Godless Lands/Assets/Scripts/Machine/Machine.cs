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
        void UpdateComponent(int index, Item item);
        void UpdateFuel(int index, Item item);
        void Hide();
    }
}
