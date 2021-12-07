using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml.Serialization;

namespace System.Diagnostics
{
	/// <summary>Provides an abstract base class to create new debugging and tracing switches.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000250 RID: 592
	public abstract class Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Switch" /> class.</summary>
		/// <param name="displayName">The name of the switch. </param>
		/// <param name="description">The description for the switch. </param>
		// Token: 0x060014CD RID: 5325 RVA: 0x00037204 File Offset: 0x00035404
		protected Switch(string displayName, string description)
		{
			this.name = displayName;
			this.description = description;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Switch" /> class, specifying the display name, description, and default value for the switch. </summary>
		/// <param name="displayName">The name of the switch. </param>
		/// <param name="description">The description of the switch. </param>
		/// <param name="defaultSwitchValue">The default value for the switch.</param>
		// Token: 0x060014CE RID: 5326 RVA: 0x00037228 File Offset: 0x00035428
		protected Switch(string displayName, string description, string defaultSwitchValue) : this(displayName, description)
		{
			this.defaultSwitchValue = defaultSwitchValue;
		}

		/// <summary>Gets a description of the switch.</summary>
		/// <returns>The description of the switch. The default value is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x0003723C File Offset: 0x0003543C
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		/// <summary>Gets a name used to identify the switch.</summary>
		/// <returns>The name used to identify the switch. The default value is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00037244 File Offset: 0x00035444
		public string DisplayName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets the current setting for this switch.</summary>
		/// <returns>The current setting for this switch. The default is zero.</returns>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x0003724C File Offset: 0x0003544C
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x00037280 File Offset: 0x00035480
		protected int SwitchSetting
		{
			get
			{
				if (!this.initialized)
				{
					this.initialized = true;
					this.GetConfigFileSetting();
					this.OnSwitchSettingChanged();
				}
				return this.switchSetting;
			}
			set
			{
				if (this.switchSetting != value)
				{
					this.switchSetting = value;
					this.OnSwitchSettingChanged();
				}
				this.initialized = true;
			}
		}

		/// <summary>Gets the custom switch attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the case-insensitive custom attributes for the trace switch.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x000372B0 File Offset: 0x000354B0
		[XmlIgnore]
		public System.Collections.Specialized.StringDictionary Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the value of the switch.</summary>
		/// <returns>A string representing the value of the switch.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The value is null.-or-The value does not consist solely of an optional negative sign followed by a sequence of digits ranging from 0 to 9.-or-The value represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x000372B8 File Offset: 0x000354B8
		// (set) Token: 0x060014D5 RID: 5333 RVA: 0x000372C0 File Offset: 0x000354C0
		protected string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
				try
				{
					this.OnValueChanged();
				}
				catch (Exception inner)
				{
					string message = string.Format("The config value for Switch '{0}' was invalid.", this.DisplayName);
					throw new ConfigurationErrorsException(message, inner);
				}
			}
		}

		/// <summary>Gets the custom attributes supported by the switch.</summary>
		/// <returns>A string array that contains the names of the custom attributes supported by the switch, or null if there no custom attributes are supported.</returns>
		// Token: 0x060014D6 RID: 5334 RVA: 0x0003731C File Offset: 0x0003551C
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		/// <summary>Invoked when the <see cref="P:System.Diagnostics.Switch.Value" /> property is changed.</summary>
		// Token: 0x060014D7 RID: 5335 RVA: 0x00037320 File Offset: 0x00035520
		protected virtual void OnValueChanged()
		{
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00037324 File Offset: 0x00035524
		private void GetConfigFileSetting()
		{
			IDictionary dictionary = (IDictionary)DiagnosticsConfiguration.Settings["switches"];
			if (dictionary != null && dictionary.Contains(this.name))
			{
				this.Value = (dictionary[this.name] as string);
				return;
			}
			if (this.defaultSwitchValue != null)
			{
				this.value = this.defaultSwitchValue;
				this.OnValueChanged();
			}
		}

		/// <summary>Invoked when the <see cref="P:System.Diagnostics.Switch.SwitchSetting" /> property is changed.</summary>
		// Token: 0x060014D9 RID: 5337 RVA: 0x00037394 File Offset: 0x00035594
		protected virtual void OnSwitchSettingChanged()
		{
		}

		// Token: 0x0400064D RID: 1613
		private string name;

		// Token: 0x0400064E RID: 1614
		private string description;

		// Token: 0x0400064F RID: 1615
		private int switchSetting;

		// Token: 0x04000650 RID: 1616
		private string value;

		// Token: 0x04000651 RID: 1617
		private string defaultSwitchValue;

		// Token: 0x04000652 RID: 1618
		private bool initialized;

		// Token: 0x04000653 RID: 1619
		private System.Collections.Specialized.StringDictionary attributes = new System.Collections.Specialized.StringDictionary();
	}
}
