using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the installer for a type that installs components.</summary>
	// Token: 0x02000164 RID: 356
	[AttributeUsage(AttributeTargets.Class)]
	public class InstallerTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InstallerTypeAttribute" /> class with the name of the component's installer type.</summary>
		/// <param name="typeName">The name of a <see cref="T:System.Type" /> that represents the installer for the component this attribute is bound to. This class must implement <see cref="T:System.ComponentModel.Design.IDesigner" />. </param>
		// Token: 0x06000CA8 RID: 3240 RVA: 0x00020324 File Offset: 0x0001E524
		public InstallerTypeAttribute(string typeName)
		{
			this.installer = Type.GetType(typeName, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InstallerTypeAttribute" /> class, when given a <see cref="T:System.Type" /> that represents the installer for a component.</summary>
		/// <param name="installerType">A <see cref="T:System.Type" /> that represents the installer for the component this attribute is bound to. This class must implement <see cref="T:System.ComponentModel.Design.IDesigner" />. </param>
		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002033C File Offset: 0x0001E53C
		public InstallerTypeAttribute(Type installerType)
		{
			this.installer = installerType;
		}

		/// <summary>Gets the type of installer associated with this attribute.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of installer associated with this attribute, or null if an installer does not exist.</returns>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0002034C File Offset: 0x0001E54C
		public virtual Type InstallerType
		{
			get
			{
				return this.installer;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.InstallerTypeAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000CAB RID: 3243 RVA: 0x00020354 File Offset: 0x0001E554
		public override bool Equals(object obj)
		{
			return obj is InstallerTypeAttribute && (obj == this || ((InstallerTypeAttribute)obj).InstallerType == this.installer);
		}

		/// <summary>Returns the hashcode for this object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.InstallerTypeAttribute" />.</returns>
		// Token: 0x06000CAC RID: 3244 RVA: 0x00020380 File Offset: 0x0001E580
		public override int GetHashCode()
		{
			return this.installer.GetHashCode();
		}

		// Token: 0x04000381 RID: 897
		private Type installer;
	}
}
