using MetadataExtractor.Formats.Jp2000.Boxes;
using MetadataExtractor.Formats.Jp2000.Records;
using MetadataExtractor.Formats.Jp2000.Records.RecordValues;

namespace MetadataExtractor.Formats.Jp2000;

public class Jp2000FileTypeReader : Jp2000DirectoryReader
{
    private readonly IRecord[] Records =
    {
        new Record(
            Jp2000FileTypeDirectory.TagMajorBrand,
            RecordType.Byte,
            new RecordLength(4),
            new Dictionary<IRecordValue, string>()
            {
                { new Records.RecordValues.Bytes("jp2 "u8.ToArray(), false), "image/jp2" },
                { new Records.RecordValues.Bytes("jpm "u8.ToArray(), false), "image/jpm" },
                { new Records.RecordValues.Bytes("jpx "u8.ToArray(), false), "image/jpx" },
                { new Records.RecordValues.Bytes("jpl "u8.ToArray(), false), "image/jxl" },
                { new Records.RecordValues.Bytes("jph "u8.ToArray(), false), "image/jph" }
            }),
        new Record(
            Jp2000FileTypeDirectory.TagMinorVersion,
            RecordType.Byte,
            new RecordLength(4),
            MinorVersion.StaticFactory),
        new Record(
            Jp2000FileTypeDirectory.TagCompatibleBrands,
            RecordType.Byte,
            new RecordLength(RecordLength.LenType.UntilEnd),
            CompatibleBrands.StaticFactory)
    };

    public bool Parse(IBox parent, ref long offset, Directory directory) => Parse(Records, parent, ref offset, directory);
}
