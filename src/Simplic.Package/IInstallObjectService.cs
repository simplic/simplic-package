using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IInstallObjectService
    {

        /// <summary>
        /// Installs an existing Object
        /// </summary>
        /// <param name="installableObject">The Object to install</param>
        /// <returns></returns>
        ///
        /// Exceptions: 
        /// Object exists, Invalid Object Exception (Object has no target / content), Database Connection failed, various DatabaseExceptions
        Task InstallObject(InstallableObject installableObject);

        /// <summary>
        /// Uninstalls an existing Object
        /// </summary>
        /// <param name="installableObject">The Object to uninstall</param>
        /// <returns></returns>
        Task UninstallObject(InstallableObject installableObject);

        /// <summary>
        /// Force installs an Object by first uninstalling an existing one and then installing the new one
        /// </summary>
        /// <param name="installableObject">The Object to overwrite</param>
        /// <returns></returns>
        Task OverwriteObject(InstallableObject installableObject);
    }
}
