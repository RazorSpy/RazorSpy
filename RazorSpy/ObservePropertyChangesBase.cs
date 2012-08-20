using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Text;
using ReactiveUI;
using System.Runtime.CompilerServices;

namespace RazorSpy
{
    // Holy Generic Type Backflips Batman!

    public abstract class ObservePropertyChangesBase<TSelf> : ObservePropertyChangesBase<TSelf, TSelf>
        where TSelf : ObservePropertyChangesBase<TSelf>
    {}

    public abstract class ObservePropertyChangesBase<TSelf, TObserveType> : IObservePropertyChanges, IObservePropertyChanges<TObserveType> 
        where TSelf : ObservePropertyChangesBase<TSelf, TObserveType>, TObserveType
    {
        private Subject<ObservedChange<TObserveType, object>> _subject = new Subject<ObservedChange<TObserveType, object>>();

        public IObservable<ObservedChange<TObserveType, object>> PropertyChanged
        {
            get { return _subject; }
        }

        protected void SetProperty<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
        {
            Debug.Assert(propertyName != null, "If CallerMemberName doesn't fill in the propertyName, you must do so manually");
            if (!Equals(backingField, newValue))
            {
                backingField = newValue;
                SignalChange(propertyName, newValue);
            }
        }

        protected virtual void SignalChange(string propertyName, object newValue)
        {
            _subject.OnNext(new ObservedChange<TObserveType, object>()
            {
                PropertyName = propertyName,
                Value = newValue,
                Sender = (TSelf)this
            });
        }

        IObservable<ObservedChange<object, object>> IObservePropertyChanges.PropertyChanged
        {
            get
            {
                return PropertyChanged.Select(c => new ObservedChange<object, object>()
                {
                    PropertyName = c.PropertyName,
                    Value = c.Value,
                    Sender = c.Sender
                });
            }
        }
    }
}
