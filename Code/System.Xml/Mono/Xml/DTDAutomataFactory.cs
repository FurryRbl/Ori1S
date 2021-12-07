using System;
using System.Collections;

namespace Mono.Xml
{
	// Token: 0x020000B5 RID: 181
	internal class DTDAutomataFactory
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x00025328 File Offset: 0x00023528
		public DTDAutomataFactory(DTDObjectModel root)
		{
			this.root = root;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00025350 File Offset: 0x00023550
		public DTDChoiceAutomata Choice(DTDAutomata left, DTDAutomata right)
		{
			Hashtable hashtable = this.choiceTable[left] as Hashtable;
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this.choiceTable[left] = hashtable;
			}
			DTDChoiceAutomata dtdchoiceAutomata = hashtable[right] as DTDChoiceAutomata;
			if (dtdchoiceAutomata == null)
			{
				dtdchoiceAutomata = new DTDChoiceAutomata(this.root, left, right);
				hashtable[right] = dtdchoiceAutomata;
			}
			return dtdchoiceAutomata;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000253B4 File Offset: 0x000235B4
		public DTDSequenceAutomata Sequence(DTDAutomata left, DTDAutomata right)
		{
			Hashtable hashtable = this.sequenceTable[left] as Hashtable;
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this.sequenceTable[left] = hashtable;
			}
			DTDSequenceAutomata dtdsequenceAutomata = hashtable[right] as DTDSequenceAutomata;
			if (dtdsequenceAutomata == null)
			{
				dtdsequenceAutomata = new DTDSequenceAutomata(this.root, left, right);
				hashtable[right] = dtdsequenceAutomata;
			}
			return dtdsequenceAutomata;
		}

		// Token: 0x040003CC RID: 972
		private DTDObjectModel root;

		// Token: 0x040003CD RID: 973
		private Hashtable choiceTable = new Hashtable();

		// Token: 0x040003CE RID: 974
		private Hashtable sequenceTable = new Hashtable();
	}
}
