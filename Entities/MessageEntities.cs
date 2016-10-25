using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class MessageEntities
    {
        public int MessID { get; set; }
        public string MessName { get; set; }
        public string MessYear { get; set; }
        public string MessMail { get; set; }
        public bool MessGen { get; set; }
        public string MessPhone { get; set; }
        public string MessBody { get; set; }
        public bool MessRead { get; set; }
        public MessageEntities()
        {

        }
    }
}