using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NStandard.Linq;

internal class DependencyCollector
{
    private readonly List<object> _dependencies = new();

    internal void Collect(Expression expression, params Type[] types)
    {
        object GetMemberValue(MemberExpression member)
        {
            if (member.Expression is null)
            {
                if (member.Member is FieldInfo fieldInfo)
                {
                    return fieldInfo.GetValue(null);
                }
                else if (member.Member is PropertyInfo propertyInfo)
                {
                    return propertyInfo.GetValue(null);
                }
                else throw new NotImplementedException();
            }
            else
            {
                var memberExpression = member.Expression;
                if (memberExpression is ConstantExpression memberConstant)
                {
                    var type = memberConstant.Value.GetType();
                    var innerMember = type.GetField(member.Member.Name);
                    return innerMember.GetValue(memberConstant.Value);
                }
                else if (memberExpression is MemberExpression memberMember)
                {
                    return GetMemberValue(memberMember);
                }
                else throw new NotImplementedException();
            }
        }

        if (expression is LambdaExpression lambda)
        {
            Collect(lambda.Body, types);
        }
        else if (expression is UnaryExpression unary)
        {
            Collect(unary.Operand, types);
        }
        else if (expression is BinaryExpression binary)
        {
            Collect(binary.Left, types);
            Collect(binary.Right, types);
        }
        else if (expression is MethodCallExpression methodCall)
        {
            foreach (var argument in methodCall.Arguments)
            {
                Collect(argument, types);
            }
        }
        else if (expression is MemberExpression member)
        {
            if (types.Any(member.Type.IsType))
            {
                var dependency = GetMemberValue(member);
                _dependencies.Add(dependency);
            }
            else if (member.Type == typeof(string))
            {
                Collect(member.Expression, types);
            }
            else if (member.Type.IsImplement(typeof(IEnumerable<>)))
            {
                var elementType = member.Type.GetElementType();
                if (types.Any(elementType.IsType))
                {
                    var dependencies = GetMemberValue(member) as IEnumerable;
                    foreach (var dependency in dependencies)
                    {
                        _dependencies.Add(dependency);
                    }
                }
            }
            else
            {
                Collect(member.Expression, types);
            }
        }
        else if (expression is NewArrayExpression newArray)
        {
            foreach (var innerExpression in newArray.Expressions)
            {
                Collect(innerExpression, types);
            }
        }
        else if (expression is NewExpression @new)
        {
            foreach (var argument in @new.Arguments)
            {
                Collect(argument, types);
            }
        }
    }

    internal IEnumerable<object> GetDependencies()
    {
        return _dependencies.AsEnumerable();
    }

    internal void Clear()
    {
        _dependencies.Clear();
    }

}
