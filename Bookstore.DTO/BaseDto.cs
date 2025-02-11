using System;

namespace Bookstore.DTO
{
    public class BaseDto
    {
        public string Id { get; set; }
    }

    public class BaseDtoWithDate : BaseDto
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }

    public class BaseAdminDtoWithDate : BaseDtoWithDate
    {
        public bool IsDeleted { get; set; }
    }
}
