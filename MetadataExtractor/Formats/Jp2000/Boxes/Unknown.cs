
using MetadataExtractor.Formats.Jp2000;

namespace MetadataExtractor.Formats.Jp2000.Boxes;

/*----------------------------------------------------------------------------
    %%Class: Unknown
    %%Qualified: Jp2000.Boxes.Unknown

    This matches all boxes. Used to skip unknown boxes
----------------------------------------------------------------------------*/
public class Unknown: BoxBase, IBox
{
    private readonly byte[] ID = { };

    ReadOnlySpan<byte> IBox.ID => ID;
    ReadOnlySpan<byte> IBox.BoxData => BoxData;

    public bool IsBigEndian { get; set; } = false;

    public string Name => "Unknown";

    public new bool Read(IndexedReader reader)
    {
        return true;
    }

    public new bool Read(ReadOnlySpan<byte> data)
    {
        return true;
    }

    public bool MatchBoxId(BoxHeader header) => true;

    public bool Parse(List<Directory> directories)
    {
        return true;
    }

    public static IBox StaticFactory()
    {
        return new Unknown();
    }
}
