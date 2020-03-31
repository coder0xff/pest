using System;
using System.Dynamic;
using System.Linq;

namespace PrivateInterfaceTester
{
    class Pest<T> : DynamicObject
    {
        private T _underlying;

        public Pest()
        {
        }

        public Pest(T underlying)
        {
            _underlying = underlying;
        }

        public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
        {
            if (_underlying == null)
            {
                throw new NullReferenceException();
            }

            result =
                Dispatch.BuildBinaryOp(binder.Operation, _underlying.GetType(), arg.GetType(), binder.ReturnType)(
                    _underlying, arg);
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (_underlying == null)
            {
                throw new NullReferenceException();
            }

            var argTypes = args.Select(x => x.GetType()).ToArray();

            result = Dispatch.BuildInstanceMemberInvoke(_underlying.GetType(), binder.Name, argTypes, binder.ReturnType)(_underlying, args);
            return true;
        }
    }
}