using System;
using System.Collections.Specialized;
using System.Reflection;

namespace System.Configuration
{
	/// <summary>Provides a method for reading values of a particular type from the configuration.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C9 RID: 457
	public class AppSettingsReader
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.AppSettingsReader" /> class.</summary>
		// Token: 0x06001008 RID: 4104 RVA: 0x0002A578 File Offset: 0x00028778
		public AppSettingsReader()
		{
			this.appSettings = ConfigurationSettings.AppSettings;
		}

		/// <summary>Gets the value for a specified key from the <see cref="P:System.Configuration.ConfigurationSettings.AppSettings" /> property and returns an object of the specified type containing the value from the configuration.</summary>
		/// <returns>The value of the specified key.</returns>
		/// <param name="key">The key for which to get the value.</param>
		/// <param name="type">The type of the object to return.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.- or -<paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="key" /> does not exist in the &lt;appSettings&gt; configuration section.- or -The value in the &lt;appSettings&gt; configuration section for <paramref name="key" /> is not of type <paramref name="type" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001009 RID: 4105 RVA: 0x0002A58C File Offset: 0x0002878C
		public object GetValue(string key, Type type)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string text = this.appSettings[key];
			if (text == null)
			{
				throw new InvalidOperationException("'" + key + "' could not be found.");
			}
			if (type == typeof(string))
			{
				return text;
			}
			MethodInfo method = type.GetMethod("Parse", new Type[]
			{
				typeof(string)
			});
			if (method == null)
			{
				throw new InvalidOperationException("Type " + type + " does not have a Parse method");
			}
			object result = null;
			try
			{
				result = method.Invoke(null, new object[]
				{
					text
				});
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("Parse error.", innerException);
			}
			return result;
		}

		// Token: 0x04000477 RID: 1143
		private System.Collections.Specialized.NameValueCollection appSettings;
	}
}
