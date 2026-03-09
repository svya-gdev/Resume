using System.Numerics;

namespace ResumeGame.Models.Usage;

public static class Application
{
    extension<T>(Stat<T> stat)
        where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
    {
        public Stat<T> Apply(Buff<T> buff)
        {
            var min = stat.Minimum;
            var cur = buff.Amount < T.MaxValue - stat.Current ? stat.Current + buff.Amount : T.MaxValue;
            return new(min, cur);
        }

        public Stat<T> Apply(Nerf<T> nerf)
        {
            var min = stat.Minimum;
            var cur = nerf.Amount < stat.Current ? stat.Current - nerf.Amount : stat.Minimum;
            return new(min, cur);
        }
    }

    extension<T>(Health<T> health)
        where T : INumber<T>, IUnsignedNumber<T>, IMinMaxValue<T>
    {
        public Health<T> Apply(Harm<T> harm)
        {
            var cur = harm.Amount < health.Current ? health.Current - harm.Amount : health.Minimum;
            var max = health.Maximum;
            return new(cur, max);
        }

        public Health<T> Apply(Heal<T> heal)
        {
            var cur = heal.Amount < health.Maximum - health.Current ? health.Current + heal.Amount : health.Maximum;
            var max = health.Maximum;
            return new(cur, max);
        }

        public Health<T> Apply(Buff<T> buff)
        {
            var cur = health.Current;
            var max = buff.Amount < T.MaxValue - health.Maximum ? health.Maximum + buff.Amount : T.MaxValue;
            return new(cur, max);
        }

        public Health<T> Apply(Nerf<T> nerf)
        {
            var cur = health.Current;
            var max = nerf.Amount < health.Maximum ? health.Maximum - nerf.Amount : health.Minimum;
            return new(cur, max);
        }
    }
}