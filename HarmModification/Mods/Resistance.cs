using System.Numerics;
using ResumeGame.Models.Usage;

namespace ResumeGame.HarmModificationEngine.Mods;

public sealed record class Resistance<THealthAndHarm, TStat, TCount> : IMod<THealthAndHarm, TStat, TCount>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealthAndHarm, TStat, TCount> ctx)
    {
        if (ctx.Victim.Strength > ctx.Attacker.Strength)
            ctx.Acc.DecreaseBy(THealthAndHarm.CreateSaturating(3));
    }
}