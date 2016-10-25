using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ConTypeEntities
    {
        public int ConTypeID { get; set; }
        public string ConTypeName { get; set; }
        public List<ConstructionEntities> listcon { get; set; } 
        public ConTypeEntities()
        {

        }
        
    }
}