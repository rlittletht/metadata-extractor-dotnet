using System.Buffers.Binary;
using MetadataExtractor.Formats.Jp2000;

namespace MetadataExtractor.Formats.Jp2000.Records.RecordValues;

public class MinorVersion : IRecordValue
{
    public struct Version
    {
        public short MM;
        public byte M1;
        public byte M2;
    }

    public byte[]? Value { get; private set; }

    private Version m_version;

    public Version VersionFromData(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return new Version
               {
                   MM = isBigEndian
                       ? BinaryPrimitives.ReadInt16BigEndian(data)
                       : BinaryPrimitives.ReadInt16LittleEndian(data),
                   M1 = data[2],
                   M2 = data[3]
               };
    }

    public void SetFromData(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        Value = new byte[data.Length];
        data.CopyTo(Value);

        m_version = VersionFromData(data, isBigEndian);
    }

    public bool IsEqual(ReadOnlySpan<byte> data)
    {
        if (Value is null)
            return false;

        return data.SequenceEqual(Value);
    }

    public MinorVersion(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        SetFromData(data, isBigEndian);
    }

    public IRecordValue Factory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return new MinorVersion(data, isBigEndian);
    }

    public static IRecordValue StaticFactory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return new MinorVersion(data, isBigEndian);
    }

    public override string ToString()
    {
        return $"{m_version.MM}.{(int)m_version.M1}.{(int)m_version.M2}";
    }
}
