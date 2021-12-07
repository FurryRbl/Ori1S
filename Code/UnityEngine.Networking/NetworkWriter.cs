using System;
using System.Text;

namespace UnityEngine.Networking
{
	// Token: 0x0200005C RID: 92
	public class NetworkWriter
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x00018F9C File Offset: 0x0001719C
		public NetworkWriter()
		{
			this.m_Buffer = new NetBuffer();
			if (NetworkWriter.s_Encoding == null)
			{
				NetworkWriter.s_Encoding = new UTF8Encoding();
				NetworkWriter.s_StringWriteBuffer = new byte[32768];
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00018FE0 File Offset: 0x000171E0
		public NetworkWriter(byte[] buffer)
		{
			this.m_Buffer = new NetBuffer(buffer);
			if (NetworkWriter.s_Encoding == null)
			{
				NetworkWriter.s_Encoding = new UTF8Encoding();
				NetworkWriter.s_StringWriteBuffer = new byte[32768];
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00019018 File Offset: 0x00017218
		public short Position
		{
			get
			{
				return (short)this.m_Buffer.Position;
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00019028 File Offset: 0x00017228
		public byte[] ToArray()
		{
			byte[] array = new byte[this.m_Buffer.AsArraySegment().Count];
			Array.Copy(this.m_Buffer.AsArraySegment().Array, array, this.m_Buffer.AsArraySegment().Count);
			return array;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001907C File Offset: 0x0001727C
		public byte[] AsArray()
		{
			return this.AsArraySegment().Array;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00019098 File Offset: 0x00017298
		internal ArraySegment<byte> AsArraySegment()
		{
			return this.m_Buffer.AsArraySegment();
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000190A8 File Offset: 0x000172A8
		public void WritePackedUInt32(uint value)
		{
			if (value <= 240U)
			{
				this.Write((byte)value);
				return;
			}
			if (value <= 2287U)
			{
				this.Write((byte)((value - 240U) / 256U + 241U));
				this.Write((byte)((value - 240U) % 256U));
				return;
			}
			if (value <= 67823U)
			{
				this.Write(249);
				this.Write((byte)((value - 2288U) / 256U));
				this.Write((byte)((value - 2288U) % 256U));
				return;
			}
			if (value <= 16777215U)
			{
				this.Write(250);
				this.Write((byte)(value & 255U));
				this.Write((byte)(value >> 8 & 255U));
				this.Write((byte)(value >> 16 & 255U));
				return;
			}
			this.Write(251);
			this.Write((byte)(value & 255U));
			this.Write((byte)(value >> 8 & 255U));
			this.Write((byte)(value >> 16 & 255U));
			this.Write((byte)(value >> 24 & 255U));
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000191D4 File Offset: 0x000173D4
		public void WritePackedUInt64(ulong value)
		{
			if (value <= 240UL)
			{
				this.Write((byte)value);
				return;
			}
			if (value <= 2287UL)
			{
				this.Write((byte)((value - 240UL) / 256UL + 241UL));
				this.Write((byte)((value - 240UL) % 256UL));
				return;
			}
			if (value <= 67823UL)
			{
				this.Write(249);
				this.Write((byte)((value - 2288UL) / 256UL));
				this.Write((byte)((value - 2288UL) % 256UL));
				return;
			}
			if (value <= 16777215UL)
			{
				this.Write(250);
				this.Write((byte)(value & 255UL));
				this.Write((byte)(value >> 8 & 255UL));
				this.Write((byte)(value >> 16 & 255UL));
				return;
			}
			if (value <= (ulong)-1)
			{
				this.Write(251);
				this.Write((byte)(value & 255UL));
				this.Write((byte)(value >> 8 & 255UL));
				this.Write((byte)(value >> 16 & 255UL));
				this.Write((byte)(value >> 24 & 255UL));
				return;
			}
			if (value <= 1099511627775UL)
			{
				this.Write(252);
				this.Write((byte)(value & 255UL));
				this.Write((byte)(value >> 8 & 255UL));
				this.Write((byte)(value >> 16 & 255UL));
				this.Write((byte)(value >> 24 & 255UL));
				this.Write((byte)(value >> 32 & 255UL));
				return;
			}
			if (value <= 281474976710655UL)
			{
				this.Write(253);
				this.Write((byte)(value & 255UL));
				this.Write((byte)(value >> 8 & 255UL));
				this.Write((byte)(value >> 16 & 255UL));
				this.Write((byte)(value >> 24 & 255UL));
				this.Write((byte)(value >> 32 & 255UL));
				this.Write((byte)(value >> 40 & 255UL));
				return;
			}
			if (value <= 72057594037927935UL)
			{
				this.Write(254);
				this.Write((byte)(value & 255UL));
				this.Write((byte)(value >> 8 & 255UL));
				this.Write((byte)(value >> 16 & 255UL));
				this.Write((byte)(value >> 24 & 255UL));
				this.Write((byte)(value >> 32 & 255UL));
				this.Write((byte)(value >> 40 & 255UL));
				this.Write((byte)(value >> 48 & 255UL));
				return;
			}
			this.Write(byte.MaxValue);
			this.Write((byte)(value & 255UL));
			this.Write((byte)(value >> 8 & 255UL));
			this.Write((byte)(value >> 16 & 255UL));
			this.Write((byte)(value >> 24 & 255UL));
			this.Write((byte)(value >> 32 & 255UL));
			this.Write((byte)(value >> 40 & 255UL));
			this.Write((byte)(value >> 48 & 255UL));
			this.Write((byte)(value >> 56 & 255UL));
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001953C File Offset: 0x0001773C
		public void Write(NetworkInstanceId value)
		{
			this.WritePackedUInt32(value.Value);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001954C File Offset: 0x0001774C
		public void Write(NetworkSceneId value)
		{
			this.WritePackedUInt32(value.Value);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001955C File Offset: 0x0001775C
		public void Write(char value)
		{
			this.m_Buffer.WriteByte((byte)value);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001956C File Offset: 0x0001776C
		public void Write(byte value)
		{
			this.m_Buffer.WriteByte(value);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001957C File Offset: 0x0001777C
		public void Write(sbyte value)
		{
			this.m_Buffer.WriteByte((byte)value);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001958C File Offset: 0x0001778C
		public void Write(short value)
		{
			this.m_Buffer.WriteByte2((byte)(value & 255), (byte)(value >> 8 & 255));
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000195AC File Offset: 0x000177AC
		public void Write(ushort value)
		{
			this.m_Buffer.WriteByte2((byte)(value & 255), (byte)(value >> 8 & 255));
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000195CC File Offset: 0x000177CC
		public void Write(int value)
		{
			this.m_Buffer.WriteByte4((byte)(value & 255), (byte)(value >> 8 & 255), (byte)(value >> 16 & 255), (byte)(value >> 24 & 255));
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00019604 File Offset: 0x00017804
		public void Write(uint value)
		{
			this.m_Buffer.WriteByte4((byte)(value & 255U), (byte)(value >> 8 & 255U), (byte)(value >> 16 & 255U), (byte)(value >> 24 & 255U));
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001963C File Offset: 0x0001783C
		public void Write(long value)
		{
			this.m_Buffer.WriteByte8((byte)(value & 255L), (byte)(value >> 8 & 255L), (byte)(value >> 16 & 255L), (byte)(value >> 24 & 255L), (byte)(value >> 32 & 255L), (byte)(value >> 40 & 255L), (byte)(value >> 48 & 255L), (byte)(value >> 56 & 255L));
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000196B0 File Offset: 0x000178B0
		public void Write(ulong value)
		{
			this.m_Buffer.WriteByte8((byte)(value & 255UL), (byte)(value >> 8 & 255UL), (byte)(value >> 16 & 255UL), (byte)(value >> 24 & 255UL), (byte)(value >> 32 & 255UL), (byte)(value >> 40 & 255UL), (byte)(value >> 48 & 255UL), (byte)(value >> 56 & 255UL));
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00019724 File Offset: 0x00017924
		public void Write(float value)
		{
			NetworkWriter.s_FloatConverter.floatValue = value;
			this.Write(NetworkWriter.s_FloatConverter.intValue);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00019744 File Offset: 0x00017944
		public void Write(double value)
		{
			NetworkWriter.s_FloatConverter.doubleValue = value;
			this.Write(NetworkWriter.s_FloatConverter.longValue);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00019764 File Offset: 0x00017964
		public void Write(string value)
		{
			if (value == null)
			{
				this.m_Buffer.WriteByte2(0, 0);
				return;
			}
			int byteCount = NetworkWriter.s_Encoding.GetByteCount(value);
			if (byteCount >= 32768)
			{
				throw new IndexOutOfRangeException("Serialize(string) too long: " + value.Length);
			}
			this.Write((ushort)byteCount);
			int bytes = NetworkWriter.s_Encoding.GetBytes(value, 0, value.Length, NetworkWriter.s_StringWriteBuffer, 0);
			this.m_Buffer.WriteBytes(NetworkWriter.s_StringWriteBuffer, (ushort)bytes);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000197EC File Offset: 0x000179EC
		public void Write(bool value)
		{
			if (value)
			{
				this.m_Buffer.WriteByte(1);
			}
			else
			{
				this.m_Buffer.WriteByte(0);
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00019814 File Offset: 0x00017A14
		public void Write(byte[] buffer, int count)
		{
			if (count > 65535)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkWriter Write: buffer is too large (" + count + ") bytes. The maximum buffer size is 64K bytes.");
				}
				return;
			}
			this.m_Buffer.WriteBytes(buffer, (ushort)count);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00019860 File Offset: 0x00017A60
		public void Write(byte[] buffer, int offset, int count)
		{
			if (count > 65535)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkWriter Write: buffer is too large (" + count + ") bytes. The maximum buffer size is 64K bytes.");
				}
				return;
			}
			this.m_Buffer.WriteBytesAtOffset(buffer, (ushort)offset, (ushort)count);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000198B0 File Offset: 0x00017AB0
		public void WriteBytesAndSize(byte[] buffer, int count)
		{
			if (buffer == null || count == 0)
			{
				this.Write(0);
				return;
			}
			if (count > 65535)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkWriter WriteBytesAndSize: buffer is too large (" + count + ") bytes. The maximum buffer size is 64K bytes.");
				}
				return;
			}
			this.Write((ushort)count);
			this.m_Buffer.WriteBytes(buffer, (ushort)count);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00019918 File Offset: 0x00017B18
		public void WriteBytesFull(byte[] buffer)
		{
			if (buffer == null)
			{
				this.Write(0);
				return;
			}
			if (buffer.Length > 65535)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkWriter WriteBytes: buffer is too large (" + buffer.Length + ") bytes. The maximum buffer size is 64K bytes.");
				}
				return;
			}
			this.Write((ushort)buffer.Length);
			this.m_Buffer.WriteBytes(buffer, (ushort)buffer.Length);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00019984 File Offset: 0x00017B84
		public void Write(Vector2 value)
		{
			this.Write(value.x);
			this.Write(value.y);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000199A0 File Offset: 0x00017BA0
		public void Write(Vector3 value)
		{
			this.Write(value.x);
			this.Write(value.y);
			this.Write(value.z);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000199CC File Offset: 0x00017BCC
		public void Write(Vector4 value)
		{
			this.Write(value.x);
			this.Write(value.y);
			this.Write(value.z);
			this.Write(value.w);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00019A10 File Offset: 0x00017C10
		public void Write(Color value)
		{
			this.Write(value.r);
			this.Write(value.g);
			this.Write(value.b);
			this.Write(value.a);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00019A54 File Offset: 0x00017C54
		public void Write(Color32 value)
		{
			this.Write(value.r);
			this.Write(value.g);
			this.Write(value.b);
			this.Write(value.a);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00019A98 File Offset: 0x00017C98
		public void Write(Quaternion value)
		{
			this.Write(value.x);
			this.Write(value.y);
			this.Write(value.z);
			this.Write(value.w);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00019ADC File Offset: 0x00017CDC
		public void Write(Rect value)
		{
			this.Write(value.xMin);
			this.Write(value.yMin);
			this.Write(value.width);
			this.Write(value.height);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00019B20 File Offset: 0x00017D20
		public void Write(Plane value)
		{
			this.Write(value.normal);
			this.Write(value.distance);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00019B48 File Offset: 0x00017D48
		public void Write(Ray value)
		{
			this.Write(value.direction);
			this.Write(value.origin);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00019B70 File Offset: 0x00017D70
		public void Write(Matrix4x4 value)
		{
			this.Write(value.m00);
			this.Write(value.m01);
			this.Write(value.m02);
			this.Write(value.m03);
			this.Write(value.m10);
			this.Write(value.m11);
			this.Write(value.m12);
			this.Write(value.m13);
			this.Write(value.m20);
			this.Write(value.m21);
			this.Write(value.m22);
			this.Write(value.m23);
			this.Write(value.m30);
			this.Write(value.m31);
			this.Write(value.m32);
			this.Write(value.m33);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00019C50 File Offset: 0x00017E50
		public void Write(NetworkHash128 value)
		{
			this.Write(value.i0);
			this.Write(value.i1);
			this.Write(value.i2);
			this.Write(value.i3);
			this.Write(value.i4);
			this.Write(value.i5);
			this.Write(value.i6);
			this.Write(value.i7);
			this.Write(value.i8);
			this.Write(value.i9);
			this.Write(value.i10);
			this.Write(value.i11);
			this.Write(value.i12);
			this.Write(value.i13);
			this.Write(value.i14);
			this.Write(value.i15);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00019D30 File Offset: 0x00017F30
		public void Write(NetworkIdentity value)
		{
			if (value == null)
			{
				this.WritePackedUInt32(0U);
				return;
			}
			this.Write(value.netId);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00019D60 File Offset: 0x00017F60
		public void Write(Transform value)
		{
			if (value == null || value.gameObject == null)
			{
				this.WritePackedUInt32(0U);
				return;
			}
			NetworkIdentity component = value.gameObject.GetComponent<NetworkIdentity>();
			if (component != null)
			{
				this.Write(component.netId);
			}
			else
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NetworkWriter " + value + " has no NetworkIdentity");
				}
				this.WritePackedUInt32(0U);
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00019DE4 File Offset: 0x00017FE4
		public void Write(GameObject value)
		{
			if (value == null)
			{
				this.WritePackedUInt32(0U);
				return;
			}
			NetworkIdentity component = value.GetComponent<NetworkIdentity>();
			if (component != null)
			{
				this.Write(component.netId);
			}
			else
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NetworkWriter " + value + " has no NetworkIdentity");
				}
				this.WritePackedUInt32(0U);
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00019E50 File Offset: 0x00018050
		public void Write(MessageBase msg)
		{
			msg.Serialize(this);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00019E5C File Offset: 0x0001805C
		public void SeekZero()
		{
			this.m_Buffer.SeekZero();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00019E6C File Offset: 0x0001806C
		public void StartMessage(short msgType)
		{
			this.SeekZero();
			this.m_Buffer.WriteByte2(0, 0);
			this.Write(msgType);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00019E88 File Offset: 0x00018088
		public void FinishMessage()
		{
			this.m_Buffer.FinishMessage();
		}

		// Token: 0x040001F9 RID: 505
		private const int k_MaxStringLength = 32768;

		// Token: 0x040001FA RID: 506
		private NetBuffer m_Buffer;

		// Token: 0x040001FB RID: 507
		private static Encoding s_Encoding;

		// Token: 0x040001FC RID: 508
		private static byte[] s_StringWriteBuffer;

		// Token: 0x040001FD RID: 509
		private static UIntFloat s_FloatConverter;
	}
}
