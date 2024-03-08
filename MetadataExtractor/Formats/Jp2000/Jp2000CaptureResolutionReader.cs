using MetadataExtractor.Formats.Jp2000.Boxes;
using MetadataExtractor.Formats.Jp2000.Records;
using MetadataExtractor.Formats.Jp2000.Records.RecordValues;

namespace MetadataExtractor.Formats.Jp2000;

public class Jp2000CaptureResolutionReader : Jp2000DirectoryReader
{
    private readonly IRecord[] Records =
    {
        new Record(
            Jp2000CaptureResolutionDirectory.TagResX,
            RecordType.Int32,
            new RecordLength(1),
            JpRational32U.StaticFactory),
        new Record(
            Jp2000CaptureResolutionDirectory.TagResY,
            RecordType.Int32,
            new RecordLength(1),
            JpRational32U.StaticFactory),
        new Record(
            Jp2000CaptureResolutionDirectory.TagXUnit,
            RecordType.Byte,
            new RecordLength(1),
            new Dictionary<IRecordValue, string>()
            {
                { new Records.RecordValues.JpByte(0xfd, false), "km" },
                { new Records.RecordValues.JpByte(0xfe, false), "100m" },
                { new Records.RecordValues.JpByte(0xff, false), "10m" },
                { new Records.RecordValues.JpByte(0x00, false), "m" },
                { new Records.RecordValues.JpByte(0x01, false), "10cm" },
                { new Records.RecordValues.JpByte(0x02, false), "cm" },
                { new Records.RecordValues.JpByte(0x03, false), "mm" },
                { new Records.RecordValues.JpByte(0x04, false), "0.1mm" },
                { new Records.RecordValues.JpByte(0x05, false), "0.01mm" },
                { new Records.RecordValues.JpByte(0x06, false), "um" }
            }),
        new Record(
            Jp2000CaptureResolutionDirectory.TagYUnit,
            RecordType.Byte,
            new RecordLength(1),
            new Dictionary<IRecordValue, string>()
            {
                { new Records.RecordValues.JpByte(0xfd, false), "km" },
                { new Records.RecordValues.JpByte(0xfe, false), "100m" },
                { new Records.RecordValues.JpByte(0xff, false), "10m" },
                { new Records.RecordValues.JpByte(0x00, false), "m" },
                { new Records.RecordValues.JpByte(0x01, false), "10cm" },
                { new Records.RecordValues.JpByte(0x02, false), "cm" },
                { new Records.RecordValues.JpByte(0x03, false), "mm" },
                { new Records.RecordValues.JpByte(0x04, false), "0.1mm" },
                { new Records.RecordValues.JpByte(0x05, false), "0.01mm" },
                { new Records.RecordValues.JpByte(0x06, false), "um" }
            }),
    };

    public bool Parse(IBox parent, ref long offset, Directory directory) => Parse(Records, parent, ref offset, directory);
}
