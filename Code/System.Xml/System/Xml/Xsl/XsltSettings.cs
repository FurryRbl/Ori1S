using System;

namespace System.Xml.Xsl
{
	/// <summary>Specifies the XSLT features to support during execution of the XSLT style sheet.</summary>
	// Token: 0x020001B8 RID: 440
	public sealed class XsltSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltSettings" /> class with default settings.</summary>
		// Token: 0x060011FE RID: 4606 RVA: 0x00051A60 File Offset: 0x0004FC60
		public XsltSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltSettings" /> class with the specified settings.</summary>
		/// <param name="enableDocumentFunction">true to enable support for the XSLT document() function; otherwise, false.</param>
		/// <param name="enableScript">true to enable support for embedded scripts blocks; otherwise, false.</param>
		// Token: 0x060011FF RID: 4607 RVA: 0x00051A68 File Offset: 0x0004FC68
		public XsltSettings(bool enableDocumentFunction, bool enableScript)
		{
			this.enableDocument = enableDocumentFunction;
			this.enableScript = enableScript;
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00051A80 File Offset: 0x0004FC80
		private XsltSettings(bool readOnly)
		{
			this.readOnly = readOnly;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00051A90 File Offset: 0x0004FC90
		static XsltSettings()
		{
			XsltSettings.trustedXslt.enableDocument = true;
			XsltSettings.trustedXslt.enableScript = true;
		}

		/// <summary>Gets an <see cref="T:System.Xml.Xsl.XsltSettings" /> object with default settings. Support for the XSLT document() function and embedded script blocks is disabled.</summary>
		/// <returns>An <see cref="T:System.Xml.Xsl.XsltSettings" /> object with the <see cref="P:System.Xml.Xsl.XsltSettings.EnableDocumentFunction" /> and <see cref="P:System.Xml.Xsl.XsltSettings.EnableScript" /> properties set to false.</returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x00051ACC File Offset: 0x0004FCCC
		public static XsltSettings Default
		{
			get
			{
				return XsltSettings.defaultSettings;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Xsl.XsltSettings" /> object that enables support for the XSLT document() function and embedded script blocks.</summary>
		/// <returns>An <see cref="T:System.Xml.Xsl.XsltSettings" /> object with the <see cref="P:System.Xml.Xsl.XsltSettings.EnableDocumentFunction" /> and <see cref="P:System.Xml.Xsl.XsltSettings.EnableScript" /> properties set to true.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00051AD4 File Offset: 0x0004FCD4
		public static XsltSettings TrustedXslt
		{
			get
			{
				return XsltSettings.trustedXslt;
			}
		}

		/// <summary>Gets or sets a value indicating whether to enable support for the XSLT document() function.</summary>
		/// <returns>true to support the XSLT document() function; otherwise, false. The default is false.</returns>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x00051ADC File Offset: 0x0004FCDC
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x00051AE4 File Offset: 0x0004FCE4
		public bool EnableDocumentFunction
		{
			get
			{
				return this.enableDocument;
			}
			set
			{
				if (!this.readOnly)
				{
					this.enableDocument = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether to enable support for embedded script blocks.</summary>
		/// <returns>true to support script blocks in XSLT style sheets; otherwise, false. The default is false.</returns>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x00051AF8 File Offset: 0x0004FCF8
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x00051B00 File Offset: 0x0004FD00
		public bool EnableScript
		{
			get
			{
				return this.enableScript;
			}
			set
			{
				if (!this.readOnly)
				{
					this.enableScript = value;
				}
			}
		}

		// Token: 0x0400075E RID: 1886
		private static readonly XsltSettings defaultSettings = new XsltSettings(true);

		// Token: 0x0400075F RID: 1887
		private static readonly XsltSettings trustedXslt = new XsltSettings(true);

		// Token: 0x04000760 RID: 1888
		private bool readOnly;

		// Token: 0x04000761 RID: 1889
		private bool enableDocument;

		// Token: 0x04000762 RID: 1890
		private bool enableScript;
	}
}
