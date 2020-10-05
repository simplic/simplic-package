﻿using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.ComboBox
{
    public class InstallComboBoxService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallComboBoxService([Dependency("comboBox")] IObjectRepository repository)
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

        public Task UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}