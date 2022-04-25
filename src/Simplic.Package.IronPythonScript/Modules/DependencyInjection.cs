using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.IronPythonScript
{
    /// <summary>
    /// Python api module path root (~import simplic)
    /// </summary>
    public static partial class PythonApiModule
    {
        /// <summary>
        /// Dependency injection module for python
        /// </summary>
        [PythonType("DependencyInjection")]
        public static class DependencyInjection
        {
            /// <summary>
            /// API-Doc string. Can be used like this: simplic.'ModuleName'.__doc__
            /// </summary>
            public const string __doc__ = @"Module which provides functions to use simplic dependency injection.";

            /// <summary>
            /// Get instance of a unity service
            /// </summary>
            /// <param name="type">Interface type</param>
            /// <returns>Instance of an unity service</returns>
            public static object resolve(IronPython.Runtime.Types.PythonType type)
            {
                return CommonServiceLocator.ServiceLocator.Current.GetInstance(type.__clrtype__());
            }

            /// <summary>
            /// Get instance of a unity service
            /// </summary>
            /// <param name="type">Interface type</param>
            /// <param name="key">Optional key</param>
            /// <returns>Instance of an unity service</returns>
            public static object resolve_with_key(IronPython.Runtime.Types.PythonType type, string key)
            {
                return CommonServiceLocator.ServiceLocator.Current.GetInstance(type.__clrtype__(), key);
            }

            /// <summary>
            /// Register instance
            /// </summary>
            /// <param name="type">Interface type</param>
            /// <param name="key">Key</param>
            /// <param name="instance">Python class instance</param>
            public static void register_instance(IronPython.Runtime.Types.PythonType type, string key, object instance)
            {
                var unity = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUnityContainer>();
                unity.RegisterInstance(type.__clrtype__(), key, instance);
            }
        }
    }
}
