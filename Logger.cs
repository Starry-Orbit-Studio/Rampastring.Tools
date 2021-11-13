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


        private static readonly object locker = new object();

        public static void Initialize(string logFilePath, string logFileName)
        {
            LogPath = logFilePath;
            LogFileName = logFileName;
            _logFullPath = Path.Combine(LogPath, LogFileName);
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
                        StreamWriter sw = new StreamWriter(_logFullPath, true);

                        DateTime now = DateTime.Now;

                        StringBuilder sb = new StringBuilder();
                        sb.Append(now.ToString("dd.MM. HH:mm:ss.fff"));
                        sb.Append("    ");
                        sb.Append(data);

                        sw.WriteLine(sb.ToString());

                        System.Diagnostics.Debug.WriteLine("[Logger]: " + sb.ToString());

                        sw.Close();
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
                        StreamWriter sw = new StreamWriter(LogPath + fileName, true);

                        DateTime now = DateTime.Now;

                        StringBuilder sb = new StringBuilder();
                        sb.Append(now.ToString("dd.MM. HH:mm:ss.fff"));
                        sb.Append("    ");
                        sb.Append(data);

                        sw.WriteLine(sb.ToString());

                        System.Diagnostics.Debug.WriteLine("[Logger]: " + sb.ToString());

                        sw.Close();
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
                        StreamWriter sw = new StreamWriter(_logFullPath, true);

                        DateTime now = DateTime.Now;

                        StringBuilder sb = new StringBuilder();
                        sb.Append(now.ToString("dd.MM. HH:mm:ss.fff"));
                        sb.Append("    ");
                        sb.Append(string.Format(data, f1));

                        sw.WriteLine(sb.ToString());

                        System.Diagnostics.Debug.WriteLine("[Logger]: " + sb.ToString());

                        sw.Close();
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
                        StreamWriter sw = new StreamWriter(_logFullPath, true);

                        DateTime now = DateTime.Now;

                        StringBuilder sb = new StringBuilder();
                        sb.Append(now.ToString("dd.MM. HH:mm:ss.fff"));
                        sb.Append("    ");
                        sb.Append(string.Format(data, f1, f2));

                        sw.WriteLine(sb.ToString());

                        System.Diagnostics.Debug.WriteLine("[Logger]: " + sb.ToString());

                        sw.Close();
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
                    StreamWriter sw = new StreamWriter(_logFullPath, true);

                    DateTime now = DateTime.Now;

                    StringBuilder sb = new StringBuilder();
                    sb.Append(now.ToString("dd.MM. HH:mm:ss.fff"));
                    sb.Append("    ");
                    sb.Append(data);

                    sw.WriteLine(sb.ToString());

                    System.Diagnostics.Debug.WriteLine("[Logger]: " + sb.ToString());

                    sw.Close();
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
                    StreamWriter sw = new StreamWriter(LogPath + fileName, true);

                    DateTime now = DateTime.Now;

                    StringBuilder sb = new StringBuilder();
                    sb.Append(now.ToString("dd.MM. HH:mm:ss.fff"));
                    sb.Append("    ");
                    sb.Append(data);

                    sw.WriteLine(sb.ToString());

                    System.Diagnostics.Debug.WriteLine("[Logger]: " + sb.ToString());

                    sw.Close();
                }
                catch
                {
                }
            }
        }

        [Conditional("DEBUG")]
        public static void Debug(string msg)
        {
            try
            {
                DateTime now = DateTime.Now;
                var trace = new StackTrace();
                int index = 0;
                string type, name;
                do
                {
                    index++;
                    var frame = trace.GetFrame(1);
                    var method = frame.GetMethod();
                    type = method.DeclaringType.FullName;
                    name = method.Name;
                }
                while (type == typeof(Logger).FullName);

                StringBuilder sb = new StringBuilder();
                sb.Append(now.ToString("dd.MM. HH:mm:ss.fff"));
                sb.Append("    ");
                sb.Append($"[{type}]::{name}(): ");
                sb.Append(msg);

                System.Diagnostics.Debug.WriteLine("[Logger - Debug]: " + sb.ToString());
                using (StreamWriter sw = new StreamWriter(_logFullPath, true))
                    sw.WriteLine("[Logger - Debug] " + sb.ToString());
            }
            catch
            {
            }
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public static void Debug(string msg, params object[] args)
            => Debug(string.Format(msg, args));
    }
}
