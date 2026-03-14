using System.Numerics;

namespace ResumeGame.HarmModificationEngine;

public interface IFight<T> : IHaveDuelTurnCount<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>;