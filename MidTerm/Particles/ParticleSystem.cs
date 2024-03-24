using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Yew
{
    public class ParticleSystem
    {
        private Dictionary<long, Particle> m_particles = new Dictionary<long, Particle>();
        public Dictionary<long, Particle>.ValueCollection particles { get { return m_particles.Values; } }
        private MyRandom random = new MyRandom();

        private Vector2 center;
        private int sizeMean; // pixels
        private int sizeStdDev;   // pixels
        private float speedMean;  // pixels per millisecond
        private float speedStDev; // pixles per millisecond
        private float lifetimeMean; // milliseconds
        private float lifetimeStdDev; // milliseconds
        private Vector2? direction = null;

        public ParticleSystem(Vector2 center, int sizeMean, int sizeStdDev, float speedMean, float speedStdDev, int lifetimeMean, int lifetimeStdDev, Vector2? dir)
        {
            this.center = center;
            this.sizeMean = sizeMean;
            this.sizeStdDev = sizeStdDev;
            this.speedMean = speedMean;
            this.speedStDev = speedStdDev;
            this.lifetimeMean = lifetimeMean;
            this.lifetimeStdDev = lifetimeStdDev;
            this.direction = dir;
        }

        private Particle create()
        {
            float size = (float)random.nextGaussian(sizeMean, sizeStdDev);
            Vector2 dir = direction != null ? (Vector2)direction : random.nextCircleVector();
            var p = new Particle(
                    center,
                    dir,
                    (float)random.nextGaussian(speedMean, speedStDev),
                    new Vector2(size, size),
                    new System.TimeSpan(0, 0, 0, 0, (int)(random.nextGaussian(lifetimeMean, lifetimeStdDev)))); ;

            return p;
        }

        public void update(GameTime gameTime, Vector2? newPos, Vector2? newDir)
        {
            // Update existing particles
            center = newPos != null ? (Vector2)newPos : center;
            direction = newDir != null ? (Vector2)newDir : direction;
            List<long> removeMe = new List<long>();
            foreach (Particle p in m_particles.Values)
            {
                if (!p.update(gameTime))
                {
                    removeMe.Add(p.name);
                }
            }

            // Remove dead particles
            foreach (long key in removeMe)
            {
                m_particles.Remove(key);
            }

            // Generate some new particles
            for (int i = 0; i < 8; i++)
            {
                var particle = create();
                m_particles.Add(particle.name, particle);
            }
        }
    }
}

