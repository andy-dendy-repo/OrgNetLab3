namespace OrgNetLab3.Data.Core
{
    public class PkNonClustered<T>
    {
        public T Id { get; set; }

        public override bool Equals(object? obj)
        {
            PkNonClustered<T> pkNonClustered = obj as PkNonClustered<T>;

            return obj != null && pkNonClustered.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
