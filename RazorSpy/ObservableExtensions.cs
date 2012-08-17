using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using ReactiveUI;

namespace RazorSpy
{
    public static class ObservableExtensions
    {
        public static IObservable<Unit> IgnoreValues<T>(this IObservable<T> self)
        {
            return self.Select(_ => Unit.Default);
        }

        public static IObservable<TRet> ForProperty<TModel, TRet>(this IObservable<ObservedChange<TModel, object>> self, Expression<Func<TModel, TRet>> property)
        {
            string propertyName = GetMemberName<TModel, TRet>(property);
            return self.Where(c => String.Equals(c.PropertyName, propertyName, StringComparison.OrdinalIgnoreCase)).Select(c => (TRet)c.Value);
        }

        public static IObservable<object> ForAllPropertiesExcept<TModel>(this IObservable<ObservedChange<TModel, object>> self, Expression<Func<TModel, object>> property)
        {
            string propertyName = GetMemberName<TModel, object>(property);
            return self.Where(c => !String.Equals(c.PropertyName, propertyName, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetMemberName<TModel, TRet>(Expression<Func<TModel, TRet>> property)
        {
            MemberExpression expr = property.Body as MemberExpression;
            Debug.Assert(expr != null, "Expression must be a member-access expression");
            string propertyName = expr.Member.Name;
            return propertyName;
        }
    }
}
