using Newtonsoft.Json;
using SinpeEmpresarial.Application.Dtos.Bitacora;
using SinpeEmpresarial.Application.Dtos.ConfiguracionComercio;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Enums;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinpeEmpresarial.Application.Services
{
    public class ConfiguracionComercioService : IConfiguracionComercioService
    {
        private readonly IConfiguracionComercioRepository _repository;
        private readonly IBitacoraService _bitacoraService;
        public ConfiguracionComercioService(IConfiguracionComercioRepository repository, IBitacoraService bitacoraService)
        {
            _repository = repository;
            _bitacoraService = bitacoraService;
        }
        public List<ConfiguracionComercioListDto> GetAllConfiguraciones()
        {
            var configuraciones = _repository.GetAll();
            return configuraciones.Select(MapToListDTO).ToList();
        }
        public ConfiguracionComercioDetailDto GetConfiguracionByComercio(int id)
        {
            var configuracion = _repository.GetByComercioId(id);
            if (configuracion == null)
                throw new Exception("Configuración no encontrada.");

            return MapToDetailDTO(configuracion);
        }
        public void RegisterConfiguracionComercio(ConfiguracionComercioCreateDto Dto)
        {
            try
            {
                if (_repository.GetByComercioId(Dto.IdComercio) != null)
                    throw new Exception("Ya existe una configuración para este comercio.");

                var entity = MapFromCreateDTO(Dto);
                _repository.Add(entity);
                _bitacoraService.RegisterEvento(new BitacoraEventoDto
                {
                    TablaDeEvento = "CONFIGURACIONES_COMERCIOS",
                    TipoDeEvento = "Registrar",
                    DescripcionDeEvento = "Registro de nueva configuracion de comercio.",
                    StackTrace = "",
                    DatosAnteriores = null,
                    DatosPosteriores = JsonConvert.SerializeObject(entity)
                });
            }catch (Exception ex)
            {
                _bitacoraService.RegisterEvento(new BitacoraEventoDto
                {
                    TablaDeEvento = "CONFIGURACIONES_COMERCIOS",
                    TipoDeEvento = "Error",
                    DescripcionDeEvento = ex.Message,
                    StackTrace = ex.ToString(),
                    DatosAnteriores = null,
                    DatosPosteriores = null
                });
                throw;
            }

        }
        public void EditConfiguracionComercio(ConfiguracionComercioEditDto Dto)
        {
            try
            {
                var entity = _repository.GetById(Dto.IdConfiguracion);
                if (entity == null)
                    throw new Exception("Configuración de comercio no encontrada.");

                var datosAnteriores = JsonConvert.SerializeObject(entity);

                MapFromEditDTO(Dto, entity);
                _repository.Update(entity);
                _bitacoraService.RegisterEvento(new BitacoraEventoDto
                {
                    TablaDeEvento = "CONFIGURACIONES_COMERCIOS",
                    TipoDeEvento = "Editar",
                    DescripcionDeEvento = "Edición de configuracion de comercio.",
                    StackTrace = "",
                    DatosAnteriores = datosAnteriores,
                    DatosPosteriores = JsonConvert.SerializeObject(entity)
                });
            }
            catch (Exception ex)
            {
                _bitacoraService.RegisterEvento(new BitacoraEventoDto
                {
                    TablaDeEvento = "CONFIGURACIONES_COMERCIOS",
                    TipoDeEvento = "Error",
                    DescripcionDeEvento = ex.Message,
                    StackTrace = ex.ToString(),
                    DatosAnteriores = null,
                    DatosPosteriores = null
                });
                throw;
            }
        }
        private ConfiguracionComercio MapFromCreateDTO(ConfiguracionComercioCreateDto Dto)
        {
            return new ConfiguracionComercio
            {
                IdComercio = Dto.IdComercio,
                TipoConfiguracion = Dto.TipoConfiguracion,
                Comision = Dto.Comision,
                FechaDeRegistro = DateTime.Now,
                Estado = true,
                FechaDeModificacion = null
            };
        }
        private void MapFromEditDTO(ConfiguracionComercioEditDto Dto, ConfiguracionComercio entity)
        {
            entity.TipoConfiguracion = Dto.TipoConfiguracion;
            entity.Comision = Dto.Comision;
            entity.Estado = Dto.Estado;
            entity.FechaDeModificacion = DateTime.Now;
        }
        private ConfiguracionComercioListDto MapToListDTO(ConfiguracionComercio entity)
        {
            return new ConfiguracionComercioListDto
            { 
                IdConfiguracion = entity.IdConfiguracion,
                IdComercio = entity.IdComercio,
                NombreComercio = entity.Comercio?.Nombre ?? "Desconocido",
                TipoConfiguracion = entity.TipoConfiguracion,
                TipoConfiguracionString = GetTipoConfiguracionProsa(entity.TipoConfiguracion),
                Comision = entity.Comision,
                FechaDeRegistro = entity.FechaDeRegistro,
                FechaDeModificacion = entity.FechaDeModificacion,
                Estado = entity.Estado,
                EstadoString = entity.Estado ? "Activo" : "Inactivo"
            };
        }
        private ConfiguracionComercioDetailDto MapToDetailDTO(ConfiguracionComercio entity)
        {
            if (entity == null) return null;

            return new ConfiguracionComercioDetailDto
            {
                IdConfiguracion = entity.IdConfiguracion,
                IdComercio = entity.IdComercio,
                TipoConfiguracion = entity.TipoConfiguracion,
                TipoConfiguracionString = GetTipoConfiguracionProsa(entity.TipoConfiguracion),
                Comision = entity.Comision,
                FechaDeRegistro = entity.FechaDeRegistro,
                FechaDeModificacion = entity.FechaDeModificacion,
                Estado = entity.Estado
            };
        }
        private string GetTipoConfiguracionProsa(int value)
        {
            switch ((TipoConfiguracionComercio)value)
                {
                case TipoConfiguracionComercio.Plataforma: return "Plataforma";
                case TipoConfiguracionComercio.Externa: return "Externa";
                case TipoConfiguracionComercio.Ambas: return "Ambas";
                default: return "Desconocido";
            }
        }

    }

}
