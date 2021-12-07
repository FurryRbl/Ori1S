using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x02000079 RID: 121
	public class Type1Message : MessageBase
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0001B86C File Offset: 0x00019A6C
		public Type1Message() : base(1)
		{
			this._domain = Environment.UserDomainName;
			this._host = Environment.MachineName;
			base.Flags = (NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateOem | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateDomainSupplied | NtlmFlags.NegotiateWorkstationSupplied | NtlmFlags.NegotiateAlwaysSign);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001B8A4 File Offset: 0x00019AA4
		public Type1Message(byte[] message) : base(1)
		{
			this.Decode(message);
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001B8B4 File Offset: 0x00019AB4
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0001B8BC File Offset: 0x00019ABC
		public string Domain
		{
			get
			{
				return this._domain;
			}
			set
			{
				this._domain = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0001B8C8 File Offset: 0x00019AC8
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0001B8D0 File Offset: 0x00019AD0
		public string Host
		{
			get
			{
				return this._host;
			}
			set
			{
				this._host = value;
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001B8DC File Offset: 0x00019ADC
		protected override void Decode(byte[] message)
		{
			base.Decode(message);
			base.Flags = (NtlmFlags)BitConverterLE.ToUInt32(message, 12);
			int count = (int)BitConverterLE.ToUInt16(message, 16);
			int index = (int)BitConverterLE.ToUInt16(message, 20);
			this._domain = Encoding.ASCII.GetString(message, index, count);
			int count2 = (int)BitConverterLE.ToUInt16(message, 24);
			this._host = Encoding.ASCII.GetString(message, 32, count2);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001B940 File Offset: 0x00019B40
		public override byte[] GetBytes()
		{
			short num = (short)this._domain.Length;
			short num2 = (short)this._host.Length;
			byte[] array = base.PrepareMessage((int)(32 + num + num2));
			array[12] = (byte)base.Flags;
			array[13] = (byte)(base.Flags >> 8);
			array[14] = (byte)(base.Flags >> 16);
			array[15] = (byte)(base.Flags >> 24);
			short num3 = 32 + num2;
			array[16] = (byte)num;
			array[17] = (byte)(num >> 8);
			array[18] = array[16];
			array[19] = array[17];
			array[20] = (byte)num3;
			array[21] = (byte)(num3 >> 8);
			array[24] = (byte)num2;
			array[25] = (byte)(num2 >> 8);
			array[26] = array[24];
			array[27] = array[25];
			array[28] = 32;
			array[29] = 0;
			byte[] bytes = Encoding.ASCII.GetBytes(this._host.ToUpper(CultureInfo.InvariantCulture));
			Buffer.BlockCopy(bytes, 0, array, 32, bytes.Length);
			byte[] bytes2 = Encoding.ASCII.GetBytes(this._domain.ToUpper(CultureInfo.InvariantCulture));
			Buffer.BlockCopy(bytes2, 0, array, (int)num3, bytes2.Length);
			return array;
		}

		// Token: 0x0400020C RID: 524
		private string _host;

		// Token: 0x0400020D RID: 525
		private string _domain;
	}
}
