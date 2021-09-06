namespace Simplic.Package.ComboBox
{
    /// <summary>
    /// Service to pack combo boxes.
    /// </summary>
    public class PackComboBoxService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PackComboBoxService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackComboBoxService(IFileService fileService) : base(fileService)
        {
        }
    }
}