using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class FooterEntities
    {
        public int FooterID { get; set; }
        public string FooterName { get; set; }
        public string FooterIcon { get; set; }
        public bool FooterAllowLink { get; set; }
        public string FooterLink { get; set; }
        public int FooterTypeID { get; set; }
        public bool FooterSubMenu { get; set; }
        public FooterEntities()
        {
                
        }

    }
}