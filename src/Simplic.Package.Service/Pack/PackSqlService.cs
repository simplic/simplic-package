﻿using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class PackSqlService : IPackObjectService
    {
        private readonly IFileService fileService;

        public PackSqlService(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public async Task<PackObjectResult> ReadAsync(ObjectListItem item)
        {
            return new PackObjectResult
            {
                File = await fileService.ReadAllBytesAsync(item.Source),
                Location = $"{item.Target}"
            };
        }
    }
}