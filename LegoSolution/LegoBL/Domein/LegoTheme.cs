public class LegoTheme
{
    public int Id { get; set; }
    public string Name { get; set; }
    private List<LegoSet> Sets=new();

    public LegoTheme(string name)
    {
        Name = name;
    }

    public LegoTheme(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public IReadOnlyList<LegoSet> LegoSets => Sets;
    public void AddLegoSet(LegoSet set)
    {
        if (Sets.Contains(set)) throw new LegoException("set already available");
        Sets.Add(set);
    }
    public override string ToString()
    {
        return $"{Id},{Name}"+string.Join("\t\n",Sets);
    }
}