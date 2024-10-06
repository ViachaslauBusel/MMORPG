using Cells;
using Items;
using Items.Data;
using Protocol.Data.Units.CraftingStation;
using Recipes;
using Recipes.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Workbench.UI.RecipeCrafting
{
    public class RecipeCraftingWindow : MonoBehaviour, IWorkbenchWindow
    {
        [SerializeField]
        private RecipeInputCell _recipeCell;
        [SerializeField]
        private GameObject _recipeComponentPref;
        [SerializeField]
        private Transform _content;
        [SerializeField]
        private Text _recipeName;
        private Canvas _canvas;
        private RecipeComponent _result;
        private RecipeData _selectRecipe;
        private RecipesRegistry _recipesRegistry;
        private DiContainer _diContainer;
        private WorkbenchInputHandler _workbenchInputHandler;
        private bool _isReadyForWork;

        public CraftingStationType StationType => CraftingStationType.Workbench;


        [Inject]
        private void Construct(RecipesRegistry recipesRegistry, DiContainer diContainer)
        {
            _recipesRegistry = recipesRegistry;
            _diContainer = diContainer;
        }

        private void Awake()
        {
            _workbenchInputHandler = GetComponentInParent<WorkbenchInputHandler>();
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
        }

        public void Hide()
        {
            _canvas.enabled = false;
        }

        public void Open(bool isReadyForWork)
        {
            _isReadyForWork = isReadyForWork;
            _canvas.enabled = true;
        }
        public void Close()
        {
            _canvas.enabled = false;
            ResetCraftingWindow();
        }

        public void Create()
        {
            if (_selectRecipe == null) return;
            _workbenchInputHandler.CreateItem(_selectRecipe.ID, null, null);
        }

        public GameObject CreatePieceField()
        {
            return _diContainer.InstantiatePrefab(_recipeComponentPref);
        }

        public void SelectRecipeItem(RecipeItemData recipeData)
        {

            _selectRecipe = _recipesRegistry.GetObjectByID(recipeData.ID);
            if (_selectRecipe == null) return;
            _recipeName.text = recipeData.Name;

            GameObject obj = CreatePieceField();
            obj.transform.SetParent(_content);
            _result = obj.GetComponent<RecipeComponent>();
            ItemBundleLink piece = new ItemBundleLink(_selectRecipe.Result.Item.ID, 0);

            _result.SetPiece(piece);
            _result.Start();
            _result.Expand();
        }

        public void ResetCraftingWindow()
        {
            if(_result != null)
            _result.Destroy();

            _result = null;
            _recipeName.text = "Insert the recipe into the cell";
            _recipeCell.PutItem(null);
        }

        public void SetStatus(bool isReadyForWork)
        {
            _isReadyForWork = isReadyForWork;
        }
    }
}