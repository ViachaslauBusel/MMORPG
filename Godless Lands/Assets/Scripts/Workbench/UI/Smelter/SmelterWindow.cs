using Cells;
using Protocol.Data.Items;
using Protocol.Data.Workbenches;
using Recipes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Workbench.UI.Smelter
{
    public class SmelterWindow : MonoBehaviour, IWorkbenchWindow
    {
        [SerializeField]
        private ComponentsDrawer _component;
        [SerializeField]
        private ComponentsDrawer _fuel;
        [SerializeField]
        private Button _createBut;
        [SerializeField]
        private WorkbenchType _workbenchType;
        private Canvas _canvas;
        private SmelterModel _smelterModel;
        private DiContainer _diContainer;
        private Recipe _selectedRecipe;
        private WorkbenchInputHandler _workbenchInputHandler;

        public Transform recipesParent;
        public GameObject RecipePrefab;
        private List<SmelterRecipeCell> recipes = new List<SmelterRecipeCell>();
       

        public MachineUse machineUse;
        private bool _isReadyForWork;


        public WorkbenchType WorkbenchType => _workbenchType;

        [Inject]
        private void Construct(SmelterModel smelterModel, DiContainer diContainer)
        {
            _smelterModel = smelterModel;
            _diContainer = diContainer;
        }

        private void Awake()
        {
            _workbenchInputHandler = GetComponentInParent<WorkbenchInputHandler>();
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
            _createBut.interactable = false;

            _component.Init(ItemStorageType.SmelerComponent);
            _fuel.Init(ItemStorageType.SmelterFuel);
        }

        private void RefreshCount()
        {
            if (!_canvas.enabled) return;
            foreach (SmelterCell cell in _component.GetCells())
            {
                if (_selectedRecipe == null) cell.DrawCount();
                else cell.SetCount(_selectedRecipe.component);
            }
            foreach (SmelterCell cell in _fuel.GetCells())
            {
                if (_selectedRecipe == null) cell.DrawCount();
                else cell.SetCount(_selectedRecipe.fuel);
            }

        }
        public void Hide()
        {
            _canvas.enabled = false;
        }
        public void Close()
        {
            _canvas.enabled = false;
            Clear();
            _smelterModel.OnContentUpdate -= OnSmelterModelUpdate;
            _smelterModel.ReleaseSmelterModel();
        }

        public void SetStatus(bool isReadyForWork)
        {
            _isReadyForWork = isReadyForWork;
        }

        public void Open(bool isReadyForWork)
        {
            SetStatus(isReadyForWork);
            _smelterModel.ReserverSmelterModel(_workbenchType);
            Clear();
            _canvas.enabled = true;
            _smelterModel.OnContentUpdate += OnSmelterModelUpdate;
        }

        private void OnSmelterModelUpdate()
        {
            _component.UpdateContent(_smelterModel.Components);
            _fuel.UpdateContent(_smelterModel.Fuels);

            DestroyRecipes();

            foreach (Recipe recipe in _smelterModel.Recipes)
            {
                CreateRecipe(recipe);
            }
            ContainsRecipe(_selectedRecipe);
            RefreshCount();
        }

        private void ContainsRecipe(Recipe recipe)
        {
            if (recipe == null) return;
            if (recipes.Count < 1) SelectRecipe(null);
            foreach (SmelterRecipeCell recipeCell in recipes)
            {
                if (recipeCell.GetRecipe().id == recipe.id) { recipeCell.Reselect(); return; }
            }
            SelectRecipe(null);
        }

        private void CreateRecipe(Recipe recipe)
        {
            GameObject obj = _diContainer.InstantiatePrefab(RecipePrefab);
            obj.transform.SetParent(recipesParent);
            SmelterRecipeCell recipeCell = obj.GetComponent<SmelterRecipeCell>();
            recipeCell.SetRecipe(recipe);
            recipes.Add(recipeCell);
        }
        private void DestroyRecipes()
        {
            foreach (SmelterRecipeCell obj in recipes)
                Destroy(obj.gameObject);
            recipes.Clear();
        }

        public void SelectRecipe(Recipe value)
        {
            _selectedRecipe = value;
            _createBut.interactable = _selectedRecipe != null;
            RefreshCount();
        }

        public void Create()
        {
            if (_selectedRecipe == null) return;
            _workbenchInputHandler.CreateItem(_selectedRecipe.id,
                                              _component.GetCells().Where(c => !c.IsEmpty()).Select(c => c.GetItem().UniqueID).ToList(),
                                              _fuel.GetCells().Where(c => !c.IsEmpty()).Select(c => c.GetItem().UniqueID).ToList());
        }

        public void Clear()
        {
            DestroyRecipes();
            _component.Clear();
            _fuel.Clear();
            SelectRecipe(null);
        }
    }
}