using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Provides the application directory as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200062D RID: 1581
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationDirectory : IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationDirectory" /> class.</summary>
		/// <param name="name">The path of the application directory. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003C2E RID: 15406 RVA: 0x000CED6C File Offset: 0x000CCF6C
		public ApplicationDirectory(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length < 1)
			{
				throw new FormatException(Locale.GetText("Empty"));
			}
			this.directory = name;
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x000CEDB4 File Offset: 0x000CCFB4
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return ((!verbose) ? 1 : 3) + this.directory.Length;
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x000CEDD0 File Offset: 0x000CCFD0
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x000CEDD4 File Offset: 0x000CCFD4
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		/// <summary>Gets the path of the application directory.</summary>
		/// <returns>The path of the application directory.</returns>
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06003C32 RID: 15410 RVA: 0x000CEDD8 File Offset: 0x000CCFD8
		public string Directory
		{
			get
			{
				return this.directory;
			}
		}

		/// <summary>Creates a new copy of the <see cref="T:System.Security.Policy.ApplicationDirectory" />.</summary>
		/// <returns>A new, identical copy of the <see cref="T:System.Security.Policy.ApplicationDirectory" />.</returns>
		// Token: 0x06003C33 RID: 15411 RVA: 0x000CEDE0 File Offset: 0x000CCFE0
		public object Copy()
		{
			return new ApplicationDirectory(this.Directory);
		}

		/// <summary>Determines whether instances of the same type of an evidence object are equivalent.</summary>
		/// <returns>true if the two instances are equivalent; otherwise, false.</returns>
		/// <param name="o">An object of same type as the current evidence object. </param>
		// Token: 0x06003C34 RID: 15412 RVA: 0x000CEDF0 File Offset: 0x000CCFF0
		public override bool Equals(object o)
		{
			ApplicationDirectory applicationDirectory = o as ApplicationDirectory;
			if (applicationDirectory != null)
			{
				this.ThrowOnInvalid(applicationDirectory.directory);
				return this.directory == applicationDirectory.directory;
			}
			return false;
		}

		/// <summary>Gets the hash code of the current application directory.</summary>
		/// <returns>The hash code of the current application directory.</returns>
		// Token: 0x06003C35 RID: 15413 RVA: 0x000CEE2C File Offset: 0x000CD02C
		public override int GetHashCode()
		{
			return this.Directory.GetHashCode();
		}

		/// <summary>Gets a string representation of the state of the <see cref="T:System.Security.Policy.ApplicationDirectory" /> evidence object.</summary>
		/// <returns>A representation of the state of the <see cref="T:System.Security.Policy.ApplicationDirectory" /> evidence object.</returns>
		// Token: 0x06003C36 RID: 15414 RVA: 0x000CEE3C File Offset: 0x000CD03C
		public override string ToString()
		{
			this.ThrowOnInvalid(this.Directory);
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.ApplicationDirectory");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Directory", this.directory));
			return securityElement.ToString();
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x000CEE8C File Offset: 0x000CD08C
		private void ThrowOnInvalid(string appdir)
		{
			if (appdir.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				string text = Locale.GetText("Invalid character(s) in directory {0}");
				throw new ArgumentException(string.Format(text, appdir), "other");
			}
		}

		// Token: 0x04001A23 RID: 6691
		private string directory;
	}
}
