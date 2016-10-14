using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class PositionEntities
    {
        private int pID;
        private string pName;
        private string pDescr;
        private int pCreatedBy;
        private DateTime pCreatedD;
        private int pLastUpdatedBy;
        private DateTime pLastUpdatedD;
        public PositionEntities() { }
        public int PID
        {
            get { return this.pID; }
            set { this.pID = value; }
        }
        public string PName
        {
            get { return this.pName; }
            set { this.pName = value; }
        }
        public string PDescr
        {
            get { return this.pDescr; }
            set { this.pDescr = value; }
        }
        public int PCreatedBy
        {
            get { return this.pCreatedBy; }
            set { this.pCreatedBy = value; }
        }
        public DateTime PCreatedD
        {
            get { return this.pCreatedD; }
            set { this.pCreatedD = value; }
        }
        public int PLastUpdatedBy
        {
            get { return this.pLastUpdatedBy; }
            set { this.pLastUpdatedBy = value; }
        }
        public DateTime PLastUpdatedD
        {
            get { return this.pLastUpdatedD; }
            set { this.pLastUpdatedD = value; }
        }
    }
}