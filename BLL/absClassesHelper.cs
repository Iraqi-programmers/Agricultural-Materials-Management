namespace BLL
{
    public abstract class absClassesHelper
    {
        protected enum enMode { AddNew, Update };
        protected enMode _mode { get; set; } = enMode.AddNew;
        public int? Id { get; protected set; }
    }
}
