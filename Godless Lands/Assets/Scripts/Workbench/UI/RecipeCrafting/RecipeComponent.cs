using Inventory;
using Items;
using Recipes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Workbench.UI.RecipeCrafting
{
    public class RecipeComponent : MonoBehaviour
    {
        [SerializeField]
        private Text _expandTxt;
        [SerializeField]
        private Text _countTxt;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Text _nameTxt;
        [SerializeField]
        private LayoutElement _element;
        [SerializeField]
        private RectTransform _pointVertical;
        [SerializeField]
        private RectTransform _pointHorizontal;
        private bool _expand = false;
        private Item _item;
        private int _allCount;
        private Recipe _nextRecipe;
        private RecipeCraftingWindow _craftingWindow;
        private List<RecipeComponent> _children;
        private bool _child = false;
        private InventoryModel _inventory;
        private ItemsFactory _itemsFactory;
        private RecipesDataHolder _recipesDataHolder;
        private bool _active = false;

        [Inject]
        public void Construct(InventoryModel inventory, ItemsFactory itemsFactory, RecipesDataHolder recipesDataHolder)
        {
            _inventory = inventory;
            _itemsFactory = itemsFactory;
            _recipesDataHolder = recipesDataHolder;
        }

        public void Start()
        {
            if (_active) return;
            _children = new List<RecipeComponent>();
            _craftingWindow = GetComponentInParent<RecipeCraftingWindow>();
            _active = true;

            transform.localScale = Vector3.one;
        }

        public bool IsChild()
        {
            return _child;
        }

        // Returns the difference in Y between this and the last child component, if there are no child components returns 0
        public float GetVertical()
        {
            if (_children.Count < 1 || !_children[_children.Count - 1]._active)
                return 0.0f;

            // Return the difference in Y position between this component and the last child component
            return _pointVertical.position.y - _children[_children.Count - 1].GetPoint();
        }

        // Returns the Y position of this element
        public float GetPoint()
        {
            return _pointHorizontal.position.y;
        }

        public Item GetItem()
        {
            return _item;
        }
        public int GetCount()
        {
            return _allCount;
        }
        public void Expand() 
        {
            _expand = !_expand;
            if (_nextRecipe == null) return;
            if (_expand) { _expandTxt.text = "-"; CreateChilds(); }
            else { _expandTxt.text = "+"; DestroyChilds(); }
        }

        public void Destroy()
        {
            DestroyChilds();
            Destroy(gameObject);
        }

        private void DestroyChilds()
        {
            foreach(RecipeComponent component in _children)
            {
                component.Destroy();
            }
            _children.Clear();
        }

        private void CreateChilds()
        {
            int index = transform.GetSiblingIndex();
            foreach (Piece piece in _nextRecipe.component)
            {
                GameObject obj = _craftingWindow.CreatePieceField();
                obj.transform.SetParent(transform.parent);
                obj.transform.SetSiblingIndex(++index);
                RecipeComponent component = obj.GetComponent<RecipeComponent>();
                component.SetPiece(piece, _element);
                _children.Add(component);
            }
            foreach (Piece piece in _nextRecipe.fuel)
            {
                GameObject obj = _craftingWindow.CreatePieceField();
                obj.transform.SetParent(transform.parent);
                obj.transform.SetSiblingIndex(++index);
                RecipeComponent component = obj.GetComponent<RecipeComponent>();
                component.SetPiece(piece,_element, true);
                _children.Add(component);
            }
        }

        public void SetPiece(Piece piece,LayoutElement parenlayout = null, bool fuel = false)
        {
            _item = _itemsFactory.CreateItem(piece.ID);
            if (parenlayout != null)// If there is a child
            {
                _element.minWidth = parenlayout.minWidth + 45.0f;
                _child = true;
            }
            _nextRecipe = _recipesDataHolder.GetRecipeByResultItemId(piece.ID);// Check if this component consists of components

            if (_nextRecipe != null)// If it consists
                _expandTxt.text = "+";

            _allCount = _inventory.GetItemCountByItemId(piece.ID);
            int itemCount = piece.count;
            if (itemCount != 0)
                 _countTxt.text = " (" + _allCount + "/" + itemCount + ") ";// The amount of items needed to create the previous component, the amount in the backpack
            else _countTxt.text = " ";

           
            
            _icon.sprite = Sprite.Create(_item.Data.texture, new Rect(0.0f, 0.0f, _item.Data.texture.width, _item.Data.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            _nameTxt.text = _item.Data.nameItem;

            if(fuel) _nameTxt.text += " (fuel)";

            if (_nextRecipe != null)
            {
                switch (_nextRecipe.use)
                {
                    case MachineUse.Smelter:
                        _nameTxt.text += " (smelter)";
                        break;
                    case MachineUse.Grindstone:
                        _nameTxt.text += " (grindstone)";
                        break;
                }
            }
        }
    }
}