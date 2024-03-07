// Copyright (c) Drew Noakes and contributors. All Rights Reserved. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

#if !NETSTANDARD1_3 && !NET8_0_OR_GREATER
using System.Runtime.Serialization;
#endif

namespace MetadataExtractor.Formats.Jp2000
{
    /// <summary>An exception class thrown upon unexpected and fatal conditions while processing a TIFF file.</summary>
    /// <author>Drew Noakes https://drewnoakes.com</author>
    /// <author>Darren Salomons</author>
#if !NETSTANDARD1_3 && !NET8_0_OR_GREATER
    [Serializable]
#endif
    public class Jp2000ProcessingException : ImageProcessingException
    {
        public Jp2000ProcessingException(string? message)
            : base(message)
        {
        }

        public Jp2000ProcessingException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public Jp2000ProcessingException(Exception? innerException)
            : base(innerException)
        {
        }

#if !NETSTANDARD1_3 && !NET8_0_OR_GREATER
        protected Jp2000ProcessingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
