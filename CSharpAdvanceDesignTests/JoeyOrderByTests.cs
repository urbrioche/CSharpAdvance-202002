﻿using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyComparer
    {
        public CombineKeyComparer(Func<Employee, string> firstKeySelector, IComparer<string> firstKeyComparer)
        {
            FirstKeySelector = firstKeySelector;
            FirstKeyComparer = firstKeyComparer;
        }

        public Func<Employee, string> FirstKeySelector { get; private set; }
        public IComparer<string> FirstKeyComparer { get; private set; }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        //[Test]
        //public void orderBy_lastName()
        //{
        //    var employees = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    };

        //    var actual = JoeyOrderByLastNameAndFirstName(employees);

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}

        [Test]
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyOrderByLastNameAndFirstName(
                employees,
                new CombineKeyComparer(employee => employee.LastName, Comparer<string>.Default),
                employee => employee.FirstName, Comparer<string>.Default);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private static int Compare(CombineKeyComparer combineKeyComparer, Employee currentElement, Employee minElement)
        {
            return combineKeyComparer.FirstKeyComparer.Compare(combineKeyComparer.FirstKeySelector(currentElement),
                                                               combineKeyComparer.FirstKeySelector(minElement));
        }

        private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName(
            IEnumerable<Employee> employees,
            CombineKeyComparer combineKeyComparer,
            Func<Employee, string> secondKeySelector,
            IComparer<string> secondKeyComparer)
        {
            //bubble sort
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var currentElement = elements[i];

                    var firstCompareResult = Compare(combineKeyComparer, currentElement, minElement);
                    if (firstCompareResult < 0)
                    {
                        minElement = currentElement;
                        index = i;
                    }
                    else if (firstCompareResult == 0)
                    {
                        var secondCompareResult =
                            secondKeyComparer.Compare(secondKeySelector(currentElement),
                                                      secondKeySelector(minElement));
                        if (secondCompareResult < 0)
                        {
                            minElement = currentElement;
                            index = i;
                        }
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}