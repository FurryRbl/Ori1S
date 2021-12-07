using System;
using System.Collections;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Controls rights to make or accept connections on a transport address.</summary>
	// Token: 0x020003EA RID: 1002
	[Serializable]
	public sealed class SocketPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.SocketPermission" /> class that allows unrestricted access to the <see cref="T:System.Net.Sockets.Socket" /> or disallows access to the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x060022AE RID: 8878 RVA: 0x00065704 File Offset: 0x00063904
		public SocketPermission(PermissionState state)
		{
			this.m_noRestriction = (state == PermissionState.Unrestricted);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.SocketPermission" /> class for the given transport address with the specified permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkAccess" /> values. </param>
		/// <param name="transport">One of the <see cref="T:System.Net.TransportType" /> values. </param>
		/// <param name="hostName">The host name for the transport address. </param>
		/// <param name="portNumber">The port number for the transport address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is null. </exception>
		// Token: 0x060022AF RID: 8879 RVA: 0x00065738 File Offset: 0x00063938
		public SocketPermission(NetworkAccess access, TransportType transport, string hostName, int portNumber)
		{
			this.m_noRestriction = false;
			this.AddPermission(access, transport, hostName, portNumber);
		}

		/// <summary>Gets a list of <see cref="T:System.Net.EndpointPermission" /> instances that identifies the endpoints that can be accepted under this permission instance.</summary>
		/// <returns>An instance that implements the <see cref="T:System.Collections.IEnumerator" /> interface that contains <see cref="T:System.Net.EndpointPermission" /> instances.</returns>
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x00065774 File Offset: 0x00063974
		public IEnumerator AcceptList
		{
			get
			{
				return this.m_acceptList.GetEnumerator();
			}
		}

		/// <summary>Gets a list of <see cref="T:System.Net.EndpointPermission" /> instances that identifies the endpoints that can be connected to under this permission instance.</summary>
		/// <returns>An instance that implements the <see cref="T:System.Collections.IEnumerator" /> interface that contains <see cref="T:System.Net.EndpointPermission" /> instances.</returns>
		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x00065784 File Offset: 0x00063984
		public IEnumerator ConnectList
		{
			get
			{
				return this.m_connectList.GetEnumerator();
			}
		}

		/// <summary>Adds a permission to the set of permissions for a transport address.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkAccess" /> values. </param>
		/// <param name="transport">One of the <see cref="T:System.Net.TransportType" /> values. </param>
		/// <param name="hostName">The host name for the transport address. </param>
		/// <param name="portNumber">The port number for the transport address. </param>
		// Token: 0x060022B2 RID: 8882 RVA: 0x00065794 File Offset: 0x00063994
		public void AddPermission(NetworkAccess access, TransportType transport, string hostName, int portNumber)
		{
			if (this.m_noRestriction)
			{
				return;
			}
			EndpointPermission value = new EndpointPermission(hostName, portNumber, transport);
			if (access == NetworkAccess.Accept)
			{
				this.m_acceptList.Add(value);
			}
			else
			{
				this.m_connectList.Add(value);
			}
		}

		/// <summary>Creates a copy of a <see cref="T:System.Net.SocketPermission" /> instance.</summary>
		/// <returns>A new instance of the <see cref="T:System.Net.SocketPermission" /> class that is a copy of the current instance.</returns>
		// Token: 0x060022B3 RID: 8883 RVA: 0x000657E4 File Offset: 0x000639E4
		public override IPermission Copy()
		{
			return new SocketPermission((!this.m_noRestriction) ? PermissionState.None : PermissionState.Unrestricted)
			{
				m_connectList = (ArrayList)this.m_connectList.Clone(),
				m_acceptList = (ArrayList)this.m_acceptList.Clone()
			};
		}

		/// <summary>Returns the logical intersection between two <see cref="T:System.Net.SocketPermission" /> instances.</summary>
		/// <returns>The <see cref="T:System.Net.SocketPermission" /> instance that represents the intersection of two <see cref="T:System.Net.SocketPermission" /> instances. If the intersection is empty, the method returns null. If the <paramref name="target" /> parameter is a null reference, the method returns null.</returns>
		/// <param name="target">The <see cref="T:System.Net.SocketPermission" /> instance to intersect with the current instance. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not a <see cref="T:System.Net.SocketPermission" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Net.DnsPermission" /> is not granted to the method caller. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060022B4 RID: 8884 RVA: 0x00065838 File Offset: 0x00063A38
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException("Argument not of type SocketPermission");
			}
			if (this.m_noRestriction)
			{
				IPermission result;
				if (this.IntersectEmpty(socketPermission))
				{
					IPermission permission = null;
					result = permission;
				}
				else
				{
					result = socketPermission.Copy();
				}
				return result;
			}
			if (socketPermission.m_noRestriction)
			{
				IPermission result2;
				if (this.IntersectEmpty(this))
				{
					IPermission permission = null;
					result2 = permission;
				}
				else
				{
					result2 = this.Copy();
				}
				return result2;
			}
			SocketPermission socketPermission2 = new SocketPermission(PermissionState.None);
			this.Intersect(this.m_connectList, socketPermission.m_connectList, socketPermission2.m_connectList);
			this.Intersect(this.m_acceptList, socketPermission.m_acceptList, socketPermission2.m_acceptList);
			return (!this.IntersectEmpty(socketPermission2)) ? socketPermission2 : null;
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x000658FC File Offset: 0x00063AFC
		private bool IntersectEmpty(SocketPermission permission)
		{
			return !permission.m_noRestriction && permission.m_connectList.Count == 0 && permission.m_acceptList.Count == 0;
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x00065938 File Offset: 0x00063B38
		private void Intersect(ArrayList list1, ArrayList list2, ArrayList result)
		{
			foreach (object obj in list1)
			{
				EndpointPermission endpointPermission = (EndpointPermission)obj;
				foreach (object obj2 in list2)
				{
					EndpointPermission perm = (EndpointPermission)obj2;
					EndpointPermission endpointPermission2 = endpointPermission.Intersect(perm);
					if (endpointPermission2 != null)
					{
						bool flag = false;
						for (int i = 0; i < result.Count; i++)
						{
							EndpointPermission perm2 = (EndpointPermission)result[i];
							EndpointPermission endpointPermission3 = endpointPermission2.Intersect(perm2);
							if (endpointPermission3 != null)
							{
								result[i] = endpointPermission3;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							result.Add(endpointPermission2);
						}
					}
				}
			}
		}

		/// <summary>Determines if the current permission is a subset of the specified permission.</summary>
		/// <returns>If <paramref name="target" /> is null, this method returns true if the current instance defines no permissions; otherwise, false. If <paramref name="target" /> is not null, this method returns true if the current instance defines a subset of <paramref name="target" /> permissions; otherwise, false.</returns>
		/// <param name="target">A <see cref="T:System.Net.SocketPermission" /> that is to be tested for the subset relationship. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a <see cref="T:System.Net.Sockets.SocketException" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Net.DnsPermission" /> is not granted to the method caller. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060022B7 RID: 8887 RVA: 0x00065A64 File Offset: 0x00063C64
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_noRestriction && this.m_connectList.Count == 0 && this.m_acceptList.Count == 0;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException("Parameter target must be of type SocketPermission");
			}
			return socketPermission.m_noRestriction || (!this.m_noRestriction && ((this.m_acceptList.Count == 0 && this.m_connectList.Count == 0) || ((socketPermission.m_acceptList.Count != 0 || socketPermission.m_connectList.Count != 0) && this.IsSubsetOf(this.m_connectList, socketPermission.m_connectList) && this.IsSubsetOf(this.m_acceptList, socketPermission.m_acceptList))));
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x00065B48 File Offset: 0x00063D48
		private bool IsSubsetOf(ArrayList list1, ArrayList list2)
		{
			foreach (object obj in list1)
			{
				EndpointPermission endpointPermission = (EndpointPermission)obj;
				bool flag = false;
				foreach (object obj2 in list2)
				{
					EndpointPermission perm = (EndpointPermission)obj2;
					if (endpointPermission.IsSubsetOf(perm))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Checks the overall permission state of the object.</summary>
		/// <returns>true if the <see cref="T:System.Net.SocketPermission" /> instance is created with the Unrestricted value from <see cref="T:System.Security.Permissions.PermissionState" />; otherwise, false.</returns>
		// Token: 0x060022B9 RID: 8889 RVA: 0x00065C30 File Offset: 0x00063E30
		public bool IsUnrestricted()
		{
			return this.m_noRestriction;
		}

		/// <summary>Creates an XML encoding of a <see cref="T:System.Net.SocketPermission" /> instance and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> instance that contains an XML-encoded representation of the <see cref="T:System.Net.SocketPermission" /> instance, including state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060022BA RID: 8890 RVA: 0x00065C38 File Offset: 0x00063E38
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			if (this.m_noRestriction)
			{
				securityElement.AddAttribute("Unrestricted", "true");
				return securityElement;
			}
			if (this.m_connectList.Count > 0)
			{
				this.ToXml(securityElement, "ConnectAccess", this.m_connectList.GetEnumerator());
			}
			if (this.m_acceptList.Count > 0)
			{
				this.ToXml(securityElement, "AcceptAccess", this.m_acceptList.GetEnumerator());
			}
			return securityElement;
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00065CE4 File Offset: 0x00063EE4
		private void ToXml(SecurityElement root, string childName, IEnumerator enumerator)
		{
			SecurityElement securityElement = new SecurityElement(childName);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				EndpointPermission endpointPermission = obj as EndpointPermission;
				SecurityElement securityElement2 = new SecurityElement("ENDPOINT");
				securityElement2.AddAttribute("host", endpointPermission.Hostname);
				securityElement2.AddAttribute("transport", endpointPermission.Transport.ToString());
				securityElement2.AddAttribute("port", (endpointPermission.Port != -1) ? endpointPermission.Port.ToString() : "All");
				securityElement.AddChild(securityElement2);
			}
			root.AddChild(securityElement);
		}

		/// <summary>Reconstructs a <see cref="T:System.Net.SocketPermission" /> instance for an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding used to reconstruct the <see cref="T:System.Net.SocketPermission" /> instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="securityElement" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="securityElement" /> is not a permission element for this type. </exception>
		// Token: 0x060022BC RID: 8892 RVA: 0x00065D88 File Offset: 0x00063F88
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException("securityElement");
			}
			string text = securityElement.Attribute("Unrestricted");
			if (text != null)
			{
				this.m_noRestriction = (string.Compare(text, "true", true) == 0);
				if (this.m_noRestriction)
				{
					return;
				}
			}
			this.m_noRestriction = false;
			this.m_connectList = new ArrayList();
			this.m_acceptList = new ArrayList();
			ArrayList children = securityElement.Children;
			foreach (object obj in children)
			{
				SecurityElement securityElement2 = (SecurityElement)obj;
				if (securityElement2.Tag == "ConnectAccess")
				{
					this.FromXml(securityElement2.Children, NetworkAccess.Connect);
				}
				else if (securityElement2.Tag == "AcceptAccess")
				{
					this.FromXml(securityElement2.Children, NetworkAccess.Accept);
				}
			}
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x00065EC4 File Offset: 0x000640C4
		private void FromXml(ArrayList endpoints, NetworkAccess access)
		{
			foreach (object obj in endpoints)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				if (!(securityElement.Tag != "ENDPOINT"))
				{
					string hostName = securityElement.Attribute("host");
					TransportType transport = (TransportType)((int)Enum.Parse(typeof(TransportType), securityElement.Attribute("transport"), true));
					string text = securityElement.Attribute("port");
					int portNumber;
					if (text == "All")
					{
						portNumber = -1;
					}
					else
					{
						portNumber = int.Parse(text);
					}
					this.AddPermission(access, transport, hostName, portNumber);
				}
			}
		}

		/// <summary>Returns the logical union between two <see cref="T:System.Net.SocketPermission" /> instances.</summary>
		/// <returns>The <see cref="T:System.Net.SocketPermission" /> instance that represents the union of two <see cref="T:System.Net.SocketPermission" /> instances. If <paramref name="target" /> parameter is null, it returns a copy of the current instance.</returns>
		/// <param name="target">The <see cref="T:System.Net.SocketPermission" /> instance to combine with the current instance. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a <see cref="T:System.Net.SocketPermission" />. </exception>
		// Token: 0x060022BE RID: 8894 RVA: 0x00065FB0 File Offset: 0x000641B0
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException("Argument not of type SocketPermission");
			}
			if (this.m_noRestriction || socketPermission.m_noRestriction)
			{
				return new SocketPermission(PermissionState.Unrestricted);
			}
			SocketPermission socketPermission2 = (SocketPermission)socketPermission.Copy();
			socketPermission2.m_acceptList.InsertRange(socketPermission2.m_acceptList.Count, this.m_acceptList);
			socketPermission2.m_connectList.InsertRange(socketPermission2.m_connectList.Count, this.m_connectList);
			return socketPermission2;
		}

		/// <summary>Defines a constant that represents all ports.</summary>
		// Token: 0x04001534 RID: 5428
		public const int AllPorts = -1;

		// Token: 0x04001535 RID: 5429
		private ArrayList m_acceptList = new ArrayList();

		// Token: 0x04001536 RID: 5430
		private ArrayList m_connectList = new ArrayList();

		// Token: 0x04001537 RID: 5431
		private bool m_noRestriction;
	}
}
