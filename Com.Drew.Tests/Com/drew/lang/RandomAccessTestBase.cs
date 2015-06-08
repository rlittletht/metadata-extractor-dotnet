/*
 * Copyright 2002-2015 Drew Noakes
 *
 *    Modified by Yakov Danilov <yakodani@gmail.com> for Imazen LLC (Ported from Java to C#)
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *
 *        http://www.apache.org/licenses/LICENSE-2.0
 *
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *
 * More information about this project is available at:
 *
 *    https://drewnoakes.com/code/exif/
 *    https://github.com/drewnoakes/metadata-extractor
 */

using System.IO;
using NUnit.Framework;
using Sharpen;

namespace Com.Drew.Lang
{
    /// <summary>
    /// Base class for testing implementations of
    /// <see cref="RandomAccessReader"/>
    /// .
    /// </summary>
    /// <author>Drew Noakes https://drewnoakes.com</author>
    public abstract class RandomAccessTestBase
    {
        protected internal abstract RandomAccessReader CreateReader(sbyte[] bytes);

        [Test]
        public virtual void TestDefaultEndianness()
        {
            Tests.AreEqual(true, CreateReader(new sbyte[1]).IsMotorolaByteOrder());
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetInt8()
        {
            sbyte[] buffer = new sbyte[] { unchecked((int)(0x00)), unchecked((int)(0x01)), unchecked((sbyte)0x7F), unchecked((sbyte)0xFF) };
            RandomAccessReader reader = CreateReader(buffer);
            Tests.AreEqual(unchecked((sbyte)0), reader.GetInt8(0));
            Tests.AreEqual(unchecked((sbyte)1), reader.GetInt8(1));
            Tests.AreEqual(unchecked((sbyte)127), reader.GetInt8(2));
            Tests.AreEqual(unchecked((sbyte)255), reader.GetInt8(3));
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetUInt8()
        {
            sbyte[] buffer = new sbyte[] { unchecked((int)(0x00)), unchecked((int)(0x01)), unchecked((sbyte)0x7F), unchecked((sbyte)0xFF) };
            RandomAccessReader reader = CreateReader(buffer);
            Tests.AreEqual(0, reader.GetUInt8(0));
            Tests.AreEqual(1, reader.GetUInt8(1));
            Tests.AreEqual(127, reader.GetUInt8(2));
            Tests.AreEqual(255, reader.GetUInt8(3));
        }

        [Test]
        public virtual void TestGetUInt8_OutOfBounds()
        {
            try
            {
                RandomAccessReader reader = CreateReader(new sbyte[2]);
                reader.GetUInt8(2);
                Assert.Fail("Exception expected");
            }
            catch (IOException ex)
            {
                Tests.AreEqual("Attempt to read from beyond end of underlying data source (requested index: 2, requested count: 1, max index: 1)", ex.Message);
            }
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetInt16()
        {
            Tests.AreEqual(-1, CreateReader(new sbyte[] { unchecked((sbyte)0xff), unchecked((sbyte)0xff) }).GetInt16(0));
            sbyte[] buffer = new sbyte[] { unchecked((int)(0x00)), unchecked((int)(0x01)), unchecked((sbyte)0x7F), unchecked((sbyte)0xFF) };
            RandomAccessReader reader = CreateReader(buffer);
            Tests.AreEqual((short)0x0001, reader.GetInt16(0));
            Tests.AreEqual((short)0x017F, reader.GetInt16(1));
            Tests.AreEqual((short)0x7FFF, reader.GetInt16(2));
            reader.SetMotorolaByteOrder(false);
            Tests.AreEqual((short)0x0100, reader.GetInt16(0));
            Tests.AreEqual((short)0x7F01, reader.GetInt16(1));
            Tests.AreEqual(unchecked((short)(0xFF7F)), reader.GetInt16(2));
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetUInt16()
        {
            sbyte[] buffer = new sbyte[] { unchecked((int)(0x00)), unchecked((int)(0x01)), unchecked((sbyte)0x7F), unchecked((sbyte)0xFF) };
            RandomAccessReader reader = CreateReader(buffer);
            Tests.AreEqual(unchecked((int)(0x0001)), reader.GetUInt16(0));
            Tests.AreEqual(unchecked((int)(0x017F)), reader.GetUInt16(1));
            Tests.AreEqual(unchecked((int)(0x7FFF)), reader.GetUInt16(2));
            reader.SetMotorolaByteOrder(false);
            Tests.AreEqual(unchecked((int)(0x0100)), reader.GetUInt16(0));
            Tests.AreEqual(unchecked((int)(0x7F01)), reader.GetUInt16(1));
            Tests.AreEqual(unchecked((int)(0xFF7F)), reader.GetUInt16(2));
        }

        [Test]
        public virtual void TestGetUInt16_OutOfBounds()
        {
            try
            {
                RandomAccessReader reader = CreateReader(new sbyte[2]);
                reader.GetUInt16(1);
                Assert.Fail("Exception expected");
            }
            catch (IOException ex)
            {
                Tests.AreEqual("Attempt to read from beyond end of underlying data source (requested index: 1, requested count: 2, max index: 1)", ex.Message);
            }
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetInt32()
        {
            Tests.AreEqual(-1, CreateReader(new sbyte[] { unchecked((sbyte)0xff), unchecked((sbyte)0xff), unchecked((sbyte)0xff), unchecked((sbyte)0xff) }).GetInt32(0));
            sbyte[] buffer = new sbyte[] { unchecked((int)(0x00)), unchecked((int)(0x01)), unchecked((sbyte)0x7F), unchecked((sbyte)0xFF), unchecked((int)(0x02)), unchecked((int)(0x03)), unchecked((int)(0x04)) };
            RandomAccessReader reader = CreateReader(buffer);
            Tests.AreEqual(unchecked((int)(0x00017FFF)), reader.GetInt32(0));
            Tests.AreEqual(unchecked((int)(0x017FFF02)), reader.GetInt32(1));
            Tests.AreEqual(unchecked((int)(0x7FFF0203)), reader.GetInt32(2));
            Tests.AreEqual(unchecked((int)(0xFF020304)), reader.GetInt32(3));
            reader.SetMotorolaByteOrder(false);
            Tests.AreEqual(unchecked((int)(0xFF7F0100)), reader.GetInt32(0));
            Tests.AreEqual(unchecked((int)(0x02FF7F01)), reader.GetInt32(1));
            Tests.AreEqual(unchecked((int)(0x0302FF7F)), reader.GetInt32(2));
            Tests.AreEqual(unchecked((int)(0x040302FF)), reader.GetInt32(3));
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetUInt32()
        {
            Tests.AreEqual(4294967295L, CreateReader(new sbyte[] { unchecked((sbyte)0xff), unchecked((sbyte)0xff), unchecked((sbyte)0xff), unchecked((sbyte)0xff) }).GetUInt32(0));
            sbyte[] buffer = new sbyte[] { unchecked((int)(0x00)), unchecked((int)(0x01)), unchecked((sbyte)0x7F), unchecked((sbyte)0xFF), unchecked((int)(0x02)), unchecked((int)(0x03)), unchecked((int)(0x04)) };
            RandomAccessReader reader = CreateReader(buffer);
            Tests.AreEqual(unchecked((long)(0x00017FFFL)), reader.GetUInt32(0));
            Tests.AreEqual(unchecked((long)(0x017FFF02L)), reader.GetUInt32(1));
            Tests.AreEqual(unchecked((long)(0x7FFF0203L)), reader.GetUInt32(2));
            Tests.AreEqual(unchecked((long)(0xFF020304L)), reader.GetUInt32(3));
            reader.SetMotorolaByteOrder(false);
            Tests.AreEqual(4286513408L, reader.GetUInt32(0));
            Tests.AreEqual(unchecked((long)(0x02FF7F01L)), reader.GetUInt32(1));
            Tests.AreEqual(unchecked((long)(0x0302FF7FL)), reader.GetUInt32(2));
            Tests.AreEqual(unchecked((long)(0x040302FFL)), reader.GetInt32(3));
        }

        [Test]
        public virtual void TestGetInt32_OutOfBounds()
        {
            try
            {
                RandomAccessReader reader = CreateReader(new sbyte[3]);
                reader.GetInt32(0);
                Assert.Fail("Exception expected");
            }
            catch (IOException ex)
            {
                Tests.AreEqual("Attempt to read from beyond end of underlying data source (requested index: 0, requested count: 4, max index: 2)", ex.Message);
            }
        }

        /// <exception cref="System.IO.IOException"/>
        [Test]
        public virtual void TestGetInt64()
        {
            sbyte[] buffer = new sbyte[] { unchecked((int)(0x00)), unchecked((int)(0x01)), unchecked((int)(0x02)), unchecked((int)(0x03)), unchecked((int)(0x04)), unchecked((int)(0x05)), unchecked((int)(0x06)), unchecked((int)(0x07)), unchecked((sbyte)0xFF
                ) };
            RandomAccessReader reader = CreateReader(buffer);
            Tests.AreEqual(unchecked((long)(0x0001020304050607L)), reader.GetInt64(0));
            Tests.AreEqual(unchecked((long)(0x01020304050607FFL)), reader.GetInt64(1));
            reader.SetMotorolaByteOrder(false);
            Tests.AreEqual(unchecked((long)(0x0706050403020100L)), reader.GetInt64(0));
            Tests.AreEqual(unchecked((long)(0xFF07060504030201L)), reader.GetInt64(1));
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetInt64_OutOfBounds()
        {
            try
            {
                RandomAccessReader reader = CreateReader(new sbyte[7]);
                reader.GetInt64(0);
                Assert.Fail("Exception expected");
            }
            catch (IOException ex)
            {
                Tests.AreEqual("Attempt to read from beyond end of underlying data source (requested index: 0, requested count: 8, max index: 6)", ex.Message);
            }
            try
            {
                RandomAccessReader reader = CreateReader(new sbyte[7]);
                reader.GetInt64(-1);
                Assert.Fail("Exception expected");
            }
            catch (IOException ex)
            {
                Tests.AreEqual("Attempt to read from buffer using a negative index (-1)", ex.Message);
            }
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetFloat32()
        {
            int nanBits = unchecked((int)(0x7fc00000));
            Tests.IsTrue(float.IsNaN(Extensions.IntBitsToFloat(nanBits)));
            sbyte[] buffer = new sbyte[] { unchecked((int)(0x7f)), unchecked((sbyte)0xc0), unchecked((int)(0x00)), unchecked((int)(0x00)) };
            RandomAccessReader reader = CreateReader(buffer);
            Tests.IsTrue(float.IsNaN(reader.GetFloat32(0)));
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetFloat64()
        {
            long nanBits = unchecked((long)(0xfff0000000000001L));
            Tests.IsTrue(double.IsNaN(Extensions.LongBitsToDouble(nanBits)));
            sbyte[] buffer = new sbyte[] { unchecked((sbyte)0xff), unchecked((sbyte)0xf0), unchecked((int)(0x00)), unchecked((int)(0x00)), unchecked((int)(0x00)), unchecked((int)(0x00)), unchecked((int)(0x00)), unchecked((int)(0x01)) };
            RandomAccessReader reader = CreateReader(buffer);
            Tests.IsTrue(double.IsNaN(reader.GetDouble64(0)));
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetNullTerminatedString()
        {
            sbyte[] bytes = new sbyte[] { unchecked((int)(0x41)), unchecked((int)(0x42)), unchecked((int)(0x43)), unchecked((int)(0x44)), unchecked((int)(0x00)), unchecked((int)(0x45)), unchecked((int)(0x46)), unchecked((int)(0x47)) };
            RandomAccessReader reader = CreateReader(bytes);
            Tests.AreEqual(string.Empty, reader.GetNullTerminatedString(0, 0));
            Tests.AreEqual("A", reader.GetNullTerminatedString(0, 1));
            Tests.AreEqual("AB", reader.GetNullTerminatedString(0, 2));
            Tests.AreEqual("ABC", reader.GetNullTerminatedString(0, 3));
            Tests.AreEqual("ABCD", reader.GetNullTerminatedString(0, 4));
            Tests.AreEqual("ABCD", reader.GetNullTerminatedString(0, 5));
            Tests.AreEqual("ABCD", reader.GetNullTerminatedString(0, 6));
            Tests.AreEqual("BCD", reader.GetNullTerminatedString(1, 3));
            Tests.AreEqual("BCD", reader.GetNullTerminatedString(1, 4));
            Tests.AreEqual("BCD", reader.GetNullTerminatedString(1, 5));
            Tests.AreEqual(string.Empty, reader.GetNullTerminatedString(4, 3));
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetString()
        {
            sbyte[] bytes = new sbyte[] { unchecked((int)(0x41)), unchecked((int)(0x42)), unchecked((int)(0x43)), unchecked((int)(0x44)), unchecked((int)(0x00)), unchecked((int)(0x45)), unchecked((int)(0x46)), unchecked((int)(0x47)) };
            RandomAccessReader reader = CreateReader(bytes);
            Tests.AreEqual(string.Empty, reader.GetString(0, 0));
            Tests.AreEqual("A", reader.GetString(0, 1));
            Tests.AreEqual("AB", reader.GetString(0, 2));
            Tests.AreEqual("ABC", reader.GetString(0, 3));
            Tests.AreEqual("ABCD", reader.GetString(0, 4));
            Tests.AreEqual("ABCD\x0", reader.GetString(0, 5));
            Tests.AreEqual("ABCD\x0000E", reader.GetString(0, 6));
            Tests.AreEqual("BCD", reader.GetString(1, 3));
            Tests.AreEqual("BCD\x0", reader.GetString(1, 4));
            Tests.AreEqual("BCD\x0000E", reader.GetString(1, 5));
            Tests.AreEqual("\x0000EF", reader.GetString(4, 3));
        }

        [Test]
        public virtual void TestIndexPlusCountExceedsIntMaxValue()
        {
            RandomAccessReader reader = CreateReader(new sbyte[10]);
            try
            {
                reader.GetBytes(unchecked((int)(0x6FFFFFFF)), unchecked((int)(0x6FFFFFFF)));
            }
            catch (IOException e)
            {
                Tests.AreEqual("Number of requested bytes summed with starting index exceed maximum range of signed 32 bit integers (requested index: 1879048191, requested count: 1879048191)", e.Message);
            }
        }

        [Test]
        public virtual void TestOverflowBoundsCalculation()
        {
            RandomAccessReader reader = CreateReader(new sbyte[10]);
            try
            {
                reader.GetBytes(5, 10);
            }
            catch (IOException e)
            {
                Tests.AreEqual("Attempt to read from beyond end of underlying data source (requested index: 5, requested count: 10, max index: 9)", e.Message);
            }
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetBytesEOF()
        {
            CreateReader(new sbyte[50]).GetBytes(0, 50);
            RandomAccessReader reader = CreateReader(new sbyte[50]);
            reader.GetBytes(25, 25);
            try
            {
                CreateReader(new sbyte[50]).GetBytes(0, 51);
                Assert.Fail("Expecting exception");
            }
            catch (IOException)
            {
            }
        }

        /// <exception cref="System.Exception"/>
        [Test]
        public virtual void TestGetInt8EOF()
        {
            CreateReader(new sbyte[1]).GetInt8(0);
            RandomAccessReader reader = CreateReader(new sbyte[2]);
            reader.GetInt8(0);
            reader.GetInt8(1);
            try
            {
                reader = CreateReader(new sbyte[1]);
                reader.GetInt8(0);
                reader.GetInt8(1);
                Assert.Fail("Expecting exception");
            }
            catch (IOException)
            {
            }
        }
    }
}