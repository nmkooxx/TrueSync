using System;
using System.Collections.Generic;

namespace TrueSync
{
	public class ArbiterClone
	{
		public static ResourcePoolContactClone poolContactClone = new ResourcePoolContactClone();

		public RigidBody body1;

		public RigidBody body2;

		public List<ContactClone> contactList = new List<ContactClone>();

		private int index;

		private int length;

		public void Reset()
		{
			this.index = 0;
			this.length = this.contactList.Count;
			while (this.index < this.length)
			{
				ArbiterClone.poolContactClone.GiveBack(this.contactList[this.index]);
				this.index++;
			}
		}

		public void Clone(Arbiter arb)
		{
			this.body1 = arb.body1;
			this.body2 = arb.body2;
			this.contactList.Clear();
			this.index = 0;
			this.length = arb.contactList.Count;
			while (this.index < this.length)
			{
				ContactClone @new = ArbiterClone.poolContactClone.GetNew();
				@new.Clone(arb.contactList[this.index]);
				this.contactList.Add(@new);
				this.index++;
			}
		}

		public void Restore(Arbiter arb)
		{
			arb.body1 = this.body1;
			arb.body2 = this.body2;
			arb.contactList.Clear();
			this.index = 0;
			this.length = this.contactList.Count;
			while (this.index < this.length)
			{
				ContactClone contactClone = this.contactList[this.index];
				Contact @new = Contact.Pool.GetNew();
				contactClone.Restore(@new);
				arb.contactList.Add(@new);
				this.index++;
			}
		}
	}
}
