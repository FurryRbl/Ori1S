using System;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x02000077 RID: 119
	public abstract class MessageBase
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x0001B70C File Offset: 0x0001990C
		protected MessageBase(int messageType)
		{
			this._type = messageType;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001B734 File Offset: 0x00019934
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0001B73C File Offset: 0x0001993C
		public NtlmFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0001B748 File Offset: 0x00019948
		public int Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001B750 File Offset: 0x00019950
		protected byte[] PrepareMessage(int messageSize)
		{
			byte[] array = new byte[messageSize];
			Buffer.BlockCopy(MessageBase.header, 0, array, 0, 8);
			array[8] = (byte)this._type;
			array[9] = (byte)(this._type >> 8);
			array[10] = (byte)(this._type >> 16);
			array[11] = (byte)(this._type >> 24);
			return array;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001B7A8 File Offset: 0x000199A8
		protected virtual void Decode(byte[] message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (message.Length < 12)
			{
				string text = Locale.GetText("Minimum message length is 12 bytes.");
				throw new ArgumentOutOfRangeException("message", message.Length, text);
			}
			if (!this.CheckHeader(message))
			{
				string message2 = string.Format(Locale.GetText("Invalid Type{0} message."), this._type);
				throw new ArgumentException(message2, "message");
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001B824 File Offset: 0x00019A24
		protected bool CheckHeader(byte[] message)
		{
			for (int i = 0; i < MessageBase.header.Length; i++)
			{
				if (message[i] != MessageBase.header[i])
				{
					return false;
				}
			}
			return (ulong)BitConverterLE.ToUInt32(message, 8) == (ulong)((long)this._type);
		}

		// Token: 0x0600044E RID: 1102
		public abstract byte[] GetBytes();

		// Token: 0x040001FE RID: 510
		private static byte[] header = new byte[]
		{
			78,
			84,
			76,
			77,
			83,
			83,
			80,
			0
		};

		// Token: 0x040001FF RID: 511
		private int _type;

		// Token: 0x04000200 RID: 512
		private NtlmFlags _flags;
	}
}
