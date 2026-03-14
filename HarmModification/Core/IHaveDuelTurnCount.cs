using System.Numerics;
using AnyGame.TurnCount;

namespace ResumeGame.HarmModificationEngine;

public interface IHaveDuelTurnCount<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    DuelTurnCount<T> DuelTurnCount { get; }
}