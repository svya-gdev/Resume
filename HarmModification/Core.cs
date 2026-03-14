using AnyGame.Accumulation;
using AnyGame.TurnCount;
using ResumeGame.Models;
using System.Numerics;

namespace ResumeGame.HarmModificationEngine;

public interface IHaveHealth<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    Health<T> Health { get; }
}

public interface IHaveDamage<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    Damage<T> Damage { get; }
}

public interface IHaveStats<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    Stat<T> Constitution { get; }
    Stat<T> Dexterity { get; }
    Stat<T> Strength { get; }
}

public interface IHaveDuelTurnCount<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    DuelTurnCount<T> DuelTurnCount { get; }
}



public interface IFighter<THealthAndHarm, TStat> : IHaveHealth<THealthAndHarm>, IHaveDamage<THealthAndHarm>, IHaveStats<TStat>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>;

public interface IFight<T> : IHaveDuelTurnCount<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>;



public interface IMod<THealthAndHarm, TStat, TCount>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    void Modify(AttackContext<THealthAndHarm, TStat, TCount> context);
}



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