﻿using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.EplReport
{
    public class InstallEplReportService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallEplReportService([Dependency("eplReport")] IObjectRepository repository)
        {
            this.repository = repository;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            return await repository.InstallObject(installableObject);
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}