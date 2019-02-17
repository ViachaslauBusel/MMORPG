using Recipes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Machine
{
    void Refresh();
    void SelectRecipe(Recipe select);
}
