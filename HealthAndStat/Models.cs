using System.Numerics;

namespace ResumeGame.Models;

public readonly record struct Buff<T>(T Amount)
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>;
public readonly record struct Nerf<T>(T Amount)
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>;

public readonly record struct Harm<T>(T Amount)
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>;
public readonly record struct Heal<T>(T Amount)
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>;

public enum DamageTypes : byte { None, Bludgeoning, Piercing, Slashing }
public readonly record struct Damage<T>(Harm<T> Harm, DamageTypes Type)
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>;

public readonly record struct Stat<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    public T Minimum { get; }
    public T Current { get; }
    public T Maximum => T.MaxValue;
    public Stat() => (Minimum, Current) = (T.MinValue, T.MinValue);
    public Stat(T minimum) => (Minimum, Current) = (minimum, minimum);
    public Stat(T minimum, T current) => (Minimum, Current) = (minimum, T.Max(minimum, current));
}

public readonly record struct Health<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    public T Minimum => T.MinValue;
    public T Current { get; }
    public T Maximum { get; }
    public Health() => (Current, Maximum) = (T.MaxValue, T.MaxValue);
    public Health(T maximum) => (Current, Maximum) = (maximum, maximum);
    public Health(T current, T maximum) => (Current, Maximum) = (T.Min(current, maximum), maximum);
}