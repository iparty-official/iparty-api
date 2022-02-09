namespace iParty.Business.Interfaces
{
    public interface IRemoveble : IIdentifiable
    {
        public bool Removed { get; }
        public void Remove();
    }
}
