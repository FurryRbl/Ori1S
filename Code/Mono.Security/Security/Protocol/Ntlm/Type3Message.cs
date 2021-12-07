using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x0200007B RID: 123
	public class Type3Message : MessageBase
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x0001BC1C File Offset: 0x00019E1C
		public Type3Message() : base(3)
		{
			this._domain = Environment.UserDomainName;
			this._host = Environment.MachineName;
			this._username = Environment.UserName;
			base.Flags = (NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateAlwaysSign);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001BC54 File Offset: 0x00019E54
		public Type3Message(byte[] message) : base(3)
		{
			this.Decode(message);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001BC64 File Offset: 0x00019E64
		~Type3Message()
		{
			if (this._challenge != null)
			{
				Array.Clear(this._challenge, 0, this._challenge.Length);
			}
			if (this._lm != null)
			{
				Array.Clear(this._lm, 0, this._lm.Length);
			}
			if (this._nt != null)
			{
				Array.Clear(this._nt, 0, this._nt.Length);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0001BCF8 File Offset: 0x00019EF8
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0001BD18 File Offset: 0x00019F18
		public byte[] Challenge
		{
			get
			{
				if (this._challenge == null)
				{
					return null;
				}
				return (byte[])this._challenge.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Challenge");
				}
				if (value.Length != 8)
				{
					string text = Locale.GetText("Invalid Challenge Length (should be 8 bytes).");
					throw new ArgumentException(text, "Challenge");
				}
				this._challenge = (byte[])value.Clone();
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0001BD68 File Offset: 0x00019F68
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0001BD70 File Offset: 0x00019F70
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0001BD7C File Offset: 0x00019F7C
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0001BD84 File Offset: 0x00019F84
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

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0001BD90 File Offset: 0x00019F90
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0001BD98 File Offset: 0x00019F98
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0001BDA4 File Offset: 0x00019FA4
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0001BDAC File Offset: 0x00019FAC
		public string Username
		{
			get
			{
				return this._username;
			}
			set
			{
				this._username = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001BDB8 File Offset: 0x00019FB8
		public byte[] LM
		{
			get
			{
				return this._lm;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		public byte[] NT
		{
			get
			{
				return this._nt;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001BDC8 File Offset: 0x00019FC8
		protected override void Decode(byte[] message)
		{
			base.Decode(message);
			if ((int)BitConverterLE.ToUInt16(message, 56) != message.Length)
			{
				string text = Locale.GetText("Invalid Type3 message length.");
				throw new ArgumentException(text, "message");
			}
			this._password = null;
			int count = (int)BitConverterLE.ToUInt16(message, 28);
			int index = 64;
			this._domain = Encoding.Unicode.GetString(message, index, count);
			int count2 = (int)BitConverterLE.ToUInt16(message, 44);
			int index2 = (int)BitConverterLE.ToUInt16(message, 48);
			this._host = Encoding.Unicode.GetString(message, index2, count2);
			int count3 = (int)BitConverterLE.ToUInt16(message, 36);
			int index3 = (int)BitConverterLE.ToUInt16(message, 40);
			this._username = Encoding.Unicode.GetString(message, index3, count3);
			this._lm = new byte[24];
			int srcOffset = (int)BitConverterLE.ToUInt16(message, 16);
			Buffer.BlockCopy(message, srcOffset, this._lm, 0, 24);
			this._nt = new byte[24];
			int srcOffset2 = (int)BitConverterLE.ToUInt16(message, 24);
			Buffer.BlockCopy(message, srcOffset2, this._nt, 0, 24);
			if (message.Length >= 64)
			{
				base.Flags = (NtlmFlags)BitConverterLE.ToUInt32(message, 60);
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001BEE4 File Offset: 0x0001A0E4
		public override byte[] GetBytes()
		{
			byte[] bytes = Encoding.Unicode.GetBytes(this._domain.ToUpper(CultureInfo.InvariantCulture));
			byte[] bytes2 = Encoding.Unicode.GetBytes(this._username);
			byte[] bytes3 = Encoding.Unicode.GetBytes(this._host.ToUpper(CultureInfo.InvariantCulture));
			byte[] array = base.PrepareMessage(64 + bytes.Length + bytes2.Length + bytes3.Length + 24 + 24);
			short num = (short)(64 + bytes.Length + bytes2.Length + bytes3.Length);
			array[12] = 24;
			array[13] = 0;
			array[14] = 24;
			array[15] = 0;
			array[16] = (byte)num;
			array[17] = (byte)(num >> 8);
			short num2 = num + 24;
			array[20] = 24;
			array[21] = 0;
			array[22] = 24;
			array[23] = 0;
			array[24] = (byte)num2;
			array[25] = (byte)(num2 >> 8);
			short num3 = (short)bytes.Length;
			short num4 = 64;
			array[28] = (byte)num3;
			array[29] = (byte)(num3 >> 8);
			array[30] = array[28];
			array[31] = array[29];
			array[32] = (byte)num4;
			array[33] = (byte)(num4 >> 8);
			short num5 = (short)bytes2.Length;
			short num6 = num4 + num3;
			array[36] = (byte)num5;
			array[37] = (byte)(num5 >> 8);
			array[38] = array[36];
			array[39] = array[37];
			array[40] = (byte)num6;
			array[41] = (byte)(num6 >> 8);
			short num7 = (short)bytes3.Length;
			short num8 = num6 + num5;
			array[44] = (byte)num7;
			array[45] = (byte)(num7 >> 8);
			array[46] = array[44];
			array[47] = array[45];
			array[48] = (byte)num8;
			array[49] = (byte)(num8 >> 8);
			short num9 = (short)array.Length;
			array[56] = (byte)num9;
			array[57] = (byte)(num9 >> 8);
			array[60] = (byte)base.Flags;
			array[61] = (byte)(base.Flags >> 8);
			array[62] = (byte)(base.Flags >> 16);
			array[63] = (byte)(base.Flags >> 24);
			Buffer.BlockCopy(bytes, 0, array, (int)num4, bytes.Length);
			Buffer.BlockCopy(bytes2, 0, array, (int)num6, bytes2.Length);
			Buffer.BlockCopy(bytes3, 0, array, (int)num8, bytes3.Length);
			using (ChallengeResponse challengeResponse = new ChallengeResponse(this._password, this._challenge))
			{
				Buffer.BlockCopy(challengeResponse.LM, 0, array, (int)num, 24);
				Buffer.BlockCopy(challengeResponse.NT, 0, array, (int)num2, 24);
			}
			return array;
		}

		// Token: 0x0400020F RID: 527
		private byte[] _challenge;

		// Token: 0x04000210 RID: 528
		private string _host;

		// Token: 0x04000211 RID: 529
		private string _domain;

		// Token: 0x04000212 RID: 530
		private string _username;

		// Token: 0x04000213 RID: 531
		private string _password;

		// Token: 0x04000214 RID: 532
		private byte[] _lm;

		// Token: 0x04000215 RID: 533
		private byte[] _nt;
	}
}
