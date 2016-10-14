using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class UserInfoEntities
	{
		private int uID;
		private string uUserName;
		private string uPassword;
		private string uFullName;
		private int pID;
		private string uAddress;
		private string uPhone;
		private string uMobilePhone;
		private string uEmail;
		private string uNotes;
		private int uGRPID;
		private int uActive;
		public UserInfoEntities(){}
		public int UID
		{
			get { return this.uID;}
			set {this.uID = value;}
		}
		public string UUserName
		{
			get { return this.uUserName;}
			set {this.uUserName = value;}
		}
		public string UPassword
		{
			get { return this.uPassword;}
			set {this.uPassword = value;}
		}
		public string UFullName
		{
			get { return this.uFullName;}
			set {this.uFullName = value;}
		}
		public int PID
		{
			get { return this.pID;}
			set {this.pID = value;}
		}
		public string UAddress
		{
			get { return this.uAddress;}
			set {this.uAddress = value;}
		}
		public string UPhone
		{
			get { return this.uPhone;}
			set {this.uPhone = value;}
		}
		public string UMobilePhone
		{
			get { return this.uMobilePhone;}
			set {this.uMobilePhone = value;}
		}
		public string UEmail
		{
			get { return this.uEmail;}
			set {this.uEmail = value;}
		}
		public string UNotes
		{
			get { return this.uNotes;}
			set {this.uNotes = value;}
		}
		public int UGRPID
		{
			get { return this.uGRPID;}
			set {this.uGRPID = value;}
		}
		public int UActive
		{
			get { return this.uActive;}
			set {this.uActive = value;}
		}
	}
}