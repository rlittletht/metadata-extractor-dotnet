using MetadataExtractor.Formats.Jp2000;
using MetadataExtractor.Formats.Jp2000.Records;

namespace MetadataExtractor.Formats.Jp2000.Boxes;

public class Jp2Header_ImageHeader: BoxBase, IBox
{
    public static Dictionary<byte[], BoxFactoryDelegate> Boxes =
        new()
        {
            { Box.BoxId_Jp2Header_ImageHeader, Jp2Header_ImageHeader.StaticFactory },
            { Array.Empty<byte>(), Unknown.StaticFactory }
        };

    private readonly byte[] ID = Box.BoxId_Jp2Header_ImageHeader;
    public bool IsBigEndian { get; set; } = false;

    ReadOnlySpan<byte> IBox.ID => ID;
    ReadOnlySpan<byte> IBox.BoxData => BoxData;

    public string Name => "ImageHeader";

    public bool Parse(List<Directory> directories)
    {
        Jp2000ImageHeaderReader reader = new();
        Jp2000ImageHeaderDirectory directory = new();

        long offset = 0;
        reader.Parse(this, ref offset, directory);

        directories.Add(directory);
        return true;
    }

    public static IBox StaticFactory()
    {
        return new Jp2Header_ImageHeader();
    }
}
