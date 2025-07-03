using System.Collections.Generic;
using SinpeEmpresarial.Application.DTOs.Caja;
using SinpeEmpresarial.Domain.Entities;

namespace SinpeEmpresarial.Application.Interfaces
{
    public interface ICajaService
    {
        List<ListCajaDto> GetCajasByComercio(int idComercio);
        void AddCaja(CreateCajaDto dto);
        void EditCaja(EditCajaDto dto);
        List<ListCajaDto> GetAll();
        Caja GetById(int idCaja);
    }
}
