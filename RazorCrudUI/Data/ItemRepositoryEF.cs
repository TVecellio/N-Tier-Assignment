﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RazorCrudUI.Data;
using RazorCrudUI.Models;

namespace RazorRepoUI.Data
{
    public class ItemRepositoryEF : IItemRepository
    {
        private ItemsContext _context;

        public ItemRepositoryEF(ItemsContext context)
        {
            _context = context;
        }
        public IEnumerable<ItemModel> GetItems()
        {
            return _context.Items.ToList();
        }

        public IEnumerable<ItemModel> GetItems(string? filter)
        {
            IQueryable<ItemModel> query = _context.Items;
            if (filter != null)
            {
                query = query.Where(item => item.Name.Contains(filter));

                return query.ToList();
            }
            return null;
        }

        public ItemModel? GetItemByID(int id)
        {
            return _context.Items.FirstOrDefault(item => item.Id == id);
        }

        public void insertItem(ItemModel item)
        {
            _context.Add(item);
            _context.SaveChanges();
        }

        public string deleteItem(int id)
        {

            string name = _context.Items.FirstOrDefault(item => item.Id == id).Name;

            if (name != null)
            {
                _context.Remove(id);
                _contexr.SaveChanges();
                return name;
            } else {
                return "No item found";
                    }
        }

        public bool updateItem(ItemModel item)
        {
            _context.Attach(item).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }
    }
}