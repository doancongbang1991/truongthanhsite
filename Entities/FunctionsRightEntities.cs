using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class FunctionsRightEntities
	{
		private int fRID;
		private int uGRPID;
		private string fID;
		private int fRView;
		private int fRAdd;
		private int fREdit;
		private int fRDelete;
		private int fRActive;
		public FunctionsRightEntities(){}
		public int FRID
		{
			get { return this.fRID;}
			set {this.fRID = value;}
		}
		public int UGRPID
		{
			get { return this.uGRPID;}
			set {this.uGRPID = value;}
		}
		public string FID
		{
			get { return this.fID;}
			set {this.fID = value;}
		}
		public int FRView
		{
			get { return this.fRView;}
			set {this.fRView = value;}
		}
		public int FRAdd
		{
			get { return this.fRAdd;}
			set {this.fRAdd = value;}
		}
		public int FREdit
		{
			get { return this.fREdit;}
			set {this.fREdit = value;}
		}
		public int FRDelete
		{
			get { return this.fRDelete;}
			set {this.fRDelete = value;}
		}
		public int FRActive
		{
			get { return this.fRActive;}
			set {this.fRActive = value;}
		}
	}
}