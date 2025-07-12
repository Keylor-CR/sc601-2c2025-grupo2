using SinpeEmpresarial.Application.Dtos.Bitacora;
using System.Collections.Generic;

namespace SinpeEmpresarial.Application.Interfaces
{
    public interface IBitacoraService
    {
        void RegisterEvento(BitacoraEventoDto dto);
        List<BitacoraEventoDto> GetAllEventos();
        List<BitacoraEventoDto> GetLast(int count);
    } 
}
