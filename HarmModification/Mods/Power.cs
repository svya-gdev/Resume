using System.Numerics;

namespace ResumeGame.HarmModificationEngine.Mods;

public sealed record class Power<THealthAndHarm, TStat, TCount> : IMod<THealthAndHarm, TStat, TCount>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealthAndHarm, TStat, TCount> ctx)
    {
        if (ctx.Fight.DuelTurnCount.Objective == TCount.Zero) return;
        if (ctx.Fight.DuelTurnCount.Subjective % TCount.CreateSaturating(3) == TCount.Zero)
            ctx.Acc.IncreaseBy(THealthAndHarm.CreateSaturating(3));
    }
}