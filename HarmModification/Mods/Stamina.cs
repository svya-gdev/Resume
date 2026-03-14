using System.Numerics;

namespace ResumeGame.HarmModificationEngine.Mods;

public sealed record class Stamina<THealthAndHarm, TStat, TCount> : IMod<THealthAndHarm, TStat, TCount>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealthAndHarm, TStat, TCount> ctx)
    {
        TCount num = ctx.Fight.DuelTurnCount.Subjective;

        if (num > TCount.CreateSaturating(3))
            ctx.Acc.DecreaseBy(THealthAndHarm.One);
        else if (num > TCount.Zero)
            ctx.Acc.IncreaseBy(THealthAndHarm.CreateSaturating(1 + 1));
    }
}