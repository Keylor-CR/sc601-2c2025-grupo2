using SinpeEmpresarial.Application.Dtos.Bitacora;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinpeEmpresarial.Application.Services
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraRepository _bitacoraRepository;
        public BitacoraService(IBitacoraRepository bitacoraRepository)
        {
            _bitacoraRepository = bitacoraRepository;
        }
        public void RegisterEvento(BitacoraEventoDto dto)
        {
            var evento = new Bitacora
            {
                TablaDeEvento = dto.TablaDeEvento,
                TipoDeEvento = dto.TipoDeEvento,
                FechaDeEvento = DateTime.Now,
                DescripcionDeEvento = dto.DescripcionDeEvento,
                StackTrace = dto.StackTrace,
                DatosAnteriores = dto.DatosAnteriores,
                DatosPosteriores = dto.DatosPosteriores
            };
            _bitacoraRepository.Add(evento);
        }
        public List<BitacoraEventoDto> GetAllEventos()
        {
            return _bitacoraRepository.GetAll()
                .Select(b => new BitacoraEventoDto
                {
                    IdEvento = b.IdEvento,
                    TablaDeEvento = b.TablaDeEvento,
                    TipoDeEvento = b.TipoDeEvento,
                    FechaDeEvento = b.FechaDeEvento,
                    DescripcionDeEvento = b.DescripcionDeEvento,
                    StackTrace = b.StackTrace,
                    DatosAnteriores = b.DatosAnteriores,
                    DatosPosteriores = b.DatosPosteriores
                }).ToList();
        }
        public List<BitacoraEventoDto> GetLast(int count)
        {
            return _bitacoraRepository.GetAll()
                .OrderByDescending(b => b.FechaDeEvento)
                .Take(count)
                .Select(b => new BitacoraEventoDto
                {
                    IdEvento = b.IdEvento,
                    TablaDeEvento = b.TablaDeEvento,
                    TipoDeEvento = b.TipoDeEvento,
                    FechaDeEvento = b.FechaDeEvento,
                    DescripcionDeEvento = b.DescripcionDeEvento,
                    StackTrace = b.StackTrace,
                    DatosAnteriores = b.DatosAnteriores,
                    DatosPosteriores = b.DatosPosteriores
                }).ToList();
        }



    }
}
