﻿using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyJoinTests
    {
        [Test]
        public void all_pets_and_owner()
        {
            var david = new Employee { FirstName = "David", LastName = "Chen" };
            var joey = new Employee { FirstName = "Joey", LastName = "Chen" };
            var tom = new Employee { FirstName = "Tom", LastName = "Chen" };

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Lala", Owner = joey},
                new Pet() {Name = "Didi", Owner = david},
                new Pet() {Name = "Fufu", Owner = tom},
                new Pet() {Name = "QQ", Owner = joey},
            };

            var actual = JoeyJoin(employees, pets, employee1 => employee1, pet1 => pet1.Owner, (employee, pet) => Tuple.Create(employee.FirstName, pet.Name));

            var expected = new[]
            {
                Tuple.Create("David", "Didi"),
                Tuple.Create("Joey", "Lala"),
                Tuple.Create("Joey", "QQ"),
                Tuple.Create("Tom", "Fufu"),
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Tuple<string, string>> JoeyJoin(
            IEnumerable<Employee> employees,
            IEnumerable<Pet> pets,
            Func<Employee, Employee> outerKeySelector,
            Func<Pet, Employee> innerKeySelector,
            Func<Employee, Pet, Tuple<string, string>> resultSelector)
        {
            var empEnumerator = employees.GetEnumerator();
            while (empEnumerator.MoveNext())
            {
                var employee = empEnumerator.Current;
                var petEnumerator = pets.GetEnumerator();
                while (petEnumerator.MoveNext())
                {
                    var pet = petEnumerator.Current;
                    if (outerKeySelector(employee) == innerKeySelector(pet))
                    {
                        yield return resultSelector(employee, pet);
                    }
                }
            }
        }
    }
}