namespace BLL
{
    public abstract class absClassesHelper
    {
        protected enum enMode { AddNew, Update };
        protected enMode _mode { get; set; } = enMode.AddNew;
        protected int? _id { get; set; }
        protected int? _userId { get; set; }
    }
}
