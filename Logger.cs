using System;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Rampastring.Tools
{
    /// <summary>
    /// A fairly self-explanatory class for logging.
    /// </summary>
    public static class Logger
    {
        public static bool WriteToConsole { get; set; }

        public static bool WriteLogFile { get; set; }

        private static string LogPath;

        private static string LogFileName;

        private static string _logFullPath;

        private static TextWriter _writer;


        private static readonly object locker = new object();

        public static void Initialize(string logFilePath, string logFileName)
        {
            LogPath = logFilePath;
            LogFileName = logFileName;
            _logFullPath = Path.Combine(LogPath, LogFileName);
            var parent = Directory.GetParent(_logFullPath);
            if (!parent.Exists)
                parent.Create();
            _writer = new StreamWriter(File.Open(_logFullPath, FileMode.Create, FileAccess.Write, FileShare.Read)) { AutoFlush = true };
        }

        public static void Log(string data)
        {
            lock (locker)
            {
                if (WriteToConsole)
                    Console.WriteLine(data);

                if (WriteLogFile)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder(GetPrefix());
                        sb.Append(data);

                        _writer.WriteLine(sb.ToString());

                        System.Diagnostics.Debug.WriteLine("[Logger]: " + sb.ToString());
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void Log(string data, string fileName)
        {
            lock (locker)
            {
                if (WriteToConsole)
                    Console.WriteLine(data);

                if (WriteLogFile)
                {
                    try
                    {


                        StringBuilder sb = new StringBuilder(GetPrefix());
                        sb.Append(data);

                        _writer.WriteLine(sb.ToString());

                        System.Diagnostics.Debug.WriteLine("[Logger]: " + sb.ToString());


                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void Log(string data, object f1)
        {
            lock (locker)
            {
                if (WriteToConsole)
                    Console.WriteLine(data);

                if (WriteLogFile)
                {
                    try
                    {


                        StringBuilder sb = new StringBuilder(GetPrefix());
                        sb.Append(string.Format(data, f1));

                        _writer.WriteLine(sb.ToString());

                        System.Diagnostics.Debug.WriteLine("[Logger]: " + sb.ToString());


                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void Log(string data, object f1, object f2)
        {
            lock (locker)
            {
                if (WriteToConsole)
                    Console.WriteLine(data);

                if (WriteLogFile)
                {
                    try
                    {


                        StringBuilder sb = new StringBuilder(GetPrefix());
                        sb.Append(string.Format(data, f1, f2));

                        _writer.WriteLine(sb.ToString());

                        System.Diagnostics.Debug.WriteLine("[Logger]: " + sb.ToString());


                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void ForceLog(string data)
        {
            lock (locker)
            {
                Console.WriteLine(data);

                try
                {


                    StringBuilder sb = new StringBuilder(GetPrefix());
                    sb.Append(data);

                    _writer.WriteLine(sb.ToString());

                    System.Diagnostics.Debug.WriteLine("[Logger]: " + sb.ToString());


                }
                catch
                {
                }
            }
        }

        public static void ForceLog(string data, string fileName)
        {
            lock (locker)
            {
                Console.WriteLine(data);

                try
                {

                    StringBuilder sb = new StringBuilder(GetPrefix());
                    sb.Append(data);

                    _writer.WriteLine(sb.ToString());

                    System.Diagnostics.Debug.WriteLine("[Logger]: " + sb.ToString());


                }
                catch
                {
                }
            }
        }
        private static string GetPrefix()
        {
            DateTime now = DateTime.Now;
#if DEBUG
            var trace = new StackTrace();
            int index = 0;
            string type, name;
            do
            {
                index++;
                var frame = trace.GetFrame(index);
                var method = frame.GetMethod();
                type = method.DeclaringType.FullName;
                name = method.Name;
            }
            while (type == typeof(Logger).FullName);

            return $"{now:dd.MM. HH:mm:ss.fff}    [{type}]::{name}(): ";
#else
            return $"{now:dd.MM. HH:mm:ss.fff}    ";
#endif
        }

        [Conditional("DEBUG")]
        public static void Debug(string msg)
        {
            try
            {
                StringBuilder sb = new StringBuilder(GetPrefix());
                sb.Append(msg);

                System.Diagnostics.Debug.WriteLine("[Logger - Debug]: " + sb.ToString());
                lock (_writer)
                    _writer.WriteLine("[Debug] " + sb.ToString());
            }
            catch
            {
            }
        }
        [Conditional("DEBUG")]
        public static void Debug(string msg, params object[] args)
            => Debug(string.Format(msg, args));

        [Conditional("TRACE")]
        public static void Trace(string msg)
        {
            try
            {

                StringBuilder sb = new StringBuilder(GetPrefix());
                sb.Append(msg);

                System.Diagnostics.Trace.WriteLine("[Logger - Trace]: " + sb.ToString());
                lock (_writer)
                    _writer.WriteLine("[Trace] " + sb.ToString());
            }
            catch
            {
            }
        }

        [Conditional("TRACE")]
        public static void Trace(string msg, params object[] args)
            => Trace(string.Format(msg, args));

        public static void Info(string msg)
        {
            try
            {

                StringBuilder sb = new StringBuilder(GetPrefix());
                sb.Append(msg);

                System.Diagnostics.Trace.WriteLine("[Logger - Info]: " + sb.ToString());
                lock (_writer)
                    _writer.WriteLine("[Info] " + sb.ToString());
            }
            catch
            {
            }
        }
        public static void Info(string msg, params object[] args)
            => Info(string.Format(msg, args));
        public static void Warn(string msg)
        {
            try
            {

                StringBuilder sb = new StringBuilder(GetPrefix());
                sb.Append(msg);

                System.Diagnostics.Trace.WriteLine("[Logger - Warn]: " + sb.ToString());
                lock (_writer)
                    _writer.WriteLine("[Warn] " + sb.ToString());
            }
            catch
            {
            }
        }
        public static void Warn(string msg, params object[] args)
            => Warn(string.Format(msg, args));
        public static void Error(string msg)
        {
            try
            {

                StringBuilder sb = new StringBuilder(GetPrefix());
                sb.Append(msg);

                System.Diagnostics.Trace.WriteLine("[Logger - Error]: " + sb.ToString());
                lock (_writer)
                    _writer.WriteLine("[Error] " + sb.ToString());
            }
            catch
            {
            }
        }
        public static void Error(string msg, params object[] args)
            => Error(string.Format(msg, args));
        public static void Fatal(string msg)
        {
            try
            {

                StringBuilder sb = new StringBuilder(GetPrefix());
                sb.Append(msg);

                System.Diagnostics.Trace.WriteLine("[Logger - Fatal]: " + sb.ToString());
                lock (_writer)
                    _writer.WriteLine("[Fatal] " + sb.ToString());
            }
            catch
            {
            }
        }
        public static void Fatal(string msg, params object[] args)
            => Fatal(string.Format(msg, args));
    }
}
