﻿using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class ComboCompare:IComparer<Employee>
    {
        public ComboCompare(IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        public IComparer<Employee> FirstComparer { get; private set; }
        public IComparer<Employee> SecondComparer { get; private set; }

        public int Compare(Employee x, Employee y)
        {
            if (FirstComparer.Compare(x, y) != 0)
            {
                return FirstComparer.Compare(x, y);
            }

            return SecondComparer.Compare(x, y);


            //if (firstCompareResult < 0)
            //{
            //    return firstCompareResult;
            //    //finalCompareResult = firstCompareResult;
            //    //y = x;
            //    //index = i;
            //}

            //if (firstCompareResult == 0)
            //{
            //    if (secondCompareResult < 0)
            //    {
            //        return secondCompareResult;
            //        //finalCompareResult = secondCompareResult;
            //        //y = x;
            //        //index = i;
            //    }
            //}

            //return 0;
        }
    }
}