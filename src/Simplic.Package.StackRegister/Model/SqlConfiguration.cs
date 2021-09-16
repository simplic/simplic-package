namespace Simplic.Package.StackRegister
{
    /// <summary>
    /// Represents the content of a sql stack register configuration.
    /// </summary>
    public class SqlConfiguration : IStackRegisterConfiguration
    {
        public string Statement { get; set; }
    }
}