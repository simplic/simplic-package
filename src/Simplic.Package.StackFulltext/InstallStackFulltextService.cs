﻿using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.StackFulltext
{
    public class InstallStackFulltextService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallStackFulltextService([Dependency("stackFulltext")] IObjectRepository repository)
        {
            this.repository = repository;
        }

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
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

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}