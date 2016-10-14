using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class MenuRightEntities
	{
		private int mRID;
		private int uGRPID;
		private int mID;
		private int mRView;
		private int mRActive;
		public MenuRightEntities(){}
		public int MRID
		{
			get { return this.mRID;}
			set {this.mRID = value;}
		}
		public int UGRPID
		{
			get { return this.uGRPID;}
			set {this.uGRPID = value;}
		}
		public int MID
		{
			get { return this.mID;}
			set {this.mID = value;}
		}
		public int MRView
		{
			get { return this.mRView;}
			set {this.mRView = value;}
		}
		public int MRActive
		{
			get { return this.mRActive;}
			set {this.mRActive = value;}
		}
	}
}