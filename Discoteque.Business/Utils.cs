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