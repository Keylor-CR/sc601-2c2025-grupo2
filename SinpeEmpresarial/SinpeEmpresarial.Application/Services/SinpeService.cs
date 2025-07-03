using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SinpeEmpresarial.Application.DTOs.Bitacora;
using SinpeEmpresarial.Application.DTOs.Sinpe;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using SinpeEmpresarial.Shared.Models;

namespace SinpeEmpresarial.Application.Services
{
    public class SinpeService : ISinpeService
    {
        private readonly ISinpeRepository _sinpeRepository;
        private readonly ICajaRepository _cajaRepository;
        private readonly IBitacoraService _bitacoraService;

        public SinpeService(ISinpeRepository sinpeRepository, ICajaRepository cajaRepository, IBitacoraService bitacoraService)
        {
            _sinpeRepository = sinpeRepository;
            _cajaRepository = cajaRepository;
            _bitacoraService = bitacoraService;
        }

        public List<ListSinpeDto> GetByCajaTelefono(string telefonoDestino)
        {
            return _sinpeRepository.GetByTelefonoDestino(telefonoDestino)
                .OrderByDescending(x => x.FechaDeRegistro) // Ordenado por fecha mas reciente segun requerimientos
                .Select(x => new ListSinpeDto
                {
                    TelefonoOrigen = x.TelefonoOrigen,
                    NombreOrigen = x.NombreOrigen,
                    TelefonoDestino = x.TelefonoDestino,
                    NombreDestino = x.NombreDestino,
                    Monto = x.Monto,
                    Descripcion = x.Descripcion,
                    Fecha = x.FechaDeRegistro,
                    Estado = x.Estado
                }).ToList();
        }

        public ResponseModel RegisterSinpe(SinpeCreateDto dto)
        {
            try
            {
                // Validacion requerida: verificar que el telefono destino existe y la caja esta activa
                var cajaDestino = _cajaRepository.GetByTelefono(dto.TelefonoDestinatario);

                if (cajaDestino == null)
                {
                    throw new InvalidOperationException("No existe una caja registrada con el numero de telefono destinatario proporcionado.");
                }

                if (!cajaDestino.Estado)
                {
                    throw new InvalidOperationException("La caja destino se encuentra inactiva y no puede recibir pagos SINPE.");
                }

                // Crear la entidad Sinpe segun los campos requeridos
                var sinpe = new Sinpe
                {
                    TelefonoOrigen = dto.TelefonoOrigen,
                    NombreOrigen = dto.NombreOrigen,
                    TelefonoDestino = dto.TelefonoDestinatario,
                    NombreDestino = dto.NombreDestinatario,
                    Monto = dto.Monto,
                    Descripcion = dto.Descripcion,
                    FechaDeRegistro = DateTime.Now, // Se asigna automaticamente segun requerimientos
                    Estado = false // Estado por defecto: No sincronizado (false) segun requerimientos
                };

                // Registrar el pago SINPE
                _sinpeRepository.Add(sinpe);

                // Registrar evento en bitacora segun requerimientos
                _bitacoraService.RegisterEvento(new BitacoraEventoDTO
                {
                    TablaDeEvento = "Sinpes",
                    TipoDeEvento = "Registrar",
                    DescripcionDeEvento = "Registro de nuevo pago SINPE",
                    StackTrace = null,
                    DatosAnteriores = null, // Para registro nuevo no hay datos anteriores
                    DatosPosteriores = JsonConvert.SerializeObject(sinpe)
                });

                return new ResponseModel(true, "El pago SINPE ha sido registrado exitosamente.");
            }
            catch (Exception ex)
            {
                // Registrar error en bitacora segun requerimientos
                _bitacoraService.RegisterEvento(new BitacoraEventoDTO
                {
                    TablaDeEvento = "Sinpes",
                    TipoDeEvento = "Error",
                    DescripcionDeEvento = ex.Message,
                    StackTrace = ex.ToString(),
                    DatosAnteriores = null,
                    DatosPosteriores = null
                });

                return new ResponseModel(false, ex.Message);
            }
        }
    }
}

