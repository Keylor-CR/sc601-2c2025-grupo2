using SinpeEmpresarial.Application.DTOs.ReportesMensuales;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinpeEmpresarial.Application.Services
{
    public class ReportesMensualesService : IReportesMensualesService
    {
        private readonly IReportesMensualesRepository _reportesMensualesRepository;
        private readonly IComercioRepository _comercioRepository;
        private readonly ISinpeRepository _sinpeRepository;
        private readonly ICajaRepository _cajaRepository;
        private readonly IConfiguracionComercioRepository _configuracionComercioRepository;

        public ReportesMensualesService(
            IReportesMensualesRepository reportesMensualesRepository,
            IComercioRepository comercioRepository,
            ISinpeRepository sinpeRepository,
            ICajaRepository cajaRepository,
            IConfiguracionComercioRepository configuracionComercioRepository)
        {
            _reportesMensualesRepository = reportesMensualesRepository;
            _comercioRepository = comercioRepository;
            _sinpeRepository = sinpeRepository;
            _cajaRepository = cajaRepository;
            _configuracionComercioRepository = configuracionComercioRepository;
        }

        public List<ListReportesMensualesDto> GetAllReportes()
        {
            var reportes = _reportesMensualesRepository.GetAll();
            return reportes.Select(r => 
            {
                var comercio = _comercioRepository.GetById(r.IdComercio);
                return new ListReportesMensualesDto
                {
                    IdReporte = r.IdReporte,
                    IdComercio = r.IdComercio,
                    NombreComercio = comercio?.Nombre ?? "Comercio no encontrado",
                    CantidadDeCajas = r.CantidadDeCajas,
                    MontoTotalRecaudado = r.MontoTotalRecaudado,
                    CantidadDeSINPES = r.CantidadDeSINPES,
                    MontoTotalComision = r.MontoTotalComision,
                    FechaDelReporte = r.FechaDelReporte
                };
            }).ToList();
        }

        public void GenerarReportesMensuales()
        {
            var comercios = _comercioRepository.GetAll();
            foreach (var comercio in comercios)
            {
                var reporteExistente = _reportesMensualesRepository.GetByComercioYFecha(comercio.IdComercio, DateTime.Now);

                var cajasDelComercio = _cajaRepository.GetAll().Where(c => c.IdComercio == comercio.IdComercio).ToList();
                var cantidadDeCajas = cajasDelComercio.Count;
                var telefonosCajas = cajasDelComercio.Select(c => c.TelefonoSINPE).ToList();
                var sinpes = _sinpeRepository.GetAll().Where(s => s.FechaDeRegistro.Month == DateTime.Now.Month && 
                                                                  s.FechaDeRegistro.Year == DateTime.Now.Year && 
                                                                  telefonosCajas.Contains(s.TelefonoDestino)).ToList();
                var montoTotalRecaudado = sinpes.Any() ? sinpes.Sum(s => s.Monto) : 0m;
                var cantidadDeSINPES = sinpes.Count();

                var configuracion = _configuracionComercioRepository.GetByComercioId(comercio.IdComercio);
                var porcentajeDeComision = configuracion != null ? configuracion.Comision / 100m : 0m;
                var montoTotalComision = montoTotalRecaudado * porcentajeDeComision;

                if (reporteExistente != null)
                {
                    reporteExistente.CantidadDeCajas = cantidadDeCajas;
                    reporteExistente.MontoTotalRecaudado = montoTotalRecaudado;
                    reporteExistente.CantidadDeSINPES = cantidadDeSINPES;
                    reporteExistente.MontoTotalComision = montoTotalComision;
                    reporteExistente.FechaDelReporte = DateTime.Now;
                    _reportesMensualesRepository.Update(reporteExistente);
                }
                else
                {
                    var nuevoReporte = new ReportesMensuales
                    {
                        IdComercio = comercio.IdComercio,
                        CantidadDeCajas = cantidadDeCajas,
                        MontoTotalRecaudado = montoTotalRecaudado,
                        CantidadDeSINPES = cantidadDeSINPES,
                        MontoTotalComision = montoTotalComision,
                        FechaDelReporte = DateTime.Now
                    };
                    _reportesMensualesRepository.Add(nuevoReporte);
                }
            }
        }
    }
}
