using System.ComponentModel;

namespace Data.Logger;

internal class LoggerPropertyChangedEventArgs : PropertyChangedEventArgs
{
    public object OldValue { get; }
    public object NewValue { get; }

    public LoggerPropertyChangedEventArgs(string? propertyName, object oldValue, object newValue) : base(propertyName)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }
}