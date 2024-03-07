namespace MetadataExtractor.Formats.Jp2000.Records;

public interface IRecord
{
    public RecordType RecordType { get; }
    public RecordLength Length { get; }
    public ReadOnlySpan<byte> Data { get; }

    public int Tag { get; }
    public IRecordValue? Parse(ReadOnlySpan<byte> data, ref long position, bool isBigEndian);
    public IRecordValue RecordValueFactory(ReadOnlySpan<byte> data, bool isBigEndian);
}
