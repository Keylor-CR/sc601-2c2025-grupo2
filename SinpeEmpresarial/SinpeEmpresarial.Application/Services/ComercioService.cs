using SinpeEmpresarial.Application.DTOs;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Domain;
using SinpeEmpresarial.Domain.Enums;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinpeEmpresarial.Application.Services
{
    public class ComercioService : IComercioService
    {
        private readonly IComercioRepository _comercioRepository;
        public List<ComercioListDTO> GetAll()
        {
            var comercios = _comercioRepository.GetAll();
            return comercios.Select(MapToListDTO).ToList();
        }

        public ComercioDetailDTO GetById(int id)
        {
            var comercio = _comercioRepository.GetById(id);
            return comercio != null ? MapToDetailDTO(comercio) : null;
        }
        public void Register(ComercioCreateDTO dto)
        {
            if (_comercioRepository.GetAll().Any(x => x.Identificacion == dto.Identificacion))
                throw new Exception("Ya existe un Comercio con esta identificacion");

            var entity = MapFromCreateDTO(dto);
            _comercioRepository.Add(entity);
        }
        public void Edit(ComercioEditDTO dto)
        {
            var entity = _comercioRepository.GetById(dto.Id);
            if (entity == null)
                throw new Exception("Comercio no encontrado");

            MapFromEditDTO(dto, entity);
            _comercioRepository.Update(entity);
        }


        private ComercioListDTO MapToListDTO(Comercio entity)
        {
            return new ComercioListDTO
            {
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
