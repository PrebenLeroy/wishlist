﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WishListRestService.Data;
using WishListRestService.Models;

namespace WishListRestService.Repository
{
    [Authorize]
    public class WishListRepository : IWishListRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<WishList> _wishLists;

        public WishListRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _wishLists = dbContext.WishLists;
        }

        public void Add(WishList item)
        {
            _wishLists.Add(item);
            SaveChanges();
        }

        public WishList Find(int id)
        {
            return _wishLists.Include(wl => wl.Wishes).Include(wl => wl.PendingInvites).Include(wl => wl.Subscribers)
                .SingleOrDefault(wl => wl.WishListId == id);
        }

        public IEnumerable<WishList> GetAll()
        {
            return _wishLists.Include(wl => wl.Wishes).Include(wl => wl.PendingInvites).Include(wl => wl.Subscribers);
        }

        public void Remove(int id)
        {
            var itemToRemove = _wishLists.SingleOrDefault(wl => wl.WishListId == id);
            if (itemToRemove != null)
                _wishLists.Remove(itemToRemove);
            SaveChanges();
        }

        public void Update(WishList item)
        {
            var itemToUpdate = _wishLists.SingleOrDefault(wl => wl.WishListId == item.WishListId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Title = item.Title; // TODO update other fields
                itemToUpdate.CreatorName = item.CreatorName;
                itemToUpdate.PendingInvites = item.PendingInvites;
                itemToUpdate.Subscribers = item.Subscribers;
                itemToUpdate.Wishes = item.Wishes;
            }
            SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}