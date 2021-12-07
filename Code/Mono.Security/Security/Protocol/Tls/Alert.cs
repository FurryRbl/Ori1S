using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200007E RID: 126
	internal class Alert
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x0001C148 File Offset: 0x0001A348
		public Alert(AlertDescription description)
		{
			this.inferAlertLevel();
			this.description = description;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001C160 File Offset: 0x0001A360
		public Alert(AlertLevel level, AlertDescription description)
		{
			this.level = level;
			this.description = description;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001C178 File Offset: 0x0001A378
		public AlertLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0001C180 File Offset: 0x0001A380
		public AlertDescription Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0001C188 File Offset: 0x0001A388
		public string Message
		{
			get
			{
				return Alert.GetAlertMessage(this.description);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0001C198 File Offset: 0x0001A398
		public bool IsWarning
		{
			get
			{
				return this.level == AlertLevel.Warning;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
		public bool IsCloseNotify
		{
			get
			{
				return this.IsWarning && this.description == AlertDescription.CloseNotify;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001C1CC File Offset: 0x0001A3CC
		private void inferAlertLevel()
		{
			AlertDescription alertDescription = this.description;
			switch (alertDescription)
			{
			case AlertDescription.HandshakeFailiure:
			case AlertDescription.BadCertificate:
			case AlertDescription.UnsupportedCertificate:
			case AlertDescription.CertificateRevoked:
			case AlertDescription.CertificateExpired:
			case AlertDescription.CertificateUnknown:
			case AlertDescription.IlegalParameter:
			case AlertDescription.UnknownCA:
			case AlertDescription.AccessDenied:
			case AlertDescription.DecodeError:
			case AlertDescription.DecryptError:
			case AlertDescription.ExportRestriction:
				break;
			default:
				switch (alertDescription)
				{
				case AlertDescription.BadRecordMAC:
				case AlertDescription.DecryptionFailed:
				case AlertDescription.RecordOverflow:
					break;
				default:
					if (alertDescription != AlertDescription.ProtocolVersion && alertDescription != AlertDescription.InsuficientSecurity)
					{
						if (alertDescription != AlertDescription.CloseNotify)
						{
							if (alertDescription == AlertDescription.UnexpectedMessage || alertDescription == AlertDescription.DecompressionFailiure || alertDescription == AlertDescription.InternalError)
							{
								break;
							}
							if (alertDescription != AlertDescription.UserCancelled && alertDescription != AlertDescription.NoRenegotiation)
							{
								break;
							}
						}
						this.level = AlertLevel.Warning;
						return;
					}
					break;
				}
				break;
			}
			this.level = AlertLevel.Fatal;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001C2B0 File Offset: 0x0001A4B0
		public static string GetAlertMessage(AlertDescription description)
		{
			return "The authentication or decryption has failed.";
		}

		// Token: 0x04000232 RID: 562
		private AlertLevel level;

		// Token: 0x04000233 RID: 563
		private AlertDescription description;
	}
}
