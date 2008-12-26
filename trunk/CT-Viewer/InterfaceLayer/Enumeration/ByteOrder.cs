
namespace Viewer.Core.Enumeration
{
    /// <summary>
    /// Enumeration, die die ByteOrder definiert unter welcher
    /// die Bilder abgespeichert wurden.
    /// </summary>
    public enum ByteOrder
    {
        /// <summary>
        /// LSB (Least Significant Bit - Little endian: x86-Architectures 
        /// ausser Java, Alpha)
        /// </summary>
        LSB,

        /// <summary>
        /// MSB (Most Significant Bit - Big endian: Java, Sun AIX)
        /// </summary>
        MSB
    }
}