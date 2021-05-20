/*
 * 
 * 
 * Part of EAFramework
 * 
 * Created by: Pierre Rossel 2020-04-01
 * 
 */

using System;
using UnityEngine;

namespace EAFramework
{
    public class Comparison
    {
        public enum Type
        {
            Equal,
            NotEqual,
            Greater,
            Smaller,
            GreaterOrEqual,
            SmallerOrEqual
        }
        
        public static bool Compare<T>(T v1, Type type, T v2) where T : IComparable<T>
        {

            switch (type)
            {
                case Type.Equal: return v1.Equals(v2);
                case Type.NotEqual: return !v1.Equals(v2);
                case Type.Greater: return v1.CompareTo(v2) > 0;
                case Type.Smaller: return v1.CompareTo(v2) < 0;
                case Type.GreaterOrEqual: return v1.Equals(v2) || v1.CompareTo(v2) > 0;
                case Type.SmallerOrEqual: return v1.Equals(v2) || v1.CompareTo(v2) < 0;
                
                default: return false;
            }   
        }

        public static void UnitTest()
        {
            void Assert(bool v1, bool v2)
            {
                Debug.Log((v1 == v2) ? "OK" : "Error");
            }

            // returns true
            Assert(Compare(1, Type.Equal, 1), true);
            Assert(Compare(1, Type.NotEqual, 2), true);
            Assert(Compare(1, Type.Greater, 0), true);
            Assert(Compare(1, Type.Smaller, 2), true);
            Assert(Compare(1, Type.GreaterOrEqual, 1), true);
            Assert(Compare(1, Type.GreaterOrEqual, 0), true);
            Assert(Compare(1, Type.SmallerOrEqual, 1), true);
            Assert(Compare(1, Type.SmallerOrEqual, 2), true);

            // return false
            Assert(Compare(1, Type.Equal, 2), false);
            Assert(Compare(1, Type.NotEqual, 1), false);
            Assert(Compare(1, Type.Greater, 2), false);
            Assert(Compare(1, Type.Smaller, 0), false);
            Assert(Compare(1, Type.GreaterOrEqual, 2), false);
            Assert(Compare(1, Type.SmallerOrEqual, 0), false);
        }

    }

}