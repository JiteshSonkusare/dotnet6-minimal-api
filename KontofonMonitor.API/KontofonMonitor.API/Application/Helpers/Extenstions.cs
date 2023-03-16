namespace KontofonMonitor.API.Application.Helpers
{
    /// <summary>
    /// Umbrella class for custom extension methods.
    /// </summary>
    public static class Extenstions
    {
        private static readonly JsonSerializer serializer;

        static Extenstions()
        {
            serializer = JsonSerializer.CreateDefault();
            serializer.NullValueHandling = NullValueHandling.Ignore;
        }

        /// <summary>
        /// Converts object to JSON string.
        /// </summary>
        /// <param name="obj">Object instance.</param>
        /// <returns>Json representation of object.</returns>
        public static string ToJson(this object obj)
        {
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, obj);
            return writer.ToString();
        }
    }
}
