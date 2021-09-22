namespace Simplic.Package.Sql
{
    /// <summary>
    /// Service to pack sql exports.
    /// </summary>
    public class PackSqlService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PackSqlService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackSqlService(IFileService fileService) : base(fileService)
        {
        }
    }
}