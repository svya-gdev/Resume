using System.Numerics;

namespace AnyGame.TurnCount.Usage;

public static class NeighbourGetting
{
    extension<T>(DuelTurnCount<T> dtc)
        where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
    {
        public bool CanGoToPrev => dtc.Objective != T.MinValue;
        public bool CanGoToNext => dtc.Objective != T.MaxValue;

        public DuelTurnCount<T> GetPrevOrThrow()
        {
            if (dtc.CanGoToPrev) return new(dtc.Objective - T.One);
            throw new InvalidOperationException("Cannot go to previous turn from zero.");
        }

        public DuelTurnCount<T> GetNextOrThrow()
        {
            if (dtc.CanGoToNext) return new(dtc.Objective + T.One);
            throw new InvalidOperationException("Cannot go to next turn from maximum.");
        }
    }
}