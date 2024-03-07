using MetadataExtractor.Formats.Jp2000.Boxes;
using MetadataExtractor.Formats.Jp2000.Records;
using MetadataExtractor.Formats.Jp2000.Records.RecordValues;

namespace MetadataExtractor.Formats.Jp2000;

public class Jp2000DirectoryReader
{
    public long Start { get; set; }
    public long LengthLong { get; set; }
//    public IDirectory? Parent { get; set; }
    public IBox Box => BoxInternal ?? throw new Exception("Box not set!");
    public Dictionary<string, string> Metadata { get; } = new();

    public IBox? BoxInternal { get; set; }

    public int Length
    {
        get
        {
            if (LengthLong > Int32.MaxValue)
                throw new Exception("can't handle directories > 2gb");
            return (int)LengthLong;
        }
    }

    public bool Parse(IRecord[] records, IBox box, ref long start, Directory directory)
    {
        long position = start;

        foreach (IRecord record in records)
        {
            IRecordValue? recordValue = record.Parse(box.BoxData, ref position, box.IsBigEndian);

            if (recordValue is JpError)
            {
                directory.AddError(recordValue.ToString());
            }
            else
            {
                directory.Set(record.Tag, recordValue?.ToString() ?? "");
            }
        }

        start = position;

        return true;
    }
}
