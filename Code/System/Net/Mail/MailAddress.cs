using System;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents the address of an electronic mail sender or recipient.</summary>
	// Token: 0x0200033D RID: 829
	public class MailAddress
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address. </summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an e-mail address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.</exception>
		// Token: 0x06001D5F RID: 7519 RVA: 0x0005903C File Offset: 0x0005723C
		public MailAddress(string address) : this(address, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address and display name.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an e-mail address.</param>
		/// <param name="displayName">A <see cref="T:System.String" /> that contains the display name associated with <paramref name="address" />. This parameter can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.-or-<paramref name="address" /> contains non-ASCII characters.</exception>
		// Token: 0x06001D60 RID: 7520 RVA: 0x00059048 File Offset: 0x00057248
		public MailAddress(string address, string displayName) : this(address, displayName, Encoding.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address, display name, and encoding.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an e-mail address.</param>
		/// <param name="displayName">A <see cref="T:System.String" /> that contains the display name associated with <paramref name="address" />.</param>
		/// <param name="displayNameEncoding">The <see cref="T:System.Text.Encoding" /> that defines the character set used for <paramref name="displayName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="displayName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").-or-<paramref name="displayName" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.-or-<paramref name="address" /> contains non-ASCII characters.</exception>
		// Token: 0x06001D61 RID: 7521 RVA: 0x00059058 File Offset: 0x00057258
		public MailAddress(string address, string displayName, Encoding displayNameEncoding)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			int num = address.IndexOf('"');
			if (num == 0)
			{
				int num2 = address.IndexOf('"', num + 1);
				if (num2 == -1)
				{
					throw MailAddress.CreateFormatException();
				}
				this.displayName = address.Substring(num + 1, num2 - 1).Trim();
				address = address.Substring(num2 + 1);
			}
			int num3 = address.IndexOf('<');
			if (num3 != -1)
			{
				if (num3 + 1 >= address.Length)
				{
					throw MailAddress.CreateFormatException();
				}
				int num4 = address.IndexOf('>', num3 + 1);
				if (num4 == -1)
				{
					throw MailAddress.CreateFormatException();
				}
				if (this.displayName == null)
				{
					this.displayName = address.Substring(0, num3).Trim();
				}
				address = address.Substring(++num3, num4 - num3);
			}
			if (displayName != null)
			{
				this.displayName = displayName.Trim();
			}
			this.address = address.Trim();
		}

		/// <summary>Gets the e-mail address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the e-mail address.</returns>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x00059150 File Offset: 0x00057350
		public string Address
		{
			get
			{
				return this.address;
			}
		}

		/// <summary>Gets the display name composed from the display name and address information specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the display name; otherwise, <see cref="F:System.String.Empty" /> ("") if no display name information was specified when this instance was created.</returns>
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x00059158 File Offset: 0x00057358
		public string DisplayName
		{
			get
			{
				if (this.displayName == null)
				{
					return string.Empty;
				}
				return this.displayName;
			}
		}

		/// <summary>Gets the host portion of the address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the host computer that accepts e-mail for the <see cref="P:System.Net.Mail.MailAddress.User" /> property.</returns>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x00059174 File Offset: 0x00057374
		public string Host
		{
			get
			{
				return this.Address.Substring(this.address.IndexOf("@") + 1);
			}
		}

		/// <summary>Gets the user information from the address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the user name portion of the <see cref="P:System.Net.Mail.MailAddress.Address" />.</returns>
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001D65 RID: 7525 RVA: 0x00059194 File Offset: 0x00057394
		public string User
		{
			get
			{
				return this.Address.Substring(0, this.address.IndexOf("@"));
			}
		}

		/// <summary>Compares two mail addresses.</summary>
		/// <returns>true if the two mail addresses are equal; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.Net.Mail.MailAddress" /> instance to compare to the current instance.</param>
		// Token: 0x06001D66 RID: 7526 RVA: 0x000591B4 File Offset: 0x000573B4
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MailAddress);
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000591C4 File Offset: 0x000573C4
		private bool Equals(MailAddress other)
		{
			return other != null && this.Address == other.Address;
		}

		/// <summary>Returns a hash value for a mail address.</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06001D68 RID: 7528 RVA: 0x000591E0 File Offset: 0x000573E0
		public override int GetHashCode()
		{
			return this.address.GetHashCode();
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the contents of this <see cref="T:System.Net.Mail.MailAddress" />.</returns>
		// Token: 0x06001D69 RID: 7529 RVA: 0x000591F0 File Offset: 0x000573F0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.DisplayName != null && this.DisplayName.Length > 0)
			{
				stringBuilder.Append("\"");
				stringBuilder.Append(this.DisplayName);
				stringBuilder.Append("\"");
				stringBuilder.Append(" ");
				stringBuilder.Append("<");
				stringBuilder.Append(this.Address);
				stringBuilder.Append(">");
			}
			else
			{
				stringBuilder.Append(this.Address);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x00059290 File Offset: 0x00057490
		private static FormatException CreateFormatException()
		{
			return new FormatException("The specified string is not in the form required for an e-mail address.");
		}

		// Token: 0x0400123C RID: 4668
		private string address;

		// Token: 0x0400123D RID: 4669
		private string displayName;
	}
}
