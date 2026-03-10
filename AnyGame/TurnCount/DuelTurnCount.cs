using System.Numerics;

namespace AnyGame.TurnCount;

public enum DuelistTypes : byte { none, Proactive, Reactive }

public readonly record struct DuelTurnCount<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    public T Objective { get; }

    public T Subjective
    {
        get
        {
            var num = BigInteger.CreateSaturating(Objective);
            var one = BigInteger.One;
            var two = BigInteger.One + BigInteger.One;
            return T.CreateSaturating((num + one) / two);
        }
    }

    public DuelistTypes Duelist
    {
        get
        {
            if (Objective == T.Zero) return DuelistTypes.none;
            if (Objective % (T.One + T.One) != T.Zero) return DuelistTypes.Proactive;
            return DuelistTypes.Reactive;
        }
    }

    public DuelTurnCount() => Objective = T.Zero;
    public DuelTurnCount(T objective) => Objective = objective;

    public DuelTurnCount(T subjective, DuelistTypes duelist)
    {
        if (subjective == T.Zero && duelist != DuelistTypes.none)
            throw new ArgumentException("Zero associated with none.");
        if (subjective != T.Zero && duelist == DuelistTypes.none)
            throw new ArgumentException("None associated with zero.");

        var num = BigInteger.CreateSaturating(subjective);
        var two = BigInteger.One + BigInteger.One;
        var dif = duelist == DuelistTypes.Proactive ? BigInteger.One : BigInteger.Zero;

        Objective = T.CreateSaturating(num * two - dif);
    }
}