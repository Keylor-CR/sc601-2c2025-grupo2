using Newtonsoft.Json;
using SinpeEmpresarial.Application.Dtos.Bitacora;
using SinpeEmpresarial.Application.Dtos.Caja;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinpeEmpresarial.Application.Services
{
    public class CajaService : ICajaService
    {
        private readonly ICajaRepository _repo;
        private readonly IBitacoraService _bitacoraService;

        public CajaService(ICajaRepository repo, IBitacoraService bitacoraService)
        {
            _repo = repo;
            _bitacoraService = bitacoraService;
        }
        public Caja GetById(int id)
        {
            return _repo.GetById(id);
        }
        public List<ListCajaDto> GetAll()
        {
            return _repo.GetAll()
                .Select(c => new ListCajaDto
                {
                    IdCaja = c.IdCaja,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion.Length > 10 ? c.Descripcion.Substring(0, 10) + "..." : c.Descripcion,
                    TelefonoSINPE = c.TelefonoSINPE,
                    FechaDeRegistro = c.FechaDeRegistro,
                    Estado = c.Estado
                })
                .ToList();
        }


        public List<ListCajaDto> GetCajasByComercio(int idComercio)
        {
            return _repo.GetByComercio(idComercio)
                .Select(c => new ListCajaDto
                {
                    IdCaja = c.IdCaja,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion.Length > 10 ? c.Descripcion.Substring(0, 10) + "..." : c.Descripcion,
                    TelefonoSINPE = c.TelefonoSINPE,
                    FechaDeRegistro = c.FechaDeRegistro,
                    Estado = c.Estado
                }).ToList();
        }

        public void AddCaja(CreateCajaDto dto)
        {
            try {
                if (_repo.GetByTelefono(dto.TelefonoSINPE) != null)
                    throw new Exception("Ya existe una caja activa con ese telefono.");

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
                _bitacoraService.RegisterEvento(new BitacoraEventoDto
                {
                    TablaDeEvento = "CAJAS",
                    TipoDeEvento = "Registrar",
                    DescripcionDeEvento = "Registro de nueva caja",
                    StackTrace = "",
                    DatosAnteriores = null,
                    DatosPosteriores = JsonConvert.SerializeObject(caja)
                });
            }
            catch(Exception ex) {
                _bitacoraService.RegisterEvento(new BitacoraEventoDto
                {
                    TablaDeEvento = "CAJAS",
                    TipoDeEvento = "Error",
                    DescripcionDeEvento = ex.Message,
                    StackTrace = ex.ToString(),
                    DatosAnteriores = null,
                    DatosPosteriores = JsonConvert.SerializeObject(dto)
                });
                throw;
            }
            
        }

        public void EditCaja(EditCajaDto dto)
        {
            try {
                var caja = _repo.GetById(dto.IdCaja) ?? throw new Exception("Caja no encontrada");
                var datosAnteriores = JsonConvert.SerializeObject(caja);
                _repo.Update(caja);
                _bitacoraService.RegisterEvento(new BitacoraEventoDto
                {
                    TablaDeEvento = "CAJAS",
                    TipoDeEvento = "Editar",
                    DescripcionDeEvento = "Edición de caja",
                    StackTrace = "",
                    DatosAnteriores = datosAnteriores,
                    DatosPosteriores = JsonConvert.SerializeObject(caja)
                });
            }
            catch (Exception ex)
            {
                _bitacoraService.RegisterEvento(new BitacoraEventoDto
                {
                    TablaDeEvento = "CAJAS",
                    TipoDeEvento = "Error",
                    DescripcionDeEvento = ex.Message,
                    StackTrace = ex.ToString(),
                    DatosAnteriores = null,
                    DatosPosteriores = JsonConvert.SerializeObject(dto)
                });
                throw;
            }

        }
    }
}
