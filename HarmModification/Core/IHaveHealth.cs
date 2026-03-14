using System.Numerics;
using ResumeGame.Models;

namespace ResumeGame.HarmModificationEngine;

public interface IHaveHealth<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    Health<T> Health { get; }
}