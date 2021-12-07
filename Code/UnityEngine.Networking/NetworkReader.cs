using System;
using System.Text;

namespace UnityEngine.Networking
{
	// Token: 0x0200004F RID: 79
	public class NetworkReader
	{
		// Token: 0x06000345 RID: 837 RVA: 0x000114B0 File Offset: 0x0000F6B0
		public NetworkReader()
		{
			this.m_buf = new NetBuffer();
			NetworkReader.Initialize();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000114C8 File Offset: 0x0000F6C8
		public NetworkReader(NetworkWriter writer)
		{
			this.m_buf = new NetBuffer(writer.AsArray());
			NetworkReader.Initialize();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000114E8 File Offset: 0x0000F6E8
		public NetworkReader(byte[] buffer)
		{
			this.m_buf = new NetBuffer(buffer);
			NetworkReader.Initialize();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00011504 File Offset: 0x0000F704
		private static void Initialize()
		{
			if (NetworkReader.s_Encoding == null)
			{
				NetworkReader.s_StringReaderBuffer = new byte[1024];
				NetworkReader.s_Encoding = new UTF8Encoding();
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0001152C File Offset: 0x0000F72C
		public uint Position
		{
			get
			{
				return this.m_buf.Position;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001153C File Offset: 0x0000F73C
		public void SeekZero()
		{
			this.m_buf.SeekZero();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001154C File Offset: 0x0000F74C
		internal void Replace(byte[] buffer)
		{
			this.m_buf.Replace(buffer);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001155C File Offset: 0x0000F75C
		public uint ReadPackedUInt32()
		{
			byte b = this.ReadByte();
			if (b < 241)
			{
				return (uint)b;
			}
			byte b2 = this.ReadByte();
			if (b >= 241 && b <= 248)
			{
				return 240U + 256U * (uint)(b - 241) + (uint)b2;
			}
			byte b3 = this.ReadByte();
			if (b == 249)
			{
				return 2288U + 256U * (uint)b2 + (uint)b3;
			}
			byte b4 = this.ReadByte();
			if (b == 250)
			{
				return (uint)((int)b2 + ((int)b3 << 8) + ((int)b4 << 16));
			}
			byte b5 = this.ReadByte();
			if (b >= 251)
			{
				return (uint)((int)b2 + ((int)b3 << 8) + ((int)b4 << 16) + ((int)b5 << 24));
			}
			throw new IndexOutOfRangeException("ReadPackedUInt32() failure: " + b);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00011628 File Offset: 0x0000F828
		public ulong ReadPackedUInt64()
		{
			byte b = this.ReadByte();
			if (b < 241)
			{
				return (ulong)b;
			}
			byte b2 = this.ReadByte();
			if (b >= 241 && b <= 248)
			{
				return 240UL + 256UL * ((ulong)b - 241UL) + (ulong)b2;
			}
			byte b3 = this.ReadByte();
			if (b == 249)
			{
				return 2288UL + 256UL * (ulong)b2 + (ulong)b3;
			}
			byte b4 = this.ReadByte();
			if (b == 250)
			{
				return (ulong)b2 + ((ulong)b3 << 8) + ((ulong)b4 << 16);
			}
			byte b5 = this.ReadByte();
			if (b == 251)
			{
				return (ulong)b2 + ((ulong)b3 << 8) + ((ulong)b4 << 16) + ((ulong)b5 << 24);
			}
			byte b6 = this.ReadByte();
			if (b == 252)
			{
				return (ulong)b2 + ((ulong)b3 << 8) + ((ulong)b4 << 16) + ((ulong)b5 << 24) + ((ulong)b6 << 32);
			}
			byte b7 = this.ReadByte();
			if (b == 253)
			{
				return (ulong)b2 + ((ulong)b3 << 8) + ((ulong)b4 << 16) + ((ulong)b5 << 24) + ((ulong)b6 << 32) + ((ulong)b7 << 40);
			}
			byte b8 = this.ReadByte();
			if (b == 254)
			{
				return (ulong)b2 + ((ulong)b3 << 8) + ((ulong)b4 << 16) + ((ulong)b5 << 24) + ((ulong)b6 << 32) + ((ulong)b7 << 40) + ((ulong)b8 << 48);
			}
			byte b9 = this.ReadByte();
			if (b == 255)
			{
				return (ulong)b2 + ((ulong)b3 << 8) + ((ulong)b4 << 16) + ((ulong)b5 << 24) + ((ulong)b6 << 32) + ((ulong)b7 << 40) + ((ulong)b8 << 48) + ((ulong)b9 << 56);
			}
			throw new IndexOutOfRangeException("ReadPackedUInt64() failure: " + b);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000117EC File Offset: 0x0000F9EC
		public NetworkInstanceId ReadNetworkId()
		{
			return new NetworkInstanceId(this.ReadPackedUInt32());
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000117FC File Offset: 0x0000F9FC
		public NetworkSceneId ReadSceneId()
		{
			return new NetworkSceneId(this.ReadPackedUInt32());
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001180C File Offset: 0x0000FA0C
		public byte ReadByte()
		{
			return this.m_buf.ReadByte();
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0001181C File Offset: 0x0000FA1C
		public sbyte ReadSByte()
		{
			return (sbyte)this.m_buf.ReadByte();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001182C File Offset: 0x0000FA2C
		public short ReadInt16()
		{
			ushort num = 0;
			num |= (ushort)this.m_buf.ReadByte();
			num |= (ushort)(this.m_buf.ReadByte() << 8);
			return (short)num;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00011860 File Offset: 0x0000FA60
		public ushort ReadUInt16()
		{
			ushort num = 0;
			num |= (ushort)this.m_buf.ReadByte();
			return num | (ushort)(this.m_buf.ReadByte() << 8);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00011894 File Offset: 0x0000FA94
		public int ReadInt32()
		{
			uint num = 0U;
			num |= (uint)this.m_buf.ReadByte();
			num |= (uint)((uint)this.m_buf.ReadByte() << 8);
			num |= (uint)((uint)this.m_buf.ReadByte() << 16);
			return (int)(num | (uint)((uint)this.m_buf.ReadByte() << 24));
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000118E4 File Offset: 0x0000FAE4
		public uint ReadUInt32()
		{
			uint num = 0U;
			num |= (uint)this.m_buf.ReadByte();
			num |= (uint)((uint)this.m_buf.ReadByte() << 8);
			num |= (uint)((uint)this.m_buf.ReadByte() << 16);
			return num | (uint)((uint)this.m_buf.ReadByte() << 24);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00011934 File Offset: 0x0000FB34
		public long ReadInt64()
		{
			ulong num = 0UL;
			ulong num2 = (ulong)this.m_buf.ReadByte();
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 8;
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 16;
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 24;
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 32;
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 40;
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 48;
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 56;
			return (long)(num | num2);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000119E4 File Offset: 0x0000FBE4
		public ulong ReadUInt64()
		{
			ulong num = 0UL;
			ulong num2 = (ulong)this.m_buf.ReadByte();
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 8;
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 16;
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 24;
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 32;
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 40;
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 48;
			num |= num2;
			num2 = (ulong)this.m_buf.ReadByte() << 56;
			return num | num2;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00011A94 File Offset: 0x0000FC94
		public float ReadSingle()
		{
			uint value = this.ReadUInt32();
			return FloatConversion.ToSingle(value);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00011AB0 File Offset: 0x0000FCB0
		public double ReadDouble()
		{
			ulong value = this.ReadUInt64();
			return FloatConversion.ToDouble(value);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00011ACC File Offset: 0x0000FCCC
		public string ReadString()
		{
			ushort num = this.ReadUInt16();
			if (num == 0)
			{
				return string.Empty;
			}
			if (num >= 32768)
			{
				throw new IndexOutOfRangeException("ReadString() too long: " + num);
			}
			while ((int)num > NetworkReader.s_StringReaderBuffer.Length)
			{
				NetworkReader.s_StringReaderBuffer = new byte[NetworkReader.s_StringReaderBuffer.Length * 2];
			}
			this.m_buf.ReadBytes(NetworkReader.s_StringReaderBuffer, (uint)num);
			char[] chars = NetworkReader.s_Encoding.GetChars(NetworkReader.s_StringReaderBuffer, 0, (int)num);
			return new string(chars);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00011B5C File Offset: 0x0000FD5C
		public char ReadChar()
		{
			return (char)this.m_buf.ReadByte();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00011B6C File Offset: 0x0000FD6C
		public bool ReadBoolean()
		{
			int num = (int)this.m_buf.ReadByte();
			return num == 1;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00011B8C File Offset: 0x0000FD8C
		public byte[] ReadBytes(int count)
		{
			if (count < 0)
			{
				throw new IndexOutOfRangeException("NetworkReader ReadBytes " + count);
			}
			byte[] array = new byte[count];
			this.m_buf.ReadBytes(array, (uint)count);
			return array;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public byte[] ReadBytesAndSize()
		{
			ushort num = this.ReadUInt16();
			if (num == 0)
			{
				return null;
			}
			return this.ReadBytes((int)num);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		public Vector2 ReadVector2()
		{
			return new Vector2(this.ReadSingle(), this.ReadSingle());
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00011C04 File Offset: 0x0000FE04
		public Vector3 ReadVector3()
		{
			return new Vector3(this.ReadSingle(), this.ReadSingle(), this.ReadSingle());
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00011C28 File Offset: 0x0000FE28
		public Vector4 ReadVector4()
		{
			return new Vector4(this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle());
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00011C54 File Offset: 0x0000FE54
		public Color ReadColor()
		{
			return new Color(this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle());
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00011C80 File Offset: 0x0000FE80
		public Color32 ReadColor32()
		{
			return new Color32(this.ReadByte(), this.ReadByte(), this.ReadByte(), this.ReadByte());
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00011CAC File Offset: 0x0000FEAC
		public Quaternion ReadQuaternion()
		{
			return new Quaternion(this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle());
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00011CD8 File Offset: 0x0000FED8
		public Rect ReadRect()
		{
			return new Rect(this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle());
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00011D04 File Offset: 0x0000FF04
		public Plane ReadPlane()
		{
			return new Plane(this.ReadVector3(), this.ReadSingle());
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00011D18 File Offset: 0x0000FF18
		public Ray ReadRay()
		{
			return new Ray(this.ReadVector3(), this.ReadVector3());
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00011D2C File Offset: 0x0000FF2C
		public Matrix4x4 ReadMatrix4x4()
		{
			return new Matrix4x4
			{
				m00 = this.ReadSingle(),
				m01 = this.ReadSingle(),
				m02 = this.ReadSingle(),
				m03 = this.ReadSingle(),
				m10 = this.ReadSingle(),
				m11 = this.ReadSingle(),
				m12 = this.ReadSingle(),
				m13 = this.ReadSingle(),
				m20 = this.ReadSingle(),
				m21 = this.ReadSingle(),
				m22 = this.ReadSingle(),
				m23 = this.ReadSingle(),
				m30 = this.ReadSingle(),
				m31 = this.ReadSingle(),
				m32 = this.ReadSingle(),
				m33 = this.ReadSingle()
			};
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00011E14 File Offset: 0x00010014
		public NetworkHash128 ReadNetworkHash128()
		{
			NetworkHash128 result;
			result.i0 = this.ReadByte();
			result.i1 = this.ReadByte();
			result.i2 = this.ReadByte();
			result.i3 = this.ReadByte();
			result.i4 = this.ReadByte();
			result.i5 = this.ReadByte();
			result.i6 = this.ReadByte();
			result.i7 = this.ReadByte();
			result.i8 = this.ReadByte();
			result.i9 = this.ReadByte();
			result.i10 = this.ReadByte();
			result.i11 = this.ReadByte();
			result.i12 = this.ReadByte();
			result.i13 = this.ReadByte();
			result.i14 = this.ReadByte();
			result.i15 = this.ReadByte();
			return result;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00011EF4 File Offset: 0x000100F4
		public Transform ReadTransform()
		{
			NetworkInstanceId networkInstanceId = this.ReadNetworkId();
			if (networkInstanceId.IsEmpty())
			{
				return null;
			}
			GameObject gameObject = ClientScene.FindLocalObject(networkInstanceId);
			if (gameObject == null)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("ReadTransform netId:" + networkInstanceId);
				}
				return null;
			}
			return gameObject.transform;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00011F50 File Offset: 0x00010150
		public GameObject ReadGameObject()
		{
			NetworkInstanceId networkInstanceId = this.ReadNetworkId();
			if (networkInstanceId.IsEmpty())
			{
				return null;
			}
			GameObject gameObject;
			if (NetworkServer.active)
			{
				gameObject = NetworkServer.FindLocalObject(networkInstanceId);
			}
			else
			{
				gameObject = ClientScene.FindLocalObject(networkInstanceId);
			}
			if (gameObject == null && LogFilter.logDebug)
			{
				Debug.Log("ReadGameObject netId:" + networkInstanceId + "go: null");
			}
			return gameObject;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00011FC0 File Offset: 0x000101C0
		public NetworkIdentity ReadNetworkIdentity()
		{
			NetworkInstanceId networkInstanceId = this.ReadNetworkId();
			if (networkInstanceId.IsEmpty())
			{
				return null;
			}
			GameObject gameObject;
			if (NetworkServer.active)
			{
				gameObject = NetworkServer.FindLocalObject(networkInstanceId);
			}
			else
			{
				gameObject = ClientScene.FindLocalObject(networkInstanceId);
			}
			if (gameObject == null)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("ReadNetworkIdentity netId:" + networkInstanceId + "go: null");
				}
				return null;
			}
			return gameObject.GetComponent<NetworkIdentity>();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00012038 File Offset: 0x00010238
		public override string ToString()
		{
			return this.m_buf.ToString();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00012048 File Offset: 0x00010248
		public TMsg ReadMessage<TMsg>() where TMsg : MessageBase, new()
		{
			TMsg result = Activator.CreateInstance<TMsg>();
			result.Deserialize(this);
			return result;
		}

		// Token: 0x04000187 RID: 391
		private const int k_MaxStringLength = 32768;

		// Token: 0x04000188 RID: 392
		private const int k_InitialStringBufferSize = 1024;

		// Token: 0x04000189 RID: 393
		private NetBuffer m_buf;

		// Token: 0x0400018A RID: 394
		private static byte[] s_StringReaderBuffer;

		// Token: 0x0400018B RID: 395
		private static Encoding s_Encoding;
	}
}
