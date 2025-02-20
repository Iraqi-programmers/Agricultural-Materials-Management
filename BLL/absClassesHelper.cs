namespace BLL
{
    public abstract class absClassesHelper
    {
        protected enum enMode { AddNew, Update };
        protected enMode _mode { get; set; } = enMode.AddNew;
        public int? Id { get; set; }

        //protected static int _GetInt(ref Dictionary<string, object> dict, string key)
        //    => dict.TryGetValue(key, out var value) && value != null ? Convert.ToInt32(value) : 0;

        //protected static string _GetString(ref Dictionary<string, object> dict, string key)
        //    => dict.TryGetValue(key, out var value) ? value?.ToString() ?? string.Empty : string.Empty;

        //protected static bool _GetBool(ref Dictionary<string, object> dict, string key)
        //    => dict.TryGetValue(key, out var value) && value != null && Convert.ToBoolean(value);

        //protected static decimal _GetDecimal(ref Dictionary<string, object> dict, string key)
        //    => dict.TryGetValue(key, out var value) && value != null ? Convert.ToDecimal(value) : 0m;

        //protected static double _GetDouble(ref Dictionary<string, object> dict, string key)
        //    => dict.TryGetValue(key, out var value) && value != null ? Convert.ToDouble(value) : 0.0;

        //protected static float _GetFloat(ref Dictionary<string, object> dict, string key)
        //    => dict.TryGetValue(key, out var value) && value != null ? Convert.ToSingle(value) : 0f;

        //protected static DateTime _GetDateTime(ref Dictionary<string, object> dict, string key)
        //    => dict.TryGetValue(key, out var value) && value != null && DateTime.TryParse(value.ToString(), out var dateTime)
        //        ? dateTime
        //        : DateTime.MinValue;
    }
}
