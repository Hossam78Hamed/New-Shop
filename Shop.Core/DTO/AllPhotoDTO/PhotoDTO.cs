using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.DTO.AllPhotoDTO
{
    public class PhotoDTO
    {
        public string? ImageName { get; set; }
       
        public int? ProductId { get; set; }
    }
}

