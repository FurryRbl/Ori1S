using System;
using System.IO;

// Token: 0x0200009B RID: 155
[PooledSafe]
[Serializable]
public class MoonGuid : ISerializable
{
	// Token: 0x06000654 RID: 1620 RVA: 0x00018B60 File Offset: 0x00016D60
	public MoonGuid(MoonGuid moonGuid)
	{
		this.A = moonGuid.A;
		this.B = moonGuid.B;
		this.C = moonGuid.C;
		this.D = moonGuid.D;
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x00018BA3 File Offset: 0x00016DA3
	public MoonGuid(int a, int b, int c, int d)
	{
		this.A = a;
		this.B = b;
		this.C = c;
		this.D = d;
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x00018BC8 File Offset: 0x00016DC8
	public MoonGuid(Guid guid)
	{
		this.Parse(guid);
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x00018BD7 File Offset: 0x00016DD7
	public MoonGuid(string guid)
	{
		this.Parse(guid);
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x00018BE6 File Offset: 0x00016DE6
	public MoonGuid(byte[] guidByteArray)
	{
		this.Parse(guidByteArray);
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x00018BF8 File Offset: 0x00016DF8
	public MoonGuid(BinaryReader reader)
	{
		this.A = reader.ReadInt32();
		this.B = reader.ReadInt32();
		this.C = reader.ReadInt32();
		this.D = reader.ReadInt32();
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00018C3C File Offset: 0x00016E3C
	private void Parse(Guid guid)
	{
		byte[] guidByteArray = guid.ToByteArray();
		this.Parse(guidByteArray);
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x00018C58 File Offset: 0x00016E58
	private void Parse(byte[] guidByteArray)
	{
		this.A = BitConverter.ToInt32(guidByteArray, 0);
		this.B = BitConverter.ToInt32(guidByteArray, 4);
		this.C = BitConverter.ToInt32(guidByteArray, 8);
		this.D = BitConverter.ToInt32(guidByteArray, 12);
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x00018C90 File Offset: 0x00016E90
	private void Parse(string guid)
	{
		string[] array = guid.Split(new char[0]);
		this.A = int.Parse(array[0]);
		this.B = int.Parse(array[1]);
		this.C = int.Parse(array[2]);
		this.D = int.Parse(array[3]);
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x00018CE4 File Offset: 0x00016EE4
	public byte[] ToByteArray()
	{
		byte[] bytes = BitConverter.GetBytes(this.A);
		byte[] bytes2 = BitConverter.GetBytes(this.B);
		byte[] bytes3 = BitConverter.GetBytes(this.C);
		byte[] bytes4 = BitConverter.GetBytes(this.D);
		return new byte[]
		{
			bytes[0],
			bytes[1],
			bytes[2],
			bytes[3],
			bytes2[0],
			bytes2[1],
			bytes2[2],
			bytes2[3],
			bytes3[0],
			bytes3[1],
			bytes3[2],
			bytes3[3],
			bytes4[0],
			bytes4[1],
			bytes4[2],
			bytes4[3]
		};
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x00018D90 File Offset: 0x00016F90
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			this.A,
			" ",
			this.B,
			" ",
			this.C,
			" ",
			this.D
		});
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x00018DF8 File Offset: 0x00016FF8
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		MoonGuid moonGuid = obj as MoonGuid;
		return !(moonGuid == null) && (moonGuid.A == this.A && moonGuid.B == this.B && moonGuid.C == this.C) && moonGuid.D == this.D;
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x00018E68 File Offset: 0x00017068
	public bool Equals(MoonGuid moonGuid)
	{
		return moonGuid != null && (moonGuid.A == this.A && moonGuid.B == this.B && moonGuid.C == this.C) && moonGuid.D == this.D;
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x00018EC4 File Offset: 0x000170C4
	public override int GetHashCode()
	{
		return this.A.GetHashCode() ^ this.B.GetHashCode() ^ this.C.GetHashCode() ^ this.D.GetHashCode();
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x06000662 RID: 1634 RVA: 0x00018F00 File Offset: 0x00017100
	public static MoonGuid Empty
	{
		get
		{
			return new MoonGuid(Guid.Empty);
		}
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x00018F0C File Offset: 0x0001710C
	public void Serialize(Archive ar)
	{
		ar.Serialize(ref this.A);
		ar.Serialize(ref this.B);
		ar.Serialize(ref this.C);
		ar.Serialize(ref this.D);
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x00018F4C File Offset: 0x0001714C
	public static bool operator ==(MoonGuid a, MoonGuid b)
	{
		return a != null && b != null && (a.A == b.A && a.B == b.B && a.C == b.C) && a.D == b.D;
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x00018FAB File Offset: 0x000171AB
	public static bool operator !=(MoonGuid a, MoonGuid b)
	{
		return !(a == b);
	}

	// Token: 0x040004EA RID: 1258
	public int A;

	// Token: 0x040004EB RID: 1259
	public int B;

	// Token: 0x040004EC RID: 1260
	public int C;

	// Token: 0x040004ED RID: 1261
	public int D;
}
