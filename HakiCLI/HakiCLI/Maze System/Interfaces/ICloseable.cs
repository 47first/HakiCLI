namespace Runtime
{
    public interface ICloseable
    {
        public bool IsOpen { get; protected set; }
        public bool Close();
        public bool Open();
    }
}
