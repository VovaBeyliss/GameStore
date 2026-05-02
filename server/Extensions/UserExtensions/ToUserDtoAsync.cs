using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Extensions;

public static class UserExtensions {
    public static async Task<UserDto> ToUserDtoAsync(this Task<User> taskSource) {
        User source = await taskSource;

        return new UserDto(source.Username, source.Email, source.Password);
    }
}