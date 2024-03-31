using Items;
using Recipes;
using UnityEngine;
using UnityEngine.UI;

namespace Machines
{
    public class SmelterRecipeCell : MonoBehaviour
    {
        private Image icon;
        private Text itemName;
        private Recipe recipe;
        private Button button;
        private SmelterWindow _smelterWindow;

        private void Awake()
        {

            icon = transform.Find("Icon").GetComponent<Image>();
            itemName = transform.Find("Name").GetComponent<Text>();
            button = GetComponent<Button>();
        }

        private void Start()
        {
            _smelterWindow = GetComponentInParent<SmelterWindow>();
            transform.localScale = Vector3.one;
        }

        public void Select()
        {
            button.interactable = false;
            _smelterWindow.SelectRecipe(recipe);
        }

        public void Reselect()
        {
            button.interactable = false;
        }

        public void SetRecipe(Recipe recipe)
        {
            this.recipe = recipe;
           //TODO PutItem(InventoryWindow.CreateItem(recipe.result));
        }

        public Recipe GetRecipe()
        {
            return recipe;
        }

        public void PutItem(ItemData item)
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