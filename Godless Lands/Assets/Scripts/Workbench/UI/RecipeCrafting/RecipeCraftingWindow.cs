using Cells;
using Items;
using Items.Data;
using Protocol.Data.Workbenches;
using Recipes;
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
        private Recipe _selectRecipe;
        private RecipesDataHolder _recipesData;
        private DiContainer _diContainer;
        private WorkbenchInputHandler _workbenchInputHandler;
        private bool _isReadyForWork;

        public WorkbenchType WorkbenchType => WorkbenchType.Workbench;


        [Inject]
        private void Construct(RecipesDataHolder recipesData, DiContainer diContainer)
        {
            _recipesData = recipesData;
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
            _workbenchInputHandler.CreateItem(_selectRecipe.id, null, null);
        }

        public GameObject CreatePieceField()
        {
            return _diContainer.InstantiatePrefab(_recipeComponentPref);
        }

        public void SelectRecipeItem(RecipeItemData recipeData)
        {

            _selectRecipe = _recipesData.GetRecipeById(recipeData.ID);
            if (_selectRecipe == null) return;
            _recipeName.text = recipeData.Name;

            GameObject obj = CreatePieceField();
            obj.transform.SetParent(_content);
            _result = obj.GetComponent<RecipeComponent>();
            Piece piece = new Piece();
            piece.ID = _selectRecipe.result;
            piece.count = 0;

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