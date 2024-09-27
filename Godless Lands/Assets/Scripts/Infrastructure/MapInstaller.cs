using Cells;
using CombatMode;
using Dialogues;
using Dialogues.NodeHandlers;
using Drop.GroundDrop;
using Equipment;
using Inventory;
using Items;
using MCamera;
using Network.Object.Interaction;
using Network.Object.Visualization.Entities.Characters;
using Network.Object.Visualization.VisualCache;
using Nickname;
using OpenWorld;
using Player;
using Professions;
using Quests;
using Quests.Journal;
using Recipes;
using Skills;
using Target;
using Test.DrawPoints;
using Trade;
using UI.ConfirmationDialog;
using Units.Registry;
using UnityEngine;
using Workbench;
using Zenject;

public class MapInstaller : MonoInstaller
{
    [SerializeField] CameraController m_cameraControllerObj;
    [SerializeField] MapLoader m_mapLoaderObj;
    [SerializeField] SkillsBook m_skillsBookObj;
    public override void InstallBindings()
    {
        Container.Bind<CameraController>().FromInstance(m_cameraControllerObj).AsSingle();
        Container.Bind<MapLoader>().FromInstance(m_mapLoaderObj).AsSingle();
        Container.Bind<SkillsBook>().FromInstance(m_skillsBookObj).AsSingle();


        Container.BindInterfacesAndSelfTo<CellContentToRenderConverter>().FromNew().AsSingle().NonLazy();

        // Object interaction
        Container.BindInterfacesAndSelfTo<InteractableObjectsRegistry>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InteractionObjectInputHandler>().FromNew().AsSingle().NonLazy();

        // Player character
        Container.BindInterfacesAndSelfTo<PlayerCharacterNetworkObjecEventNotifier>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerCharacterVisualEventNotifier>().FromNew().AsSingle().NonLazy();
       
        // Inventory
        Container.BindInterfacesAndSelfTo<InventoryModel>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InventoryListener>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ItemUsageService>().FromNew().AsSingle().NonLazy();

        //Equipment
        Container.BindInterfacesAndSelfTo<EquipmentListener>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EquipmentModel>().FromNew().AsSingle().NonLazy();

        // Unit visualisation
        Container.BindInterfacesAndSelfTo<UnitVisualCacheService>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CharacterMeshProviderService>().FromNew().AsSingle().NonLazy();

        // Target
        Container.BindInterfacesAndSelfTo<TargetListener>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UnitTargetRequestSender>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TargetObjectProvider>().FromNew().AsSingle().NonLazy();

        // Combat mode
        Container.BindInterfacesAndSelfTo<CombatModeController>().FromNew().AsSingle().NonLazy();

        //Recipes
        Container.BindInterfacesAndSelfTo<RecipeComponentMatcherService>().FromNew().AsSingle().NonLazy();

        //Workbench
        Container.BindInterfacesAndSelfTo<WorkbenchListener>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SmelterModel>().FromNew().AsSingle().NonLazy();

        //Professions
        Container.BindInterfacesAndSelfTo<ProfessionsModel>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ProfessionsListener>().FromNew().AsSingle().NonLazy();

        //Quests
        Container.BindInterfacesAndSelfTo<QuestsController>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<QuestsModel>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<QuestsListener>().FromNew().AsSingle().NonLazy();

        //Quests Journal
        Container.BindInterfacesAndSelfTo<QuestJournalModel>().FromNew().AsSingle().NonLazy();

        //Dialogues
        Container.BindInterfacesAndSelfTo<DialogueNodeHandlerStorage>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<QuestLevelCheckNodeHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<QuestLevelUpNodeHandler>().FromNew().AsSingle().NonLazy();   
       
        //Ground drop
        Container.BindInterfacesAndSelfTo<GroundDropInputHandler>().FromNew().AsSingle().NonLazy();

        //Trade
        Container.BindInterfacesAndSelfTo<TradeInputHandler>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TradeListener>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TradeModel>().FromNew().AsSingle().NonLazy();

        //Confirmation dialog
        Container.BindInterfacesAndSelfTo<ConfirmationDialogController>().FromNew().AsSingle().NonLazy();
     
        // TEST
        // DRAW POINTS
        Container.BindInterfacesAndSelfTo<DrawPointsModel>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DrawPointsListener>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PointsRenderer>().FromNew().AsSingle().NonLazy();

        //Nicknames 
        Container.BindInterfacesAndSelfTo<NetworkNicknameProviderService>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<NicknameProviderService>().FromNew().AsSingle();

        //Units
        Container.BindInterfacesAndSelfTo<UnitVisualObjectRegistry>().FromNew().AsSingle().NonLazy();

    }
}