using Items;
using Recipes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Machines
{
    public class RecipeCell : MonoBehaviour
    {
        private Image icon;
        private Text itemName;
        private Recipe recipe;
        private Button button;
        private Machine machine;

        private void Awake()
        {

            icon = transform.Find("Icon").GetComponent<Image>();
            itemName = transform.Find("Name").GetComponent<Text>();
            button = GetComponent<Button>();
        }
        private void Start()
        {
            machine = GetComponentInParent<Machine>();
            transform.localScale = Vector3.one;
        }
        public void Select()
        {
            button.interactable = false;
            machine.SelectRecipe(recipe);
        }
        public void Reselect()
        {
            button.interactable = false;
        }

        public void SetRecipe(Recipe recipe)
        {
            this.recipe = recipe;
            PutItem(Inventory.GetItem(recipe.result));
        }

        public Recipe GetRecipe()
        {
            return recipe;
        }

        public void PutItem(Item item)
        {
            if (item == null)
            {
                icon.enabled = false;
                return;
            }
            icon.enabled = true;
            icon.sprite = Sprite.Create(item.texture, new Rect(0.0f, 0.0f, item.texture.width, item.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            itemName.text = item.nameItem;
        }

    }
}