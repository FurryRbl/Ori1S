using System;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Identifies the property tab or tabs to display for the specified class or classes.</summary>
	// Token: 0x02000198 RID: 408
	[AttributeUsage(AttributeTargets.All)]
	public class PropertyTabAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class.</summary>
		// Token: 0x06000E71 RID: 3697 RVA: 0x000251BC File Offset: 0x000233BC
		public PropertyTabAttribute()
		{
			this.tabs = Type.EmptyTypes;
			this.scopes = new PropertyTabScope[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class using the specified tab class name.</summary>
		/// <param name="tabClassName">The assembly qualified name of the type of tab to create. For an example of this format convention, see <see cref="P:System.Type.AssemblyQualifiedName" />. </param>
		// Token: 0x06000E72 RID: 3698 RVA: 0x000251DC File Offset: 0x000233DC
		public PropertyTabAttribute(string tabClassName) : this(tabClassName, PropertyTabScope.Component)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class using the specified type of tab.</summary>
		/// <param name="tabClass">The type of tab to create. </param>
		// Token: 0x06000E73 RID: 3699 RVA: 0x000251E8 File Offset: 0x000233E8
		public PropertyTabAttribute(Type tabClass) : this(tabClass, PropertyTabScope.Component)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class using the specified tab class name and tab scope.</summary>
		/// <param name="tabClassName">The assembly qualified name of the type of tab to create. For an example of this format convention, see <see cref="P:System.Type.AssemblyQualifiedName" />. </param>
		/// <param name="tabScope">A <see cref="T:System.ComponentModel.PropertyTabScope" /> that indicates the scope of this tab. If the scope is <see cref="F:System.ComponentModel.PropertyTabScope.Component" />, it is shown only for components with the corresponding <see cref="T:System.ComponentModel.PropertyTabAttribute" />. If it is <see cref="F:System.ComponentModel.PropertyTabScope.Document" />, it is shown for all components on the document. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tabScope" /> is not <see cref="F:System.ComponentModel.PropertyTabScope.Document" /> or <see cref="F:System.ComponentModel.PropertyTabScope.Component" />.</exception>
		// Token: 0x06000E74 RID: 3700 RVA: 0x000251F4 File Offset: 0x000233F4
		public PropertyTabAttribute(string tabClassName, PropertyTabScope tabScope)
		{
			if (tabClassName == null)
			{
				throw new ArgumentNullException("tabClassName");
			}
			this.InitializeArrays(new string[]
			{
				tabClassName
			}, new PropertyTabScope[]
			{
				tabScope
			});
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class using the specified type of tab and tab scope.</summary>
		/// <param name="tabClass">The type of tab to create. </param>
		/// <param name="tabScope">A <see cref="T:System.ComponentModel.PropertyTabScope" /> that indicates the scope of this tab. If the scope is <see cref="F:System.ComponentModel.PropertyTabScope.Component" />, it is shown only for components with the corresponding <see cref="T:System.ComponentModel.PropertyTabAttribute" />. If it is <see cref="F:System.ComponentModel.PropertyTabScope.Document" />, it is shown for all components on the document. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tabScope" /> is not <see cref="F:System.ComponentModel.PropertyTabScope.Document" /> or <see cref="F:System.ComponentModel.PropertyTabScope.Component" />.</exception>
		// Token: 0x06000E75 RID: 3701 RVA: 0x00025228 File Offset: 0x00023428
		public PropertyTabAttribute(Type tabClass, PropertyTabScope tabScope)
		{
			if (tabClass == null)
			{
				throw new ArgumentNullException("tabClass");
			}
			this.InitializeArrays(new Type[]
			{
				tabClass
			}, new PropertyTabScope[]
			{
				tabScope
			});
		}

		/// <summary>Gets the types of tabs that this attribute uses.</summary>
		/// <returns>An array of types indicating the types of tabs that this attribute uses.</returns>
		/// <exception cref="T:System.TypeLoadException">The types specified by the <see cref="P:System.ComponentModel.PropertyTabAttribute.TabClassNames" /> property could not be found.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0002525C File Offset: 0x0002345C
		public Type[] TabClasses
		{
			get
			{
				return this.tabs;
			}
		}

		/// <summary>Gets an array of tab scopes of each tab of this <see cref="T:System.ComponentModel.PropertyTabAttribute" />.</summary>
		/// <returns>An array of <see cref="T:System.ComponentModel.PropertyTabScope" /> objects that indicate the scopes of the tabs.</returns>
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x00025264 File Offset: 0x00023464
		public PropertyTabScope[] TabScopes
		{
			get
			{
				return this.scopes;
			}
		}

		/// <summary>Gets the names of the tab classes that this attribute uses.</summary>
		/// <returns>The names of the tab classes that this attribute uses.</returns>
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x0002526C File Offset: 0x0002346C
		protected string[] TabClassNames
		{
			get
			{
				string[] array = new string[this.tabs.Length];
				for (int i = 0; i < this.tabs.Length; i++)
				{
					array[i] = this.tabs[i].Name;
				}
				return array;
			}
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="other" /> refers to the same <see cref="T:System.ComponentModel.PropertyTabAttribute" /> instance; otherwise, false.</returns>
		/// <param name="other">An object to compare to this instance, or null.</param>
		/// <exception cref="T:System.TypeLoadException">The types specified by the <see cref="P:System.ComponentModel.PropertyTabAttribute.TabClassNames" /> property of the<paramref name=" other" /> parameter could not be found.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E79 RID: 3705 RVA: 0x000252B4 File Offset: 0x000234B4
		public override bool Equals(object other)
		{
			return other is PropertyTabAttribute && this.Equals((PropertyTabAttribute)other);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified attribute.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> instances are equal; otherwise, false.</returns>
		/// <param name="other">A <see cref="T:System.ComponentModel.PropertyTabAttribute" /> to compare to this instance, or null.</param>
		/// <exception cref="T:System.TypeLoadException">The types specified by the <see cref="P:System.ComponentModel.PropertyTabAttribute.TabClassNames" /> property of the <paramref name="other" /> parameter cannot be found.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E7A RID: 3706 RVA: 0x000252D0 File Offset: 0x000234D0
		public bool Equals(PropertyTabAttribute other)
		{
			if (other != this)
			{
				if (other.TabClasses.Length != this.tabs.Length)
				{
					return false;
				}
				for (int i = 0; i < this.tabs.Length; i++)
				{
					if (this.tabs[i] != other.TabClasses[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x06000E7B RID: 3707 RVA: 0x0002532C File Offset: 0x0002352C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Initializes the attribute using the specified names of tab classes and array of tab scopes.</summary>
		/// <param name="tabClassNames">An array of fully qualified type names of the types to create for tabs on the Properties window. </param>
		/// <param name="tabScopes">The scope of each tab. If the scope is <see cref="F:System.ComponentModel.PropertyTabScope.Component" />, it is shown only for components with the corresponding <see cref="T:System.ComponentModel.PropertyTabAttribute" />. If it is <see cref="F:System.ComponentModel.PropertyTabScope.Document" />, it is shown for all components on the document. </param>
		/// <exception cref="T:System.ArgumentException">One or more of the values in <paramref name="tabScopes" /> is not <see cref="F:System.ComponentModel.PropertyTabScope.Document" /> or <see cref="F:System.ComponentModel.PropertyTabScope.Component" />.-or-The length of the <paramref name="tabClassNames" /> and <paramref name="tabScopes" /> arrays do not match.-or-<paramref name="tabClassNames" /> or <paramref name="tabScopes" /> is null.</exception>
		// Token: 0x06000E7C RID: 3708 RVA: 0x00025334 File Offset: 0x00023534
		protected void InitializeArrays(string[] tabClassNames, PropertyTabScope[] tabScopes)
		{
			if (tabScopes == null)
			{
				throw new ArgumentNullException("tabScopes");
			}
			if (tabClassNames == null)
			{
				throw new ArgumentNullException("tabClassNames");
			}
			this.scopes = tabScopes;
			this.tabs = new Type[tabClassNames.Length];
			for (int i = 0; i < tabClassNames.Length; i++)
			{
				this.tabs[i] = this.GetTypeFromName(tabClassNames[i]);
			}
		}

		/// <summary>Initializes the attribute using the specified names of tab classes and array of tab scopes.</summary>
		/// <param name="tabClasses">The types of tabs to create. </param>
		/// <param name="tabScopes">The scope of each tab. If the scope is <see cref="F:System.ComponentModel.PropertyTabScope.Component" />, it is shown only for components with the corresponding <see cref="T:System.ComponentModel.PropertyTabAttribute" />. If it is <see cref="F:System.ComponentModel.PropertyTabScope.Document" />, it is shown for all components on the document. </param>
		/// <exception cref="T:System.ArgumentException">One or more of the values in <paramref name="tabScopes" /> is not <see cref="F:System.ComponentModel.PropertyTabScope.Document" /> or <see cref="F:System.ComponentModel.PropertyTabScope.Component" />.-or-The length of the <paramref name="tabClassNames" /> and <paramref name="tabScopes" /> arrays do not match.-or-<paramref name="tabClassNames" /> or <paramref name="tabScopes" /> is null.</exception>
		// Token: 0x06000E7D RID: 3709 RVA: 0x000253A0 File Offset: 0x000235A0
		protected void InitializeArrays(Type[] tabClasses, PropertyTabScope[] tabScopes)
		{
			if (tabScopes == null)
			{
				throw new ArgumentNullException("tabScopes");
			}
			if (tabClasses == null)
			{
				throw new ArgumentNullException("tabClasses");
			}
			if (tabClasses.Length != tabScopes.Length)
			{
				throw new ArgumentException("tabClasses.Length != tabScopes.Length");
			}
			this.tabs = tabClasses;
			this.scopes = tabScopes;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x000253F4 File Offset: 0x000235F4
		private Type GetTypeFromName(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			int num = typeName.IndexOf(",");
			if (num != -1)
			{
				string name = typeName.Substring(0, num);
				string assemblyString = typeName.Substring(num + 1);
				Assembly assembly = Assembly.Load(assemblyString);
				if (assembly != null)
				{
					return assembly.GetType(name, true);
				}
			}
			return Type.GetType(typeName, true);
		}

		// Token: 0x04000407 RID: 1031
		private Type[] tabs;

		// Token: 0x04000408 RID: 1032
		private PropertyTabScope[] scopes;
	}
}
