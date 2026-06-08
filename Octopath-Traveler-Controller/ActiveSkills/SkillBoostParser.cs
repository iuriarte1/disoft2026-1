namespace Octopath_Traveler.ActiveSkills;

public class SkillBoostParser
{
    public static double ParseModifierPercent(string boost)
    {
        if (boost == null || !boost.Contains('%')) return 0;

        foreach (string word in boost.Split(' '))
        {
            if (word.EndsWith('%'))
            {
                string number = word.Replace("%", "");
                if (int.TryParse(number, out int percent))
                    return percent / 100.0;
            }
        }
        return 0;
    }
}