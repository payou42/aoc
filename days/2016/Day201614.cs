using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aoc.Common;
using Aoc.Common.Strings;

namespace Aoc
{
    public class Day201614 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _salt;

        public Day201614()
        {
            Codename = "2016-14";
            Name = "One-Time Pad";
        }

        public void Init()
        {
            _salt = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                long pepper = -1;
                List<(long, string)> keys = new List<(long, string)>();
                Dictionary<char, List<(long, string)>> candidates = new Dictionary<char, List<(long, string)>>();
                using (MD5 md5Hash = MD5.Create())
                {
                    while (keys.Count < 64)
                    {
                        // Increment our ounter
                        pepper++;

                        // Clean invalid potential keys
                        CleanCandidates(candidates, pepper - 1000);

                        // Compute the hash
                        string hash = ComputeMD5(md5Hash, _salt + pepper.ToString());

                        // Check if we have quintuples
                        foreach (var kvp in candidates)
                        {
                            if (kvp.Value.Any() && Repetition.Has(hash, kvp.Key, 5))
                            {
                                // Validate those keys
                                keys.AddRange(kvp.Value);

                                // Check if we have more than 64 keys
                                if (keys.Count >= 64)
                                {
                                    return keys[63].Item1.ToString();
                                }

                                // Clear candidates
                                kvp.Value.Clear();
                            }
                        }
                        
                        // Check if we have a triplicates
                        char? firstTriple = Repetition.First(hash, 3);
                        if (firstTriple.HasValue)
                        {
                            if (!candidates.ContainsKey(firstTriple.Value))
                            {
                                candidates.Add(firstTriple.Value, new List<(long, string)>());
                            }
                            candidates[firstTriple.Value].Add((pepper, hash));
                        }
                    }
                }

                return pepper.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                long pepper = -1;
                List<(long, string)> keys = new List<(long, string)>();
                Dictionary<char, List<(long, string)>> candidates = new Dictionary<char, List<(long, string)>>();
                using (MD5 md5Hash = MD5.Create())
                {
                    while (keys.Count < 64)
                    {
                        // Increment our ounter
                        pepper++;

                        // Clean invalid potential keys
                        CleanCandidates(candidates, pepper - 1000);

                        // Compute the hash
                        string hash = ComputeMD5(md5Hash, _salt + pepper.ToString());
                        for (int i = 0; i < 2016; ++i)
                        {
                            hash = ComputeMD5(md5Hash, hash);
                        }

                        // Check if we have quintuples
                        foreach (var kvp in candidates)
                        {
                            if (kvp.Value.Any() && Repetition.Has(hash, kvp.Key, 5))
                            {
                                // Validate those keys
                                keys.AddRange(kvp.Value);

                                // Check if we have more than 64 keys
                                if (keys.Count >= 64)
                                {
                                    return keys[63].Item1.ToString();
                                }

                                // Clear candidates
                                kvp.Value.Clear();
                            }
                        }
                        
                        // Check if we have a triplicates
                        char? firstTriple = Repetition.First(hash, 3);
                        if (firstTriple.HasValue)
                        {
                            if (!candidates.ContainsKey(firstTriple.Value))
                            {
                                candidates.Add(firstTriple.Value, new List<(long, string)>());
                            }
                            candidates[firstTriple.Value].Add((pepper, hash));
                        }
                    }
                }

                return pepper.ToString();
            }

            return "";
        }

        private string ComputeMD5(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void CleanCandidates(Dictionary<char, List<(long, string)>> candidates, long limit)
        {
            foreach (var key in candidates.Keys.ToList())            
            {
                candidates[key] = candidates[key].Where(item => item.Item1 >= limit).ToList();
            }
        }
    }   
}