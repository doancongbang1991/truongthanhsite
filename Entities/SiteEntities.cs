using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class SiteEntities
    {
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteNameVi { get; set; }
        public string SiteDetail { get; set; }
        public string SiteDesp { get; set; }
        public string SiteLink { get; set; }
        public int SiteOrder { get; set; }
        public bool SiteHidden { get; set; }
        public SiteEntities()
        {

        }

    }
}