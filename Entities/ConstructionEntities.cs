using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ConstructionEntities
    {
        public int ConID { get; set; }
        public string ConName { get; set; }
        public string ConDetail { get; set; }
        public int ConTypeID { get; set; }
        public string ConImg { get; set; }
        public string ConTypeName { get; set; }
        public ConstructionEntities()
        {

        }
    }
}