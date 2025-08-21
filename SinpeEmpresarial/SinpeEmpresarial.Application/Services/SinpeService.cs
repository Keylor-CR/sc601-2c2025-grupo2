using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SinpeEmpresarial.Application.Dtos.Bitacora;
using SinpeEmpresarial.Application.Dtos.Sinpe;
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
                    IdSinpe = x.IdSinpe,
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

                
            var sinpe = new Sinpe
            {
                TelefonoOrigen = dto.TelefonoOrigen,
                NombreOrigen = dto.NombreOrigen,
                TelefonoDestino = dto.TelefonoDestinatario,
                NombreDestino = dto.NombreDestinatario,
                Monto = dto.Monto,
                Descripcion = dto.Descripcion,
                FechaDeRegistro = DateTime.Now, 
                Estado = false 
            };

            // Registrar el pago SINPE
            _sinpeRepository.Add(sinpe);

            _bitacoraService.RegisterEvento(new BitacoraEventoDto
            {
                TablaDeEvento = "SINPES",
                TipoDeEvento = "Registrar",
                DescripcionDeEvento = "Registro de nuevo pago SINPE",
                StackTrace = "",
                DatosAnteriores = null, 
                DatosPosteriores = JsonConvert.SerializeObject(sinpe)
            });

            return new ResponseModel(true, "El pago SINPE ha sido registrado exitosamente.");

        }
        public List<ListSinpeDto> GetLast(int count)
        {
            return _sinpeRepository.GetAll()
                .Take(count)
                .Select(x => new ListSinpeDto
                {
                    TelefonoOrigen = x.TelefonoOrigen,
                    NombreOrigen = x.NombreOrigen,
                    TelefonoDestino = x.TelefonoDestino,
                    NombreDestino = x.NombreDestino,
                    Monto = x.Monto,
                    Descripcion = x.Descripcion,
                    Estado = x.Estado
                }).ToList();
        }
        public ResponseModel Sincronizar(int idSinpe)
        {
            var sinpe = _sinpeRepository.GetById(idSinpe);
            if (sinpe == null) throw new Exception("SINPE no encontrado");
            if (sinpe.Estado) return new ResponseModel(false, "El pago SINPE ya ha sido sincronizado previamente.");

            var beforeJson = JsonConvert.SerializeObject(sinpe);

            sinpe.Estado = true;
            _sinpeRepository.Update(sinpe);

            var sinpe_after = _sinpeRepository.GetById(idSinpe);

            var afterJson = JsonConvert.SerializeObject(sinpe_after);

            //registrar evento en bitacora
            _bitacoraService.RegisterEvento(new BitacoraEventoDto
            {
                TablaDeEvento = "SINPES",
                TipoDeEvento = "Sincronizar",
                DescripcionDeEvento = "Pago SINPE sincronizado exitosamente",
                StackTrace = "",
                DatosAnteriores = beforeJson,
                DatosPosteriores = afterJson
            });
            return new ResponseModel(true, "El pago SINPE ha sido sincronizado exitosamente.");

        }
    }
}

