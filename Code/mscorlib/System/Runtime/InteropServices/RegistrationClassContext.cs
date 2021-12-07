using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the set of execution contexts in which a class object will be made available for requests to construct instances.</summary>
	// Token: 0x020003B2 RID: 946
	[Flags]
	public enum RegistrationClassContext
	{
		/// <summary>Disables activate-as-activator (AAA) activations for this activation only.</summary>
		// Token: 0x0400116F RID: 4463
		DisableActivateAsActivator = 32768,
		/// <summary>Enables activate-as-activator (AAA) activations for this activation only.</summary>
		// Token: 0x04001170 RID: 4464
		EnableActivateAsActivator = 65536,
		/// <summary>Allows the downloading of code from the Directory Service or the Internet.</summary>
		// Token: 0x04001171 RID: 4465
		EnableCodeDownload = 8192,
		/// <summary>Begin this activation from the default context of the current apartment.</summary>
		// Token: 0x04001172 RID: 4466
		FromDefaultContext = 131072,
		/// <summary>The code that manages objects of this class is an in-process handler.</summary>
		// Token: 0x04001173 RID: 4467
		InProcessHandler = 2,
		/// <summary>Not used.</summary>
		// Token: 0x04001174 RID: 4468
		InProcessHandler16 = 32,
		/// <summary>The code that creates and manages objects of this class is a DLL that runs in the same process as the caller of the function specifying the class context.</summary>
		// Token: 0x04001175 RID: 4469
		InProcessServer = 1,
		/// <summary>Not used.</summary>
		// Token: 0x04001176 RID: 4470
		InProcessServer16 = 8,
		/// <summary>The EXE code that creates and manages objects of this class runs on same machine but is loaded in a separate process space.</summary>
		// Token: 0x04001177 RID: 4471
		LocalServer = 4,
		/// <summary>Disallows the downloading of code from the Directory Service or the Internet.</summary>
		// Token: 0x04001178 RID: 4472
		NoCodeDownload = 1024,
		/// <summary>Specifies whether activation fails if it uses custom marshaling.</summary>
		// Token: 0x04001179 RID: 4473
		NoCustomMarshal = 4096,
		/// <summary>Overrides the logging of failures.</summary>
		// Token: 0x0400117A RID: 4474
		NoFailureLog = 16384,
		/// <summary>A remote machine context.</summary>
		// Token: 0x0400117B RID: 4475
		RemoteServer = 16,
		/// <summary>Not used.</summary>
		// Token: 0x0400117C RID: 4476
		Reserved1 = 64,
		/// <summary>Not used.</summary>
		// Token: 0x0400117D RID: 4477
		Reserved2 = 128,
		/// <summary>Not used.</summary>
		// Token: 0x0400117E RID: 4478
		Reserved3 = 256,
		/// <summary>Not used.</summary>
		// Token: 0x0400117F RID: 4479
		Reserved4 = 512,
		/// <summary>Not used.</summary>
		// Token: 0x04001180 RID: 4480
		Reserved5 = 2048
	}
}
