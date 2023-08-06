using System.Net;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;

namespace Discoteque.Business.Utils;

public static class Utils {

    #region Gneral Utilities
    public static string GetLenghtInMinuteNotation(int seconds) {
        var value = new DateTime();
        value = value.AddSeconds(seconds);
        return value.ToString("mm:ss");
    }

    public static string GetISODateTimeFormat(DateTime date) {
        return date.ToString("yyyy-MM-dd HH':'mm':'ss");
    }
    #endregion

    #region EntityMessage Responses
    
    #endregion
}

public static class BuildResponseClass<TEntity> {
    public static EntityMessage<TEntity> BuildResponse(HttpStatusCode statusCode, string message, List<TEntity>? elements = null) {
        return new EntityMessage<TEntity>{
            StatusCode = statusCode,
            Message = message,
            TotalElements = (elements != null && elements.Any()) ? elements.Count : 0,
            Elements = elements ?? new List<TEntity>()
            // The null-coalescing operator ?? returns the value of its left-hand operand if it isn't null; 
            // otherwise, it evaluates the right-hand operand and returns its result. The ?? operator doesn't 
            // evaluate its right-hand operand if the left-hand operand evaluates to non-null.
        };
    }
}