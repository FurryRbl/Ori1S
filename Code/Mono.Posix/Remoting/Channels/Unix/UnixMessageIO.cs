using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Text;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000083 RID: 131
	internal class UnixMessageIO
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x0000E8AC File Offset: 0x0000CAAC
		public static MessageStatus ReceiveMessageStatus(Stream networkStream, byte[] buffer)
		{
			try
			{
				UnixMessageIO.StreamRead(networkStream, buffer, 6);
			}
			catch (Exception innerException)
			{
				throw new RemotingException("Unix transport error.", innerException);
			}
			MessageStatus result;
			try
			{
				bool[] array = new bool[UnixMessageIO._msgHeaders.Length];
				bool flag = true;
				int num = 0;
				while (flag)
				{
					flag = false;
					byte b = buffer[num];
					for (int i = 0; i < UnixMessageIO._msgHeaders.Length; i++)
					{
						if (num <= 0 || array[i])
						{
							array[i] = (b == UnixMessageIO._msgHeaders[i][num]);
							if (array[i] && num == UnixMessageIO._msgHeaders[i].Length - 1)
							{
								return (MessageStatus)i;
							}
							flag = (flag || array[i]);
						}
					}
					num++;
				}
				result = MessageStatus.Unknown;
			}
			catch (Exception innerException2)
			{
				throw new RemotingException("Unix transport error.", innerException2);
			}
			return result;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		private static bool StreamRead(Stream networkStream, byte[] buffer, int count)
		{
			int num = 0;
			for (;;)
			{
				int num2 = networkStream.Read(buffer, num, count - num);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
				if (num >= count)
				{
					return true;
				}
			}
			throw new RemotingException("Connection closed");
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000EA08 File Offset: 0x0000CC08
		public static void SendMessageStream(Stream networkStream, Stream data, ITransportHeaders requestHeaders, byte[] buffer)
		{
			if (buffer == null)
			{
				buffer = new byte[UnixMessageIO.DefaultStreamBufferSize];
			}
			byte[] array = UnixMessageIO._msgHeaders[0];
			networkStream.Write(array, 0, array.Length);
			if (requestHeaders["__RequestUri"] != null)
			{
				buffer[0] = 0;
			}
			else
			{
				buffer[0] = 2;
			}
			buffer[1] = 0;
			buffer[2] = 0;
			buffer[3] = 0;
			int num = (int)data.Length;
			buffer[4] = (byte)num;
			buffer[5] = (byte)(num >> 8);
			buffer[6] = (byte)(num >> 16);
			buffer[7] = (byte)(num >> 24);
			networkStream.Write(buffer, 0, 8);
			UnixMessageIO.SendHeaders(networkStream, requestHeaders, buffer);
			if (data is MemoryStream)
			{
				MemoryStream memoryStream = (MemoryStream)data;
				networkStream.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			else
			{
				for (int i = data.Read(buffer, 0, buffer.Length); i > 0; i = data.Read(buffer, 0, buffer.Length))
				{
					networkStream.Write(buffer, 0, i);
				}
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		private static void SendHeaders(Stream networkStream, ITransportHeaders requestHeaders, byte[] buffer)
		{
			if (networkStream != null)
			{
				foreach (object obj in requestHeaders)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = dictionaryEntry.Key.ToString();
					if (text == null)
					{
						goto IL_AA;
					}
					if (UnixMessageIO.<>f__switch$map1 == null)
					{
						UnixMessageIO.<>f__switch$map1 = new Dictionary<string, int>(2)
						{
							{
								"__RequestUri",
								0
							},
							{
								"Content-Type",
								1
							}
						};
					}
					int num;
					if (!UnixMessageIO.<>f__switch$map1.TryGetValue(text, out num))
					{
						goto IL_AA;
					}
					if (num != 0)
					{
						if (num != 1)
						{
							goto IL_AA;
						}
						networkStream.Write(UnixMessageIO.msgContentTypeTransportKey, 0, 4);
					}
					else
					{
						networkStream.Write(UnixMessageIO.msgUriTransportKey, 0, 4);
					}
					IL_D6:
					UnixMessageIO.SendString(networkStream, dictionaryEntry.Value.ToString(), buffer);
					continue;
					IL_AA:
					networkStream.Write(UnixMessageIO.msgDefaultTransportKey, 0, 3);
					UnixMessageIO.SendString(networkStream, dictionaryEntry.Key.ToString(), buffer);
					networkStream.WriteByte(1);
					goto IL_D6;
				}
			}
			networkStream.Write(UnixMessageIO.msgHeaderTerminator, 0, 2);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000EC00 File Offset: 0x0000CE00
		public static ITransportHeaders ReceiveHeaders(Stream networkStream, byte[] buffer)
		{
			UnixMessageIO.StreamRead(networkStream, buffer, 2);
			byte b = buffer[0];
			TransportHeaders transportHeaders = new TransportHeaders();
			while (b != 0)
			{
				UnixMessageIO.StreamRead(networkStream, buffer, 1);
				string key;
				switch (b)
				{
				case 1:
					key = UnixMessageIO.ReceiveString(networkStream, buffer);
					break;
				case 2:
				case 3:
				case 5:
					goto IL_6B;
				case 4:
					key = "__RequestUri";
					break;
				case 6:
					key = "Content-Type";
					break;
				default:
					goto IL_6B;
				}
				UnixMessageIO.StreamRead(networkStream, buffer, 1);
				transportHeaders[key] = UnixMessageIO.ReceiveString(networkStream, buffer);
				UnixMessageIO.StreamRead(networkStream, buffer, 2);
				b = buffer[0];
				continue;
				IL_6B:
				throw new NotSupportedException("Unknown header code: " + b);
			}
			return transportHeaders;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000ECBC File Offset: 0x0000CEBC
		public static Stream ReceiveMessageStream(Stream networkStream, out ITransportHeaders headers, byte[] buffer)
		{
			headers = null;
			if (buffer == null)
			{
				buffer = new byte[UnixMessageIO.DefaultStreamBufferSize];
			}
			UnixMessageIO.StreamRead(networkStream, buffer, 8);
			int num = (int)buffer[4] | (int)buffer[5] << 8 | (int)buffer[6] << 16 | (int)buffer[7] << 24;
			headers = UnixMessageIO.ReceiveHeaders(networkStream, buffer);
			byte[] buffer2 = new byte[num];
			UnixMessageIO.StreamRead(networkStream, buffer2, num);
			return new MemoryStream(buffer2);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000ED20 File Offset: 0x0000CF20
		private static void SendString(Stream networkStream, string str, byte[] buffer)
		{
			int num = Encoding.UTF8.GetMaxByteCount(str.Length) + 4;
			if (num > buffer.Length)
			{
				buffer = new byte[num];
			}
			int bytes = Encoding.UTF8.GetBytes(str, 0, str.Length, buffer, 4);
			buffer[0] = (byte)bytes;
			buffer[1] = (byte)(bytes >> 8);
			buffer[2] = (byte)(bytes >> 16);
			buffer[3] = (byte)(bytes >> 24);
			networkStream.Write(buffer, 0, bytes + 4);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000ED90 File Offset: 0x0000CF90
		private static string ReceiveString(Stream networkStream, byte[] buffer)
		{
			UnixMessageIO.StreamRead(networkStream, buffer, 4);
			int num = (int)buffer[0] | (int)buffer[1] << 8 | (int)buffer[2] << 16 | (int)buffer[3] << 24;
			if (num == 0)
			{
				return string.Empty;
			}
			if (num > buffer.Length)
			{
				buffer = new byte[num];
			}
			UnixMessageIO.StreamRead(networkStream, buffer, num);
			char[] chars = Encoding.UTF8.GetChars(buffer, 0, num);
			return new string(chars);
		}

		// Token: 0x0400043B RID: 1083
		private static byte[][] _msgHeaders = new byte[][]
		{
			new byte[]
			{
				46,
				78,
				69,
				84,
				1,
				0
			},
			new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue
			}
		};

		// Token: 0x0400043C RID: 1084
		public static int DefaultStreamBufferSize = 1000;

		// Token: 0x0400043D RID: 1085
		private static byte[] msgUriTransportKey = new byte[]
		{
			4,
			0,
			1,
			1
		};

		// Token: 0x0400043E RID: 1086
		private static byte[] msgContentTypeTransportKey = new byte[]
		{
			6,
			0,
			1,
			1
		};

		// Token: 0x0400043F RID: 1087
		private static byte[] msgDefaultTransportKey = new byte[]
		{
			1,
			0,
			1
		};

		// Token: 0x04000440 RID: 1088
		private static byte[] msgHeaderTerminator = new byte[2];
	}
}
