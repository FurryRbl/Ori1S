﻿using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines GUID identifiers that correspond to the standard set of tool windows that are available in the design environment.</summary>
	// Token: 0x0200013C RID: 316
	public class StandardToolWindows
	{
		/// <summary>Gets the GUID for the object browser. This field is read-only.</summary>
		// Token: 0x0400034A RID: 842
		public static readonly Guid ObjectBrowser = new Guid("970d9861-ee83-11d0-a778-00a0c91110c3");

		/// <summary>Gets the GUID for the output window. This field is read-only.</summary>
		// Token: 0x0400034B RID: 843
		public static readonly Guid OutputWindow = new Guid("34e76e81-ee4a-11d0-ae2e-00a0c90fffc3");

		/// <summary>Gets the GUID for the solution explorer. This field is read-only.</summary>
		// Token: 0x0400034C RID: 844
		public static readonly Guid ProjectExplorer = new Guid("3ae79031-e1bc-11d0-8f78-00a0c9110057");

		/// <summary>Gets the GUID for the Properties window. This field is read-only.</summary>
		// Token: 0x0400034D RID: 845
		public static readonly Guid PropertyBrowser = new Guid("eefa5220-e298-11d0-8f78-00a0c9110057");

		/// <summary>Gets the GUID for the related links frame. This field is read-only.</summary>
		// Token: 0x0400034E RID: 846
		public static readonly Guid RelatedLinks = new Guid("66dba47c-61df-11d2-aa79-00c04f990343");

		/// <summary>Gets the GUID for the server explorer. This field is read-only.</summary>
		// Token: 0x0400034F RID: 847
		public static readonly Guid ServerExplorer = new Guid("74946827-37a0-11d2-a273-00c04f8ef4ff");

		/// <summary>Gets the GUID for the task list. This field is read-only.</summary>
		// Token: 0x04000350 RID: 848
		public static readonly Guid TaskList = new Guid("4a9b7e51-aa16-11d0-a8c5-00a0c921a4d2");

		/// <summary>Gets the GUID for the Toolbox. This field is read-only.</summary>
		// Token: 0x04000351 RID: 849
		public static readonly Guid Toolbox = new Guid("b1e99781-ab81-11d0-b683-00aa00a3ee26");
	}
}
