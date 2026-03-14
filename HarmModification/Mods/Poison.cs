using System.Numerics;

namespace ResumeGame.HarmModificationEngine.Mods;

public sealed record class Poison<THealthAndHarm, TStat, TCount> : IMod<THealthAndHarm, TStat, TCount>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealthAndHarm, TStat, TCount> ctx)
    {
        if (ctx.Fight.DuelTurnCount.Objective == TCount.Zero) return;

        TCount num = ctx.Fight.DuelTurnCount.Subjective;
        TCount one = TCount.One;

        ctx.Acc.IncreaseBy(THealthAndHarm.CreateSaturating(num - one));
    }
}