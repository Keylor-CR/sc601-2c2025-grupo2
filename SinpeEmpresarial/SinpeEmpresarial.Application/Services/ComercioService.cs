using SinpeEmpresarial.Application.DTOs;
using SinpeEmpresarial.Application.DTOs.Bitacora;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Domain;
using SinpeEmpresarial.Domain.Enums;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SinpeEmpresarial.Application.Services
{
    public class ComercioService : IComercioService
    {
        private readonly IComercioRepository _comercioRepository;
        private readonly IBitacoraService _bitacoraService;
        public ComercioService(IComercioRepository comercioRepository, IBitacoraService bitacoraService)
        {
            _comercioRepository = comercioRepository;
            _bitacoraService = bitacoraService;
        }
        public List<ComercioListDTO> GetAllComercios()
        {
            var comercios = _comercioRepository.GetAll();
            return comercios.Select(MapToListDTO).ToList();
        }

        public ComercioDetailDTO GetComercioById(int id)
        {
            var comercio = _comercioRepository.GetById(id);
            return comercio != null ? MapToDetailDTO(comercio) : null;
        }

        public ComercioDetailDTO GetComercioByIdentificacion(string id)
        {
            throw new NotImplementedException();
        }

        public void RegisterComercio(ComercioCreateDTO dto)
        {
            try
            {
                if (_comercioRepository.GetByIdentificacion(dto.Identificacion) != null)
                    throw new Exception("Ya existe un Comercio con esta identificación");

                var entity = MapFromCreateDTO(dto);
                _comercioRepository.Add(entity);
                _bitacoraService.RegisterEvento(new BitacoraEventoDTO
                {
                    TablaDeEvento = "Comercios",
                    TipoDeEvento = "Registrar",
                    DescripcionDeEvento = "Registro de nuevo comercio",
                    StackTrace = "",
                    DatosAnteriores = null,
                    DatosPosteriores = JsonConvert.SerializeObject(entity)
                });
            }
            catch (Exception ex)
            {
                _bitacoraService.RegisterEvento(new BitacoraEventoDTO
                {
                    TablaDeEvento = "Comercios",
                    TipoDeEvento = "Error",
                    DescripcionDeEvento = ex.Message,
                    StackTrace = ex.ToString(),
                    DatosAnteriores = null,
                    DatosPosteriores = JsonConvert.SerializeObject(dto)
                });
                throw; // Rethrow si quieres que la UI maneje el error también
            }
        }
        public void EditComercio(ComercioEditDTO dto)
        {
            try
            {
                var entity = _comercioRepository.GetById(dto.IdComercio);
                if (entity == null)
                    throw new Exception("Comercio no encontrado");

                var datosAnteriores = JsonConvert.SerializeObject(entity);

                MapFromEditDTO(dto, entity);
                _comercioRepository.Update(entity);
                _bitacoraService.RegisterEvento(new BitacoraEventoDTO
                {
                    TablaDeEvento = "Comercios",
                    TipoDeEvento = "Editar",
                    DescripcionDeEvento = "Edición de comercio",
                    StackTrace = "",
                    DatosAnteriores = datosAnteriores,
                    DatosPosteriores = JsonConvert.SerializeObject(entity)
                });
            }
            catch (Exception ex)
            {
                _bitacoraService.RegisterEvento(new BitacoraEventoDTO
                {
                    TablaDeEvento = "Comercios",
                    TipoDeEvento = "Error",
                    DescripcionDeEvento = ex.Message,
                    StackTrace = ex.ToString(),
                    DatosAnteriores = null,
                    DatosPosteriores = JsonConvert.SerializeObject(dto)
                });
                throw;
            }
        }


        private ComercioListDTO MapToListDTO(Comercio entity)
        {
            return new ComercioListDTO
            {
                IdComercio = entity.IdComercio,
                Identificacion = entity.Identificacion,
                TipoIdentificacion = entity.TipoIdentificacion,
                TipoIdentificacionString = GetTipoIdentificacionProsa(entity.TipoIdentificacion),
                Nombre = entity.Nombre,
                TipoDeComercio = entity.TipoDeComercio,
                TipoDeComercioString = GetTipoComercioProsa(entity.TipoDeComercio),
                Telefono = entity.Telefono,
                CorreoElectronico = entity.CorreoElectronico,
                Estado = entity.Estado,
                EstadoString = entity.Estado ? "Activo" : "Inactivo"
            };
        }

        private Comercio MapFromCreateDTO(ComercioCreateDTO dto)
        {
            return new Comercio
            {
                Identificacion = dto.Identificacion,
                TipoIdentificacion = dto.TipoIdentificacion,
                Nombre = dto.Nombre,
                TipoDeComercio = dto.TipoDeComercio,
                Telefono = dto.Telefono,
                CorreoElectronico = dto.CorreoElectronico,
                Direccion = dto.Direccion,
                Estado = true,
                FechaDeRegistro = DateTime.Now,
                FechaDeModificacion = null
            };
        }

        private void MapFromEditDTO(ComercioEditDTO dto, Comercio entity)
        {
            entity.Nombre = dto.Nombre;
            entity.TipoDeComercio = dto.TipoDeComercio;
            entity.Telefono = dto.Telefono;
            entity.CorreoElectronico = dto.CorreoElectronico;
            entity.Direccion = dto.Direccion;
            entity.FechaDeModificacion = DateTime.Now;
            entity.Estado = dto.Estado;
        }

        private ComercioDetailDTO MapToDetailDTO(Comercio entity)
        {
            return new ComercioDetailDTO
            {
                IdComercio = entity.IdComercio,
                Identificacion = entity.Identificacion,
                TipoIdentificacion = entity.TipoIdentificacion,
                TipoIdentificacionString = GetTipoIdentificacionProsa(entity.TipoIdentificacion),
                Nombre = entity.Nombre,
                TipoDeComercio = entity.TipoDeComercio,
                TipoDeComercioString = GetTipoComercioProsa(entity.TipoDeComercio),
                Telefono = entity.Telefono,
                CorreoElectronico = entity.CorreoElectronico,
                Direccion = entity.Direccion,
                FechaDeRegistro = entity.FechaDeRegistro,
                FechaDeModificacion = entity.FechaDeModificacion,
                Estado = entity.Estado,
                EstadoString = entity.Estado ? "Activo" : "Inactivo"
            };
        }

        private string GetTipoIdentificacionProsa(int value)
        {
            switch ((TipoIdentificacion)value)
            {
                case TipoIdentificacion.Fisica: return "Física";
                case TipoIdentificacion.Juridica: return "Jurídica";
                default: return "Desconocido";
            }
        }

        private string GetTipoComercioProsa(int value)
        {
            switch ((TipoComercio)value)
            {
                case TipoComercio.Restaurante: return "Restaurantes";
                case TipoComercio.Supermercado: return "Supermercados";
                case TipoComercio.Ferreteria: return "Ferreterías";
                case TipoComercio.Otros: return "Otros";
                default: return "Desconocido";
            }
        }
    }
}