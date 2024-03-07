
namespace MetadataExtractor.Formats.Jp2000.Boxes;

public class Jp2Header: BoxBase, IBox
{
    public static Dictionary<byte[], BoxFactoryDelegate> Boxes =
        new()
        {
            { Box.BoxId_Jp2Header_ImageHeader, Jp2Header_ImageHeader.StaticFactory },
            { Box.BoxId_Jp2Header_Resolution, Jp2Header_Resolution.StaticFactory },
            { Array.Empty<byte>(), Unknown.StaticFactory }
        };

    private readonly byte[] ID = Box.BoxId_Jp2Header;
    public bool IsBigEndian { get; set; } = false;

    ReadOnlySpan<byte> IBox.ID => ID;
    ReadOnlySpan<byte> IBox.BoxData => BoxData;

    public string Name => "Jp2Header";

    public bool Parse(List<Directory> directories)
    {
        Box.ReadBoxesInRange(Boxes, BoxData, 0, BoxLength, directories, IsBigEndian);

        return true;
    }

    public static IBox StaticFactory()
    {
        return new Jp2Header();
    }
}
