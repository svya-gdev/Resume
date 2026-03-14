using System.Numerics;
using ResumeGame.Models.Usage;

namespace ResumeGame.HarmModificationEngine.Mods;

public sealed record class Superiority<THealthAndHarm, TStat, TCount> : IMod<THealthAndHarm, TStat, TCount>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealthAndHarm, TStat, TCount> ctx)
    {
        if (ctx.Attacker.Dexterity > ctx.Victim.Dexterity)
            ctx.Acc.IncreaseBy(THealthAndHarm.One);
    }
}