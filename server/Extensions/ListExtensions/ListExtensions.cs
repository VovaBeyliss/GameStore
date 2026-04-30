using System.Threading.Tasks;

namespace GameStore.Extensions;

public static class ListExtensions {
    public static async Task<List<T>> OrderByAsync<T, TResult>(this Task<List<T>> taskSource, Func<T, TResult> selector) {
        var source = await taskSource;
        return source.OrderBy(selector).ToList();
    }
}