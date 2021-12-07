using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;

namespace System.Net.NetworkInformation
{
	/// <summary>Allows security actions for <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> to be applied to code using declarative security.</summary>
	// Token: 0x020003A6 RID: 934
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class NetworkInformationPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationPermissionAttribute" /> class.</summary>
		/// <param name="action">A <see cref="T:System.Security.Permissions.SecurityAction" /> value that specifies the permission behavior.</param>
		// Token: 0x0600208F RID: 8335 RVA: 0x0005FE4C File Offset: 0x0005E04C
		public NetworkInformationPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Creates and returns a new <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> object.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> that corresponds to this attribute.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002090 RID: 8336 RVA: 0x0005FE58 File Offset: 0x0005E058
		[MonoTODO("verify implementation")]
		public override IPermission CreatePermission()
		{
			NetworkInformationAccess networkInformationAccess = NetworkInformationAccess.None;
			string text = this.Access;
			if (text != null)
			{
				if (NetworkInformationPermissionAttribute.<>f__switch$map11 == null)
				{
					NetworkInformationPermissionAttribute.<>f__switch$map11 = new Dictionary<string, int>(2)
					{
						{
							"Read",
							0
						},
						{
							"Full",
							1
						}
					};
				}
				int num;
				if (NetworkInformationPermissionAttribute.<>f__switch$map11.TryGetValue(text, out num))
				{
					if (num != 0)
					{
						if (num == 1)
						{
							networkInformationAccess = (NetworkInformationAccess.Read | NetworkInformationAccess.Ping);
						}
					}
					else
					{
						networkInformationAccess = NetworkInformationAccess.Read;
					}
				}
			}
			return new NetworkInformationPermission(networkInformationAccess);
		}

		/// <summary>Gets or sets the network information access level.</summary>
		/// <returns>A string that specifies the access level.</returns>
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x0005FEDC File Offset: 0x0005E0DC
		// (set) Token: 0x06002092 RID: 8338 RVA: 0x0005FEE4 File Offset: 0x0005E0E4
		public string Access
		{
			get
			{
				return this.access;
			}
			set
			{
				string text = this.access;
				if (text != null)
				{
					if (NetworkInformationPermissionAttribute.<>f__switch$map10 == null)
					{
						NetworkInformationPermissionAttribute.<>f__switch$map10 = new Dictionary<string, int>(3)
						{
							{
								"Read",
								0
							},
							{
								"Full",
								0
							},
							{
								"None",
								0
							}
						};
					}
					int num;
					if (NetworkInformationPermissionAttribute.<>f__switch$map10.TryGetValue(text, out num))
					{
						if (num == 0)
						{
							this.access = value;
							return;
						}
					}
				}
				throw new ArgumentException("Only 'Read', 'Full' and 'None' are allowed");
			}
		}

		// Token: 0x040013D7 RID: 5079
		private string access;
	}
}
