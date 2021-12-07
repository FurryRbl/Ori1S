using System;
using System.Security.Cryptography;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x0200007A RID: 122
	public class Type2Message : MessageBase
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x0001BA5C File Offset: 0x00019C5C
		public Type2Message() : base(2)
		{
			this._nonce = new byte[8];
			RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			randomNumberGenerator.GetBytes(this._nonce);
			base.Flags = (NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateAlwaysSign);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001BA9C File Offset: 0x00019C9C
		public Type2Message(byte[] message) : base(2)
		{
			this._nonce = new byte[8];
			this.Decode(message);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001BAB8 File Offset: 0x00019CB8
		~Type2Message()
		{
			if (this._nonce != null)
			{
				Array.Clear(this._nonce, 0, this._nonce.Length);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0001BB0C File Offset: 0x00019D0C
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0001BB20 File Offset: 0x00019D20
		public byte[] Nonce
		{
			get
			{
				return (byte[])this._nonce.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Nonce");
				}
				if (value.Length != 8)
				{
					string text = Locale.GetText("Invalid Nonce Length (should be 8 bytes).");
					throw new ArgumentException(text, "Nonce");
				}
				this._nonce = (byte[])value.Clone();
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001BB70 File Offset: 0x00019D70
		protected override void Decode(byte[] message)
		{
			base.Decode(message);
			base.Flags = (NtlmFlags)BitConverterLE.ToUInt32(message, 20);
			Buffer.BlockCopy(message, 24, this._nonce, 0, 8);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001BBA4 File Offset: 0x00019DA4
		public override byte[] GetBytes()
		{
			byte[] array = base.PrepareMessage(40);
			short num = (short)array.Length;
			array[16] = (byte)num;
			array[17] = (byte)(num >> 8);
			array[20] = (byte)base.Flags;
			array[21] = (byte)(base.Flags >> 8);
			array[22] = (byte)(base.Flags >> 16);
			array[23] = (byte)(base.Flags >> 24);
			Buffer.BlockCopy(this._nonce, 0, array, 24, this._nonce.Length);
			return array;
		}

		// Token: 0x0400020E RID: 526
		private byte[] _nonce;
	}
}
