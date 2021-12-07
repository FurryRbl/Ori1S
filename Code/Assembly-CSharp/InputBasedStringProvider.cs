using System;
using System.Collections.Generic;

// Token: 0x0200067B RID: 1659
public class InputBasedStringProvider : MessageProvider
{
	// Token: 0x06002853 RID: 10323 RVA: 0x000AEDE2 File Offset: 0x000ACFE2
	public override IEnumerable<MessageDescriptor> GetMessages()
	{
		return this.Keyboard.GetMessages();
	}

	// Token: 0x040023D2 RID: 9170
	public MessageProvider Keyboard;

	// Token: 0x040023D3 RID: 9171
	public MessageProvider Xbox360Controller;

	// Token: 0x040023D4 RID: 9172
	public MessageProvider XboxOneController;

	// Token: 0x040023D5 RID: 9173
	public MessageProvider TouchDevice;
}
