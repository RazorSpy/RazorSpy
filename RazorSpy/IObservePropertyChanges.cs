using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReactiveUI;

namespace RazorSpy
{
    public interface IObservePropertyChanges
    {
        IObservable<ObservedChange<object, object>> PropertyChanged { get; }
    }

    public interface IObservePropertyChanges<TSelf> : IObservePropertyChanges
    {
        new IObservable<ObservedChange<TSelf, object>> PropertyChanged { get; }
    }
}
