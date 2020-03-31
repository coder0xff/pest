using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

static class Memoizer
{
    public static Func<T1, TResult> Memoize<T1, TResult>(this Func<T1, TResult> f)
    {
        var cache = new ConcurrentDictionary<T1, TResult>();

        return argument => cache.GetOrAdd(argument, f);
    }

    public static Func<T1, T2, TResult> Memoize<T1, T2, TResult>(this Func<T1, T2, TResult> f)
    {
        var m = Memoize<Tuple<T1, T2>, TResult>(args => f(args.Item1, args.Item2));
        return (a1, a2) => m(new Tuple<T1, T2>(a1, a2));
    }

    public static Func<T1, T2, T3, TResult> Memoize<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> f)
    {
        var m = Memoize<Tuple<T1, T2, T3>, TResult>(args => f(args.Item1, args.Item2, args.Item3));
        return (a1, a2, a3) => m(new Tuple<T1, T2, T3>(a1, a2, a3));
    }

    public static Func<T1, T2, T3, T4, TResult> Memoize<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> f)
    {
        var m = Memoize<Tuple<T1, T2, T3, T4>, TResult>(args => f(args.Item1, args.Item2, args.Item3, args.Item4));
        return (a1, a2, a3, a4) => m(new Tuple<T1, T2, T3, T4>(a1, a2, a3, a4));
    }

}
