using System;
using System.Collections;
using System.Text;

namespace Mono
{
	// Token: 0x0200008C RID: 140
	internal abstract class DataConverter
	{
		// Token: 0x0600082F RID: 2095
		public abstract double GetDouble(byte[] data, int index);

		// Token: 0x06000830 RID: 2096
		public abstract float GetFloat(byte[] data, int index);

		// Token: 0x06000831 RID: 2097
		public abstract long GetInt64(byte[] data, int index);

		// Token: 0x06000832 RID: 2098
		public abstract int GetInt32(byte[] data, int index);

		// Token: 0x06000833 RID: 2099
		public abstract short GetInt16(byte[] data, int index);

		// Token: 0x06000834 RID: 2100
		[CLSCompliant(false)]
		public abstract uint GetUInt32(byte[] data, int index);

		// Token: 0x06000835 RID: 2101
		[CLSCompliant(false)]
		public abstract ushort GetUInt16(byte[] data, int index);

		// Token: 0x06000836 RID: 2102
		[CLSCompliant(false)]
		public abstract ulong GetUInt64(byte[] data, int index);

		// Token: 0x06000837 RID: 2103
		public abstract void PutBytes(byte[] dest, int destIdx, double value);

		// Token: 0x06000838 RID: 2104
		public abstract void PutBytes(byte[] dest, int destIdx, float value);

		// Token: 0x06000839 RID: 2105
		public abstract void PutBytes(byte[] dest, int destIdx, int value);

		// Token: 0x0600083A RID: 2106
		public abstract void PutBytes(byte[] dest, int destIdx, long value);

		// Token: 0x0600083B RID: 2107
		public abstract void PutBytes(byte[] dest, int destIdx, short value);

		// Token: 0x0600083C RID: 2108
		[CLSCompliant(false)]
		public abstract void PutBytes(byte[] dest, int destIdx, ushort value);

		// Token: 0x0600083D RID: 2109
		[CLSCompliant(false)]
		public abstract void PutBytes(byte[] dest, int destIdx, uint value);

		// Token: 0x0600083E RID: 2110
		[CLSCompliant(false)]
		public abstract void PutBytes(byte[] dest, int destIdx, ulong value);

