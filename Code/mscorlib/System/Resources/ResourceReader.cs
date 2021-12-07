using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;

namespace System.Resources
{
	/// <summary>Enumerates .resources files and streams, reading sequential resource name and value pairs.</summary>
	// Token: 0x02000309 RID: 777
	[ComVisible(true)]
	public sealed class ResourceReader : IEnumerable, IDisposable, IResourceReader
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceReader" /> class for the specified stream.</summary>
		/// <param name="stream">The input stream for reading resources. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> is not readable. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred while accessing <paramref name="stream" />. </exception>
		// Token: 0x06002809 RID: 10249 RVA: 0x0008EAD0 File Offset: 0x0008CCD0
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public ResourceReader(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream was not readable.");
			}
			this.reader = new BinaryReader(stream, Encoding.UTF8);
			this.formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
			this.ReadHeaders();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceReader" /> class for the specified resource file.</summary>
		/// <param name="fileName">The path of the resource file to be read. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.BadImageFormatException">The resource file has an invalid format. For example, the length of the file is zero.</exception>
		// Token: 0x0600280A RID: 10250 RVA: 0x0008EB4C File Offset: 0x0008CD4C
		public ResourceReader(string fileName)
		{
			this.reader = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read));
			this.formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
			this.ReadHeaders();
		}

		/// <summary>Returns an enumerator for this <see cref="T:System.Resources.ResourceReader" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for this <see cref="T:System.Resources.ResourceReader" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The reader has already been closed, and thus cannot be accessed. </exception>
		// Token: 0x0600280B RID: 10251 RVA: 0x0008EBA4 File Offset: 0x0008CDA4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IResourceReader)this).GetEnumerator();
		}

		/// <summary>Releases the resources used by the <see cref="T:System.Resources.ResourceReader" />.</summary>
		// Token: 0x0600280C RID: 10252 RVA: 0x0008EBAC File Offset: 0x0008CDAC
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x0008EBB8 File Offset: 0x0008CDB8
		private void ReadHeaders()
		{
			try
			{
				int num = this.reader.ReadInt32();
				if (num != ResourceManager.MagicNumber)
				{
					throw new ArgumentException(string.Format("Stream is not a valid .resources file, magic=0x{0:x}", num));
				}
				int num2 = this.reader.ReadInt32();
				int num3 = this.reader.ReadInt32();
				if (num2 > ResourceManager.HeaderVersionNumber)
				{
					this.reader.BaseStream.Seek((long)num3, SeekOrigin.Current);
				}
				else
				{
					string text = this.reader.ReadString();
					if (!text.StartsWith("System.Resources.ResourceReader"))
					{
						throw new NotSupportedException("This .resources file requires reader class " + text);
					}
					string text2 = this.reader.ReadString();
					if (!text2.StartsWith(typeof(ResourceSet).FullName) && !text2.StartsWith("System.Resources.RuntimeResourceSet"))
					{
						throw new NotSupportedException("This .resources file requires set class " + text2);
					}
				}
				this.resource_ver = this.reader.ReadInt32();
				if (this.resource_ver != 1 && this.resource_ver != 2)
				{
					throw new NotSupportedException("This .resources file requires unsupported set class version: " + this.resource_ver.ToString());
				}
				this.resourceCount = this.reader.ReadInt32();
				this.typeCount = this.reader.ReadInt32();
				this.typeNames = new string[this.typeCount];
				for (int i = 0; i < this.typeCount; i++)
				{
					this.typeNames[i] = this.reader.ReadString();
				}
				int num4 = (int)(this.reader.BaseStream.Position & 7L);
				int num5 = 0;
				if (num4 != 0)
				{
					num5 = 8 - num4;
				}
				for (int j = 0; j < num5; j++)
				{
					byte b = this.reader.ReadByte();
					if ((char)b != "PAD"[j % 3])
					{
						throw new ArgumentException("Malformed .resources file (padding values incorrect)");
					}
				}
				this.hashes = new int[this.resourceCount];
				for (int k = 0; k < this.resourceCount; k++)
				{
					this.hashes[k] = this.reader.ReadInt32();
				}
				long[] array = new long[this.resourceCount];
				for (int l = 0; l < this.resourceCount; l++)
				{
					array[l] = (long)this.reader.ReadInt32();
				}
				this.dataSectionOffset = this.reader.ReadInt32();
				this.nameSectionOffset = this.reader.BaseStream.Position;
				long position = this.reader.BaseStream.Position;
				this.infos = new ResourceReader.ResourceInfo[this.resourceCount];
				for (int m = 0; m < this.resourceCount; m++)
				{
					this.CreateResourceInfo(array[m], ref this.infos[m]);
				}
				this.reader.BaseStream.Seek(position, SeekOrigin.Begin);
			}
			catch (EndOfStreamException innerException)
			{
				throw new ArgumentException("Stream is not a valid .resources file!  It was possibly truncated.", innerException);
			}
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x0008EEF8 File Offset: 0x0008D0F8
		private void CreateResourceInfo(long position, ref ResourceReader.ResourceInfo info)
		{
			long offset = position + this.nameSectionOffset;
			this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
			int num = this.Read7BitEncodedInt();
			byte[] array = new byte[num];
			this.reader.Read(array, 0, num);
			string @string = Encoding.Unicode.GetString(array);
			long offset2 = (long)(this.reader.ReadInt32() + this.dataSectionOffset);
			this.reader.BaseStream.Seek(offset2, SeekOrigin.Begin);
			int type_index = this.Read7BitEncodedInt();
			info = new ResourceReader.ResourceInfo(@string, this.reader.BaseStream.Position, type_index);
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x0008EF94 File Offset: 0x0008D194
		private int Read7BitEncodedInt()
		{
			int num = 0;
			int num2 = 0;
			byte b;
			do
			{
				b = this.reader.ReadByte();
				num |= (int)(b & 127) << num2;
				num2 += 7;
			}
			while ((b & 128) == 128);
			return num;
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x0008EFD4 File Offset: 0x0008D1D4
		private object ReadValueVer2(int type_index)
		{
			switch (type_index)
			{
			case 0:
				return null;
			case 1:
				return this.reader.ReadString();
			case 2:
				return this.reader.ReadBoolean();
			case 3:
				return (char)this.reader.ReadUInt16();
			case 4:
				return this.reader.ReadByte();
			case 5:
				return this.reader.ReadSByte();
			case 6:
				return this.reader.ReadInt16();
			case 7:
				return this.reader.ReadUInt16();
			case 8:
				return this.reader.ReadInt32();
			case 9:
				return this.reader.ReadUInt32();
			case 10:
				return this.reader.ReadInt64();
			case 11:
				return this.reader.ReadUInt64();
			case 12:
				return this.reader.ReadSingle();
			case 13:
				return this.reader.ReadDouble();
			case 14:
				return this.reader.ReadDecimal();
			case 15:
				return new DateTime(this.reader.ReadInt64());
			case 16:
				return new TimeSpan(this.reader.ReadInt64());
			case 32:
				return this.reader.ReadBytes(this.reader.ReadInt32());
			case 33:
			{
				byte[] array = new byte[this.reader.ReadUInt32()];
				this.reader.Read(array, 0, array.Length);
				return new MemoryStream(array);
			}
			}
			type_index -= 64;
			return this.ReadNonPredefinedValue(Type.GetType(this.typeNames[type_index], true));
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x0008F1E8 File Offset: 0x0008D3E8
		private object ReadValueVer1(Type type)
		{
			if (type == typeof(string))
			{
				return this.reader.ReadString();
			}
			if (type == typeof(int))
			{
				return this.reader.ReadInt32();
			}
			if (type == typeof(byte))
			{
				return this.reader.ReadByte();
			}
			if (type == typeof(double))
			{
				return this.reader.ReadDouble();
			}
			if (type == typeof(short))
			{
				return this.reader.ReadInt16();
			}
			if (type == typeof(long))
			{
				return this.reader.ReadInt64();
			}
			if (type == typeof(sbyte))
			{
				return this.reader.ReadSByte();
			}
			if (type == typeof(float))
			{
				return this.reader.ReadSingle();
			}
			if (type == typeof(TimeSpan))
			{
				return new TimeSpan(this.reader.ReadInt64());
			}
			if (type == typeof(ushort))
			{
				return this.reader.ReadUInt16();
			}
			if (type == typeof(uint))
			{
				return this.reader.ReadUInt32();
			}
			if (type == typeof(ulong))
			{
				return this.reader.ReadUInt64();
			}
			if (type == typeof(decimal))
			{
				return this.reader.ReadDecimal();
			}
			if (type == typeof(DateTime))
			{
				return new DateTime(this.reader.ReadInt64());
			}
			return this.ReadNonPredefinedValue(type);
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x0008F3D0 File Offset: 0x0008D5D0
		private object ReadNonPredefinedValue(Type exp_type)
		{
			object obj = this.formatter.Deserialize(this.reader.BaseStream);
			if (obj.GetType() != exp_type)
			{
				throw new InvalidOperationException("Deserialized object is wrong type");
			}
			return obj;
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x0008F40C File Offset: 0x0008D60C
		private void LoadResourceValues(ResourceReader.ResourceCacheItem[] store)
		{
			object obj = this.readerLock;
			lock (obj)
			{
				for (int i = 0; i < this.resourceCount; i++)
				{
					ResourceReader.ResourceInfo resourceInfo = this.infos[i];
					if (resourceInfo.TypeIndex == -1)
					{
						store[i] = new ResourceReader.ResourceCacheItem(resourceInfo.ResourceName, null);
					}
					else
					{
						this.reader.BaseStream.Seek(resourceInfo.ValuePosition, SeekOrigin.Begin);
						object value;
						if (this.resource_ver == 2)
						{
							value = this.ReadValueVer2(resourceInfo.TypeIndex);
						}
						else
						{
							value = this.ReadValueVer1(Type.GetType(this.typeNames[resourceInfo.TypeIndex], true));
						}
						store[i] = new ResourceReader.ResourceCacheItem(resourceInfo.ResourceName, value);
					}
				}
			}
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x0008F510 File Offset: 0x0008D710
		internal unsafe UnmanagedMemoryStream ResourceValueAsStream(string name, int index)
		{
			ResourceReader.ResourceInfo resourceInfo = this.infos[index];
			if (resourceInfo.TypeIndex != 33)
			{
				throw new InvalidOperationException(string.Format("Resource '{0}' was not a Stream. Use GetObject() instead.", name));
			}
			object obj = this.readerLock;
			UnmanagedMemoryStream result;
			lock (obj)
			{
				this.reader.BaseStream.Seek(resourceInfo.ValuePosition, SeekOrigin.Begin);
				long num = (long)this.reader.ReadInt32();
				UnmanagedMemoryStream unmanagedMemoryStream = this.reader.BaseStream as UnmanagedMemoryStream;
				if (unmanagedMemoryStream != null)
				{
					result = new UnmanagedMemoryStream(unmanagedMemoryStream.PositionPointer, num);
				}
				else
				{
					IntPtr ptr = Marshal.AllocHGlobal((int)num);
					byte* pointer = (byte*)ptr.ToPointer();
					UnmanagedMemoryStream unmanagedMemoryStream2 = new UnmanagedMemoryStream(pointer, num, num, FileAccess.ReadWrite);
					unmanagedMemoryStream2.Closed += delegate(object o, EventArgs e)
					{
						Marshal.FreeHGlobal(ptr);
					};
					byte[] array = new byte[(num >= 1024L) ? 1024L : num];
					while (num > 0L)
					{
						int num2 = this.reader.Read(array, 0, (int)Math.Min((long)array.Length, num));
						if (num2 == 0)
						{
							throw new FormatException("The resource data is corrupt. Resource stream ended");
						}
						unmanagedMemoryStream2.Write(array, 0, num2);
						num -= (long)num2;
					}
					unmanagedMemoryStream2.Seek(0L, SeekOrigin.Begin);
					result = unmanagedMemoryStream2;
				}
			}
			return result;
		}

		/// <summary>Releases all operating system resources associated with this <see cref="T:System.Resources.ResourceReader" />.</summary>
		// Token: 0x06002815 RID: 10261 RVA: 0x0008F69C File Offset: 0x0008D89C
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Returns an enumerator for this <see cref="T:System.Resources.ResourceReader" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for this <see cref="T:System.Resources.ResourceReader" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The reader has been closed or disposed, and cannot be accessed. </exception>
		// Token: 0x06002816 RID: 10262 RVA: 0x0008F6A8 File Offset: 0x0008D8A8
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this.reader == null)
			{
				throw new InvalidOperationException("ResourceReader is closed.");
			}
			return new ResourceReader.ResourceEnumerator(this);
		}

		/// <summary>Retrieves the type name and data content of a named resource from an open resource file or stream.</summary>
		/// <param name="resourceName">The name of a resource.</param>
		/// <param name="resourceType">When this method returns, contains a string that is the type name of the retrieved type. This parameter is passed uninitialized.</param>
		/// <param name="resourceData">When this method returns, contains a byte array that is the binary representation of the retrieved type. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="resourceName" /> does not exist.</exception>
		/// <exception cref="T:System.FormatException">The retrieved resource data is corrupted.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Resources.ResourceReader" /> object is not initialized. The probable cause is that the <see cref="T:System.Resources.ResourceReader" /> object is closed.</exception>
		// Token: 0x06002817 RID: 10263 RVA: 0x0008F6C8 File Offset: 0x0008D8C8
		public void GetResourceData(string resourceName, out string resourceType, out byte[] resourceData)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			ResourceReader.ResourceEnumerator resourceEnumerator = new ResourceReader.ResourceEnumerator(this);
			while (resourceEnumerator.MoveNext())
			{
				if ((string)resourceEnumerator.Key == resourceName)
				{
					this.GetResourceDataAt(resourceEnumerator.Index, out resourceType, out resourceData);
					return;
				}
			}
			throw new ArgumentException(string.Format("Specified resource not found: {0}", resourceName));
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x0008F734 File Offset: 0x0008D934
		private void GetResourceDataAt(int index, out string resourceType, out byte[] data)
		{
			ResourceReader.ResourceInfo resourceInfo = this.infos[index];
			int typeIndex = resourceInfo.TypeIndex;
			if (typeIndex == -1)
			{
				throw new FormatException("The resource data is corrupt");
			}
			object obj = this.readerLock;
			lock (obj)
			{
				this.reader.BaseStream.Seek(resourceInfo.ValuePosition, SeekOrigin.Begin);
				long position = this.reader.BaseStream.Position;
				if (this.resource_ver == 2)
				{
					if (typeIndex >= 64)
					{
						int num = typeIndex - 64;
						if (num >= this.typeNames.Length)
						{
							throw new FormatException("The resource data is corrupt. Invalid index to types");
						}
						resourceType = this.typeNames[num];
					}
					else
					{
						resourceType = "ResourceTypeCode." + (PredefinedResourceType)typeIndex;
					}
					this.ReadValueVer2(typeIndex);
				}
				else
				{
					resourceType = "ResourceTypeCode.Null";
					this.ReadValueVer1(Type.GetType(this.typeNames[typeIndex], true));
				}
				int num2 = (int)(this.reader.BaseStream.Position - position);
				this.reader.BaseStream.Seek((long)(-(long)num2), SeekOrigin.Current);
				data = new byte[num2];
				this.reader.BaseStream.Read(data, 0, num2);
			}
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x0008F898 File Offset: 0x0008DA98
		private void Dispose(bool disposing)
		{
			if (disposing && this.reader != null)
			{
				this.reader.Close();
			}
			this.reader = null;
			this.hashes = null;
			this.infos = null;
			this.typeNames = null;
			this.cache = null;
		}

		// Token: 0x04001028 RID: 4136
		private BinaryReader reader;

		// Token: 0x04001029 RID: 4137
		private object readerLock = new object();

		// Token: 0x0400102A RID: 4138
		private IFormatter formatter;

		// Token: 0x0400102B RID: 4139
		internal int resourceCount;

		// Token: 0x0400102C RID: 4140
		private int typeCount;

		// Token: 0x0400102D RID: 4141
		private string[] typeNames;

		// Token: 0x0400102E RID: 4142
		private int[] hashes;

		// Token: 0x0400102F RID: 4143
		private ResourceReader.ResourceInfo[] infos;

		// Token: 0x04001030 RID: 4144
		private int dataSectionOffset;

		// Token: 0x04001031 RID: 4145
		private long nameSectionOffset;

		// Token: 0x04001032 RID: 4146
		private int resource_ver;

		// Token: 0x04001033 RID: 4147
		private ResourceReader.ResourceCacheItem[] cache;

		// Token: 0x04001034 RID: 4148
		private object cache_lock = new object();

		// Token: 0x0200030A RID: 778
		private struct ResourceInfo
		{
			// Token: 0x0600281A RID: 10266 RVA: 0x0008F8E4 File Offset: 0x0008DAE4
			public ResourceInfo(string resourceName, long valuePosition, int type_index)
			{
				this.ValuePosition = valuePosition;
				this.ResourceName = resourceName;
				this.TypeIndex = type_index;
			}

			// Token: 0x04001035 RID: 4149
			public readonly long ValuePosition;

			// Token: 0x04001036 RID: 4150
			public readonly string ResourceName;

			// Token: 0x04001037 RID: 4151
			public readonly int TypeIndex;
		}

		// Token: 0x0200030B RID: 779
		private struct ResourceCacheItem
		{
			// Token: 0x0600281B RID: 10267 RVA: 0x0008F8FC File Offset: 0x0008DAFC
			public ResourceCacheItem(string name, object value)
			{
				this.ResourceName = name;
				this.ResourceValue = value;
			}

			// Token: 0x04001038 RID: 4152
			public readonly string ResourceName;

			// Token: 0x04001039 RID: 4153
			public readonly object ResourceValue;
		}

		// Token: 0x0200030C RID: 780
		internal sealed class ResourceEnumerator : IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x0600281C RID: 10268 RVA: 0x0008F90C File Offset: 0x0008DB0C
			internal ResourceEnumerator(ResourceReader readerToEnumerate)
			{
				this.reader = readerToEnumerate;
				this.FillCache();
			}

			// Token: 0x17000724 RID: 1828
			// (get) Token: 0x0600281D RID: 10269 RVA: 0x0008F928 File Offset: 0x0008DB28
			public int Index
			{
				get
				{
					return this.index;
				}
			}

			// Token: 0x17000725 RID: 1829
			// (get) Token: 0x0600281E RID: 10270 RVA: 0x0008F930 File Offset: 0x0008DB30
			public DictionaryEntry Entry
			{
				get
				{
					if (this.reader.reader == null)
					{
						throw new InvalidOperationException("ResourceReader is closed.");
					}
					if (this.index < 0)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					return new DictionaryEntry(this.Key, this.Value);
				}
			}

			// Token: 0x17000726 RID: 1830
			// (get) Token: 0x0600281F RID: 10271 RVA: 0x0008F980 File Offset: 0x0008DB80
			public object Key
			{
				get
				{
					if (this.reader.reader == null)
					{
						throw new InvalidOperationException("ResourceReader is closed.");
					}
					if (this.index < 0)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					return this.reader.cache[this.index].ResourceName;
				}
			}

			// Token: 0x17000727 RID: 1831
			// (get) Token: 0x06002820 RID: 10272 RVA: 0x0008F9DC File Offset: 0x0008DBDC
			public object Value
			{
				get
				{
					if (this.reader.reader == null)
					{
						throw new InvalidOperationException("ResourceReader is closed.");
					}
					if (this.index < 0)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					return this.reader.cache[this.index].ResourceValue;
				}
			}

			// Token: 0x17000728 RID: 1832
			// (get) Token: 0x06002821 RID: 10273 RVA: 0x0008FA38 File Offset: 0x0008DC38
			public UnmanagedMemoryStream ValueAsStream
			{
				get
				{
					if (this.reader.reader == null)
					{
						throw new InvalidOperationException("ResourceReader is closed.");
					}
					if (this.index < 0)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					return this.reader.ResourceValueAsStream((string)this.Key, this.index);
				}
			}

			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x06002822 RID: 10274 RVA: 0x0008FA94 File Offset: 0x0008DC94
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06002823 RID: 10275 RVA: 0x0008FAA4 File Offset: 0x0008DCA4
			public bool MoveNext()
			{
				if (this.reader.reader == null)
				{
					throw new InvalidOperationException("ResourceReader is closed.");
				}
				if (this.finished)
				{
					return false;
				}
				if (++this.index < this.reader.resourceCount)
				{
					return true;
				}
				this.finished = true;
				return false;
			}

			// Token: 0x06002824 RID: 10276 RVA: 0x0008FB04 File Offset: 0x0008DD04
			public void Reset()
			{
				if (this.reader.reader == null)
				{
					throw new InvalidOperationException("ResourceReader is closed.");
				}
				this.index = -1;
				this.finished = false;
			}

			// Token: 0x06002825 RID: 10277 RVA: 0x0008FB30 File Offset: 0x0008DD30
			private void FillCache()
			{
				if (this.reader.cache != null)
				{
					return;
				}
				object cache_lock = this.reader.cache_lock;
				lock (cache_lock)
				{
					if (this.reader.cache == null)
					{
						ResourceReader.ResourceCacheItem[] array = new ResourceReader.ResourceCacheItem[this.reader.resourceCount];
						this.reader.LoadResourceValues(array);
						this.reader.cache = array;
					}
				}
			}

			// Token: 0x0400103A RID: 4154
			private ResourceReader reader;

			// Token: 0x0400103B RID: 4155
			private int index = -1;

			// Token: 0x0400103C RID: 4156
			private bool finished;
		}
	}
}
