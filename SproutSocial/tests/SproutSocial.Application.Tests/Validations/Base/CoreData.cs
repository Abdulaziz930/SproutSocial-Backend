using System;
using System.Collections.Generic;

namespace SproutSocial.Application.Tests.Validations.Base;

public class CoreData
{
    public static IEnumerable<object[]> String101
    {
        get
        {
            yield return new object[] { "" };
            yield return new object[] { " " };
            yield return new object[] { null };
            yield return new object[] { new String('*', 101) };
        }
    }

    public static IEnumerable<object[]> Id
    {
        get
        {
            yield return new object[] { String.Empty };
            yield return new object[] { null };
            yield return new object[] { new String('*', 1) };
        }
    }
}
