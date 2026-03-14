using System.Numerics;
using ResumeGame.Models;
using AnyGame.Accumulation;

namespace ResumeGame.HarmModificationEngine;

public sealed record class AttackContext<THealthAndHarm, TStat, TCount>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public IFighter<THealthAndHarm, TStat> Attacker { get; }
    public IFighter<THealthAndHarm, TStat> Victim { get; }
    public IFight<TCount> Fight { get; }
    public BinaryAcc<THealthAndHarm> Acc { get; set; }

    public AttackContext(
        IFighter<THealthAndHarm, TStat> attacker,
        IFighter<THealthAndHarm, TStat> victim,
        IFight<TCount> fight)
    {
        Attacker = attacker;
        Victim = victim;
        Fight = fight;
        Acc = new(Attacker.Damage.Harm.Amount);
    }

    public void Modify(params IMod<THealthAndHarm, TStat, TCount>[] mods)
    {
        foreach (var m in mods)
            m.Modify(this);
    }

    public Harm<THealthAndHarm> GetHarm() => new(Acc.Amount);
}