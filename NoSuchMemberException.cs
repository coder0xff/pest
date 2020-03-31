using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateInterfaceTester
{
    class NoSuchMemberException : Exception
    {
        public string Type { get; }
        public string Member { get; }

        public NoSuchMemberException(string fullyQualifiedName, string member)
        {
            Type = fullyQualifiedName;
            Member = member;
        }

        public override string ToString()
        {
            return "Type '" + Type + "' does not contain a member called '" + Member + "'.";
        }
    }
}
