using System.Numerics;
using ResumeGame.Models;
using ResumeGame.HarmModificationEngine;
using AnyGame.TurnCount;
using AnyGame.TurnCount.Usage;

public interface IBattler<THealthAndHarm, TStat, TCount> : IFighter<THealthAndHarm, TStat>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>
    where TCount : INumber<TCount>, IUnsignedNumber<TCount>, IMinMaxValue<TCount>
{
    new Health<THealthAndHarm> Health { get; set; }
    IEnumerable<IMod<THealthAndHarm, TStat, TCount>> OffensiveMods { get; }
    IEnumerable<IMod<THealthAndHarm, TStat, TCount>> DefensiveMods { get; }
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

    public Battle(IBattler<THealthAndHarm, TStat, TCount> priorityBattler, IBattler<THealthAndHarm, TStat, TCount> battler)
    {
        (ProactiveBattler, ReactiveBattler)
            = (priorityBattler.Dexterity.Current >= battler.Dexterity.Current)
            ? (priorityBattler, battler)
            : (battler, priorityBattler);
        DuelTurnCount = new();
    }

    public void Attack()
    {
        DuelTurnCount = DuelTurnCount.GetNextOrThrow();

        (IBattler<THealthAndHarm, TStat, TCount> one, IBattler<THealthAndHarm, TStat, TCount> two) they
            = (DuelTurnCount.Duelist == DuelistTypes.Proactive)
            ? (ProactiveBattler, ReactiveBattler)
            : (ReactiveBattler, ProactiveBattler);

        AttackContext<THealthAndHarm, TStat, TCount> ctx = new(they.one, they.two, this);
    }
}