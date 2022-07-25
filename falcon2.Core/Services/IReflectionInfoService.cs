using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace falcon2.Core.Services
{
    public interface IReflectionInfoService
    {
        void GetStaticInfo(Type t);

        void GetInstanceInfo(Type t);
    }
}
