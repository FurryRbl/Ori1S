using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Specifies security actions to control <see cref="T:System.Net.Sockets.Socket" /> connections. This class cannot be inherited.</summary>
	// Token: 0x020003E9 RID: 1001
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SocketPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.SocketPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="T:System.Security.Permissions.SecurityAction" /> value. </exception>
		// Token: 0x060022A3 RID: 8867 RVA: 0x0006542C File Offset: 0x0006362C
		public SocketPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the network access method that is allowed by this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the network access method that is allowed by this instance of <see cref="T:System.Net.SocketPermissionAttribute" />. Valid values are "Accept" and "Connect." </returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Net.SocketPermissionAttribute.Access" /> property is not null when you attempt to set the value. To specify more than one Access method, use an additional attribute declaration statement. </exception>
		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x00065438 File Offset: 0x00063638
		// (set) Token: 0x060022A5 RID: 8869 RVA: 0x00065440 File Offset: 0x00063640
		public string Access
		{
			get
			{
				return this.m_access;
			}
			set
			{
				if (this.m_access != null)
				{
					this.AlreadySet("Access");
				}
				this.m_access = value;
			}
		}

		/// <summary>Gets or sets the DNS host name or IP address that is specified by this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the DNS host name or IP address that is associated with this instance of <see cref="T:System.Net.SocketPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.SocketPermissionAttribute.Host" /> is not null when you attempt to set the value. To specify more than one host, use an additional attribute declaration statement. </exception>
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x00065460 File Offset: 0x00063660
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x00065468 File Offset: 0x00063668
		public string Host
		{
			get
			{
				return this.m_host;
			}
			set
			{
				if (this.m_host != null)
				{
					this.AlreadySet("Host");
				}
				this.m_host = value;
			}
		}

		/// <summary>Gets or sets the port number that is associated with this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the port number that is associated with this instance of <see cref="T:System.Net.SocketPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Net.SocketPermissionAttribute.Port" /> property is null when you attempt to set the value. To specify more than one port, use an additional attribute declaration statement. </exception>
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x00065488 File Offset: 0x00063688
		// (set) Token: 0x060022A9 RID: 8873 RVA: 0x00065490 File Offset: 0x00063690
		public string Port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				if (this.m_port != null)
				{
					this.AlreadySet("Port");
				}
				this.m_port = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Net.TransportType" /> that is specified by this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the <see cref="T:System.Net.TransportType" /> that is associated with this <see cref="T:System.Net.SocketPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.SocketPermissionAttribute.Transport" /> is not null when you attempt to set the value. To specify more than one transport type, use an additional attribute declaration statement. </exception>
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x000654B0 File Offset: 0x000636B0
		// (set) Token: 0x060022AB RID: 8875 RVA: 0x000654B8 File Offset: 0x000636B8
		public string Transport
		{
			get
			{
				return this.m_transport;
			}
			set
			{
				if (this.m_transport != null)
				{
					this.AlreadySet("Transport");
				}
				this.m_transport = value;
			}
		}

		/// <summary>Creates and returns a new instance of the <see cref="T:System.Net.SocketPermission" /> class.</summary>
		/// <returns>An instance of the <see cref="T:System.Net.SocketPermission" /> class that corresponds to the security declaration.</returns>
		/// <exception cref="T:System.ArgumentException">One or more of the current instance's <see cref="P:System.Net.SocketPermissionAttribute.Access" />, <see cref="P:System.Net.SocketPermissionAttribute.Host" />, <see cref="P:System.Net.SocketPermissionAttribute.Transport" />, or <see cref="P:System.Net.SocketPermissionAttribute.Port" /> properties is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060022AC RID: 8876 RVA: 0x000654D8 File Offset: 0x000636D8
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new SocketPermission(PermissionState.Unrestricted);
			}
			string text = string.Empty;
			if (this.m_access == null)
			{
				text += "Access, ";
			}
			if (this.m_host == null)
			{
				text += "Host, ";
			}
			if (this.m_port == null)
			{
				text += "Port, ";
			}
			if (this.m_transport == null)
			{
				text += "Transport, ";
			}
			if (text.Length > 0)
			{
				string text2 = Locale.GetText("The value(s) for {0} must be specified.");
				text = text.Substring(0, text.Length - 2);
				throw new ArgumentException(string.Format(text2, text));
			}
			int num = -1;
			NetworkAccess access;
			if (string.Compare(this.m_access, "Connect", true) == 0)
			{
				access = NetworkAccess.Connect;
			}
			else
			{
				if (string.Compare(this.m_access, "Accept", true) != 0)
				{
					string text3 = Locale.GetText("The parameter value for 'Access', '{1}, is invalid.");
					throw new ArgumentException(string.Format(text3, this.m_access));
				}
				access = NetworkAccess.Accept;
			}
			if (string.Compare(this.m_port, "All", true) != 0)
			{
				try
				{
					num = int.Parse(this.m_port);
				}
				catch
				{
					string text4 = Locale.GetText("The parameter value for 'Port', '{1}, is invalid.");
					throw new ArgumentException(string.Format(text4, this.m_port));
				}
				new IPEndPoint(1L, num);
			}
			TransportType transport;
			try
			{
				transport = (TransportType)((int)Enum.Parse(typeof(TransportType), this.m_transport, true));
			}
			catch
			{
				string text5 = Locale.GetText("The parameter value for 'Transport', '{1}, is invalid.");
				throw new ArgumentException(string.Format(text5, this.m_transport));
			}
			SocketPermission socketPermission = new SocketPermission(PermissionState.None);
			socketPermission.AddPermission(access, transport, this.m_host, num);
			return socketPermission;
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000656DC File Offset: 0x000638DC
		internal void AlreadySet(string property)
		{
			string text = Locale.GetText("The parameter '{0}' can be set only once.");
			throw new ArgumentException(string.Format(text, property), property);
		}

		// Token: 0x04001530 RID: 5424
		private string m_access;

		// Token: 0x04001531 RID: 5425
		private string m_host;

		// Token: 0x04001532 RID: 5426
		private string m_port;

		// Token: 0x04001533 RID: 5427
		private string m_transport;
	}
}
