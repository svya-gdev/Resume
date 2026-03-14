using System.Numerics;

namespace ResumeGame.HarmModificationEngine.Mods;

public sealed record class Initiative<THealthAndHarm, TStat, TCount> : IMod<THealthAndHarm, TStat, TCount>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealthAndHarm, TStat, TCount> ctx)
    {
        if (ctx.Fight.DuelTurnCount.Objective == TCount.One)
            ctx.Acc.IncreaseBy(ctx.Attacker.Damage.Harm.Amount);
    }
}