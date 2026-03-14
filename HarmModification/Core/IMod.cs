using System.Numerics;

namespace ResumeGame.HarmModificationEngine;

public interface IMod<THealthAndHarm, TStat, TCount>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    void Modify(AttackContext<THealthAndHarm, TStat, TCount> context);
}