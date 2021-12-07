using System;
using System.Security.Permissions;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x02000057 RID: 87
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class AesCryptoServiceProvider : Aes
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x00015E38 File Offset: 0x00014038
		public override void GenerateIV()
		{
			this.IVValue = KeyBuilder.IV(this.BlockSizeValue >> 3);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00015E50 File Offset: 0x00014050
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00015E68 File Offset: 0x00014068
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return new AesTransform(this, false, rgbKey, rgbIV);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00015E74 File Offset: 0x00014074
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return new AesTransform(this, true, rgbKey, rgbIV);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00015E80 File Offset: 0x00014080
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x00015E88 File Offset: 0x00014088
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00015E94 File Offset: 0x00014094
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00015E9C File Offset: 0x0001409C
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00015EA8 File Offset: 0x000140A8
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x00015EB0 File Offset: 0x000140B0
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

		// Token: 0x06000502 RID: 1282 RVA: 0x00015EBC File Offset: 0x000140BC
		public override ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00015ED0 File Offset: 0x000140D0
		public override ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00015EE4 File Offset: 0x000140E4
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
