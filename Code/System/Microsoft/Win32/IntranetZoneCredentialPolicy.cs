﻿using System;
using System.Net;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace Microsoft.Win32
{
	/// <summary>Defines a credential policy to be used for resource requests that are made using <see cref="T:System.Net.WebRequest" /> and its derived classes.</summary>
	// Token: 0x02000012 RID: 18
	public class IntranetZoneCredentialPolicy : System.Net.ICredentialPolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.IntranetZoneCredentialPolicy" /> class.</summary>
		// Token: 0x060000F0 RID: 240 RVA: 0x00009DFC File Offset: 0x00007FFC
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
		public IntranetZoneCredentialPolicy()
		{
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether the client's credentials are sent with a request for a resource that was made using <see cref="T:System.Net.WebRequest" />.</summary>
		/// <returns>true if the requested resource is in the same domain as the client making the request; otherwise, false.</returns>
		/// <param name="challengeUri">The <see cref="T:System.Uri" /> that will receive the request.</param>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> that represents the resource being requested.</param>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> that will be sent with the request if this method returns true.</param>
		/// <param name="authModule">The <see cref="T:System.Net.IAuthenticationModule" /> that will conduct the authentication, if authentication is required.</param>
		// Token: 0x060000F1 RID: 241 RVA: 0x00009E04 File Offset: 0x00008004
		public virtual bool ShouldSendCredential(System.Uri challengeUri, System.Net.WebRequest request, System.Net.NetworkCredential credential, System.Net.IAuthenticationModule authenticationModule)
		{
			Zone zone = Zone.CreateFromUrl(challengeUri.AbsoluteUri);
			return zone.SecurityZone == SecurityZone.Intranet;
		}
	}
}
