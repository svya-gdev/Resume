using System.Numerics;
using ResumeGame.Models;
using AnyGame.TurnCount;

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

public interface IFighter<THealth, TDamage, TStat> : IHaveHealth<THealth>, IHaveDamage<TDamage>, IHaveStats<TStat>
    where THealth : INumber<THealth>, IUnsignedNumber<THealth>, IMinMaxValue<THealth>
    where TDamage : INumber<TDamage>, IUnsignedNumber<TDamage>, IMinMaxValue<TDamage>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>;

public interface IHaveDuelTurnCount<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    DuelTurnCount<T> DuelTurnCount { get; }
}

public interface IFight<T> : IHaveDuelTurnCount<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>;
