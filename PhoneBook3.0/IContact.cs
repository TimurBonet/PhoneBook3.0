using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace PhoneBook3._0
{

    public interface IContact 
    {
        public CType ContactType
        { get; set; }

        public string Name
        {get;set;}

        public int PhoneNumber
        { get; set; }

        public string TextComment
        { get; set; }
       
    }
}
