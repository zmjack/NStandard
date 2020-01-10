using System.Reflection;

namespace NStd
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

        public object Call(params object[] parameters) => MethodInfo.Invoke(DeclaringObject, parameters);
        public object Invoke(object obj, params object[] parameters) => MethodInfo.Invoke(obj, parameters);
    }

}
