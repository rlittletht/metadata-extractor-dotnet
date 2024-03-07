
namespace MetadataExtractor.Formats.Jp2000.Records.RecordValues;

class JpUtf8String : IRecordValue
{
    public string? Value { get; private set; }

    public void SetFromData(ReadOnlySpan<byte> data, bool _)
    {
        Value = Encoding.UTF8.GetString(data);
    }

    public bool IsEqual(ReadOnlySpan<byte> other)
    {
        if (Value is null)
            return false;

        return Value == Encoding.UTF8.GetString(other);
    }

    public JpUtf8String(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        SetFromData(data, isBigEndian);
    }

    public IRecordValue Factory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return StaticFactory(data, isBigEndian);
    }

    public static IRecordValue StaticFactory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return new JpUtf8String(data, isBigEndian);
    }

    public override string ToString()
    {
        return Value ?? "<null>";
    }
}
