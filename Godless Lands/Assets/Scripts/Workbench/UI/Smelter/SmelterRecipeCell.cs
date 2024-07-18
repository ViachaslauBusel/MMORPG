using Items;
using Items.Data;
using Recipes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Workbench.UI.Smelter
{
    public class SmelterRecipeCell : MonoBehaviour
    {
        private Image _icon;
        private Text _itemName;
        private RecipeItemData _recipe;
        private Button _button;
        private SmelterWindow _smelterWindow;
        private ItemsFactory _itemsFactory;


        [Inject]
        private void Construct(ItemsFactory itemsFactory)
        {
            _itemsFactory = itemsFactory;
        }

        private void Awake()
        {

            _icon = transform.Find("Icon").GetComponent<Image>();
            _itemName = transform.Find("Name").GetComponent<Text>();
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _smelterWindow = GetComponentInParent<SmelterWindow>();
            transform.localScale = Vector3.one;
        }

        public void Select()
        {
            _button.interactable = false;
            _smelterWindow.SelectRecipe(_recipe);
        }

        public void Reselect()
        {
            _button.interactable = false;
        }

        public void SetRecipe(RecipeItemData recipe)
        {
            _recipe = recipe;
            PutItem(_itemsFactory.CreateItem(_recipe.Result.ID));
        }

        public RecipeItemData GetRecipe()
        {
            return _recipe;
        }

        public void PutItem(Item item)
        {
            if (item == null)
            {
                _icon.enabled = false;
                return;
            }

            _icon.enabled = true;
            _icon.sprite = Sprite.Create(item.Data.Icon, new Rect(0.0f, 0.0f, item.Data.Icon.width, item.Data.Icon.height), new Vector2(0.5f, 0.5f), 100.0f);
            _itemName.text = item.Data.Name;
        }

    }
}