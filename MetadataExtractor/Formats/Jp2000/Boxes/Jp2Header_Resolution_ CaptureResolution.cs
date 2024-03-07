namespace MetadataExtractor.Formats.Jp2000.Boxes;

public class Jp2Header_Resolution_CaptureResolution: BoxBase, IBox
{
    private readonly byte[] ID = Box.BoxId_Jp2Header_Resolution_CaptureResolution;

    ReadOnlySpan<byte> IBox.ID => ID;
    ReadOnlySpan<byte> IBox.BoxData => BoxData;
    public bool IsBigEndian { get; set; } = false;

    public string Name => "Capture";

    public bool Parse(List<Directory> directories)
    {
        Jp2000CaptureResolutionReader reader = new();
        Jp2000CaptureResolutionDirectory directory = new();

        long offset = 0;
        reader.Parse(this, ref offset, directory);

        directories.Add(directory);

        return true;
    }

    public static IBox StaticFactory()
    {
        return new Jp2Header_Resolution_CaptureResolution();
    }
}
