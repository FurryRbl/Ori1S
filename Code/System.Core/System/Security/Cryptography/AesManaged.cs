using System;
using System.Security.Permissions;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x02000056 RID: 86
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class AesManaged : Aes
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x00015D78 File Offset: 0x00013F78
		public override void GenerateIV()
		{
			this.IVValue = KeyBuilder.IV(this.BlockSizeValue >> 3);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00015D90 File Offset: 0x00013F90
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00015DA8 File Offset: 0x00013FA8
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return new AesTransform(this, false, rgbKey, rgbIV);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00015DB4 File Offset: 0x00013FB4
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return new AesTransform(this, true, rgbKey, rgbIV);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00015DC0 File Offset: 0x00013FC0
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00015DC8 File Offset: 0x00013FC8
		public override byte[] IV
		{
			get
			{
				return base.IV;
			}
			set
			{
				base.IV = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00015DD4 File Offset: 0x00013FD4
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x00015DDC File Offset: 0x00013FDC
		public override byte[] Key
		{
			get
			{
				return base.Key;
			}
			set
			{
				base.Key = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00015DE8 File Offset: 0x00013FE8
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x00015DF0 File Offset: 0x00013FF0
		public override int KeySize
		{
			get
			{
				return base.KeySize;
			}
			set
			{
				base.KeySize = value;
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00015DFC File Offset: 0x00013FFC
		public override ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00015E10 File Offset: 0x00014010
		public override ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00015E24 File Offset: 0x00014024
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
