using System.Numerics;
using ResumeGame.Models;

namespace ResumeGame.HarmModificationEngine.Mods;

public sealed record class Regeneration<THealthAndHarm, TStat, TCount> : IMod<THealthAndHarm, TStat, TCount>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealthAndHarm, TStat, TCount> ctx)
    {
        if (ctx.Attacker.Damage.Type == DamageTypes.Slashing)
            ctx.Acc.DecreaseBy(ctx.Attacker.Damage.Harm.Amount);
    }
}