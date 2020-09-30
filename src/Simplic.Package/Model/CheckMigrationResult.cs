namespace Simplic.Package
{
    public class CheckMigrationResult
    {
        public bool CanMigrate { get; set; }
        public string LogMessage { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}