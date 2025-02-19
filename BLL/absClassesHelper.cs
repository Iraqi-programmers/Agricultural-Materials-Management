namespace BLL
{
    public abstract class absClassesHelper
    {
        protected enum enMode { AddNew, Update };
        protected enMode _mode { get; set; } = enMode.AddNew;
        public int Id { get; set; }

        protected static int _GetInt(ref Dictionary<string, object> dict, string key) 
            => dict.TryGetValue(key, out var value) && value != null ? Convert.ToInt32(value) : 0;
        protected static string _GetString(ref Dictionary<string, object> dict, string key) 
            => dict.TryGetValue(key, out var value) ? value?.ToString() ?? string.Empty : string.Empty;
        protected static bool _GetBool(ref Dictionary<string, object> dict, string key) 
            => dict.TryGetValue(key, out var value) && value != null && Convert.ToBoolean(value);
    }
}
