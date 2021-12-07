using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the Visual Studio Custom Action Installer or the Installer Tool (Installutil.exe) should be invoked when the assembly is installed.</summary>
	// Token: 0x020001A3 RID: 419
	[AttributeUsage(AttributeTargets.Class)]
	public class RunInstallerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RunInstallerAttribute" /> class.</summary>
		/// <param name="runInstaller">true if an installer should be invoked during installation of an assembly; otherwise, false. </param>
		// Token: 0x06000EBE RID: 3774 RVA: 0x000264B0 File Offset: 0x000246B0
		public RunInstallerAttribute(bool runInstaller)
		{
			this.runInstaller = runInstaller;
		}

		/// <summary>Determines whether the value of the specified <see cref="T:System.ComponentModel.RunInstallerAttribute" /> is equivalent to the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.RunInstallerAttribute" /> is equal to the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x06000EC0 RID: 3776 RVA: 0x000264E4 File Offset: 0x000246E4
		public override bool Equals(object obj)
		{
			return obj is RunInstallerAttribute && ((RunInstallerAttribute)obj).RunInstaller.Equals(this.runInstaller);
		}

		/// <summary>Generates a hash code for the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</returns>
		// Token: 0x06000EC1 RID: 3777 RVA: 0x00026518 File Offset: 0x00024718
		public override int GetHashCode()
		{
			return this.runInstaller.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000EC2 RID: 3778 RVA: 0x00026528 File Offset: 0x00024728
		public override bool IsDefaultAttribute()
		{
			return this.Equals(RunInstallerAttribute.Default);
		}

		/// <summary>Gets a value indicating whether an installer should be invoked during installation of an assembly.</summary>
		/// <returns>true if an installer should be invoked during installation of an assembly; otherwise, false.</returns>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00026538 File Offset: 0x00024738
		public bool RunInstaller
		{
			get
			{
				return this.runInstaller;
			}
		}

		/// <summary>Specifies that the Visual Studio Custom Action Installer or the Installer Tool (Installutil.exe) should be invoked when the assembly is installed. This static field is read-only.</summary>
		// Token: 0x0400042E RID: 1070
		public static readonly RunInstallerAttribute Yes = new RunInstallerAttribute(true);

		/// <summary>Specifies that the Visual Studio Custom Action Installer or the Installer Tool (Installutil.exe) should not be invoked when the assembly is installed. This static field is read-only.</summary>
		// Token: 0x0400042F RID: 1071
		public static readonly RunInstallerAttribute No = new RunInstallerAttribute(false);

		/// <summary>Specifies the default visiblity, which is <see cref="F:System.ComponentModel.RunInstallerAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x04000430 RID: 1072
		public static readonly RunInstallerAttribute Default = new RunInstallerAttribute(false);

		// Token: 0x04000431 RID: 1073
		private bool runInstaller;
	}
}
