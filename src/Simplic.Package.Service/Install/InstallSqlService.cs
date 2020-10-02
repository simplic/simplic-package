﻿using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Service.Install
{
    public class InstallSqlService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallSqlService([Dependency("sql")] IObjectRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            return await repository.CheckMigration(installableObject);
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