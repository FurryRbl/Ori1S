using System;

namespace System.Text
{
	/// <summary>Provides basic information about an encoding.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000684 RID: 1668
	[Serializable]
	public sealed class EncodingInfo
	{
		// Token: 0x06003F63 RID: 16227 RVA: 0x000D95A4 File Offset: 0x000D77A4
		internal EncodingInfo(int cp)
		{
			this.codepage = cp;
		}

		/// <summary>Gets the code page identifier of the encoding.</summary>
		/// <returns>The code page identifier of the encoding.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x000D95B4 File Offset: 0x000D77B4
		public int CodePage
		{
			get
			{
				return this.codepage;
			}
		}

		/// <summary>Gets the human-readable description of the encoding.</summary>
		/// <returns>The human-readable description of the encoding.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x000D95BC File Offset: 0x000D77BC
		[MonoTODO]
		public string DisplayName
		{
			get
			{
				return this.Name;
			}
		}

		/// <summary>Gets the name registered with the Internet Assigned Numbers Authority (IANA) for the encoding.</summary>
		/// <returns>The IANA name for the encoding. For more information about the IANA, see www.iana.org.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x000D95C4 File Offset: 0x000D77C4
		public string Name
		{
			get
			{
				if (this.encoding == null)
				{
					this.encoding = this.GetEncoding();
				}
				return this.encoding.WebName;
			}
		}

		/// <summary>Gets a value indicating whether the specified object is equal to the current <see cref="T:System.Text.EncodingInfo" /> object.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Text.EncodingInfo" /> object and is equal to the current <see cref="T:System.Text.EncodingInfo" /> object; otherwise, false.</returns>
		/// <param name="value">An object to compare to the current <see cref="T:System.Text.EncodingInfo" /> object.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F67 RID: 16231 RVA: 0x000D95F4 File Offset: 0x000D77F4
		public override bool Equals(object value)
		{
			EncodingInfo encodingInfo = value as EncodingInfo;
			return encodingInfo != null && encodingInfo.codepage == this.codepage;
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Text.EncodingInfo" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F68 RID: 16232 RVA: 0x000D9620 File Offset: 0x000D7820
		public override int GetHashCode()
		{
			return this.codepage;
		}

		/// <summary>Returns a <see cref="T:System.Text.Encoding" /> object that corresponds to the current <see cref="T:System.Text.EncodingInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.Encoding" /> object that corresponds to the current <see cref="T:System.Text.EncodingInfo" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003F69 RID: 16233 RVA: 0x000D9628 File Offset: 0x000D7828
		public Encoding GetEncoding()
		{
			return Encoding.GetEncoding(this.codepage);
		}

		// Token: 0x04001B6B RID: 7019
		private readonly int codepage;

		// Token: 0x04001B6C RID: 7020
		private Encoding encoding;
	}
}
