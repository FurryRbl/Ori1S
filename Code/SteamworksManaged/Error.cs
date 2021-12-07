using System;
using System.Diagnostics;
using ManagedSteam.Exceptions;

namespace ManagedSteam
{
	// Token: 0x0200016A RID: 362
	internal static class Error
	{
		// Token: 0x06000BF0 RID: 3056 RVA: 0x00010440 File Offset: 0x0000E640
		[Conditional("STEAMAPIWRAP_LITE")]
		public static void NotAvailableInLite()
		{
			throw new NotAvailableInLiteException();
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00010448 File Offset: 0x0000E648
		public static void ThrowError(ErrorCodes code, params object[] args)
		{
			switch (code)
			{
			case ErrorCodes.AlreadyLoaded:
				throw new AlreadyLoadedException(code, args);
			case ErrorCodes.SteamInitializeFailed:
				throw new SteamInitializeFailedException(code, args);
			case ErrorCodes.SteamInterfaceInitializeFailed:
				throw new SteamInterfaceInitializeFailedException(code, args);
			default:
				switch (code)
				{
				case ErrorCodes.InvalidInterfaceVersion:
					throw new InvalidInterfaceVersionException(code, args);
				case ErrorCodes.UsageAfterAPIShutdown:
					throw new UsageAfterAPIShutdownException(code, args);
				case ErrorCodes.CallbackStructSizeMissmatch:
					throw new CallbackStructSizeMismatchException(code, args);
				default:
					if (code >= ErrorCodes.StartOfNativeErrors && code <= ErrorCodes.EndOfNativeErrors)
					{
						throw new NativeException(code, new object[0]);
					}
					if (code >= ErrorCodes.StartOfManagedErrors && code <= ErrorCodes.EndOfManagedErrors)
					{
						throw new ManagedException(code, new object[0]);
					}
					throw new UnexpectedException(code, new object[0]);
				}
				break;
			}
		}
	}
}
