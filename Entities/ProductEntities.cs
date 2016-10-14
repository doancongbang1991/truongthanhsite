using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class ProductEntities
	{
		private int pID;
	    private string pName;
        private string pDes;
        private string pVer;
        public ProductEntities() { }
		public int PID
		{
			get { return this.pID;}
			set {this.pID = value;}
		}
        public string PNAME
        {
            get { return this.pName; }
            set { this.pName = value; }
        }
        public string PDES
        {
            get { return this.pDes; }
            set { this.pDes = value; }
        }
        public string PVER
        {
            get { return this.pVer; }
            set { this.pVer = value; }
        }
	
	}
}