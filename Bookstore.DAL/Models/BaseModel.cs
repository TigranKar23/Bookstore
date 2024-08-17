using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.DAL.Models
{
    [NotMapped]
    public class BaseModel
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}