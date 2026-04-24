namespace Octopath_Traveler_View;

public class BasicAttackResult
{
    public string AttackerName { get; }
    public string VictimName   { get; }
    public string WeaponType   { get; }
    public int    Damage       { get; }

    public BasicAttackResult(string attackerName, string victimName, string weaponType, int damage)
    {
        AttackerName = attackerName;
        VictimName   = victimName;
        WeaponType   = weaponType;
        Damage       = damage;
    }
}