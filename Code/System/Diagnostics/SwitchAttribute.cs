using System;
using System.Reflection;

namespace System.Diagnostics
{
	/// <summary>Identifies a switch used in an assembly, class, or member.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000251 RID: 593
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	[MonoLimitation("This attribute is not considered in trace support.")]
	public sealed class SwitchAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SwitchAttribute" /> class, specifying the name and the type of the switch. </summary>
		/// <param name="switchName">The display name of the switch.</param>
		/// <param name="switchType">The type of the switch.</param>
		// Token: 0x060014DA RID: 5338 RVA: 0x00037398 File Offset: 0x00035598
		public SwitchAttribute(string switchName, Type switchType)
		{
			if (switchName == null)
			{
				throw new ArgumentNullException("switchName");
			}
			if (switchType == null)
			{
				throw new ArgumentNullException("switchType");
			}
			this.name = switchName;
			this.type = switchType;
		}

		/// <summary>Returns all switch attributes for the specified assembly.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.SwitchAttribute" /> that contains all the switch attributes for the assembly.</returns>
		/// <param name="assembly">The <see cref="T:System.Reflection.Assembly" /> to check for switch attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014DB RID: 5339 RVA: 0x000373E8 File Offset: 0x000355E8
		public static SwitchAttribute[] GetAll(Assembly assembly)
		{
			object[] customAttributes = assembly.GetCustomAttributes(typeof(SwitchAttribute), false);
			SwitchAttribute[] array = new SwitchAttribute[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = (SwitchAttribute)customAttributes[i];
			}
			return array;
		}

		/// <summary>Gets or sets the display name of the switch.</summary>
		/// <returns>The display name of the switch.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.SwitchAttribute.SwitchName" /> is set to null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Diagnostics.SwitchAttribute.SwitchName" /> is set to an empty string.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00037430 File Offset: 0x00035630
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x00037438 File Offset: 0x00035638
		public string SwitchName
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.name == null)
				{
					throw new ArgumentNullException("value");
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets the description of the switch.</summary>
		/// <returns>The description of the switch.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x00037458 File Offset: 0x00035658
		// (set) Token: 0x060014DF RID: 5343 RVA: 0x00037460 File Offset: 0x00035660
		public string SwitchDescription
		{
			get
			{
				return this.desc;
			}
			set
			{
				if (this.desc == null)
				{
					throw new ArgumentNullException("value");
				}
				this.desc = value;
			}
		}

		/// <summary>Gets or sets the type of the switch.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of the switch.</returns>
		/// <exception cref="T:System.ArgumentNullException">P:System.Diagnostics.SwitchAttribute.SwitchType is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x00037480 File Offset: 0x00035680
		// (set) Token: 0x060014E1 RID: 5345 RVA: 0x00037488 File Offset: 0x00035688
		public Type SwitchType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (this.type == null)
				{
					throw new ArgumentNullException("value");
				}
				this.type = value;
			}
		}

		// Token: 0x04000654 RID: 1620
		private string name;

		// Token: 0x04000655 RID: 1621
		private string desc = string.Empty;

		// Token: 0x04000656 RID: 1622
		private Type type;
	}
}
