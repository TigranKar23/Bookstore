using AutoMapper;
using StaffProjects.DAL;
using StaffProjects.DTO;
using System.Data.Entity;
using System.Threading.Tasks;

namespace StaffProjects.BLL.Services.ErrorService
{
    public class ErrorService : IErrorService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;

        public ErrorService(IMapper mapper, AppDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<ErrorModelDto> GetById(long id)
        {
            var error = await _db.Errors.FirstOrDefaultAsync(x => x.Id == id);

            return new ErrorModelDto()
            {
                Code = error.Id,
                Description = error.Name
            };
        }
    }
}
