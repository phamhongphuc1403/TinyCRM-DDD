﻿using TinyCRM.API.Modules.Account.DTOs;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Account.Services
{
    public interface IAccountService
    {
        Task<PaginationResponse<GetAccountDTO>> GetAllAsync(AccountQueryDTO query);

        Task<GetAccountDTO> GetByIdAsync(Guid id);

        Task<GetAccountDTO> AddAsync(AddOrUpdateAccountDTO dto);

        Task<GetAccountDTO> UpdateAsync(AddOrUpdateAccountDTO dto, Guid id);

        Task DeleteAsync(Guid id);
    }
}