using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Recipes
{
    [System.Serializable]
    public class Recipe
    {
        public int id;
        public MachineUse use;
        public int result;
        public List<Piece> component;
        public List<Piece> fuel;

        public Recipe()
        {
            component = new List<Piece>();
            fuel = new List<Piece>();
        }
    }
}