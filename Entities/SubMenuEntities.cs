using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class SubMenuEntities
    {
        public int SubMenuID { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuDetail { get; set; }
        public string SubMenuLink { get; set; }
        public int SubMenuParentID { get; set; }
        public SubMenuEntities()
        {

        }

    }
}