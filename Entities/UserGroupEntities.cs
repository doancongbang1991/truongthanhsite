using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class UserGroupEntities
	{
		private int uGRPID;
		private string uGRPName;
		private string uGRPParent;
		private int uGRPActive;
		public UserGroupEntities(){}
		public int UGRPID
		{
			get { return this.uGRPID;}
			set {this.uGRPID = value;}
		}
		public string UGRPName
		{
			get { return this.uGRPName;}
			set {this.uGRPName = value;}
		}
		public string UGRPParent
		{
			get { return this.uGRPParent;}
			set {this.uGRPParent = value;}
		}
		public int UGRPActive
		{
			get { return this.uGRPActive;}
			set {this.uGRPActive = value;}
		}
	}
}