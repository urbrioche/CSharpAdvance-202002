﻿using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    [Ignore("not yet")]
    public class JoeyFirstOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<TSource>();

            var actual = JoeyFirstOrDefault(employees);

            Assert.IsNull(actual);
        }

        private TSource JoeyFirstOrDefault(IEnumerable<TSource> employees)
        {
            throw new NotImplementedException();
        }
    }
}