using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Extensions;

public static class UserExtensions {
    public static UserDto ToUserDto(this User source) {
        return new UserDto(source.Username, source.Email, source.Password);
    }
}