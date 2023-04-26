namespace BuildUtility.Entity
{
    public sealed class Build
    {
        public string Name { get; init; }

        public IList<string> Dependencies { get; private set; }

        public IList<string> Actions { get; init; }

        public Build(string name)
        {
            Name = name;
            Dependencies = new List<string>();
            Actions = new List<string>();
        }
    }
}
