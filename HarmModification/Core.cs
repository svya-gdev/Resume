using AnyGame.Accumulation;
using System.Numerics;

public sealed record class AttackContext<THealth, TDamage, TStat, TCount>
    where THealth : INumber<THealth>, IUnsignedNumber<THealth>, IMinMaxValue<THealth>
    where TDamage : INumber<TDamage>, IUnsignedNumber<TDamage>, IMinMaxValue<TDamage>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public IFighter<THealth, TDamage, TStat> Attacker { get; }
    public IFighter<THealth, TDamage, TStat> Victim { get; }
    public IFight<TCount> Fight { get; }
    public BinaryAcc<TDamage> Acc { get; set; }

    public AttackContext(
        IFighter<THealth, TDamage, TStat> attacker,
        IFighter<THealth, TDamage, TStat> victim,
        IFight<TCount> fight)
    {
        Attacker = attacker;
        Victim = victim;
        Fight = fight;
        Acc = new(Attacker.Damage.Harm.Amount);
    }
}

public interface IMod<THealth, TDamage, TStat, TCount>
    where THealth : INumber<THealth>, IUnsignedNumber<THealth>, IMinMaxValue<THealth>
    where TDamage : INumber<TDamage>, IUnsignedNumber<TDamage>, IMinMaxValue<TDamage>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    void Modify(AttackContext<THealth, TDamage, TStat, TCount> context);
}