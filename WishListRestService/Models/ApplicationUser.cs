using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WishListRestService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<WishList> CreatedWishLists { get; set; }
        public ICollection<PendingInvite> PendingInvites { get; set; }
        public ICollection<WishListSubscriber> SubbedWishLists { get; set; }

        public ApplicationUser()
        {
            PendingInvites = new List<PendingInvite>();
            CreatedWishLists = new List<WishList>();
            SubbedWishLists = new List<WishListSubscriber>();
        }

        public ApplicationUser(ICollection<PendingInvite> pendingInvites, ICollection<WishList> createdWishLists,
            ICollection<WishListSubscriber> subbedWishLists) : this()
        {
            PendingInvites = pendingInvites;
            CreatedWishLists = createdWishLists;
            SubbedWishLists = subbedWishLists;
        }

        public void CreateWishList(WishList wishList)
        {
            CreatedWishLists.Add(wishList);
            wishList.CreatorName = UserName;
        }

        public void AddInvite(PendingInvite invite)
        {
            PendingInvites.Add(invite);
        }

        public void Subscribe(WishListSubscriber subscriber)
        {
            SubbedWishLists.Add(subscriber);
        }
    }
}