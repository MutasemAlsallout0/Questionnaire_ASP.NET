using Microsoft.AspNetCore.Identity;
using Questionnaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Questionnaire.Services
{
   
    public interface IManageRolees 
    {
        Task<Roles> GetRole(string id);
        Task<List<Roles>> GetRoles();
        Task<Roles> CreateRole(Roles role);
        Task<Roles> UpdateRole(string Id, Roles role);
        Task DeleteRole(string Id);
    }
     public  class ManageRoles : IManageRolees
    {
        private readonly RoleManager<IdentityRole> _roleManager;


        public ManageRoles(RoleManager<IdentityRole> roleManager)
        {
            _roleManager  = roleManager;
        }

        public async Task<Roles> GetRole(string id)
        {
            var roleEntity = await _roleManager.FindByIdAsync(id);
            if (roleEntity == null)
                throw new Exception("Role does noet exict");
            var calims = await _roleManager.GetClaimsAsync(roleEntity);
            var roleview = new Roles()
            {
                Id = id,
                Name = roleEntity.Name,
                Claims = calims.Select(x => x.Type).ToList(),
            };
            return roleview;    
        }

        public async Task<List<Roles>> GetRoles()
        {
            var roles = _roleManager.Roles.ToList(); 
            var listRoleView = new List<Roles>();
            foreach (var role in roles)
            {
                listRoleView.Add(new Roles()
                {
                    Id=role.Id, 
                    Name = role.Name,
                    Claims = (await _roleManager.GetClaimsAsync(role)).Select(x => x.Type).ToList()
                });;
            }
            return listRoleView;
        } 

        public async Task<Roles> CreateRole(Roles role)
        {
            var newRole = new IdentityRole(role.Name);
            if (await _roleManager.RoleExistsAsync(role.Name))
                throw new Exception("Role already exist");
            await _roleManager.CreateAsync(newRole);
            foreach (var item in role.Claims)
            {
                await _roleManager.AddClaimAsync(newRole, new Claim(item, ""));
            }
            return role;

        }

        public async Task<Roles> UpdateRole(string Id, Roles role)
        {
            var EditRole = await _roleManager.FindByIdAsync(Id);
            if (EditRole == null)
                throw new Exception("Role does not exist");

            EditRole.Name = role.Name;
            await _roleManager.UpdateAsync(EditRole);


            var claims = await _roleManager.GetClaimsAsync(EditRole);
            var clamisToAdd = role.Claims.Where(x => !claims.Any(y => y.Type == x));
            var clamisToRemove = claims.Where(x => !role.Claims.Any(y => x.Type == y));

            foreach (var item in clamisToAdd)
            {
                await _roleManager.AddClaimAsync(EditRole, new Claim(item, ""));
            }
            foreach (var item in clamisToRemove)
            {
                await _roleManager.RemoveClaimAsync(EditRole, item);
            }
            return role;
        }

        public async Task DeleteRole(string Id)
        {
            var DeleteRole = await _roleManager.FindByIdAsync(Id);
            if (DeleteRole == null)
                throw new Exception("Role does not exist");
            await _roleManager.DeleteAsync(DeleteRole);

        }

    }
}
