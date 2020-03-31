using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;

namespace PrivateInterfaceTester
{
    internal static class Dispatch
    {
        static Dispatch()
        {
            BuildBinaryOp = BuildBinaryOpImpl;
            BuildBinaryOp = BuildBinaryOp.Memoize();

            BuildInstanceMemberInvoke = BuildInstanceMemberInvokeImpl;
            BuildInstanceMemberInvoke = BuildInstanceMemberInvoke.Memoize();

            BuildStaticMemberInvoke = BuildStaticMemberInvokeImpl;
            //BuildStaticMemberInvoke = BuildStaticMemberInvoke.Memoize();
        }

        [CanBeNull]
        private static Func<object, object, object> BuildBinaryOpImpl(ExpressionType op, Type lhs, Type rhs, Type ret)
        {
            MethodInfo m = null;
            string opName = null;
            bool assignResult;
            switch (op)
            {
                case ExpressionType.AddAssign:
                    opName = "op_Addition";
                    break;
                case ExpressionType.Add:
                    opName = "op_Addition";
                    break;
                case ExpressionType.AddAssignChecked:
                    throw new NotImplementedException();
                    break;
                case ExpressionType.AddChecked:
                    throw new NotImplementedException();
                    break;
                case ExpressionType.And:
                    opName = "op_BitwiseAnd";
                    break;
                //case ExpressionType.AndAlso:
                //    break;
                case ExpressionType.AndAssign:
                    opName = "op_BitwiseAnd";
                    break;
                case ExpressionType.ArrayIndex:
                    if (lhs.IsArray)
                    {
                        return delegate(object array, object index) { return ((IList<object>) array)[(int) index]; };
                    }

                    var p = lhs.GetProperty("Item", BindingFlags.Instance);
                    if (p.GetIndexParameters().Length == 0)
                    {
                        throw new ApplicationException("Expected " + lhs.Name + ".Item to be an indexable property.");
                    }

                    return (o, index) => p.GetValue(o, new[] {index});
                //case ExpressionType.ArrayLength:
                //    break;
                //case ExpressionType.Assign:
                //    break;
                //case ExpressionType.Block:
                //    break;
                //case ExpressionType.Call:
                //    break;
                //case ExpressionType.Coalesce:
                //    break;
                //case ExpressionType.Conditional:
                //    break;
                //case ExpressionType.Constant:
                //    break;
                //case ExpressionType.Convert:
                //    break;
                //case ExpressionType.ConvertChecked:
                //    break;
                //case ExpressionType.DebugInfo:
                //    break;
                //case ExpressionType.Decrement:
                //    break;
                //case ExpressionType.Default:
                //    break;
                //case ExpressionType.Divide:
                //    break;
                //case ExpressionType.DivideAssign:
                //    break;
                //case ExpressionType.Dynamic:
                //    break;
                //case ExpressionType.Equal:
                //    break;
                //case ExpressionType.ExclusiveOr:
                //    break;
                //case ExpressionType.ExclusiveOrAssign:
                //    break;
                //case ExpressionType.Extension:
                //    break;
                //case ExpressionType.Goto:
                //    break;
                //case ExpressionType.GreaterThan:
                //    break;
                //case ExpressionType.GreaterThanOrEqual:
                //    break;
                //case ExpressionType.Increment:
                //    break;
                //case ExpressionType.Index:
                //    break;
                //case ExpressionType.Invoke:
                //    break;
                //case ExpressionType.IsFalse:
                //    break;
                //case ExpressionType.IsTrue:
                //    break;
                //case ExpressionType.Label:
                //    break;
                //case ExpressionType.Lambda:
                //    break;
                //case ExpressionType.LeftShift:
                //    break;
                //case ExpressionType.LeftShiftAssign:
                //    break;
                //case ExpressionType.LessThan:
                //    break;
                //case ExpressionType.LessThanOrEqual:
                //    break;
                //case ExpressionType.ListInit:
                //    break;
                //case ExpressionType.Loop:
                //    break;
                //case ExpressionType.MemberAccess:
                //    break;
                //case ExpressionType.MemberInit:
                //    break;
                //case ExpressionType.Modulo:
                //    break;
                //case ExpressionType.ModuloAssign:
                //    break;
                //case ExpressionType.Multiply:
                //    break;
                //case ExpressionType.MultiplyAssign:
                //    break;
                //case ExpressionType.MultiplyAssignChecked:
                //    break;
                //case ExpressionType.MultiplyChecked:
                //    break;
                //case ExpressionType.Negate:
                //    break;
                //case ExpressionType.NegateChecked:
                //    break;
                //case ExpressionType.New:
                //    break;
                //case ExpressionType.NewArrayBounds:
                //    break;
                //case ExpressionType.NewArrayInit:
                //    break;
                //case ExpressionType.Not:
                //    break;
                //case ExpressionType.NotEqual:
                //    break;
                //case ExpressionType.OnesComplement:
                //    break;
                //case ExpressionType.Or:
                //    break;
                //case ExpressionType.OrAssign:
                //    break;
                //case ExpressionType.OrElse:
                //    break;
                //case ExpressionType.Parameter:
                //    break;
                //case ExpressionType.PostDecrementAssign:
                //    break;
                //case ExpressionType.PostIncrementAssign:
                //    break;
                //case ExpressionType.Power:
                //    break;
                //case ExpressionType.PowerAssign:
                //    break;
                //case ExpressionType.PreDecrementAssign:
                //    break;
                //case ExpressionType.PreIncrementAssign:
                //    break;
                //case ExpressionType.Quote:
                //    break;
                //case ExpressionType.RightShift:
                //    break;
                //case ExpressionType.RightShiftAssign:
                //    break;
                //case ExpressionType.RuntimeVariables:
                //    break;
                //case ExpressionType.Subtract:
                //    break;
                //case ExpressionType.SubtractAssign:
                //    break;
                //case ExpressionType.SubtractAssignChecked:
                //    break;
                //case ExpressionType.SubtractChecked:
                //    break;
                //case ExpressionType.Switch:
                //    break;
                //case ExpressionType.Throw:
                //    break;
                //case ExpressionType.Try:
                //    break;
                //case ExpressionType.TypeAs:
                //    break;
                //case ExpressionType.TypeEqual:
                //    break;
                //case ExpressionType.TypeIs:
                //    break;
                //case ExpressionType.UnaryPlus:
                //    break;
                //case ExpressionType.Unbox:
                //    break;
                default:
                    throw new NotImplementedException();
            }

            if (opName != null)
            {
                m = lhs.GetMethod(opName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] {null, lhs, rhs}, null);
                return (o, arg) => m.Invoke(o, new[] {arg});
            }

            throw new ApplicationException();
        }

        public static readonly Func<ExpressionType /*op*/, Type /*lhs*/, Type /*rhs*/, Type /*ret*/
            , Func<object, object, object>> BuildBinaryOp;

        private static Func<object, object[], object> BuildInstanceMemberInvokeImpl(Type t, string name, Type[] argTypes,
            Type returnType)
        {
            var method = t.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, argTypes, null);
            if (method == null)
            {
                throw new NoSuchMemberException(t.FullName, name);
            }
            return method.Invoke;
        }

        public static readonly Func<Type /*t*/, string /*name*/, Type[] /*argTypes*/, Type /*returnType*/, Func<object, object[], object>> BuildInstanceMemberInvoke;
        private static Func<object[], object> BuildStaticMemberInvokeImpl(Type t, string name, Type[] argTypes,
            Type returnType)
        {
            var method = t.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, argTypes, null);
            if (method == null)
            {
                throw new NoSuchMemberException(t.FullName, name);
            }

            return args => method.Invoke(null, args);
        }

        public static readonly Func<Type /*t*/, string /*name*/, Type[] /*argTypes*/, Type /*returnType*/, Func<object[], object>> BuildStaticMemberInvoke;

    }
}