using System.Numerics;
using ResumeGame.Models;

namespace ResumeGame.HarmModificationEngine;

public interface IHaveStats<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    Stat<T> Constitution { get; }
    Stat<T> Dexterity { get; }
    Stat<T> Strength { get; }
}