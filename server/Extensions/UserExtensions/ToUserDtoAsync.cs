using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Extensions;

public static class UserExtensions {
    public static async Task<UserDto?> ToUserDtoAsync(this Task<User> taskSource) {
        var source = await taskSource;

        if (source == null) {
            return null;
        }

        return new UserDto(source.Username, source.Email, source.Password);
    }
}