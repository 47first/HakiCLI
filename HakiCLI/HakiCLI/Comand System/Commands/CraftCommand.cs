namespace Runtime
{
    public sealed class CraftCommand : ICommand
    {
        private readonly Dictionary<GameItem, IEnumerable<KeyValuePair<GameItem, int>>> _craftIngredients = new();
        private Inventory _inventory;
        private ILogger _logger;

        public CraftCommand(Inventory inventory, ILogger logger)
        {
            _inventory = inventory;
            _logger = logger;
        }

        public void AddIngredient(GameItem itemToCraft, IEnumerable<KeyValuePair<GameItem, int>> ingredients)
        {
            _craftIngredients.Add(itemToCraft, ingredients);
        }

        public void Execute(CommandContext context)
        {
            if (context.Args.Contains("craft") == false)
                return;

            foreach (var arg in context.Args)
            {
                if (TryGetItemToCraft(arg, out GameItem argItem)
                    && IsIngredientsInInventory(argItem))
                {
                    context.IsHandled = true;
                    CraftItem(argItem);
                }
            }

            if(context.IsHandled == false)
                context.FailureMessage = "There are no ingredients to craft this item...";
        }

        private bool TryGetItemToCraft(string name, out GameItem itemToCraft)
        {
            itemToCraft = default;

            if (_craftIngredients.Keys.Any(item => item.name.ToLower() == name))
            {
                itemToCraft = _craftIngredients.Keys.First(item => item.name.ToLower() == name);
                return true;
            }

            return false;
        }

        private bool IsIngredientsInInventory(GameItem itemToCraft)
        {
            foreach (var item in _craftIngredients[itemToCraft])
            {
                if (_inventory.GetItemAmount(item.Key) < item.Value)
                    return false;
            }

            return true;
        }

        private void CraftItem(GameItem itemToCraft)
        {
            if (_craftIngredients.TryGetValue(itemToCraft, out var craftIngredients) == false)
                throw new Exception("No ingredients");

            foreach (var item in craftIngredients)
                _inventory.RemoveItem(item.Key, item.Value);

            _inventory.AddItem(itemToCraft);

            _logger.Log($"{itemToCraft.name} successfully created!");
        }
    }
}
