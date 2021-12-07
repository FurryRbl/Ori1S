using System;

// Token: 0x0200016F RID: 367
public interface IRecorderPlugin
{
	// Token: 0x06000E7E RID: 3710
	void PlayCycle(int frame);

	// Token: 0x06000E7F RID: 3711
	void RecordCycle(int frame);

	// Token: 0x06000E80 RID: 3712
	void Exit();
}
