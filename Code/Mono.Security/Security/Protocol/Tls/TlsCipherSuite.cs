using System;
using System.Security.Cryptography;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200009F RID: 159
	internal class TlsCipherSuite : CipherSuite
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x00022918 File Offset: 0x00020B18
		public TlsCipherSuite(short code, string name, CipherAlgorithmType cipherAlgorithmType, HashAlgorithmType hashAlgorithmType, ExchangeAlgorithmType exchangeAlgorithmType, bool exportable, bool blockMode, byte keyMaterialSize, byte expandedKeyMaterialSize, short effectiveKeyBytes, byte ivSize, byte blockSize) : base(code, name, cipherAlgorithmType, hashAlgorithmType, exchangeAlgorithmType, exportable, blockMode, keyMaterialSize, expandedKeyMaterialSize, effectiveKeyBytes, ivSize, blockSize)
		{
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0002294C File Offset: 0x00020B4C
		public override byte[] ComputeServerRecordMAC(ContentType contentType, byte[] fragment)
		{
			object obj = this.headerLock;
			byte[] hash;
			lock (obj)
			{
				if (this.header == null)
				{
					this.header = new byte[13];
				}
				ulong value = (!(base.Context is ClientContext)) ? base.Context.WriteSequenceNumber : base.Context.ReadSequenceNumber;
				base.Write(this.header, 0, value);
				this.header[8] = (byte)contentType;
				base.Write(this.header, 9, base.Context.Protocol);
				base.Write(this.header, 11, (short)fragment.Length);
				HashAlgorithm serverHMAC = base.ServerHMAC;
				serverHMAC.TransformBlock(this.header, 0, this.header.Length, this.header, 0);
				serverHMAC.TransformBlock(fragment, 0, fragment.Length, fragment, 0);
				serverHMAC.TransformFinalBlock(CipherSuite.EmptyArray, 0, 0);
				hash = serverHMAC.Hash;
			}
			return hash;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00022A64 File Offset: 0x00020C64
		public override byte[] ComputeClientRecordMAC(ContentType contentType, byte[] fragment)
		{
			object obj = this.headerLock;
			byte[] hash;
			lock (obj)
			{
				if (this.header == null)
				{
					this.header = new byte[13];
				}
				ulong value = (!(base.Context is ClientContext)) ? base.Context.ReadSequenceNumber : base.Context.WriteSequenceNumber;
				base.Write(this.header, 0, value);
				this.header[8] = (byte)contentType;
				base.Write(this.header, 9, base.Context.Protocol);
				base.Write(this.header, 11, (short)fragment.Length);
				HashAlgorithm clientHMAC = base.ClientHMAC;
				clientHMAC.TransformBlock(this.header, 0, this.header.Length, this.header, 0);
				clientHMAC.TransformBlock(fragment, 0, fragment.Length, fragment, 0);
				clientHMAC.TransformFinalBlock(CipherSuite.EmptyArray, 0, 0);
				hash = clientHMAC.Hash;
			}
			return hash;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00022B7C File Offset: 0x00020D7C
		public override void ComputeMasterSecret(byte[] preMasterSecret)
		{
			base.Context.MasterSecret = new byte[preMasterSecret.Length];
			base.Context.MasterSecret = base.PRF(preMasterSecret, "master secret", base.Context.RandomCS, 48);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00022BC0 File Offset: 0x00020DC0
		public override void ComputeKeys()
		{
			TlsStream tlsStream = new TlsStream(base.PRF(base.Context.MasterSecret, "key expansion", base.Context.RandomSC, base.KeyBlockSize));
			base.Context.Negotiating.ClientWriteMAC = tlsStream.ReadBytes(base.HashSize);
			base.Context.Negotiating.ServerWriteMAC = tlsStream.ReadBytes(base.HashSize);
			base.Context.ClientWriteKey = tlsStream.ReadBytes((int)base.KeyMaterialSize);
			base.Context.ServerWriteKey = tlsStream.ReadBytes((int)base.KeyMaterialSize);
			if (!base.IsExportable)
			{
				if (base.IvSize != 0)
				{
					base.Context.ClientWriteIV = tlsStream.ReadBytes((int)base.IvSize);
					base.Context.ServerWriteIV = tlsStream.ReadBytes((int)base.IvSize);
				}
				else
				{
					base.Context.ClientWriteIV = CipherSuite.EmptyArray;
					base.Context.ServerWriteIV = CipherSuite.EmptyArray;
				}
			}
			else
			{
				byte[] clientWriteKey = base.PRF(base.Context.ClientWriteKey, "client write key", base.Context.RandomCS, (int)base.ExpandedKeyMaterialSize);
				byte[] serverWriteKey = base.PRF(base.Context.ServerWriteKey, "server write key", base.Context.RandomCS, (int)base.ExpandedKeyMaterialSize);
				base.Context.ClientWriteKey = clientWriteKey;
				base.Context.ServerWriteKey = serverWriteKey;
				if (base.IvSize > 0)
				{
					byte[] src = base.PRF(CipherSuite.EmptyArray, "IV block", base.Context.RandomCS, (int)(base.IvSize * 2));
					base.Context.ClientWriteIV = new byte[(int)base.IvSize];
					Buffer.BlockCopy(src, 0, base.Context.ClientWriteIV, 0, base.Context.ClientWriteIV.Length);
					base.Context.ServerWriteIV = new byte[(int)base.IvSize];
					Buffer.BlockCopy(src, (int)base.IvSize, base.Context.ServerWriteIV, 0, base.Context.ServerWriteIV.Length);
				}
				else
				{
					base.Context.ClientWriteIV = CipherSuite.EmptyArray;
					base.Context.ServerWriteIV = CipherSuite.EmptyArray;
				}
			}
			ClientSessionCache.SetContextInCache(base.Context);
			tlsStream.Reset();
		}

		// Token: 0x040002F0 RID: 752
		private const int MacHeaderLength = 13;

		// Token: 0x040002F1 RID: 753
		private byte[] header;

		// Token: 0x040002F2 RID: 754
		private object headerLock = new object();
	}
}
