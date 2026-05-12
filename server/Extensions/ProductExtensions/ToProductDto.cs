using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Extensions;

public static class ProductExtensions {
    public static GetProductDto ToProductDto(this Product source) => new GetProductDto(source.Quantity, source.Name, source.Description, source.Price);
}