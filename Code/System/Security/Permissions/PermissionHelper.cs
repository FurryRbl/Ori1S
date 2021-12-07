using System;
using System.Globalization;

namespace System.Security.Permissions
{
	// Token: 0x0200045B RID: 1115
	internal sealed class PermissionHelper
	{
		// Token: 0x06002802 RID: 10242 RVA: 0x0007E9D0 File Offset: 0x0007CBD0
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x0007EA2C File Offset: 0x0007CC2C
		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			if (state != PermissionState.None)
			{
				if (state != PermissionState.Unrestricted)
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), state);
					throw new ArgumentException(message, "state");
				}
			}
			return state;
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x0007EA7C File Offset: 0x0007CC7C
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Attribute("class") == null)
			{
				string text = Locale.GetText("Missing 'class' attribute.");
				throw new ArgumentException(text, parameterName);
			}
			int num = minimumVersion;
			string text2 = se.Attribute("version");
			if (text2 != null)
			{
				try
				{
					num = int.Parse(text2);
				}
				catch (Exception innerException)
				{
					string text3 = Locale.GetText("Couldn't parse version from '{0}'.");
					text3 = string.Format(text3, text2);
					throw new ArgumentException(text3, parameterName, innerException);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				string text4 = Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}'].");
				text4 = string.Format(text4, num, minimumVersion, maximumVersion);
				throw new ArgumentException(text4, parameterName);
			}
			return num;
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x0007EB5C File Offset: 0x0007CD5C
		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			return text != null && string.Compare(text, bool.TrueString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x0007EB94 File Offset: 0x0007CD94
		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			string text = Locale.GetText("Invalid permission type '{0}', expected type '{1}'.");
			text = string.Format(text, target.GetType(), expected);
			throw new ArgumentException(text, "target");
		}
	}
}
