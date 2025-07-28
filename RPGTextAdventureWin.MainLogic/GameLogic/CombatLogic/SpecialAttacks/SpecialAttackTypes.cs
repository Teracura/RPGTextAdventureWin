namespace MainLogic.GameLogic.CombatLogic.SpecialAttacks;

public enum SpecialAttackTypes
{
    // Warrior
    StrikeOfTheAvenger, //high dmg, low mana cost, shield penetration
    AbsoluteGuard, //lower damage, but very high defense for a few rounds, average mana cost 
    UltimateRageStrike, //very high damage scales the lower the HP is, high mana cost

    // Mage
    SoulSwap, //swap mana and Hp percentages, any fractions will get floored, no mana cost but 4 round cooldown
    SparkOfTheMightiestMage, //Mana set to 0 instantly, deals 5% damage per 1 mana available, no upper damage limit
    SpiritLeech, //Deals lower than average attack damage, fills the mana bar based on damage dealt, no mana cost

    // Archer
    TheFierceShot, //deals higher damage the higher HP is, at lowest point it's 100% of normal damage, low mana cost
    ElvenCursedArrow, //steals life of enemy and adds it to current HP, average mana cost
    FocusShot //deals very high damage, but takes in more damage, average mana cost
}