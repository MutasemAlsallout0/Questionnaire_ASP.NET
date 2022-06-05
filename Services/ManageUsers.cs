using Microsoft.AspNetCore.Identity;
using Questionnaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire.Services
{
    public interface IManageUsers
    {
        Task<Users> GetUser(string id);
        Task<List<Users>> GetUsers();
        Task AssignRoles(Users user);

    }
      public class ManageUsers : IManageUsers
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IManageRolees _manageRolees;
        public ManageUsers(UserManager<IdentityUser> userManager, IManageRolees manageRolees)
        {
            _userManager = userManager;
            _manageRolees = manageRolees;
        }

        public async Task<Users> GetUser(string id)
        {
            var FindUser = await _userManager.FindByIdAsync(id);
            if (FindUser == null)
                throw new  Exception("User does not exist");
            var roles = await _userManager.GetRolesAsync(FindUser);
            var user = new Users()
            {
                Id = id,
                Name = FindUser.UserName,
                Roles = roles.ToList()
            };
            return user; 
        }

        public async Task<List<Users>> GetUsers()
        {
             var users = _userManager.Users.ToList(); 
            var listUser = new List<Users>();
            foreach (var user in users)
            {
                listUser.Add(new Users()
                {
                    Id=user.Id,
                    Name = user.UserName,
                    Roles = (await _userManager.GetRolesAsync(user)).ToList()

                });
            }
            return listUser;
        }

        public async Task AssignRoles(Users user)
        {
            var FindUser = await _userManager.FindByIdAsync(user.Id);
            if(FindUser == null)
                throw new Exception("User does not exist");

            var roles = await _userManager.GetRolesAsync(FindUser); 
            if(user.Roles == null || user.Roles.Count == 0)
            {
                await _userManager.RemoveFromRolesAsync(FindUser, roles);
            }
            else
            {
                var rolesToAdd = user.Roles.Where(x => !roles.Any(y => y == x));
                var rolesToRemove = roles.Where(x => !user.Roles.Any(y => x == y));

                await _userManager.AddToRolesAsync(FindUser, rolesToAdd);
                await _userManager.RemoveFromRolesAsync(FindUser, rolesToRemove);
            }
        }

    }
}