		// Token: 0x0600083F RID: 2111 RVA: 0x0001E0DC File Offset: 0x0001C2DC
		public byte[] GetBytes(double value)
		{
			byte[] array = new byte[8];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001E0FC File Offset: 0x0001C2FC
		public byte[] GetBytes(float value)
		{
			byte[] array = new byte[4];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001E11C File Offset: 0x0001C31C
		public byte[] GetBytes(int value)
		{
			byte[] array = new byte[4];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001E13C File Offset: 0x0001C33C
		public byte[] GetBytes(long value)
		{
			byte[] array = new byte[8];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001E15C File Offset: 0x0001C35C
		public byte[] GetBytes(short value)
		{
			byte[] array = new byte[2];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001E17C File Offset: 0x0001C37C
		[CLSCompliant(false)]
		public byte[] GetBytes(ushort value)
		{
			byte[] array = new byte[2];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001E19C File Offset: 0x0001C39C
		[CLSCompliant(false)]
		public byte[] GetBytes(uint value)
		{
			byte[] array = new byte[4];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001E1BC File Offset: 0x0001C3BC
		[CLSCompliant(false)]
		public byte[] GetBytes(ulong value)
		{
			byte[] array = new byte[8];
			this.PutBytes(array, 0, value);
			return array;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0001E1DC File Offset: 0x0001C3DC
		public static DataConverter LittleEndian
		{
			get
			{
				return (!BitConverter.IsLittleEndian) ? DataConverter.SwapConv : DataConverter.CopyConv;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0001E1F8 File Offset: 0x0001C3F8
		public static DataConverter BigEndian
		{
			get
			{
				return (!BitConverter.IsLittleEndian) ? DataConverter.CopyConv : DataConverter.SwapConv;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0001E214 File Offset: 0x0001C414
		public static DataConverter Native
		{
			get
			{
				return DataConverter.CopyConv;
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001E21C File Offset: 0x0001C41C
		private static int Align(int current, int align)
		{
			return (current + align - 1) / align * align;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001E228 File Offset: 0x0001C428
		public static byte[] Pack(string description, params object[] args)
		{
			int num = 0;
			DataConverter.PackContext packContext = new DataConverter.PackContext();
			packContext.conv = DataConverter.CopyConv;
			packContext.description = description;
			packContext.i = 0;
			while (packContext.i < description.Length)
			{
				object oarg;
				if (num < args.Length)
				{
					oarg = args[num];
				}
				else
				{
					if (packContext.repeat != 0)
					{
						break;
					}
					oarg = null;
				}
				int i = packContext.i;
				if (DataConverter.PackOne(packContext, oarg))
				{
					num++;
					if (packContext.repeat > 0)
					{
						if (--packContext.repeat > 0)
						{
							packContext.i = i;
						}
						else
						{
							packContext.i++;
						}
					}
					else
					{
						packContext.i++;
					}
				}
				else
				{
					packContext.i++;
				}
			}
			return packContext.Get();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001E314 File Offset: 0x0001C514
		public static byte[] PackEnumerable(string description, IEnumerable args)
		{
			DataConverter.PackContext packContext = new DataConverter.PackContext();
			packContext.conv = DataConverter.CopyConv;
			packContext.description = description;
			IEnumerator enumerator = args.GetEnumerator();
			bool flag = enumerator.MoveNext();
			packContext.i = 0;
			while (packContext.i < description.Length)
			{
				object oarg;
				if (flag)
				{
					oarg = enumerator.Current;
				}
				else
				{
					if (packContext.repeat != 0)
					{
						break;
					}
					oarg = null;
				}
				int i = packContext.i;
				if (DataConverter.PackOne(packContext, oarg))
				{
					flag = enumerator.MoveNext();
					if (packContext.repeat > 0)
					{
						if (--packContext.repeat > 0)
						{
							packContext.i = i;
						}
						else
						{
							packContext.i++;
						}
					}
					else
					{
						packContext.i++;
					}
				}
				else
				{
					packContext.i++;
				}
			}
			return packContext.Get();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001E410 File Offset: 0x0001C610
		private static bool PackOne(DataConverter.PackContext b, object oarg)
		{
			char c = b.description[b.i];
			switch (c)
			{
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				b.repeat = (int)((short)b.description[b.i] - 48);
				return false;
			default:
				switch (c)
				{
				case '[':
				{
					int num = -1;
					int i;
					for (i = b.i + 1; i < b.description.Length; i++)
					{
						if (b.description[i] == ']')
						{
							break;
						}
						int num2 = (int)((short)b.description[i] - 48);
						if (num2 >= 0 && num2 <= 9)
						{
							if (num == -1)
							{
								num = num2;
							}
							else
							{
								num = num * 10 + num2;
							}
						}
					}
					if (num == -1)
					{
						throw new ArgumentException("invalid size specification");
					}
					b.i = i;
					b.repeat = num;
					return false;
				}
				default:
				{
					switch (c)
					{
					case '!':
						b.align = -1;
						return false;
					default:
						switch (c)
						{
						case 'I':
							b.Add(b.conv.GetBytes(Convert.ToUInt32(oarg)));
							return true;
						default:
							switch (c)
							{
							case 'x':
								b.Add(new byte[1]);
								return false;
							default:
								if (c == '*')
								{
									b.repeat = int.MaxValue;
									return false;
								}
								if (c == 'S')
								{
									b.Add(b.conv.GetBytes(Convert.ToUInt16(oarg)));
									return true;
								}
								if (c != 's')
								{
									throw new ArgumentException(string.Format("invalid format specified `{0}'", b.description[b.i]));
								}
								b.Add(b.conv.GetBytes(Convert.ToInt16(oarg)));
								return true;
							case 'z':
								break;
							}
							break;
						case 'L':
							b.Add(b.conv.GetBytes(Convert.ToUInt64(oarg)));
							return true;
						}
						break;
					case '$':
						break;
					case '%':
						b.conv = DataConverter.Native;
						return false;
					}
					bool flag = b.description[b.i] == 'z';
					b.i++;
					if (b.i >= b.description.Length)
					{
						throw new ArgumentException("$ description needs a type specified", "description");
					}
					char c2 = b.description[b.i];
					char c3 = c2;
					int num2;
					Encoding encoding;
					switch (c3)
					{
					case '3':
						encoding = Encoding.GetEncoding(12000);
						num2 = 4;
						break;
					case '4':
						encoding = Encoding.GetEncoding(12001);
						num2 = 4;
						break;
					default:
						if (c3 != 'b')
						{
							throw new ArgumentException("Invalid format for $ specifier", "description");
						}
						encoding = Encoding.BigEndianUnicode;
						num2 = 2;
						break;
					case '6':
						encoding = Encoding.Unicode;
						num2 = 2;
						break;
					case '7':
						encoding = Encoding.UTF7;
						num2 = 1;
						break;
					case '8':
						encoding = Encoding.UTF8;
						num2 = 1;
						break;
					}
					if (b.align == -1)
					{
						b.align = 4;
					}
					b.Add(encoding.GetBytes(Convert.ToString(oarg)));
					if (flag)
					{
						b.Add(new byte[num2]);
					}
					break;
				}
				case '^':
					b.conv = DataConverter.BigEndian;
					return false;
				case '_':
					b.conv = DataConverter.LittleEndian;
					return false;
				case 'b':
					b.Add(new byte[]
					{
						Convert.ToByte(oarg)
					});
					break;
				case 'c':
					b.Add(new byte[]
					{
						(byte)Convert.ToSByte(oarg)
					});
					break;
				case 'd':
					b.Add(b.conv.GetBytes(Convert.ToDouble(oarg)));
					break;
				case 'f':
					b.Add(b.conv.GetBytes(Convert.ToSingle(oarg)));
					break;
				case 'i':
					b.Add(b.conv.GetBytes(Convert.ToInt32(oarg)));
					break;
				case 'l':
					b.Add(b.conv.GetBytes(Convert.ToInt64(oarg)));
					break;
				}
				break;
			case 'C':
				b.Add(new byte[]
				{
					Convert.ToByte(oarg)
				});
				break;
			}
			return true;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001E8D4 File Offset: 0x0001CAD4
		private static bool Prepare(byte[] buffer, ref int idx, int size, ref bool align)
		{
			if (align)
			{
				idx = DataConverter.Align(idx, size);
				align = false;
			}
			if (idx + size > buffer.Length)
			{
				idx = buffer.Length;
				return false;
			}
			return true;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001E90C File Offset: 0x0001CB0C
		public static IList Unpack(string description, byte[] buffer, int startIndex)
		{
			DataConverter dataConverter = DataConverter.CopyConv;
			ArrayList arrayList = new ArrayList();
			int num = startIndex;
			bool flag = false;
			int num2 = 0;
			int num3 = 0;
			while (num3 < description.Length && num < buffer.Length)
			{
				int num4 = num3;
				char c = description[num3];
				switch (c)
				{
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					num2 = (int)((short)description[num3] - 48);
					num4 = num3 + 1;
					break;
				default:
					switch (c)
					{
					case '[':
					{
						int num5 = -1;
						int i;
						for (i = num3 + 1; i < description.Length; i++)
						{
							if (description[i] == ']')
							{
								break;
							}
							int num6 = (int)((short)description[i] - 48);
							if (num6 >= 0 && num6 <= 9)
							{
								if (num5 == -1)
								{
									num5 = num6;
								}
								else
								{
									num5 = num5 * 10 + num6;
								}
							}
						}
						if (num5 == -1)
						{
							throw new ArgumentException("invalid size specification");
						}
						num3 = i;
						num2 = num5;
						break;
					}
					default:
					{
						switch (c)
						{
						case '!':
							flag = true;
							goto IL_683;
						default:
							switch (c)
							{
							case 'I':
								if (DataConverter.Prepare(buffer, ref num, 4, ref flag))
								{
									arrayList.Add(dataConverter.GetUInt32(buffer, num));
									num += 4;
								}
								goto IL_683;
							default:
								switch (c)
								{
								case 'x':
									num++;
									goto IL_683;
								default:
									if (c == '*')
									{
										num2 = int.MaxValue;
										goto IL_683;
									}
									if (c == 'S')
									{
										if (DataConverter.Prepare(buffer, ref num, 2, ref flag))
										{
											arrayList.Add(dataConverter.GetUInt16(buffer, num));
											num += 2;
										}
										goto IL_683;
									}
									if (c != 's')
									{
										throw new ArgumentException(string.Format("invalid format specified `{0}'", description[num3]));
									}
									if (DataConverter.Prepare(buffer, ref num, 2, ref flag))
									{
										arrayList.Add(dataConverter.GetInt16(buffer, num));
										num += 2;
									}
									goto IL_683;
								case 'z':
									break;
								}
								break;
							case 'L':
								if (DataConverter.Prepare(buffer, ref num, 8, ref flag))
								{
									arrayList.Add(dataConverter.GetUInt64(buffer, num));
									num += 8;
								}
								goto IL_683;
							}
							break;
						case '$':
							break;
						case '%':
							dataConverter = DataConverter.Native;
							goto IL_683;
						}
						num3++;
						if (num3 >= description.Length)
						{
							throw new ArgumentException("$ description needs a type specified", "description");
						}
						char c2 = description[num3];
						if (flag)
						{
							num = DataConverter.Align(num, 4);
							flag = false;
						}
						if (num < buffer.Length)
						{
							char c3 = c2;
							int num6;
							Encoding encoding;
							switch (c3)
							{
							case '3':
								encoding = Encoding.GetEncoding(12000);
								num6 = 4;
								break;
							case '4':
								encoding = Encoding.GetEncoding(12001);
								num6 = 4;
								break;
							default:
								if (c3 != 'b')
								{
									throw new ArgumentException("Invalid format for $ specifier", "description");
								}
								encoding = Encoding.BigEndianUnicode;
								num6 = 2;
								break;
							case '6':
								encoding = Encoding.Unicode;
								num6 = 2;
								break;
							case '7':
								encoding = Encoding.UTF7;
								num6 = 1;
								break;
							case '8':
								encoding = Encoding.UTF8;
								num6 = 1;
								break;
							}
							int j = num;
							switch (num6)
							{
							case 1:
								while (j < buffer.Length && buffer[j] != 0)
								{
									j++;
								}
								arrayList.Add(encoding.GetChars(buffer, num, j - num));
								if (j == buffer.Length)
								{
									num = j;
								}
								else
								{
									num = j + 1;
								}
								break;
							case 2:
								while (j < buffer.Length)
								{
									if (j + 1 == buffer.Length)
									{
										j++;
										break;
									}
									if (buffer[j] == 0 && buffer[j + 1] == 0)
									{
										break;
									}
									j++;
								}
								arrayList.Add(encoding.GetChars(buffer, num, j - num));
								if (j == buffer.Length)
								{
									num = j;
								}
								else
								{
									num = j + 2;
								}
								break;
							case 4:
								while (j < buffer.Length)
								{
									if (j + 3 >= buffer.Length)
									{
										j = buffer.Length;
										break;
									}
									if (buffer[j] == 0 && buffer[j + 1] == 0 && buffer[j + 2] == 0 && buffer[j + 3] == 0)
									{
										break;
									}
									j++;
								}
								arrayList.Add(encoding.GetChars(buffer, num, j - num));
								if (j == buffer.Length)
								{
									num = j;
								}
								else
								{
									num = j + 4;
								}
								break;
							}
						}
						break;
					}
					case '^':
						dataConverter = DataConverter.BigEndian;
						break;
					case '_':
						dataConverter = DataConverter.LittleEndian;
						break;
					case 'b':
						if (DataConverter.Prepare(buffer, ref num, 1, ref flag))
						{
							arrayList.Add(buffer[num]);
							num++;
						}
						break;
					case 'c':
						goto IL_300;
					case 'd':
						if (DataConverter.Prepare(buffer, ref num, 8, ref flag))
						{
							arrayList.Add(dataConverter.GetDouble(buffer, num));
							num += 8;
						}
						break;
					case 'f':
						if (DataConverter.Prepare(buffer, ref num, 4, ref flag))
						{
							arrayList.Add(dataConverter.GetDouble(buffer, num));
							num += 4;
						}
						break;
					case 'i':
						if (DataConverter.Prepare(buffer, ref num, 4, ref flag))
						{
							arrayList.Add(dataConverter.GetInt32(buffer, num));
							num += 4;
						}
						break;
					case 'l':
						if (DataConverter.Prepare(buffer, ref num, 8, ref flag))
						{
							arrayList.Add(dataConverter.GetInt64(buffer, num));
							num += 8;
						}
						break;
					}
					break;
				case 'C':
					goto IL_300;
				}
				IL_683:
				if (num2 > 0)
				{
					if (--num2 > 0)
					{
						num3 = num4;
					}
					continue;
				}
				num3++;
				continue;
				IL_300:
				if (DataConverter.Prepare(buffer, ref num, 1, ref flag))
				{
					char c4;
					if (description[num3] == 'c')
					{
						c4 = (char)((sbyte)buffer[num]);
					}
					else
					{
						c4 = (char)buffer[num];
					}
					arrayList.Add(c4);
					num++;
				}
				goto IL_683;
			}
			return arrayList;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001EFD8 File Offset: 0x0001D1D8
		internal void Check(byte[] dest, int destIdx, int size)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (destIdx < 0 || destIdx > dest.Length - size)
			{
				throw new ArgumentException("destIdx");
			}
		}

		// Token: 0x040001A7 RID: 423
		private static DataConverter SwapConv = new DataConverter.SwapConverter();

		// Token: 0x040001A8 RID: 424
		private static DataConverter CopyConv = new DataConverter.CopyConverter();

		// Token: 0x040001A9 RID: 425
		public static readonly bool IsLittleEndian = BitConverter.IsLittleEndian;

		// Token: 0x0200008D RID: 141
		private class PackContext
		{
			// Token: 0x06000852 RID: 2130 RVA: 0x0001F01C File Offset: 0x0001D21C
			public void Add(byte[] group)
			{
				if (this.buffer == null)
				{
					this.buffer = group;
					this.next = group.Length;
					return;
				}
				if (this.align != 0)
				{
					if (this.align == -1)
					{
						this.next = DataConverter.Align(this.next, group.Length);
					}
					else
					{
						this.next = DataConverter.Align(this.next, this.align);
					}
					this.align = 0;
				}
				if (this.next + group.Length > this.buffer.Length)
				{
					byte[] destinationArray = new byte[Math.Max(this.next, 16) * 2 + group.Length];
					Array.Copy(this.buffer, destinationArray, this.buffer.Length);
					Array.Copy(group, 0, destinationArray, this.next, group.Length);
					this.next += group.Length;
					this.buffer = destinationArray;
				}
				else
				{
					Array.Copy(group, 0, this.buffer, this.next, group.Length);
					this.next += group.Length;
				}
			}

			// Token: 0x06000853 RID: 2131 RVA: 0x0001F12C File Offset: 0x0001D32C
			public byte[] Get()
			{
				if (this.buffer == null)
				{
					return new byte[0];
				}
				if (this.buffer.Length != this.next)
				{
					byte[] array = new byte[this.next];
					Array.Copy(this.buffer, array, this.next);
					return array;
				}
				return this.buffer;
			}

			// Token: 0x040001AA RID: 426
			public byte[] buffer;

			// Token: 0x040001AB RID: 427
			private int next;

			// Token: 0x040001AC RID: 428
			public string description;

			// Token: 0x040001AD RID: 429
			public int i;

			// Token: 0x040001AE RID: 430
			public DataConverter conv;

			// Token: 0x040001AF RID: 431
			public int repeat;

			// Token: 0x040001B0 RID: 432
			public int align;
		}

		// Token: 0x0200008E RID: 142
		private class CopyConverter : DataConverter
		{
			// Token: 0x06000855 RID: 2133 RVA: 0x0001F18C File Offset: 0x0001D38C
			public unsafe override double GetDouble(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 8)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				double result;
				for (int i = 0; i < 8; i++)
				{
					*(ref result + i) = data[index + i];
				}
				return result;
			}

			// Token: 0x06000856 RID: 2134 RVA: 0x0001F1F4 File Offset: 0x0001D3F4
			public unsafe override ulong GetUInt64(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 8)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				ulong result;
				for (int i = 0; i < 8; i++)
				{
					*(ref result + i) = data[index + i];
				}
				return result;
			}

			// Token: 0x06000857 RID: 2135 RVA: 0x0001F25C File Offset: 0x0001D45C
			public unsafe override long GetInt64(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 8)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				long result;
				for (int i = 0; i < 8; i++)
				{
					*(ref result + i) = data[index + i];
				}
				return result;
			}

			// Token: 0x06000858 RID: 2136 RVA: 0x0001F2C4 File Offset: 0x0001D4C4
			public unsafe override float GetFloat(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 4)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				float result;
				for (int i = 0; i < 4; i++)
				{
					*(ref result + i) = data[index + i];
				}
				return result;
			}

			// Token: 0x06000859 RID: 2137 RVA: 0x0001F32C File Offset: 0x0001D52C
			public unsafe override int GetInt32(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 4)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				int result;
				for (int i = 0; i < 4; i++)
				{
					*(ref result + i) = data[index + i];
				}
				return result;
			}

			// Token: 0x0600085A RID: 2138 RVA: 0x0001F394 File Offset: 0x0001D594
			public unsafe override uint GetUInt32(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 4)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				uint result;
				for (int i = 0; i < 4; i++)
				{
					*(ref result + i) = data[index + i];
				}
				return result;
			}

			// Token: 0x0600085B RID: 2139 RVA: 0x0001F3FC File Offset: 0x0001D5FC
			public unsafe override short GetInt16(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 2)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				short result;
				for (int i = 0; i < 2; i++)
				{
					*(ref result + i) = data[index + i];
				}
				return result;
			}

			// Token: 0x0600085C RID: 2140 RVA: 0x0001F464 File Offset: 0x0001D664
			public unsafe override ushort GetUInt16(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 2)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				ushort result;
				for (int i = 0; i < 2; i++)
				{
					*(ref result + i) = data[index + i];
				}
				return result;
			}

			// Token: 0x0600085D RID: 2141 RVA: 0x0001F4CC File Offset: 0x0001D6CC
			public unsafe override void PutBytes(byte[] dest, int destIdx, double value)
			{
				base.Check(dest, destIdx, 8);
				fixed (byte* ptr = &dest[destIdx])
				{
					*(long*)ptr = (long)value;
				}
			}

			// Token: 0x0600085E RID: 2142 RVA: 0x0001F4F4 File Offset: 0x0001D6F4
			public unsafe override void PutBytes(byte[] dest, int destIdx, float value)
			{
				base.Check(dest, destIdx, 4);
				fixed (byte* ptr = &dest[destIdx])
				{
					*(int*)ptr = (int)value;
				}
			}

			// Token: 0x0600085F RID: 2143 RVA: 0x0001F51C File Offset: 0x0001D71C
			public unsafe override void PutBytes(byte[] dest, int destIdx, int value)
			{
				base.Check(dest, destIdx, 4);
				fixed (byte* ptr = &dest[destIdx])
				{
					*(int*)ptr = value;
				}
			}

			// Token: 0x06000860 RID: 2144 RVA: 0x0001F544 File Offset: 0x0001D744
			public unsafe override void PutBytes(byte[] dest, int destIdx, uint value)
			{
				base.Check(dest, destIdx, 4);
				fixed (byte* ptr = &dest[destIdx])
				{
					*(int*)ptr = (int)value;
				}
			}

			// Token: 0x06000861 RID: 2145 RVA: 0x0001F56C File Offset: 0x0001D76C
			public unsafe override void PutBytes(byte[] dest, int destIdx, long value)
			{
				base.Check(dest, destIdx, 8);
				fixed (byte* ptr = &dest[destIdx])
				{
					*(long*)ptr = value;
				}
			}

			// Token: 0x06000862 RID: 2146 RVA: 0x0001F594 File Offset: 0x0001D794
			public unsafe override void PutBytes(byte[] dest, int destIdx, ulong value)
			{
				base.Check(dest, destIdx, 8);
				fixed (byte* ptr = &dest[destIdx])
				{
					*(long*)ptr = (long)value;
				}
			}

			// Token: 0x06000863 RID: 2147 RVA: 0x0001F5BC File Offset: 0x0001D7BC
			public unsafe override void PutBytes(byte[] dest, int destIdx, short value)
			{
				base.Check(dest, destIdx, 2);
				fixed (byte* ptr = &dest[destIdx])
				{
					*(short*)ptr = value;
				}
			}

			// Token: 0x06000864 RID: 2148 RVA: 0x0001F5E4 File Offset: 0x0001D7E4
			public unsafe override void PutBytes(byte[] dest, int destIdx, ushort value)
			{
				base.Check(dest, destIdx, 2);
				fixed (byte* ptr = &dest[destIdx])
				{
					*(short*)ptr = (short)value;
				}
			}
		}

		// Token: 0x0200008F RID: 143
		private class SwapConverter : DataConverter
		{
			// Token: 0x06000866 RID: 2150 RVA: 0x0001F614 File Offset: 0x0001D814
			public unsafe override double GetDouble(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 8)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				double result;
				for (int i = 0; i < 8; i++)
				{
					*(ref result + (7 - i)) = data[index + i];
				}
				return result;
			}

			// Token: 0x06000867 RID: 2151 RVA: 0x0001F67C File Offset: 0x0001D87C
			public unsafe override ulong GetUInt64(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 8)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				ulong result;
				for (int i = 0; i < 8; i++)
				{
					*(ref result + (7 - i)) = data[index + i];
				}
				return result;
			}

			// Token: 0x06000868 RID: 2152 RVA: 0x0001F6E4 File Offset: 0x0001D8E4
			public unsafe override long GetInt64(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 8)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				long result;
				for (int i = 0; i < 8; i++)
				{
					*(ref result + (7 - i)) = data[index + i];
				}
				return result;
			}

			// Token: 0x06000869 RID: 2153 RVA: 0x0001F74C File Offset: 0x0001D94C
			public unsafe override float GetFloat(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 4)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				float result;
				for (int i = 0; i < 4; i++)
				{
					*(ref result + (3 - i)) = data[index + i];
				}
				return result;
			}

			// Token: 0x0600086A RID: 2154 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
			public unsafe override int GetInt32(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 4)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				int result;
				for (int i = 0; i < 4; i++)
				{
					*(ref result + (3 - i)) = data[index + i];
				}
				return result;
			}

			// Token: 0x0600086B RID: 2155 RVA: 0x0001F81C File Offset: 0x0001DA1C
			public unsafe override uint GetUInt32(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 4)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				uint result;
				for (int i = 0; i < 4; i++)
				{
					*(ref result + (3 - i)) = data[index + i];
				}
				return result;
			}

			// Token: 0x0600086C RID: 2156 RVA: 0x0001F884 File Offset: 0x0001DA84
			public unsafe override short GetInt16(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 2)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				short result;
				for (int i = 0; i < 2; i++)
				{
					*(ref result + (1 - i)) = data[index + i];
				}
				return result;
			}

			// Token: 0x0600086D RID: 2157 RVA: 0x0001F8EC File Offset: 0x0001DAEC
			public unsafe override ushort GetUInt16(byte[] data, int index)
			{
				if (data == null)
				{
					throw new ArgumentNullException("data");
				}
				if (data.Length - index < 2)
				{
					throw new ArgumentException("index");
				}
				if (index < 0)
				{
					throw new ArgumentException("index");
				}
				ushort result;
				for (int i = 0; i < 2; i++)
				{
					*(ref result + (1 - i)) = data[index + i];
				}
				return result;
			}

			// Token: 0x0600086E RID: 2158 RVA: 0x0001F954 File Offset: 0x0001DB54
			public unsafe override void PutBytes(byte[] dest, int destIdx, double value)
			{
				base.Check(dest, destIdx, 8);
				fixed (byte* ptr = &dest[destIdx])
				{
					for (int i = 0; i < 8; i++)
					{
						ptr[i] = *(ref value + (7 - i));
					}
				}
			}

			// Token: 0x0600086F RID: 2159 RVA: 0x0001F994 File Offset: 0x0001DB94
			public unsafe override void PutBytes(byte[] dest, int destIdx, float value)
			{
				base.Check(dest, destIdx, 4);
				fixed (byte* ptr = &dest[destIdx])
				{
					for (int i = 0; i < 4; i++)
					{
						ptr[i] = *(ref value + (3 - i));
					}
				}
			}

			// Token: 0x06000870 RID: 2160 RVA: 0x0001F9D4 File Offset: 0x0001DBD4
			public unsafe override void PutBytes(byte[] dest, int destIdx, int value)
			{
				base.Check(dest, destIdx, 4);
				fixed (byte* ptr = &dest[destIdx])
				{
					for (int i = 0; i < 4; i++)
					{
						ptr[i] = *(ref value + (3 - i));
					}
				}
			}

			// Token: 0x06000871 RID: 2161 RVA: 0x0001FA14 File Offset: 0x0001DC14
			public unsafe override void PutBytes(byte[] dest, int destIdx, uint value)
			{
				base.Check(dest, destIdx, 4);
				fixed (byte* ptr = &dest[destIdx])
				{
					for (int i = 0; i < 4; i++)
					{
						ptr[i] = *(ref value + (3 - i));
					}
				}
			}

			// Token: 0x06000872 RID: 2162 RVA: 0x0001FA54 File Offset: 0x0001DC54
			public unsafe override void PutBytes(byte[] dest, int destIdx, long value)
			{
				base.Check(dest, destIdx, 8);
				fixed (byte* ptr = &dest[destIdx])
				{
					for (int i = 0; i < 8; i++)
					{
						ptr[i] = *(ref value + (7 - i));
					}
				}
			}

			// Token: 0x06000873 RID: 2163 RVA: 0x0001FA94 File Offset: 0x0001DC94
			public unsafe override void PutBytes(byte[] dest, int destIdx, ulong value)
			{
				base.Check(dest, destIdx, 8);
				fixed (byte* ptr = &dest[destIdx])
				{
					for (int i = 0; i < 4; i++)
					{
						ptr[i] = *(ref value + (7 - i));
					}
				}
			}

			// Token: 0x06000874 RID: 2164 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
			public unsafe override void PutBytes(byte[] dest, int destIdx, short value)
			{
				base.Check(dest, destIdx, 2);
				fixed (byte* ptr = &dest[destIdx])
				{
					for (int i = 0; i < 2; i++)
					{
						ptr[i] = *(ref value + (1 - i));
					}
				}
			}

			// Token: 0x06000875 RID: 2165 RVA: 0x0001FB14 File Offset: 0x0001DD14
			public unsafe override void PutBytes(byte[] dest, int destIdx, ushort value)
			{
				base.Check(dest, destIdx, 2);
				fixed (byte* ptr = &dest[destIdx])
				{
					for (int i = 0; i < 2; i++)
					{
						ptr[i] = *(ref value + (1 - i));
					}
				}
			}
		}
	}
}
