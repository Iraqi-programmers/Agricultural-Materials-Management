namespace BLL
{
    public abstract class absTransaction : absBaseEntity
    {
        public DateTime Date { get; set; }
        public clsUsers? UserInfo { get; protected set; }
    }
}
