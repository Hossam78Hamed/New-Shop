using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.DTO.AllAccountDTO
{
    
    public class EmailDTO
    {
        public EmailDTO(string _To, string _From, string _Subject, string _Content) {
            To = _To; From = _From;
            Subject = _Subject;Content= _Content;
            
        
        }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
