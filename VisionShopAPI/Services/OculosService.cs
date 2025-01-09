using AutoMapper;
using VisionShopAPI.Data;
using VisionShopAPI.Data.Dtos;
using VisionShopAPI.Models;

namespace VisionShopAPI.Services
{
    public class OculosService
    {
        private VisionShopContext _context;
        private IMapper _mapper;

        public OculosService(VisionShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadOculosDto CriarOculos(CreateOculosDto dto)
        {
            var oculos = _mapper.Map<Oculos>(dto);
            _context.Oculos.Add(oculos);
            _context.SaveChanges();

            return _mapper.Map<ReadOculosDto>(oculos);
        }

        public ReadOculosDto ObterPorId(int id)
        {
            var oculos = _context.Oculos.Find(id);
            if (oculos == null) return null;

            return _mapper.Map<ReadOculosDto>(oculos);
        }

        public List<ReadOculosDto> ObterTodos()
        {
            var oculos = _context.Oculos.ToList();
            return _mapper.Map<List<ReadOculosDto>>(oculos);
        }
    }
}
