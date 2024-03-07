namespace MetadataExtractor.Formats.Jp2000.Records;

public class RecordLength
{
    public enum LenType
    {
        Constant,
        Until0,
        Until00,
        Until0000,
        UntilEnd
    }

    public LenType Type;
    public int Count;

    public RecordLength(LenType type)
    {
        Type = type;
    }

    public RecordLength(int count)
    {
        Type = LenType.Constant;
        Count = count;
    }
}
