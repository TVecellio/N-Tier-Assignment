using Domain.IItemRepository;
using Domain.Models;


public class ItemRepositoryMem : IItemRepository
{
    IList<ItemModel> _list;

    // constructor
    // init our list with our starting items
    public ItemRepositoryMem()
    {
        _list = new List<ItemModel>
        {
            new ItemModel { Id = 1, Name = "Item 1",
            Description = "Description 1", Price = 1.99m },

            new ItemModel { Id = 2, Name = "Item 2",
            Description = "Description 2", Price = 2.99m },

            new ItemModel { Id = 3, Name = "Item 3",
            Description = "Description 3", Price = 3.99m },

            new ItemModel { Id = 4, Name = "Item 4",
            Description = "Description 4", Price = 4.99m },

            new ItemModel { Id = 5, Name = "Item 5",
            Description = "Description 5", Price = 5.99m }
        };
    }

    public IEnumerable<ItemModel> GetItems()
    {
        return (IEnumerable< ItemModel >)_list.AsEnumerable();
    }

    public IEnumerable<ItemModel> GetItems(string? searchString)
    {
        if (string.IsNullOrEmpty(searchString))
            return (IEnumerable<ItemModel>)GetItems();
        return _list.Where(i => i.Name.Contains(searchString));
    }

    public void insertItem(ItemModel item)
    {
        // ID PROBLEM
        // the database took care of creating new unique ids for us
        // that isn't happening anymore and we will need to do it ourselves
        // so you will have to generate and set a new id that will be
        // unique in the list before adding it
        item.Id = _list.Max(x => x.Id) + 1;

        _list.Add(item);
    }

    public string deleteItem(int id)
    {
        // find the item
        // return if you can not
        var item = _list.FirstOrDefault(x => x.Id == id);
        if (item == null)
            return null;
        // we found item so delete it            		    
        _list.Remove(item);
        return item.Name;
    }

    public ItemModel? GetItemByID(int id)
    {
        return _list.FirstOrDefault(x => x.Id == id);
    }

    public bool updateItem(ItemModel item)
    {
        // find the item
        // return false if you can not
        var existingItem = _list.FirstOrDefault(x => x.Id == item.Id);
        if (existingItem == null)
            return false;

        // we found existing item       
        // existingItem is a reference type
        // so making changes to it here WILL change it in the list as well
        existingItem.Name = item.Name;
        existingItem.Description = item.Description;
        existingItem.Price = item.Price;
        return true;
    }
}