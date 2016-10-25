using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class FurTypeEntities
    {
        public int FurTypeID { get; set; }
        public string FurTypeName { get; set; }
        public List<FurnitureEntities> listfur { get; set; }
        public FurTypeEntities()
        {

        }
    }
}