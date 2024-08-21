using AutoMapper;
using Bookstore.DAL;
using Bookstore.DTO;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bookstore.BLL.Services.ErrorService
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

            if (error == null)
            {
                return null; // или выбросите исключение, если это необходимо
            }

            // Использование AutoMapper для преобразования модели в DTO
            var errorDto = _mapper.Map<ErrorModelDto>(error);
            return errorDto;
        }
    }
}