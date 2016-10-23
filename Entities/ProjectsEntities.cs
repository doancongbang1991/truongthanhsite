using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ProjectsEntities
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDetail { get; set; }
        public int ProjectTypeID { get; set; }
        public string ProjectImg { get; set; }
        public ProjectsEntities()
        {

        }
    }
}