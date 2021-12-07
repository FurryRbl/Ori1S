using System;
using System.Text;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x020000AD RID: 173
	internal class TlsServerCertificateRequest : HandshakeMessage
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x00024654 File Offset: 0x00022854
		public TlsServerCertificateRequest(Context context, byte[] buffer) : base(context, HandshakeType.CertificateRequest, buffer)
		{
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00024660 File Offset: 0x00022860
		public override void Update()
		{
			base.Update();
			base.Context.ServerSettings.CertificateTypes = this.certificateTypes;
			base.Context.ServerSettings.DistinguisedNames = this.distinguisedNames;
			base.Context.ServerSettings.CertificateRequest = true;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000246B0 File Offset: 0x000228B0
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x000246B8 File Offset: 0x000228B8
		protected override void ProcessAsTls1()
		{
			int num = (int)base.ReadByte();
			this.certificateTypes = new ClientCertificateType[num];
			for (int i = 0; i < num; i++)
			{
				this.certificateTypes[i] = (ClientCertificateType)base.ReadByte();
			}
			if (base.ReadInt16() != 0)
			{
				ASN1 asn = new ASN1(base.ReadBytes((int)base.ReadInt16()));
				this.distinguisedNames = new string[asn.Count];
				for (int j = 0; j < asn.Count; j++)
				{
					ASN1 asn2 = new ASN1(asn[j].Value);
					this.distinguisedNames[j] = Encoding.UTF8.GetString(asn2[1].Value);
				}
			}
		}

		// Token: 0x04000320 RID: 800
		private ClientCertificateType[] certificateTypes;

		// Token: 0x04000321 RID: 801
		private string[] distinguisedNames;
	}
}
