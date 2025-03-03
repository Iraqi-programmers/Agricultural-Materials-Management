namespace BLL
{
    public abstract class absTransaction : absBaseEntity
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public clsUsers? UserInfo { get; protected set; }
    }
}
