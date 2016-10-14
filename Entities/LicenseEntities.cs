using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class LicenseEntities
    {
        private int licID;
        private string licSerial;
        private string licKey;
        private string licDes;
        private string licStatus;
        private string lUID;
        private string licProduct;
        private string licDomain;
        private DateTime lRegDate;
        private DateTime lAppDate;
        

        public LicenseEntities() { }
        public int LicID
        {
            get { return this.licID; }
            set { this.licID = value; }
        }
        public string LicSerial
        {
            get { return this.licSerial; }
            set { this.licSerial = value; }
        }
        public string LicDomain
        {
            get { return this.licDomain; }
            set { this.licDomain = value; }
        }
        public string LicProduct
        {
            get { return this.licProduct; }
            set { this.licProduct = value; }
        }
        public string LicKey
        {
            get { return this.licKey; }
            set { this.licKey = value; }
        }
        public string LicDes
        {
            get { return this.licDes; }
            set { this.licDes = value; }
        }
        public string LicStatus
        {
            get { return this.licStatus; }
            set { this.licStatus = value; }
        }
        public string LUID
        {
            get { return this.lUID; }
            set { this.lUID = value; }
        }
        public DateTime LRegDate
        {
            get { return this.lRegDate; }
            set { this.lRegDate = value; }
        }
        public DateTime LAppDate
        {
            get { return this.lAppDate; }
            set { this.lAppDate = value; }
        }
    }
}