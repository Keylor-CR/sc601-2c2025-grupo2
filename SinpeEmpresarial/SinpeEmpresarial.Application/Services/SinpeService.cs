using System.Collections.Generic;
using System.Linq;
using SinpeEmpresarial.Application.DTOs.Sinpe;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Domain.Interfaces.Repositories;

namespace SinpeEmpresarial.Application.Services
{
    public class SinpeService : ISinpeService
    {
        private readonly ISinpeRepository _repo;

        public SinpeService(ISinpeRepository repo)
        {
            _repo = repo;
        }

        public List<ListSinpeDto> GetByCajaTelefono(string telefonoDestino)
        {
            return _repo.GetByTelefonoDestino(telefonoDestino)
                .OrderByDescending(x => x.Fecha)
                .Select(x => new ListSinpeDto
                {
                    TelefonoOrigen = x.TelefonoOrigen,
                    NombreOrigen = x.NombreOrigen,
                    TelefonoDestino = x.TelefonoDestino,
                    NombreDestino = x.NombreDestino,
                    Monto = x.Monto,
                    Descripcion = x.Descripcion,
                    Fecha = x.Fecha,
                    Estado = x.Estado
                }).ToList();
        }
    }
}

