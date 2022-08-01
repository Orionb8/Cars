namespace TestProject.Interfaces {
    public interface IHasId<TKey> {
        TKey Id { get; set; }
    }
}
