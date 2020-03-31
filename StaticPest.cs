using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace PrivateInterfaceTester
{
    public class StaticPest<T> : DynamicObject
    {
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var argTypes = args.Select(x => x.GetType()).ToArray();

            result = Dispatch.BuildStaticMemberInvoke(typeof(T), binder.Name, argTypes, binder.ReturnType)(args);
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var asNestedType = typeof(T).GetNestedType(binder.Name, BindingFlags.Public | BindingFlags.NonPublic);
            if (asNestedType != null)
            {
                result = (DynamicObject)Activator.CreateInstance(typeof(StaticPest<>).MakeGenericType(asNestedType));
                return true;
            }

            throw new NotImplementedException();
        }
    }
}