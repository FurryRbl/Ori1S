using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Resources
{
	// Token: 0x02000319 RID: 793
	internal class Win32VersionResource : Win32Resource
	{
		// Token: 0x06002866 RID: 10342 RVA: 0x00090E2C File Offset: 0x0008F02C
		public Win32VersionResource(int id, int language, bool compilercontext) : base(Win32ResourceType.RT_VERSION, id, language)
		{
			this.signature = (long)((ulong)-17890115);
			this.struct_version = 65536;
			this.file_flags_mask = 63;
			this.file_flags = 0;
			this.file_os = 4;
			this.file_type = 2;
			this.file_subtype = 0;
			this.file_date = 0L;
			this.file_lang = ((!compilercontext) ? 127 : 0);
			this.file_codepage = 1200;
			this.properties = new Hashtable();
			string value = (!compilercontext) ? " " : string.Empty;
			foreach (string key in this.WellKnownProperties)
			{
				this.properties[key] = value;
			}
			this.LegalCopyright = " ";
			this.FileDescription = " ";
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002867 RID: 10343 RVA: 0x00090F54 File Offset: 0x0008F154
		// (set) Token: 0x06002868 RID: 10344 RVA: 0x00090FE4 File Offset: 0x0008F1E4
		public string Version
		{
			get
			{
				return string.Concat(new object[]
				{
					string.Empty,
					this.file_version >> 48,
					".",
					this.file_version >> 32 & 65535L,
					".",
					this.file_version >> 16 & 65535L,
					".",
					this.file_version & 65535L
				});
			}
			set
			{
				long[] array = new long[4];
				if (value != null)
				{
					string[] array2 = value.Split(new char[]
					{
						'.'
					});
					try
					{
						for (int i = 0; i < array2.Length; i++)
						{
							if (i < array.Length)
							{
								array[i] = (long)int.Parse(array2[i]);
							}
						}
					}
					catch (FormatException)
					{
					}
				}
				this.file_version = (array[0] << 48 | array[1] << 32 | (array[2] << 16) + array[3]);
				this.properties["FileVersion"] = this.Version;
			}
		}

		// Token: 0x17000736 RID: 1846
		public virtual string this[string key]
		{
			set
			{
				this.properties[key] = value;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600286A RID: 10346 RVA: 0x000910A4 File Offset: 0x0008F2A4
		// (set) Token: 0x0600286B RID: 10347 RVA: 0x000910BC File Offset: 0x0008F2BC
		public virtual string Comments
		{
			get
			{
				return (string)this.properties["Comments"];
			}
			set
			{
				this.properties["Comments"] = ((!(value == string.Empty)) ? value : " ");
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x000910EC File Offset: 0x0008F2EC
		// (set) Token: 0x0600286D RID: 10349 RVA: 0x00091104 File Offset: 0x0008F304
		public virtual string CompanyName
		{
			get
			{
				return (string)this.properties["CompanyName"];
			}
			set
			{
				this.properties["CompanyName"] = ((!(value == string.Empty)) ? value : " ");
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600286E RID: 10350 RVA: 0x00091134 File Offset: 0x0008F334
		// (set) Token: 0x0600286F RID: 10351 RVA: 0x0009114C File Offset: 0x0008F34C
		public virtual string LegalCopyright
		{
			get
			{
				return (string)this.properties["LegalCopyright"];
			}
			set
			{
				this.properties["LegalCopyright"] = ((!(value == string.Empty)) ? value : " ");
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x0009117C File Offset: 0x0008F37C
		// (set) Token: 0x06002871 RID: 10353 RVA: 0x00091194 File Offset: 0x0008F394
		public virtual string LegalTrademarks
		{
			get
			{
				return (string)this.properties["LegalTrademarks"];
			}
			set
			{
				this.properties["LegalTrademarks"] = ((!(value == string.Empty)) ? value : " ");
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002872 RID: 10354 RVA: 0x000911C4 File Offset: 0x0008F3C4
		// (set) Token: 0x06002873 RID: 10355 RVA: 0x000911DC File Offset: 0x0008F3DC
		public virtual string OriginalFilename
		{
			get
			{
				return (string)this.properties["OriginalFilename"];
			}
			set
			{
				this.properties["OriginalFilename"] = ((!(value == string.Empty)) ? value : " ");
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x0009120C File Offset: 0x0008F40C
		// (set) Token: 0x06002875 RID: 10357 RVA: 0x00091224 File Offset: 0x0008F424
		public virtual string ProductName
		{
			get
			{
				return (string)this.properties["ProductName"];
			}
			set
			{
				this.properties["ProductName"] = ((!(value == string.Empty)) ? value : " ");
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002876 RID: 10358 RVA: 0x00091254 File Offset: 0x0008F454
		// (set) Token: 0x06002877 RID: 10359 RVA: 0x0009126C File Offset: 0x0008F46C
		public virtual string ProductVersion
		{
			get
			{
				return (string)this.properties["ProductVersion"];
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					value = " ";
				}
				long[] array = new long[4];
				string[] array2 = value.Split(new char[]
				{
					'.'
				});
				try
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (i < array.Length)
						{
							array[i] = (long)int.Parse(array2[i]);
						}
					}
				}
				catch (FormatException)
				{
				}
				this.properties["ProductVersion"] = value;
				this.product_version = (array[0] << 48 | array[1] << 32 | (array[2] << 16) + array[3]);
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x0009132C File Offset: 0x0008F52C
		// (set) Token: 0x06002879 RID: 10361 RVA: 0x00091344 File Offset: 0x0008F544
		public virtual string InternalName
		{
			get
			{
				return (string)this.properties["InternalName"];
			}
			set
			{
				this.properties["InternalName"] = ((!(value == string.Empty)) ? value : " ");
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x00091374 File Offset: 0x0008F574
		// (set) Token: 0x0600287B RID: 10363 RVA: 0x0009138C File Offset: 0x0008F58C
		public virtual string FileDescription
		{
			get
			{
				return (string)this.properties["FileDescription"];
			}
			set
			{
				this.properties["FileDescription"] = ((!(value == string.Empty)) ? value : " ");
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x000913BC File Offset: 0x0008F5BC
		// (set) Token: 0x0600287D RID: 10365 RVA: 0x000913C4 File Offset: 0x0008F5C4
		public virtual int FileLanguage
		{
			get
			{
				return this.file_lang;
			}
			set
			{
				this.file_lang = value;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x000913D0 File Offset: 0x0008F5D0
		// (set) Token: 0x0600287F RID: 10367 RVA: 0x000913E8 File Offset: 0x0008F5E8
		public virtual string FileVersion
		{
			get
			{
				return (string)this.properties["FileVersion"];
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					value = " ";
				}
				long[] array = new long[4];
				string[] array2 = value.Split(new char[]
				{
					'.'
				});
				try
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (i < array.Length)
						{
							array[i] = (long)int.Parse(array2[i]);
						}
					}
				}
				catch (FormatException)
				{
				}
				this.properties["FileVersion"] = value;
				this.file_version = (array[0] << 48 | array[1] << 32 | (array[2] << 16) + array[3]);
			}
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000914A8 File Offset: 0x0008F6A8
		private void emit_padding(BinaryWriter w)
		{
			Stream baseStream = w.BaseStream;
			if (baseStream.Position % 4L != 0L)
			{
				w.Write(0);
			}
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000914D4 File Offset: 0x0008F6D4
		private void patch_length(BinaryWriter w, long len_pos)
		{
			Stream baseStream = w.BaseStream;
			long position = baseStream.Position;
			baseStream.Position = len_pos;
			w.Write((short)(position - len_pos));
			baseStream.Position = position;
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x00091508 File Offset: 0x0008F708
		public override void WriteTo(Stream ms)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(ms, Encoding.Unicode))
			{
				binaryWriter.Write(0);
				binaryWriter.Write(52);
				binaryWriter.Write(0);
				binaryWriter.Write("VS_VERSION_INFO".ToCharArray());
				binaryWriter.Write(0);
				this.emit_padding(binaryWriter);
				binaryWriter.Write((uint)this.signature);
				binaryWriter.Write(this.struct_version);
				binaryWriter.Write((int)(this.file_version >> 32));
				binaryWriter.Write((int)(this.file_version & (long)((ulong)-1)));
				binaryWriter.Write((int)(this.product_version >> 32));
				binaryWriter.Write((int)(this.product_version & (long)((ulong)-1)));
				binaryWriter.Write(this.file_flags_mask);
				binaryWriter.Write(this.file_flags);
				binaryWriter.Write(this.file_os);
				binaryWriter.Write(this.file_type);
				binaryWriter.Write(this.file_subtype);
				binaryWriter.Write((int)(this.file_date >> 32));
				binaryWriter.Write((int)(this.file_date & (long)((ulong)-1)));
				this.emit_padding(binaryWriter);
				long position = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write("VarFileInfo".ToCharArray());
				binaryWriter.Write(0);
				if (ms.Position % 4L != 0L)
				{
					binaryWriter.Write(0);
				}
				long position2 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(4);
				binaryWriter.Write(0);
				binaryWriter.Write("Translation".ToCharArray());
				binaryWriter.Write(0);
				if (ms.Position % 4L != 0L)
				{
					binaryWriter.Write(0);
				}
				binaryWriter.Write((short)this.file_lang);
				binaryWriter.Write((short)this.file_codepage);
				this.patch_length(binaryWriter, position2);
				this.patch_length(binaryWriter, position);
				long position3 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write("StringFileInfo".ToCharArray());
				this.emit_padding(binaryWriter);
				long position4 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write(string.Format("{0:x4}{1:x4}", this.file_lang, this.file_codepage).ToCharArray());
				this.emit_padding(binaryWriter);
				foreach (object obj in this.properties.Keys)
				{
					string text = (string)obj;
					string text2 = (string)this.properties[text];
					long position5 = ms.Position;
					binaryWriter.Write(0);
					binaryWriter.Write((short)(text2.ToCharArray().Length + 1));
					binaryWriter.Write(1);
					binaryWriter.Write(text.ToCharArray());
					binaryWriter.Write(0);
					this.emit_padding(binaryWriter);
					binaryWriter.Write(text2.ToCharArray());
					binaryWriter.Write(0);
					this.emit_padding(binaryWriter);
					this.patch_length(binaryWriter, position5);
				}
				this.patch_length(binaryWriter, position4);
				this.patch_length(binaryWriter, position3);
				this.patch_length(binaryWriter, 0L);
			}
		}

		// Token: 0x04001066 RID: 4198
		public string[] WellKnownProperties = new string[]
		{
			"Comments",
			"CompanyName",
			"FileVersion",
			"InternalName",
			"LegalTrademarks",
			"OriginalFilename",
			"ProductName",
			"ProductVersion"
		};

		// Token: 0x04001067 RID: 4199
		private long signature;

		// Token: 0x04001068 RID: 4200
		private int struct_version;

		// Token: 0x04001069 RID: 4201
		private long file_version;

		// Token: 0x0400106A RID: 4202
		private long product_version;

		// Token: 0x0400106B RID: 4203
		private int file_flags_mask;

		// Token: 0x0400106C RID: 4204
		private int file_flags;

		// Token: 0x0400106D RID: 4205
		private int file_os;

		// Token: 0x0400106E RID: 4206
		private int file_type;

		// Token: 0x0400106F RID: 4207
		private int file_subtype;

		// Token: 0x04001070 RID: 4208
		private long file_date;

		// Token: 0x04001071 RID: 4209
		private int file_lang;

		// Token: 0x04001072 RID: 4210
		private int file_codepage;

		// Token: 0x04001073 RID: 4211
		private Hashtable properties;
	}
}
