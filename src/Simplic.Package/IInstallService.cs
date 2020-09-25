﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IInstallService
    {
        /// <summary>
        /// Installs a package by writing it the database
        /// </summary>
        /// <param name="unpackedPackage">The package to install</param>
        /// <returns></returns>
        Task Install(UnpackedPackage unpackedPackage);
        
        /// <summary>
        /// Uninstalls a existing package by removing it from the database
        /// </summary>
        /// <param name="unpackedPackage">The package to uninstall</param>
        /// <returns></returns>
        Task Uninstall(UnpackedPackage unpackedPackage);

        /// <summary>
        /// Force installs a package by removing a potentially existing one and then installing it
        /// </summary>
        /// <param name="unpackedPackage">The package to overwrite</param>
        /// <returns></returns>
        Task Overwrite(UnpackedPackage unpackedPackage);
    }
}
