namespace BLL.Product
{
    public abstract class absClassesHelperAdvance : absClassesHelperBasc
    {
        protected enum enMode { AddNew, Update }
        protected enMode _mode { get; set; }
    }

}
