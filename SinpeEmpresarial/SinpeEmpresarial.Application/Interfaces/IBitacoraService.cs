using SinpeEmpresarial.Application.DTOs.Bitacora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Application.Interfaces
{
    public interface IBitacoraService
    {
        void RegisterEvento(BitacoraEventoDTO dto);
        List<BitacoraEventoDTO> GetAllEventos();
    } 
}
