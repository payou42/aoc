using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace _20._Particles
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the poarticles swarm
            List<Particle> particles = new List<Particle>();
            string[] input = Input.RAW.Split("\r\n");
            foreach (string item in input)
            {
                particles.Add( new Particle(item) );
            }

            // Move them long enough
            int closest   = -1;
            int iteration = 0;
            while (iteration < 1000)
            {                
                // Compute closest
                Int64 min = Int64.MaxValue;
                int idx = -1;
                for (int i = 0; i < particles.Count; ++i)
                {
                    Int64 dst = particles[i].Distance;
                    if (dst < min)
                    {
                        min = dst;
                        idx = i;
                    }
                }
                closest = idx;

                // Simulate
                foreach (Particle p in particles)
                {
                    p.Move();
                }

                // Check collision
                HashSet<Particle> collided = new HashSet<Particle>();
                for (int i = 0; i < particles.Count - 1; ++i)
                {
                    for (int j = i + 1; j < particles.Count; ++j)
                    {
                        if (Particle.Collide(particles[i], particles[j]))
                        {
                            collided.Add(particles[i]);
                            collided.Add(particles[j]);
                        }

                    }
                }

                // Remove collided particles
                foreach (Particle p in collided)
                {
                    particles.Remove(p);
                }

                // Next iteration
                Console.WriteLine("Closest particle is {0}, determined after iteration {1}; particles count = {2}", closest, iteration, particles.Count);
                iteration++;
            }

            

        }
    }
}
