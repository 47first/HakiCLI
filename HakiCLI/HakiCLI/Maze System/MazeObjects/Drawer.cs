namespace Runtime
{
    public sealed class Drawer: MazeObject, ICheckable
    {
        private readonly Dictionary<GameItem, int> _items = new();

        public void AddItem(GameItem gameItem) => _items.Add(gameItem, 1);

        public bool Check(Inventory inventory)
        {
            if (_items.Count < 1)
                return false;

            foreach (var item in _items)
            {
                inventory.AddItem(item.Key, item.Value);

                _items.Remove(item.Key);
            }

            return true;
        }
    }
}
