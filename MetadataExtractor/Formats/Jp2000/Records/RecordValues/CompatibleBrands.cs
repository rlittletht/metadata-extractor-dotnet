
namespace MetadataExtractor.Formats.Jp2000.Records.RecordValues;

public class CompatibleBrands: IRecordValue
{
    private readonly List<string> m_brands = new();

    public byte[]? Value { get; private set; }

    public void FillBrandsFromData(List<string> brands, ReadOnlySpan<byte> data)
    {
        // each 4 bytes is a brand
        int position = 0;

        while (position + 4 <= data.Length)
        {
            brands.Add(Encoding.UTF8.GetString(data.Slice(position, 4)));
            position += 4;
        }
    }

    public void SetFromData(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        Value = new byte[data.Length];
        data.CopyTo(Value);

        FillBrandsFromData(m_brands, data);
    }

    public bool IsEqual(ReadOnlySpan<byte> data)
    {
        if (Value is null)
            return false;

        return data.SequenceEqual(Value);
    }

    public CompatibleBrands(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        SetFromData(data, isBigEndian);
    }

    public IRecordValue Factory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return new CompatibleBrands(data, isBigEndian);
    }

    public static IRecordValue StaticFactory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return new CompatibleBrands(data, isBigEndian);
    }

    public override string ToString()
    {
        return string.Join(",", m_brands);
    }
}
