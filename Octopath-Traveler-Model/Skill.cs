namespace Octopath_Traveler_Model;

public class Skill
{
    public string Name { get; set; }
    public int SP { get; set; }
    public string Type { get; set; }
    public double Modifier { get; set; }
    public string Target { get; set; }
    public string Description { get; set; }
    public string Boost { get; set; }
    public int Hits { get; set; } = 1;
    public bool IsDivine => Boost != null && Boost.StartsWith("[Divine Skill]");
    public static Skill WithHits(Skill skill, int hits)
    {
        return new Skill
        {
            Name = skill.Name,
            SP = skill.SP,
            Type = skill.Type,
            Modifier = skill.Modifier,
            Target = skill.Target,
            Description = skill.Description,
            Boost = skill.Boost,
            Hits = hits
        };
    }
}