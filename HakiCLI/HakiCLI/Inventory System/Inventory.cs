﻿namespace Runtime
{
    public sealed class Inventory
    {
        private readonly Dictionary<GameItem, int> _itemCells = new();

        public event Action<GameItem, int> OnNewItems;
        public event Action<GameItem, int> OnRemoveItems;

        public IEnumerable<KeyValuePair<GameItem, int>> ItemCells => _itemCells;

        public void AddItem(GameItem item, int amount = 1)
        {
            if(_itemCells.ContainsKey(item))
                _itemCells[item] += amount;

            else
                _itemCells.Add(item, amount);

            OnNewItems?.Invoke(item, amount);
        }

        public void RemoveItem(GameItem item, int amount = 1)
        {
            if (_itemCells.ContainsKey(item) == false)
                return;

            _itemCells[item] -= amount;

            if(_itemCells[item] <= 0)
                _itemCells.Remove(item);

            OnRemoveItems?.Invoke(item, amount);
        }

        public int GetItemAmount(GameItem item)
        {
            if (_itemCells.ContainsKey(item) == false)
                return 0;

            return _itemCells[item];
        }
    }
}
