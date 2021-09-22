namespace Simplic.Package.Stack
{
    /// <summary>
    /// Represents the content of a fulltext.
    /// </summary>
    public class FullText
    {
        /// <summary>
        /// Getor sets the use fulltext index flag.
        /// </summary>
        public bool UseFullTextIndex { get; set; }

        /// <summary>
        /// Gets or sets the improve ocr text flag.
        /// </summary>
        public bool ImproveOCRText { get; set; }

        /// <summary>
        /// Gets or sets the use dce flag.
        /// </summary>
        public bool UseDCE { get; set; }
    }
}