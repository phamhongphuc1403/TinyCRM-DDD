﻿using TinyCRM.Application.Modules.Role.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Role.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleEntity>> GetAllAsync();
        Task<RoleEntity> GetUserRoleAsync(string userId);
        Task UpdateUserRoleAsync(string userId, UpdateUserRoleDto model);
    }
}
