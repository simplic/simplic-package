using Simplic.TenantSystem;
using System;
using System.Collections.Generic;

namespace Simplic.Package.CLI
{
    public class OrganizationService : IOrganizationService
    {
        public OrganizationMode Mode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Guid CreateOrGetGroup(IList<Organization> Organizations)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Organization obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Organization Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Organization> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Organization> GetAvailableOrganizations(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Organization> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Organization> GetGroupsBySubOrganizationCount(int count)
        {
            throw new NotImplementedException();
        }

        public bool Save(Organization obj)
        {
            throw new NotImplementedException();
        }
    }
}