using Entities.Models;
using Entities.DataTransferObjects;
using Repositories.Contracts;
using Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Services
{
    public class UserManager : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManager(IRepositoryManager manager, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(bool trackChanges)
        {
            var users = await _manager.User.GetAllUsersAsync(trackChanges);
            return _mapper.Map<IEnumerable<UserDto>>(users); 
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId, bool trackChanges)
        {
            var user = await _manager.User.GetUserById(userId, trackChanges);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> RegisterUserAsync(UserDtoForRegistration userDto)
        {
            var entity = _mapper.Map<User>(userDto);
            var result = await _userManager.CreateAsync(entity, userDto.Password);
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(entity, "User");
            }
            var returnValue = _mapper.Map<UserDto>(entity);
            return returnValue;
        }
        public async Task UpdateUserByAdminAsync(Guid userId, UserDtoForUpdate userDto, bool trackChanges)
        {
            var entity = await _userManager.FindByIdAsync(userId.ToString());
            if (entity == null)
                throw new KeyNotFoundException("User not found.");

            await _userManager.RemovePasswordAsync(entity);
            await _userManager.AddPasswordAsync(entity, userDto.NewPassword);

            _mapper.Map(userDto, entity);

            var result = await _userManager.UpdateAsync(entity);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("User update failed.");
            }

            await _manager.SaveAsync();
        }



        public async Task DeleteUserAsync(Guid userId, bool trackChanges)
        {
            var user = await _manager.User.GetUserById(userId, trackChanges);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            _manager.User.DeleteUser(user);
            await _manager.SaveAsync();
        }

        public async Task UpdateUserAsync(UserDtoForUpdate userDto, bool trackChanges)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(userId, out Guid id);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            var passwordCheck = await _userManager.CheckPasswordAsync(user, userDto.CurrentPassword);
            if (!passwordCheck)
                throw new UnauthorizedAccessException("Current password is wrong.");

            var result = await _userManager.ChangePasswordAsync(user, userDto.CurrentPassword, userDto.NewPassword);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Password change failed.");
            }

            _mapper.Map(userDto, user);
            await _userManager.UpdateAsync(user);
            await _manager.SaveAsync();
        }

    }
}
