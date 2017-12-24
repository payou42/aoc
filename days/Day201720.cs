using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201720 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private List<Particle> _particles;

        private int _closest;


        public Day201720()
        {            
            Codename = "2017-20";
            Name = "Particle Swarm";
        }

        public string Run(Part part)
        {

            if (part == Part.Part1)
            {
                BuildParticles(part);
                return _closest.ToString();
            }

            if (part == Part.Part2)
            {
                BuildParticles(part);
                return _particles.Count.ToString();
            }

            return "";
        }

        private void BuildParticles(Part part)
        {
            // Create the poarticles swarm
            _particles = new List<Particle>();
            _closest = -1;
            string[] input = Input.GetStringVector(this);
            int loopsWithoutChange = 0;
            foreach (string item in input)
            {
                _particles.Add( new Particle(item) );
            }

            // Move them long enough            
            while (loopsWithoutChange < 300)
            {                
                // Compute closest
                Int64 min = Int64.MaxValue;
                int idx = -1;
                loopsWithoutChange++;
                for (int i = 0; i < _particles.Count; ++i)
                {
                    Int64 dst = _particles[i].Distance;
                    if (dst < min)
                    {
                        min = dst;
                        idx = i;
                    }
                }
                if (_closest != idx)
                {
                    loopsWithoutChange = 0;
                    _closest = idx;
                }
                

                // Simulate
                foreach (Particle p in _particles)
                {
                    p.Move();
                }

                if (part == Part.Part2)
                {
                    // Check collision
                    HashSet<Particle> collided = new HashSet<Particle>();
                    for (int i = 0; i < _particles.Count - 1; ++i)
                    {
                        for (int j = i + 1; j < _particles.Count; ++j)
                        {
                            if (Particle.Collide(_particles[i], _particles[j]))
                            {
                                collided.Add(_particles[i]);
                                collided.Add(_particles[j]);
                                loopsWithoutChange = 0;
                            }

                        }
                    }
                
                    // Remove collided particles
                    foreach (Particle p in collided)
                    {
                        _particles.Remove(p);
                    }
                }
            }
            
        }
    }   
}