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
        public void girls_all_adult_yes()
        {
            var girls = new List<Girl>
            {
                new Girl {Age = 20},
                new Girl {Age = 21},
                new Girl {Age = 18},
                new Girl {Age = 18},
                new Girl {Age = 30},
            };

            var actual = JoeyAll(girls, girl => girl.Age >= 18);
            Assert.IsTrue(actual);
        }

        private bool JoeyAll<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (!predicate(current))
                {
                    return false;
                }
            }

            return true;
        }
    }
}