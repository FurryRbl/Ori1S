using System;
using System.IO;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class Archive
{
	// Token: 0x06000807 RID: 2055 RVA: 0x00022BB4 File Offset: 0x00020DB4
	public Archive()
	{
		this.MemoryStream = new MemoryStream();
	}

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x06000808 RID: 2056 RVA: 0x00022BD2 File Offset: 0x00020DD2
	// (set) Token: 0x06000809 RID: 2057 RVA: 0x00022BDC File Offset: 0x00020DDC
	public MemoryStream MemoryStream
	{
		get
		{
			return this.m_memoryStream;
		}
		set
		{
			if (this.m_memoryStream != null)
			{
				((IDisposable)this.m_memoryStream).Dispose();
			}
			if (this.m_binaryReader != null)
			{
				((IDisposable)this.m_binaryReader).Dispose();
			}
			if (this.m_binaryWriter != null)
			{
				((IDisposable)this.m_binaryWriter).Dispose();
			}
			this.m_memoryStream = value;
			this.m_binaryReader = new BinaryReader(this.m_memoryStream);
			this.m_binaryWriter = new BinaryWriter(this.m_memoryStream);
		}
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00022C54 File Offset: 0x00020E54
	public void WriteMemoryStreamToBinaryWriter(BinaryWriter binaryWriter)
	{
		binaryWriter.Write((int)this.MemoryStream.Length);
		this.MemoryStream.WriteTo(binaryWriter.BaseStream);
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00022C84 File Offset: 0x00020E84
	public void ReadMemoryStreamFromBinaryReader(BinaryReader binaryReader)
	{
		int num = binaryReader.ReadInt32();
		this.MemoryStream.SetLength((long)num);
		binaryReader.Read(this.MemoryStream.GetBuffer(), 0, num);
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x0600080C RID: 2060 RVA: 0x00022CB9 File Offset: 0x00020EB9
	public bool Reading
	{
		get
		{
			return !this.m_write;
		}
	}

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x0600080D RID: 2061 RVA: 0x00022CC4 File Offset: 0x00020EC4
	public bool Writing
	{
		get
		{
			return this.m_write;
		}
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00022CCC File Offset: 0x00020ECC
	public void ResetStream()
	{
		this.MemoryStream.Position = 0L;
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00022CDB File Offset: 0x00020EDB
	public void WriteMode()
	{
		this.ResetStream();
		this.m_write = true;
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00022CEA File Offset: 0x00020EEA
	public void ReadMode()
	{
		this.m_memoryStream.Position = 0L;
		this.m_write = false;
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00022D00 File Offset: 0x00020F00
	public void Serialize(ref float value)
	{
		value = this.Serialize(value);
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00022D0C File Offset: 0x00020F0C
	public void Serialize(ref int value)
	{
		value = this.Serialize(value);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00022D18 File Offset: 0x00020F18
	public void Serialize(ref bool value)
	{
		value = this.Serialize(value);
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00022D24 File Offset: 0x00020F24
	public void Serialize(ref string value)
	{
		value = this.Serialize(value);
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x00022D30 File Offset: 0x00020F30
	public void Serialize(ref Vector2 value)
	{
		value = this.Serialize(value);
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00022D44 File Offset: 0x00020F44
	public void Serialize(ref Vector3 value)
	{
		value = this.Serialize(value);
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00022D58 File Offset: 0x00020F58
	public void Serialize(ref Quaternion value)
	{
		value = this.Serialize(value);
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00022D6C File Offset: 0x00020F6C
	public float Serialize(float value)
	{
		if (this.m_write)
		{
			this.m_binaryWriter.Write(value);
			return value;
		}
		return this.m_binaryReader.ReadSingle();
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00022DA0 File Offset: 0x00020FA0
	public int Serialize(int value)
	{
		if (this.m_write)
		{
			this.m_binaryWriter.Write(value);
			return value;
		}
		return this.m_binaryReader.ReadInt32();
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00022DD4 File Offset: 0x00020FD4
	public bool Serialize(bool value)
	{
		if (this.m_write)
		{
			this.m_binaryWriter.Write(value);
			return value;
		}
		return this.m_binaryReader.ReadBoolean();
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00022E08 File Offset: 0x00021008
	public string Serialize(string value)
	{
		if (this.m_write)
		{
			this.m_binaryWriter.Write(value);
			return value;
		}
		return this.m_binaryReader.ReadString();
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00022E39 File Offset: 0x00021039
	public Vector2 Serialize(Vector2 value)
	{
		value.x = this.Serialize(value.x);
		value.y = this.Serialize(value.y);
		return value;
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x00022E64 File Offset: 0x00021064
	public Vector3 Serialize(Vector3 value)
	{
		value.x = this.Serialize(value.x);
		value.y = this.Serialize(value.y);
		value.z = this.Serialize(value.z);
		return value;
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00022EA4 File Offset: 0x000210A4
	public Quaternion Serialize(Quaternion value)
	{
		value.x = this.Serialize(value.x);
		value.y = this.Serialize(value.y);
		value.z = this.Serialize(value.z);
		value.w = this.Serialize(value.w);
		return value;
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00022F02 File Offset: 0x00021102
	public void SerializeVersion(ref int version)
	{
	}

	// Token: 0x0400067B RID: 1659
	private MemoryStream m_memoryStream = new MemoryStream();

	// Token: 0x0400067C RID: 1660
	private BinaryReader m_binaryReader;

	// Token: 0x0400067D RID: 1661
	private BinaryWriter m_binaryWriter;

	// Token: 0x0400067E RID: 1662
	private bool m_write;
}
