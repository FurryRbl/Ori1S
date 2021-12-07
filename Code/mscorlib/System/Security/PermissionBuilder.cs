using System;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x0200053C RID: 1340
	internal static class PermissionBuilder
	{
		// Token: 0x060034A9 RID: 13481 RVA: 0x000AC900 File Offset: 0x000AAB00
		public static IPermission Create(string fullname, PermissionState state)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", fullname);
			securityElement.AddAttribute("version", "1");
			if (state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return PermissionBuilder.CreatePermission(fullname, securityElement);
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000AC964 File Offset: 0x000AAB64
		public static IPermission Create(SecurityElement se)
		{
			if (se == null)
			{
				throw new ArgumentNullException("se");
			}
			string text = se.Attribute("class");
			if (text == null || text.Length == 0)
			{
				throw new ArgumentException("class");
			}
			return PermissionBuilder.CreatePermission(text, se);
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000AC9B4 File Offset: 0x000AABB4
		public static IPermission Create(string fullname, SecurityElement se)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			if (se == null)
			{
				throw new ArgumentNullException("se");
			}
			return PermissionBuilder.CreatePermission(fullname, se);
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000AC9E0 File Offset: 0x000AABE0
		public static IPermission Create(Type type)
		{
			return (IPermission)Activator.CreateInstance(type, PermissionBuilder.psNone);
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000AC9F4 File Offset: 0x000AABF4
		internal static IPermission CreatePermission(string fullname, SecurityElement se)
		{
			Type type = Type.GetType(fullname);
			if (type == null)
			{
				string text = Locale.GetText("Can't create an instance of permission class {0}.");
				throw new TypeLoadException(string.Format(text, fullname));
			}
			IPermission permission = PermissionBuilder.Create(type);
			permission.FromXml(se);
			return permission;
		}

		// Token: 0x04001621 RID: 5665
		private static object[] psNone = new object[]
		{
			PermissionState.None
		};
	}
}
