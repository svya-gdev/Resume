using System.Numerics;

namespace ResumeGame.HarmModificationEngine;

public interface IFighter<THealthAndHarm, TStat> : IHaveHealth<THealthAndHarm>, IHaveDamage<THealthAndHarm>, IHaveStats<TStat>
    where THealthAndHarm : INumber<THealthAndHarm>, IUnsignedNumber<THealthAndHarm>, IMinMaxValue<THealthAndHarm>
    where TStat : INumber<TStat>, IUnsignedNumber<TStat>, IMinMaxValue<TStat>;