using System;
using System.Runtime.Serialization;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x020000A1 RID: 161
	[Serializable]
	internal sealed class TlsException : Exception
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x00022EDC File Offset: 0x000210DC
		internal TlsException(string message) : base(message)
		{
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00022EE8 File Offset: 0x000210E8
		internal TlsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00022EF4 File Offset: 0x000210F4
		internal TlsException(string message, Exception ex) : base(message, ex)
		{
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00022F00 File Offset: 0x00021100
		internal TlsException(AlertLevel level, AlertDescription description) : this(level, description, Alert.GetAlertMessage(description))
		{
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00022F10 File Offset: 0x00021110
		internal TlsException(AlertLevel level, AlertDescription description, string message) : base(message)
		{
			this.alert = new Alert(level, description);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00022F28 File Offset: 0x00021128
		internal TlsException(AlertDescription description) : this(description, Alert.GetAlertMessage(description))
		{
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00022F38 File Offset: 0x00021138
		internal TlsException(AlertDescription description, string message) : base(message)
		{
			this.alert = new Alert(description);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00022F50 File Offset: 0x00021150
		public Alert Alert
		{
			get
			{
				return this.alert;
			}
		}

		// Token: 0x040002F7 RID: 759
		private Alert alert;
	}
}
