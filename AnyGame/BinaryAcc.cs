using System.Numerics;

namespace AnyGame.Accumulation;

public sealed class BinaryAcc<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    private BigInteger acc { get; set; }
    public T Amount => T.CreateSaturating(acc);
    public BinaryAcc(T amount) => acc = BigInteger.CreateSaturating(amount);
    public void IncreaseBy(T amount) => acc += BigInteger.CreateSaturating(amount);
    public void DecreaseBy(T amount) => acc -= BigInteger.CreateSaturating(amount);
}