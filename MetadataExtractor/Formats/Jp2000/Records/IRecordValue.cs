namespace MetadataExtractor.Formats.Jp2000.Records;

public interface IRecordValue
{
    bool IsEqual(ReadOnlySpan<byte> other);
    void SetFromData(ReadOnlySpan<byte> data, bool isBigEndian);
    IRecordValue Factory(ReadOnlySpan<byte> data, bool isBigEndian);
    string ToString();
}
