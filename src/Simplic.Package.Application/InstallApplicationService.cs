﻿using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Application
{
    public class InstallApplicationService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallApplicationService([Dependency("application")] IObjectRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            return await repository.InstallObject(installableObject);
        }

        public Task OverwriteObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}