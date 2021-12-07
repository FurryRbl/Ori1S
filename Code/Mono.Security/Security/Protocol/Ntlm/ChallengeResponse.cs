using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x02000076 RID: 118
	public class ChallengeResponse : IDisposable
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x0001B264 File Offset: 0x00019464
		public ChallengeResponse()
		{
			this._disposed = false;
			this._lmpwd = new byte[21];
			this._ntpwd = new byte[21];
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001B290 File Offset: 0x00019490
		public ChallengeResponse(string password, byte[] challenge) : this()
		{
			this.Password = password;
			this.Challenge = challenge;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001B2E4 File Offset: 0x000194E4
		~ChallengeResponse()
		{
			if (!this._disposed)
			{
				this.Dispose();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0001B32C File Offset: 0x0001952C
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0001B330 File Offset: 0x00019530
		public string Password
		{
			get
			{
				return null;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				DES des = DES.Create();
				des.Mode = CipherMode.ECB;
				if (value == null || value.Length < 1)
				{
					Buffer.BlockCopy(ChallengeResponse.nullEncMagic, 0, this._lmpwd, 0, 8);
				}
				else
				{
					des.Key = this.PasswordToKey(value, 0);
					ICryptoTransform cryptoTransform = des.CreateEncryptor();
					cryptoTransform.TransformBlock(ChallengeResponse.magic, 0, 8, this._lmpwd, 0);
				}
				if (value == null || value.Length < 8)
				{
					Buffer.BlockCopy(ChallengeResponse.nullEncMagic, 0, this._lmpwd, 8, 8);
				}
				else
				{
					des.Key = this.PasswordToKey(value, 7);
					ICryptoTransform cryptoTransform = des.CreateEncryptor();
					cryptoTransform.TransformBlock(ChallengeResponse.magic, 0, 8, this._lmpwd, 8);
				}
				MD4 md = MD4.Create();
				byte[] array = (value != null) ? Encoding.Unicode.GetBytes(value) : new byte[0];
				byte[] array2 = md.ComputeHash(array);
				Buffer.BlockCopy(array2, 0, this._ntpwd, 0, 16);
				Array.Clear(array, 0, array.Length);
				Array.Clear(array2, 0, array2.Length);
				des.Clear();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0001B464 File Offset: 0x00019664
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0001B468 File Offset: 0x00019668
		public byte[] Challenge
		{
			get
			{
				return null;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Challenge");
				}
				if (this._disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				this._challenge = (byte[])value.Clone();
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0001B4B0 File Offset: 0x000196B0
		public byte[] LM
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				return this.GetResponse(this._lmpwd);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001B4E0 File Offset: 0x000196E0
		public byte[] NT
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				return this.GetResponse(this._ntpwd);
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001B510 File Offset: 0x00019710
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001B520 File Offset: 0x00019720
		private void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				Array.Clear(this._lmpwd, 0, this._lmpwd.Length);
				Array.Clear(this._ntpwd, 0, this._ntpwd.Length);
				if (this._challenge != null)
				{
					Array.Clear(this._challenge, 0, this._challenge.Length);
				}
				this._disposed = true;
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001B588 File Offset: 0x00019788
		private byte[] GetResponse(byte[] pwd)
		{
			byte[] array = new byte[24];
			DES des = DES.Create();
			des.Mode = CipherMode.ECB;
			des.Key = this.PrepareDESKey(pwd, 0);
			ICryptoTransform cryptoTransform = des.CreateEncryptor();
			cryptoTransform.TransformBlock(this._challenge, 0, 8, array, 0);
			des.Key = this.PrepareDESKey(pwd, 7);
			cryptoTransform = des.CreateEncryptor();
			cryptoTransform.TransformBlock(this._challenge, 0, 8, array, 8);
			des.Key = this.PrepareDESKey(pwd, 14);
			cryptoTransform = des.CreateEncryptor();
			cryptoTransform.TransformBlock(this._challenge, 0, 8, array, 16);
			return array;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001B620 File Offset: 0x00019820
		private byte[] PrepareDESKey(byte[] key56bits, int position)
		{
			return new byte[]
			{
				key56bits[position],
				(byte)((int)key56bits[position] << 7 | key56bits[position + 1] >> 1),
				(byte)((int)key56bits[position + 1] << 6 | key56bits[position + 2] >> 2),
				(byte)((int)key56bits[position + 2] << 5 | key56bits[position + 3] >> 3),
				(byte)((int)key56bits[position + 3] << 4 | key56bits[position + 4] >> 4),
				(byte)((int)key56bits[position + 4] << 3 | key56bits[position + 5] >> 5),
				(byte)((int)key56bits[position + 5] << 2 | key56bits[position + 6] >> 6),
				(byte)(key56bits[position + 6] << 1)
			};
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001B6B8 File Offset: 0x000198B8
		private byte[] PasswordToKey(string password, int position)
		{
			byte[] array = new byte[7];
			int charCount = Math.Min(password.Length - position, 7);
			Encoding.ASCII.GetBytes(password.ToUpper(CultureInfo.CurrentCulture), position, charCount, array, 0);
			byte[] result = this.PrepareDESKey(array, 0);
			Array.Clear(array, 0, array.Length);
			return result;
		}

		// Token: 0x040001F8 RID: 504
		private static byte[] magic = new byte[]
		{
			75,
			71,
			83,
			33,
			64,
			35,
			36,
			37
		};

		// Token: 0x040001F9 RID: 505
		private static byte[] nullEncMagic = new byte[]
		{
			170,
			211,
			180,
			53,
			181,
			20,
			4,
			238
		};

		// Token: 0x040001FA RID: 506
		private bool _disposed;

		// Token: 0x040001FB RID: 507
		private byte[] _challenge;

		// Token: 0x040001FC RID: 508
		private byte[] _lmpwd;

		// Token: 0x040001FD RID: 509
		private byte[] _ntpwd;
	}
}
