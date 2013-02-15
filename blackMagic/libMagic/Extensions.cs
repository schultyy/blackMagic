using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Jurassic.Library;

namespace libMagic
{
    public static class Extensions
    {
        public static object GetPropertyValue<TProperty>(this ObjectInstance instance, Expression<Func<TProperty>> property)
        {
            var propertyName = property.GetMemberInfo().Name;
            return instance.GetPropertyValue(propertyName);
        }

        public static void SetPropertyValue<TProperty>(this ObjectInstance instance, Expression<Func<TProperty>> property, object value)
        {
            instance.SetPropertyValue(property.GetMemberInfo().Name, value, true);
        }

        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            return (!(lambdaExpression.Body is UnaryExpression) ? (MemberExpression)lambdaExpression.Body : (MemberExpression)((UnaryExpression)lambdaExpression.Body).Operand).Member;
        }
    }
}
