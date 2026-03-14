using System.Numerics;
using ResumeGame.Models;
using ResumeGame.Models.Usage;
using AnyGame.TurnCount;
using AnyGame.TurnCount.Usage;
using ResumeGame.HarmModificationEngine;

public interface IBattler<THealthAndHarm, TStat, TCount> : IFighter<THealthAndHarm, TStat>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    new Health<THealthAndHarm> Health { get; set; }
    IMod<THealthAndHarm, TStat, TCount>[] OffensiveMods { get; }
    IMod<THealthAndHarm, TStat, TCount>[] DefensiveMods { get; }
}

public static class IBattlerExtensions
{
    extension<THealthAndHarm, TStat, TCount>(IBattler<THealthAndHarm, TStat, TCount> battler)
        where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
        where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
        where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
    {
        public bool IsDead => battler.Health.Current == battler.Health.Minimum;
        public bool IsFull => battler.Health.Current == battler.Health.Maximum;
        public void Take(Harm<THealthAndHarm> harm) => battler.Health.Apply(harm);
    }
}

public sealed record class Battle<THealthAndHarm, TStat, TCount> : IFight<TCount>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    public IBattler<THealthAndHarm, TStat, TCount> ProactiveBattler { get; }
    public IBattler<THealthAndHarm, TStat, TCount> ReactiveBattler { get; }
    public DuelTurnCount<TCount> DuelTurnCount { get; private set; }

    public IBattler<THealthAndHarm, TStat, TCount> Attacker
    {
        get
        {
            if (DuelTurnCount.Duelist == DuelistTypes.none) throw new InvalidOperationException("There is no none-battler");
            return DuelTurnCount.Duelist == DuelistTypes.Proactive ? ProactiveBattler : ReactiveBattler;
        }
    }

    public IBattler<THealthAndHarm, TStat, TCount> Victim
    {
        get
        {
            if (DuelTurnCount.Duelist == DuelistTypes.none) throw new InvalidOperationException("There is no none-battler");
            return DuelTurnCount.Duelist == DuelistTypes.Proactive ? ReactiveBattler : ProactiveBattler;
        }
    }

    public Battle(IBattler<THealthAndHarm, TStat, TCount> proactiveBattler, IBattler<THealthAndHarm, TStat, TCount> reactiveBattler)
    {
        ProactiveBattler = proactiveBattler;
        ReactiveBattler = reactiveBattler;
        DuelTurnCount = new();
    }

    public void Attack()
    {
        DuelTurnCount = DuelTurnCount.GetNextOrThrow();

        AttackContext<THealthAndHarm, TStat, TCount> ctx = new(Attacker, Victim, this);

        ctx.Modify(Attacker.OffensiveMods);
        ctx.Modify(Victim.DefensiveMods);
        Victim.Take(ctx.GetHarm());
    }
}