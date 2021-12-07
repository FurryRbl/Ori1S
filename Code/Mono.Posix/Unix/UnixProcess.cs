using System;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200001F RID: 31
	public sealed class UnixProcess
	{
		// Token: 0x06000182 RID: 386 RVA: 0x000071D4 File Offset: 0x000053D4
		internal UnixProcess(int pid)
		{
			this.pid = pid;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000071E4 File Offset: 0x000053E4
		public int Id
		{
			get
			{
				return this.pid;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000071EC File Offset: 0x000053EC
		public bool HasExited
		{
			get
			{
				int processStatus = this.GetProcessStatus();
				return Syscall.WIFEXITED(processStatus);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007208 File Offset: 0x00005408
		private int GetProcessStatus()
		{
			int num2;
			int num = Syscall.waitpid(this.pid, out num2, WaitOptions.WNOHANG | WaitOptions.WUNTRACED);
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
			return num;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000722C File Offset: 0x0000542C
		public int ExitCode
		{
			get
			{
				if (!this.HasExited)
				{
					throw new InvalidOperationException(Locale.GetText("Process hasn't exited"));
				}
				int processStatus = this.GetProcessStatus();
				return Syscall.WEXITSTATUS(processStatus);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00007264 File Offset: 0x00005464
		public bool HasSignaled
		{
			get
			{
				int processStatus = this.GetProcessStatus();
				return Syscall.WIFSIGNALED(processStatus);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00007280 File Offset: 0x00005480
		public Signum TerminationSignal
		{
			get
			{
				if (!this.HasSignaled)
				{
					throw new InvalidOperationException(Locale.GetText("Process wasn't terminated by a signal"));
				}
				int processStatus = this.GetProcessStatus();
				return Syscall.WTERMSIG(processStatus);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000072B8 File Offset: 0x000054B8
		public bool HasStopped
		{
			get
			{
				int processStatus = this.GetProcessStatus();
				return Syscall.WIFSTOPPED(processStatus);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000072D4 File Offset: 0x000054D4
		public Signum StopSignal
		{
			get
			{
				if (!this.HasStopped)
				{
					throw new InvalidOperationException(Locale.GetText("Process isn't stopped"));
				}
				int processStatus = this.GetProcessStatus();
				return Syscall.WSTOPSIG(processStatus);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000730C File Offset: 0x0000550C
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000731C File Offset: 0x0000551C
		public int ProcessGroupId
		{
			get
			{
				return Syscall.getpgid(this.pid);
			}
			set
			{
				int retval = Syscall.setpgid(this.pid, value);
				UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000733C File Offset: 0x0000553C
		public int SessionId
		{
			get
			{
				int num = Syscall.getsid(this.pid);
				UnixMarshal.ThrowExceptionForLastErrorIf(num);
				return num;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000735C File Offset: 0x0000555C
		public static UnixProcess GetCurrentProcess()
		{
			return new UnixProcess(UnixProcess.GetCurrentProcessId());
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007368 File Offset: 0x00005568
		public static int GetCurrentProcessId()
		{
			return Syscall.getpid();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007370 File Offset: 0x00005570
		public void Kill()
		{
			this.Signal(Signum.SIGKILL);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000737C File Offset: 0x0000557C
		[CLSCompliant(false)]
		public void Signal(Signum signal)
		{
			int retval = Syscall.kill(this.pid, signal);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000739C File Offset: 0x0000559C
		public void WaitForExit()
		{
			int num;
			do
			{
				int num2;
				num = Syscall.waitpid(this.pid, out num2, (WaitOptions)0);
			}
			while (UnixMarshal.ShouldRetrySyscall(num));
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
		}

		// Token: 0x04000075 RID: 117
		private int pid;
	}
}
