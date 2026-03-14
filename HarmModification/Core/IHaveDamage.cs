using System.Numerics;
using ResumeGame.Models;

namespace ResumeGame.HarmModificationEngine;

public interface IHaveDamage<T>
    where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
{
    Damage<T> Damage { get; }
}