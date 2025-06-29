using System;
using System.Collections.Generic;
using System.Linq;
using SinpeEmpresarial.Application.DTOs.Caja;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;

namespace SinpeEmpresarial.Application.Services
{
    public class CajaService : ICajaService
    {
        private readonly ICajaRepository _repo;

        public CajaService(ICajaRepository repo)
        {
            _repo = repo;
        }

        public List<ListCajaDto> GetCajasByComercio(int idComercio)
        {
            return _repo.GetByComercio(idComercio)
                .Select(c => new ListCajaDto
                {
                    IdCaja = c.IdCaja,
                    Nombre = c.Nombre,
                    Descripcion = string.IsNullOrEmpty(c.Descripcion) ? "" : (c.Descripcion.Length > 10 ? c.Descripcion.Substring(0, 10) + "..." : c.Descripcion),
                    TelefonoSINPE = c.TelefonoSINPE,
                    FechaDeRegistro = c.FechaDeRegistro,
                    Estado = c.Estado
                }).ToList();
        }

        public void AddCaja(CreateCajaDto dto)
        {
            if (_repo.GetByTelefono(dto.TelefonoSINPE) != null)
                throw new Exception("Ya existe una caja activa con ese teléfono.");

            if (_repo.GetByNombre(dto.Nombre, dto.IdComercio) != null)
                throw new Exception("Ya existe una caja con ese nombre en el comercio.");

            var caja = new Caja
            {
                IdComercio = dto.IdComercio,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                TelefonoSINPE = dto.TelefonoSINPE,
                FechaDeRegistro = DateTime.Now,
                Estado = true
            };

            _repo.Add(caja);
        }

        public void EditCaja(EditCajaDto dto)
        {
            var caja = _repo.GetById(dto.IdCaja) ?? throw new Exception("Caja no encontrada");
            caja.Edit(dto.Nombre, dto.Descripcion, dto.TelefonoSINPE, dto.Estado);
            _repo.Update(caja);
        }
    }
}
