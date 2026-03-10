using System.Numerics;
using ResumeGame.Models;

public sealed record class Superiority<THealth, TDamage, TStat, TCount> : IMod<THealth, TDamage, TStat, TCount>
    where THealth : INumber<THealth>, IUnsignedNumber<THealth>, IMinMaxValue<THealth>
    where TDamage : INumber<TDamage>, IUnsignedNumber<TDamage>, IMinMaxValue<TDamage>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealth, TDamage, TStat, TCount> ctx)
    {
        if (ctx.Attacker.Dexterity.Current > ctx.Victim.Dexterity.Current)
            ctx.Acc.IncreaseBy(TDamage.One);
    }
}

public sealed record class Poison<THealth, TDamage, TStat, TCount> : IMod<THealth, TDamage, TStat, TCount>
    where THealth : INumber<THealth>, IUnsignedNumber<THealth>, IMinMaxValue<THealth>
    where TDamage : INumber<TDamage>, IUnsignedNumber<TDamage>, IMinMaxValue<TDamage>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealth, TDamage, TStat, TCount> ctx)
    {
        if (ctx.Fight.DuelTurnCount.Objective == TCount.Zero) return;

        TCount num = ctx.Fight.DuelTurnCount.Subjective;
        TCount one = TCount.One;

        ctx.Acc.IncreaseBy(TDamage.CreateSaturating(num - one));
    }
}

public sealed record class Initiative<THealth, TDamage, TStat, TCount> : IMod<THealth, TDamage, TStat, TCount>
    where THealth : INumber<THealth>, IUnsignedNumber<THealth>, IMinMaxValue<THealth>
    where TDamage : INumber<TDamage>, IUnsignedNumber<TDamage>, IMinMaxValue<TDamage>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealth, TDamage, TStat, TCount> ctx)
    {
        if (ctx.Fight.DuelTurnCount.Objective == TCount.One)
            ctx.Acc.IncreaseBy(ctx.Attacker.Damage.Harm.Amount);
    }
}

public sealed record class Resistance<THealth, TDamage, TStat, TCount> : IMod<THealth, TDamage, TStat, TCount>
    where THealth : INumber<THealth>, IUnsignedNumber<THealth>, IMinMaxValue<THealth>
    where TDamage : INumber<TDamage>, IUnsignedNumber<TDamage>, IMinMaxValue<TDamage>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealth, TDamage, TStat, TCount> ctx)
    {
        if (ctx.Victim.Strength.Current > ctx.Attacker.Strength.Current)
            ctx.Acc.DecreaseBy(TDamage.CreateSaturating(3));
    }
}

public sealed record class Stamina<THealth, TDamage, TStat, TCount> : IMod<THealth, TDamage, TStat, TCount>
    where THealth : INumber<THealth>, IUnsignedNumber<THealth>, IMinMaxValue<THealth>
    where TDamage : INumber<TDamage>, IUnsignedNumber<TDamage>, IMinMaxValue<TDamage>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealth, TDamage, TStat, TCount> ctx)
    {
        TCount num = ctx.Fight.DuelTurnCount.Subjective;
        if (num > TCount.CreateSaturating(3)) ctx.Acc.DecreaseBy(TDamage.One);
        else if (num > TCount.Zero) ctx.Acc.IncreaseBy(TDamage.CreateSaturating(1 + 1));
    }
}

public sealed record class Tolerance<THealth, TDamage, TStat, TCount> : IMod<THealth, TDamage, TStat, TCount>
    where THealth : INumber<THealth>, IUnsignedNumber<THealth>, IMinMaxValue<THealth>
    where TDamage : INumber<TDamage>, IUnsignedNumber<TDamage>, IMinMaxValue<TDamage>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealth, TDamage, TStat, TCount> ctx)
    {
        TStat num = ctx.Victim.Constitution.Current;
        ctx.Acc.DecreaseBy(TDamage.CreateSaturating(num));
    }
}

public sealed record class Fragility<THealth, TDamage, TStat, TCount> : IMod<THealth, TDamage, TStat, TCount>
    where THealth : INumber<THealth>, IUnsignedNumber<THealth>, IMinMaxValue<THealth>
    where TDamage : INumber<TDamage>, IUnsignedNumber<TDamage>, IMinMaxValue<TDamage>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealth, TDamage, TStat, TCount> ctx)
    {
        if (ctx.Attacker.Damage.Type == DamageTypes.Bludgeoning)
            ctx.Acc.IncreaseBy(ctx.Attacker.Damage.Harm.Amount);
    }
}

public sealed record class Regeneration<THealth, TDamage, TStat, TCount> : IMod<THealth, TDamage, TStat, TCount>
    where THealth : INumber<THealth>, IUnsignedNumber<THealth>, IMinMaxValue<THealth>
    where TDamage : INumber<TDamage>, IUnsignedNumber<TDamage>, IMinMaxValue<TDamage>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealth, TDamage, TStat, TCount> ctx)
    {
        if (ctx.Attacker.Damage.Type == DamageTypes.Slashing)
            ctx.Acc.DecreaseBy(ctx.Attacker.Damage.Harm.Amount);
    }
}

public sealed record class Power<THealth, TDamage, TStat, TCount> : IMod<THealth, TDamage, TStat, TCount>
    where THealth : INumber<THealth>, IUnsignedNumber<THealth>, IMinMaxValue<THealth>
    where TDamage : INumber<TDamage>, IUnsignedNumber<TDamage>, IMinMaxValue<TDamage>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public void Modify(AttackContext<THealth, TDamage, TStat, TCount> ctx)
    {
        if (ctx.Fight.DuelTurnCount.Objective == TCount.Zero) return;
        if (ctx.Fight.DuelTurnCount.Subjective % TCount.CreateSaturating(3) == TCount.Zero)
            ctx.Acc.IncreaseBy(TDamage.CreateSaturating(3));
    }
}