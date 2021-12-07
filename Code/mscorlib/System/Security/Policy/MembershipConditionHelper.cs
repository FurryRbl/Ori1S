using System;

namespace System.Security.Policy
{
	// Token: 0x02000647 RID: 1607
	internal sealed class MembershipConditionHelper
	{
		// Token: 0x06003D1D RID: 15645 RVA: 0x000D1E24 File Offset: 0x000D0024
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != MembershipConditionHelper.XmlTag)
			{
				string message = string.Format(Locale.GetText("Invalid tag {0}, expected {1}."), se.Tag, MembershipConditionHelper.XmlTag);
				throw new ArgumentException(message, parameterName);
			}
			int result = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					result = int.Parse(text);
				}
				catch (Exception innerException)
				{
					string text2 = Locale.GetText("Couldn't parse version from '{0}'.");
					text2 = string.Format(text2, text);
					throw new ArgumentException(text2, parameterName, innerException);
				}
			}
			return result;
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x000D1EDC File Offset: 0x000D00DC
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement(MembershipConditionHelper.XmlTag);
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x04001A79 RID: 6777
		private static readonly string XmlTag = "IMembershipCondition";
	}
}
