using System;
using System.ComponentModel.Design;

namespace System.ComponentModel
{
	/// <summary>Specifies the class used to implement design-time services for a component.</summary>
	// Token: 0x02000105 RID: 261
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the name of the type that provides design-time services.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in. </param>
		// Token: 0x06000A83 RID: 2691 RVA: 0x0001D61C File Offset: 0x0001B81C
		public DesignerAttribute(string designerTypeName)
		{
			if (designerTypeName == null)
			{
				throw new NullReferenceException();
			}
			this.name = designerTypeName;
			this.basetypename = typeof(System.ComponentModel.Design.IDesigner).FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the type that provides design-time services.</summary>
		/// <param name="designerType">A <see cref="T:System.Type" /> that represents the class that provides design-time services for the component this attribute is bound to. </param>
		// Token: 0x06000A84 RID: 2692 RVA: 0x0001D658 File Offset: 0x0001B858
		public DesignerAttribute(Type designerType) : this(designerType.AssemblyQualifiedName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class, using the name of the designer class and the base class for the designer.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in. </param>
		/// <param name="designerBaseType">A <see cref="T:System.Type" /> that represents the base class to associate with the <paramref name="designerTypeName" />. </param>
		// Token: 0x06000A85 RID: 2693 RVA: 0x0001D668 File Offset: 0x0001B868
		public DesignerAttribute(string designerTypeName, Type designerBaseType) : this(designerTypeName, designerBaseType.AssemblyQualifiedName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the types of the designer and designer base class.</summary>
		/// <param name="designerType">A <see cref="T:System.Type" /> that represents the class that provides design-time services for the component this attribute is bound to. </param>
		/// <param name="designerBaseType">A <see cref="T:System.Type" /> that represents the base class to associate with the <paramref name="designerType" />. </param>
		// Token: 0x06000A86 RID: 2694 RVA: 0x0001D678 File Offset: 0x0001B878
		public DesignerAttribute(Type designerType, Type designerBaseType) : this(designerType.AssemblyQualifiedName, designerBaseType.AssemblyQualifiedName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the designer type and the base class for the designer.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in. </param>
		/// <param name="designerBaseTypeName">The fully qualified name of the base class to associate with the designer class. </param>
		// Token: 0x06000A87 RID: 2695 RVA: 0x0001D68C File Offset: 0x0001B88C
		public DesignerAttribute(string designerTypeName, string designerBaseTypeName)
		{
			if (designerTypeName == null)
			{
				throw new NullReferenceException();
			}
			this.name = designerTypeName;
			this.basetypename = designerBaseTypeName;
		}

		/// <summary>Gets the name of the base type of this designer.</summary>
		/// <returns>The name of the base type of this designer.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0001D6BC File Offset: 0x0001B8BC
		public string DesignerBaseTypeName
		{
			get
			{
				return this.basetypename;
			}
		}

		/// <summary>Gets the name of the designer type associated with this designer attribute.</summary>
		/// <returns>The name of the designer type associated with this designer attribute.</returns>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
		public string DesignerTypeName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0001D6CC File Offset: 0x0001B8CC
		public override object TypeId
		{
			get
			{
				string text = this.basetypename;
				int num = text.IndexOf(',');
				if (num != -1)
				{
					text = text.Substring(0, num);
				}
				return base.GetType().ToString() + text;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignerAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000A8B RID: 2699 RVA: 0x0001D70C File Offset: 0x0001B90C
		public override bool Equals(object obj)
		{
			return obj is DesignerAttribute && ((DesignerAttribute)obj).DesignerBaseTypeName.Equals(this.basetypename) && ((DesignerAttribute)obj).DesignerTypeName.Equals(this.name);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001D75C File Offset: 0x0001B95C
		public override int GetHashCode()
		{
			return (this.name + this.basetypename).GetHashCode();
		}

		// Token: 0x040002C9 RID: 713
		private string name;

		// Token: 0x040002CA RID: 714
		private string basetypename;
	}
}
