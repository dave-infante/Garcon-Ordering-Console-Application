using Garcon.Business.Repository.Interface;
using Garcon.Business.Provider.Interface;
using Garcon.Business.Repository;
using Garcon.Business.Provider;
using Garcon.Business.Model;
using Garcon.App.Controller;


namespace Garcon.App.App
{
    public sealed class GarconApp
    {
        private readonly IMenuIngredientRepository MenuIngredientRepository;
        private readonly IIngredientItemRepository IngredientItemRepository;
        private readonly IMenuItemRepository MenuItemRepository;

        private readonly IProvider<MenuIngredient> MenuIngredientProvider;
        private readonly IProvider<IngredientItem> IngredientItemProvider;
        private readonly IProvider<MenuItem> MenuItemProvider;
        private readonly IUnitOfWork UnitOfWork;

        public readonly OrderController orderController;
        public readonly MenuController menuController;
        private readonly Router router;


        /// <summary>
        /// Initiates the ordering application by defining all of the dependencies and router that are needed,
        /// </summary>
        public GarconApp()
        {
            // Providers
            IngredientItemProvider = new IngredientItemProvider();
            MenuIngredientProvider = new MenuIngredientProvider();
            MenuItemProvider = new MenuItemProvider();

            // Data Repositories
            IngredientItemRepository = new IngredientItemRepository(IngredientItemProvider);
            MenuIngredientRepository = new MenuIngredientRepository(MenuIngredientProvider, IngredientItemRepository);
            MenuItemRepository = new MenuItemRepository(MenuItemProvider, MenuIngredientRepository);
            UnitOfWork = new UnitOfWork(MenuIngredientRepository, IngredientItemRepository, MenuItemRepository);

            // Controllers
            orderController = new OrderController(UnitOfWork);
            menuController = new MenuController(UnitOfWork);

            // Router
            router = new Router(this);
        }

        /// <summary>
        /// Initiate the app by triggering the routing process.
        /// </summary>
        public void Start()
        {
            router.Initialize();
        }
    }
}
