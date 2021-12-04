using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WPF_Tranning.ModelAndView
{
    public static class DeBugTrace
    {
        private static readonly string Namespace;
        public static bool IsDebugEnabled;
        public static bool IsTraceEnabled;
        static DeBugTrace()
        {
            Namespace = CreateNamespace();
#if DEBUG 
            IsDebugEnabled = true;
            IsTraceEnabled = true;
        
#endif // 디버그 아닐때 (일반)
            IsTraceEnabled = true;
        }
        public static void DebugWriteLine(string message)
        {
            if (IsDebugEnabled)
            {
                message = String.Format("[{0}] DEBUG - {1}", Namespace, message);
                System.Diagnostics.Debug.WriteLine(message);
            }
        }
        public static void TraceWriteLine(string message)
        {
            if (IsTraceEnabled)
                message = String.Format("[{0}] TRACE - {1}", Namespace, message);
            System.Diagnostics.Trace.WriteLine(message);
        }

        private static string CreateNamespace()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(DeBugTrace));
            return assembly.GetName().Name;
        }

    }
}



