<<<<<<< Updated upstream
using System.Collections.Generic;
using System;
=======
using System;
using System.Collections.Generic;
>>>>>>> Stashed changes

namespace Features.Inventory.Items
{
    internal interface IItemsRepository : IDisposable
    {
        IReadOnlyDictionary<string, IItem> Items { get; }
    }

    internal class ItemsRepository : IItemsRepository
<<<<<<< Updated upstream
        
    {
        private readonly Dictionary<string, IItem> _items;
        private bool _isDisposed;
        public IReadOnlyDictionary<string, IItem> Items => _items;
        public ItemsRepository(IEnumerable<ItemConfig> configs) =>
            _items = CreateCollection(configs);
        
=======
    {
        private readonly Dictionary<string, IItem> _items;

        private bool _isDisposed;
        public IReadOnlyDictionary<string, IItem> Items => _items;
        public ItemsRepository(IEnumerable<ItemConfig> configs) 
        {
            _items = CreateCollection(configs);
        }
>>>>>>> Stashed changes
        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;
            _items.Clear();
        }
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
        private Dictionary<string,IItem> CreateCollection(IEnumerable<ItemConfig> configs)
        {
            var collection = new Dictionary<string, IItem>();
            foreach (ItemConfig config in configs)
<<<<<<< Updated upstream
                collection[config.Id] = CreateItem(config);

            return collection;
        }

        private static Item CreateItem(ItemConfig config) =>
=======
            {
                collection[config.Id] = CreateItem(config);
            }
            return collection;
        }
        private static IItem CreateItem(ItemConfig config) =>
>>>>>>> Stashed changes
            new Item
            (
                config.Id,
                new ItemInfo
                (
                    config.Title,
                    config.Icon
                )
            );
    }

}