using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	/// <summary>Specifies the resource consumed by the member of a class. This class cannot be inherited.</summary>
	// Token: 0x0200052D RID: 1325
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceConsumptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ResourceConsumptionAttribute" /> class specifying the scope of the consumed resource. </summary>
		/// <param name="resourceScope">The <see cref="T:System.Runtime.Versioning.ResourceScope" /> for the consumed resource.</param>
		// Token: 0x0600344E RID: 13390 RVA: 0x000ABCC0 File Offset: 0x000A9EC0
		public ResourceConsumptionAttribute(ResourceScope resourceScope)
		{
			this.resource = resourceScope;
			this.consumption = resourceScope;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ResourceConsumptionAttribute" /> class specifying the scope of the consumed resource and the scope of how it is consumed.</summary>
		/// <param name="resourceScope">The <see cref="T:System.Runtime.Versioning.ResourceScope" /> for the consumed resource.</param>
		/// <param name="consumptionScope">The <see cref="T:System.Runtime.Versioning.ResourceScope" /> used by this member.</param>
		// Token: 0x0600344F RID: 13391 RVA: 0x000ABCD8 File Offset: 0x000A9ED8
		public ResourceConsumptionAttribute(ResourceScope resourceScope, ResourceScope consumptionScope)
		{
			this.resource = resourceScope;
			this.consumption = consumptionScope;
		}

		/// <summary>Gets the consumption scope for this member.</summary>
		/// <returns>A <see cref="T:System.Runtime.Versioning.ResourceScope" /> object specifying the resource scope used by this member.</returns>
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x000ABCF0 File Offset: 0x000A9EF0
		public ResourceScope ConsumptionScope
		{
			get
			{
				return this.consumption;
			}
		}

		/// <summary>Gets the resource scope for the consumed resource.</summary>
		/// <returns>A <see cref="T:System.Runtime.Versioning.ResourceScope" /> object specifying the resource scope of the consumed member.</returns>
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06003451 RID: 13393 RVA: 0x000ABCF8 File Offset: 0x000A9EF8
		public ResourceScope ResourceScope
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x0400160A RID: 5642
		private ResourceScope resource;

		// Token: 0x0400160B RID: 5643
		private ResourceScope consumption;
	}
}
