
namespace MetadataExtractor.Formats.Jp2000.Boxes;

public class Ftyp: BoxBase, IBox
{
    private readonly byte[] ID = Box.BoxId_Ftyp;

    ReadOnlySpan<byte> IBox.ID => ID;
    ReadOnlySpan<byte> IBox.BoxData => BoxData;

    public bool IsBigEndian { get; set; } = false;

    public string Name => "FileType";

    public bool Parse(List<Directory> directories)
    {
        Jp2000FileTypeReader fileTypeReader = new Jp2000FileTypeReader();
        Jp2000FileTypeDirectory directory = new Jp2000FileTypeDirectory();

        long offset = 0;
        fileTypeReader.Parse(this, ref offset, directory);

        directories.Add(directory);
        return true;
    }

    public static IBox StaticFactory()
    {
        return new Ftyp();
    }
}
