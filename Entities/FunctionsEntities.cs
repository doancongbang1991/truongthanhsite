using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class FunctionsEntities
	{
		private string fID;
		private string fName;
		private string fParent;
		private int fOrder;
		private int fActive;
		public FunctionsEntities(){}
		public string FID
		{
			get { return this.fID;}
			set {this.fID = value;}
		}
		public string FName
		{
			get { return this.fName;}
			set {this.fName = value;}
		}
		public string FParent
		{
			get { return this.fParent;}
			set {this.fParent = value;}
		}
		public int FOrder
		{
			get { return this.fOrder;}
			set {this.fOrder = value;}
		}
		public int FActive
		{
			get { return this.fActive;}
			set {this.fActive = value;}
		}
	}
}