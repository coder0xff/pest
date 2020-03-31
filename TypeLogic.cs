using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

static class TypeLogic
{
    static TypeLogic()
    {
        GetBaseTypes = GetBaseTypesImpl;
        GetBaseTypes = GetBaseTypes.Memoize();
    }

    private static List<Type> GetBaseTypesImpl(Type t)
    {
        var results = new List<Type>();
        var q = new Queue<Type>();
        q.Enqueue(t);
        while (q.Count > 0)
        {
            var v = q.Dequeue();
            results.Add(v);
            var baseType = v.BaseType;
            if (baseType != null)
            {
                q.Enqueue(baseType);
            }
            foreach (var interfaceType in v.GetInterfaces())
            {
                q.Enqueue(interfaceType);
            }
        }

        return results;
    }

    private static Func<Type, List<Type>> GetBaseTypes;

}
