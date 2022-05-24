using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Data;

internal class Logger
{
    private const string StartPart = "{\n" +
                                     "\t\"logs\": [";

    private const string EndPart = "\t]\n" +
                                   "}";

    private const string ChangeLogPattern = "\t\t{{\n" +
                                            "\t\t\t\"time_stamp\": \"{0}\",\n" +
                                            "\t\t\t\"object_id\": {1},\n" +
                                            "\t\t\t\"changed_property\": \"{2}\",\n" +
                                            "\t\t\t\"new_value\": {3}\n" +
                                            "\t\t}},";

    private const string LogLinePattern = "\t\t\t\"{0}\": \"{1}\",\n";

    private const string CreateLogPattern = "\t\t{{\n" +
                                            "\t\t\t\"time_stamp\": \"{0}\",\n" +
                                            "\t\t\t\"object_id\": {1},\n" +
                                            "{2}" +
                                            "\t\t}},";

    private readonly string _fileName;
    private object _fileLock = new();

    public Logger()
    {
        _fileName = "../../../../logs_" + DateTime.Now.ToFileTime() + ".json";

        Write(StartPart);
    }

    ~Logger()
    {
        EndLogging();
    }
    
    public void EndLogging()
    {
        if (Monitor.TryEnter(_fileLock, new TimeSpan(0, 0, 1, 0)))
        {
            try
            {
                Write(EndPart);
            }
            finally
            {
                Monitor.Exit(_fileLock);
            }
        }
    }

    public void LogChange(object? s, PropertyChangedEventArgs e)
    {
        Log(
            string.Format(
                ChangeLogPattern,
                DateTime.Now.ToString(CultureInfo.CurrentCulture) + ":" + DateTime.Now.Millisecond,
                s!.GetHashCode(), e.PropertyName, typeof(IBallData).GetProperty(e.PropertyName!)!.GetValue(s)
            )
        );
    }

    public void LogCreate(object o)
    {
        StringBuilder sb = new();
        foreach (PropertyInfo propertyInfo in o.GetType().GetProperties())
        {
            sb.AppendFormat(LogLinePattern, propertyInfo.Name, o.GetType().GetProperty(propertyInfo.Name)!.GetValue(o));
        }
        Log(
            string.Format(
                CreateLogPattern, 
                DateTime.Now.ToString(CultureInfo.CurrentCulture) + ":" + DateTime.Now.Millisecond, 
                o!.GetHashCode(), sb.Remove(sb.Length - 2, 1)
            )
        );
    }

    [SuppressMessage("ReSharper", "EmptyEmbeddedStatement")]
    private void Log(string text)
    {
        lock (_fileLock)
        {
            Write(text);
        }
    }

    private void Write(string text)
    {
        using StreamWriter writer = File.AppendText(_fileName);
        writer.WriteLineAsync(text);
        writer.Close();
    }
}