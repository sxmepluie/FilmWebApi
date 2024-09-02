using Entities.DataTransferObjects;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync(bool trackChanges);
        Task<UserDto> GetUserByIdAsync(Guid userId, bool trackChanges);
        Task UpdateUserByAdminAsync(Guid userId, UserDtoForUpdate user, bool trackChanges);
        Task UpdateUserAsync(UserDtoForUpdate user , bool trackChanges);
        Task<UserDto> RegisterUserAsync(UserDtoForRegistration userDto);
        Task DeleteUserAsync(Guid userId, bool trackChanges);
    }
}
