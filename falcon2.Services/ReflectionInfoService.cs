using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using falcon2.Core.Services;

namespace falcon2.Services
{
    public class ReflectionInfoService : IReflectionInfoService
    {
        private void PrintInfo(MemberInfo[] mi)
        {
            foreach (MemberInfo m in mi)
            {
                Console.WriteLine("{0}{1}", "   ", m);
            }
            Console.WriteLine();
        }

        public void GetStaticInfo(Type t)
        {
            FieldInfo[] fi = t.GetFields(BindingFlags.Static |
                BindingFlags.Public
                | BindingFlags.NonPublic);
            PropertyInfo[] pi = t.GetProperties(BindingFlags.Static |
                BindingFlags.Public
                | BindingFlags.NonPublic);
            EventInfo[] ei = t.GetEvents(BindingFlags.Static |
                BindingFlags.Public
                | BindingFlags.NonPublic);
            MethodInfo[] mi = t.GetMethods(BindingFlags.Static |
                BindingFlags.Public
                | BindingFlags.NonPublic);
            Console.WriteLine($"\t\tSTATIC FIELD INFO FOR {t}");
            PrintInfo(fi);
            Console.WriteLine($"\t\tSTATIC PROPERTY INFO FOR {t}");
            PrintInfo(pi);
            Console.WriteLine($"\t\tSTATIC EVENT INFO FOR {t}");
            PrintInfo(ei);
            Console.WriteLine($"\t\tSTATIC METHOD INFO FOR {t}");
            PrintInfo(mi);
        }

        public void GetInstanceInfo(Type t)
        {
            FieldInfo[] fi = t.GetFields(BindingFlags.Instance |
                BindingFlags.Public
                | BindingFlags.NonPublic);
            PropertyInfo[] pi = t.GetProperties(BindingFlags.Instance |
                BindingFlags.Public
                | BindingFlags.NonPublic);
            EventInfo[] ei = t.GetEvents(BindingFlags.Instance |
                BindingFlags.Public
                | BindingFlags.NonPublic);
            MethodInfo[] mi = t.GetMethods(BindingFlags.Instance |
                BindingFlags.Public
                | BindingFlags.NonPublic);
            Console.WriteLine($"\t\tINSTANCE FIELD INFO FOR {t}");
            PrintInfo(fi);
            Console.WriteLine($"\t\tINSTANCE PROPERTY INFO FOR {t}");
            PrintInfo(pi);
            Console.WriteLine($"\t\tINSTANCE EVENT INFO FOR {t}");
            PrintInfo(ei);
            Console.WriteLine($"\t\tINSTANCE METHOD INFO FOR {t}");
            PrintInfo(mi);
        }
    }
}
