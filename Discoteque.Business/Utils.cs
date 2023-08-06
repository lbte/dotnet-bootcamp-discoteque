using System.Net;
using Discoteque.Data.Dto;

namespace Discoteque.Business;

public static class Utils {
    public static string GetLenghtInMinuteNotation(int seconds) {
        var value = new DateTime();
        value = value.AddSeconds(seconds);
        return value.ToString("mm:ss");
    }

    public static string GetISODateTimeFormat(DateTime date) {
        return date.ToString("yyyy-MM-dd HH':'mm':'ss");
    }

}

public static class BuildResponseClass<TEntity> {
    public static EntityMessage<TEntity> BuildResponse(HttpStatusCode statusCode, string message) {
        return new EntityMessage<TEntity>{
            Message = message,
            TotalElements = 0,
            StatusCode = statusCode
        };
    }

    public static EntityMessage<TEntity> BuildResponse(HttpStatusCode statusCode, string message, List<TEntity> elements) {
        return new EntityMessage<TEntity>{
            Message = message,
            TotalElements = elements.Count,
            StatusCode = statusCode,
            Elements = elements
        };
    }
}