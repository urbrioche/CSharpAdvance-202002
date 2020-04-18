﻿using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAppendTests
    {
        [Test]
        public void append_an_employee_to_employees()
        {
            var employees = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen"},
            };

            var newEmployee = new Employee() { FirstName = "Tom", LastName = "Li" };

            var actual = JoeyAppend(employees, newEmployee);

            var expected = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen"},
                new Employee() {FirstName = "Tom", LastName = "Li"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyAppend(IEnumerable<Employee> employees, Employee newEmployee)
        {
            var enumerator = employees.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                yield return current;
            }

            yield return newEmployee;
        }
    }
}