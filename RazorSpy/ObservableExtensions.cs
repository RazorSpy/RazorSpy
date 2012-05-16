using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

namespace RazorSpy
{
    public static class ObservableExtensions
    {
        public static IObservable<Unit> IgnoreValues<T>(this IObservable<T> self)
        {
            return self.Select(_ => Unit.Default);
        }
    }
}
