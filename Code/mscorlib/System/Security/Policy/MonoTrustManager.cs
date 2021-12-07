using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000648 RID: 1608
	internal class MonoTrustManager : ISecurityEncodable, IApplicationTrustManager
	{
		// Token: 0x06003D20 RID: 15648 RVA: 0x000D1F40 File Offset: 0x000D0140
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
		public ApplicationTrust DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			return null;
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x000D1F54 File Offset: 0x000D0154
		public void FromXml(SecurityElement e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (e.Tag != "IApplicationTrustManager")
			{
				throw new ArgumentException("e", Locale.GetText("Invalid XML tag."));
			}
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x000D1F94 File Offset: 0x000D0194
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IApplicationTrustManager");
			securityElement.AddAttribute("class", typeof(MonoTrustManager).AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x04001A7A RID: 6778
		private const string tag = "IApplicationTrustManager";
	}
}
