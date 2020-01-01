using System.Reflection;

namespace NStandard
{
    public class MethodReflector
    {
        public readonly MethodInfo MethodInfo;
        public object DeclaringObject;

        public MethodReflector(MethodInfo methodInfo, object declaringObj)
        {
            MethodInfo = methodInfo;
            DeclaringObject = declaringObj;
        }

        public object Invoke(params object[] parameters) => MethodInfo.Invoke(DeclaringObject, parameters);
    }

}
