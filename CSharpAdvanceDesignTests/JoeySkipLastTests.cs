﻿using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySkipLastTests
    {
        [Test]
        public void skip_last_2()
        {
            var numbers = new[] { 10, 20, 30, 40, 50 };
            var actual = JoeySkipLast(numbers, 2);

            var expected = new[] { 10, 20, 30 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }


        [Test]
        public void skip_last_2_with_2_numbers()
        {
            var numbers = new[] { 40, 50 };
            var actual = JoeySkipLast(numbers, 2);

            var expected = new int[]{ };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void skip_last_2_with_1_numbers()
        {
            var numbers = new[] { 40 };
            var actual = JoeySkipLast(numbers, 2);

            var expected = new int[]{ };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeySkipLast(IEnumerable<int> numbers, int count)
        {
            var queue = new Queue<int>();
            var enumerator = numbers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (queue.Count == count)
                {
                    yield return queue.Dequeue();
                }

                queue.Enqueue(current);
            }

            //var queue = new Queue<int>(numbers);

            //var enumerator = numbers.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    if (queue.Count > count)
            //    {
            //        yield return queue.Dequeue();
            //    }
            //}
        }
    }
}