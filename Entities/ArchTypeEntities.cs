using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ArchTypeEntities
    {
        public int ArchTypeID { get; set; }
        public string ArchTypeName { get; set; }
        public List<ArchEntities> listarch { get; set; }
        public ArchTypeEntities()
        {

        }
    }
}