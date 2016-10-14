using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class MenuEntities
	{
		private int mID;
		private int mParentID;
		private string mDecription;
		private string mUrl;
		private int mOrder;
		private string mIcon;
		private int mIsHidden;
		private int mActive;
		public MenuEntities(){}
		public int MID
		{
			get { return this.mID;}
			set {this.mID = value;}
		}
		public int MParentID
		{
			get { return this.mParentID;}
			set {this.mParentID = value;}
		}
		public string MDecription
		{
			get { return this.mDecription;}
			set {this.mDecription = value;}
		}
		public string MUrl
		{
			get { return this.mUrl;}
			set {this.mUrl = value;}
		}
		public int MOrder
		{
			get { return this.mOrder;}
			set {this.mOrder = value;}
		}
		public string MIcon
		{
			get { return this.mIcon;}
			set {this.mIcon = value;}
		}
		public int MIsHidden
		{
			get { return this.mIsHidden;}
			set {this.mIsHidden = value;}
		}
		public int MActive
		{
			get { return this.mActive;}
			set {this.mActive = value;}
		}
	}
}