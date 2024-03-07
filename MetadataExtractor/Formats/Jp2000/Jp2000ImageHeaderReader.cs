using MetadataExtractor.Formats.Jp2000.Boxes;
using MetadataExtractor.Formats.Jp2000.Records;
using MetadataExtractor.Formats.Jp2000.Records.RecordValues;

namespace MetadataExtractor.Formats.Jp2000;

public class Jp2000ImageHeaderReader : Jp2000DirectoryReader
{
    private readonly IRecord[] Records =
    {
        new Record(
            Jp2000ImageHeaderDirectory.TagImageHeight,
            RecordType.Int32,
            new RecordLength(1),
            JpInt32U.StaticFactory),
        new Record(
            Jp2000ImageHeaderDirectory.TagImageWidth,
            RecordType.Int32,
            new RecordLength(1),
            JpInt32U.StaticFactory),
        new Record(
            Jp2000ImageHeaderDirectory.TagNumberOfComponents,
            RecordType.Int16,
            new RecordLength(1),
            JpInt16U.StaticFactory),
        new Record(
            Jp2000ImageHeaderDirectory.TagBitsPerComponent,
            RecordType.Byte,
            new RecordLength(1),
            JpByte.StaticFactory),
        new Record(
            Jp2000ImageHeaderDirectory.TagCompression,
            RecordType.Byte,
            new RecordLength(1),
            new Dictionary<IRecordValue, string>()
            {
                { new Records.RecordValues.JpByte(0x00, false), "Uncompressed" },
                { new Records.RecordValues.JpByte(0x01, false), "Modified Huffman" },
                { new Records.RecordValues.JpByte(0x02, false), "Modified READ" },
                { new Records.RecordValues.JpByte(0x03, false), "Modified Modified READ" },
                { new Records.RecordValues.JpByte(0x04, false), "JBIG" },
                { new Records.RecordValues.JpByte(0x05, false), "JPEG" },
                { new Records.RecordValues.JpByte(0x06, false), "JPEG-LS" },
                { new Records.RecordValues.JpByte(0x07, false), "JPEG 2000" },
                { new Records.RecordValues.JpByte(0x08, false), "JBIG2" }
            })
    };

    public bool Parse(IBox parent, ref long offset, Directory directory) => Parse(Records, parent, ref offset, directory);
}
