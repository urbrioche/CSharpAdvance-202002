﻿using System;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAllTests
    {
        [Test]
        public void girls_all_adult()
        {
            var girls = new List<Girl>
            {
                new Girl {Age = 20},
                new Girl {Age = 21},
                new Girl {Age = 17},
                new Girl {Age = 18},
                new Girl {Age = 30},
            };

            var actual = JoeyAll(girls, girl => girl.Age >= 18);
            Assert.IsFalse(actual);
        }

        [Test]
        public void girls_all_married()
        {
            var girls = new List<Girl>
            {
                new Girl {IsMarried = true},
                new Girl {IsMarried = true},
                new Girl {IsMarried = true},
                new Girl {IsMarried = true},
            };

            var actual = JoeyAll(girls, girl => girl.IsMarried);
            Assert.IsTrue(actual);
        }

        private bool JoeyAll(IEnumerable<Girl> girls, Func<Girl, bool> predicate)
        {
            var enumerator = girls.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var girl = enumerator.Current;
                if (predicate(girl))
                {
                    continue;
                }

                return false;
            }

            return true;
        }
    }
}