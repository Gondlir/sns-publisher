namespace Sns.Publisher.Contracts
{
    public sealed class CustomerCreated 
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string GitHubUsername { get; init; }
    }
}
