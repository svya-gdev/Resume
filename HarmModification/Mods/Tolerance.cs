using System.Numerics;

namespace ResumeGame.HarmModificationEngine.Mods;

public sealed record class Tolerance<THealthAndHarm, TStat, TCount> : IMod<THealthAndHarm, TStat, TCount>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealthAndHarm, TStat, TCount> ctx)
    {
        TStat num = ctx.Victim.Constitution.Current;
        ctx.Acc.DecreaseBy(THealthAndHarm.CreateSaturating(num));
    }
}