using SinpeEmpresarial.Application.DTOs.ReportesMensuales;
using SinpeEmpresarial.Application.Dtos.Bitacora;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Domain.Entities;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SinpeEmpresarial.Application.Services
{
    public class ReportesMensualesService : IReportesMensualesService
    {
        private readonly IReportesMensualesRepository _reportesMensualesRepository;
        private readonly IComercioRepository _comercioRepository;
        private readonly ISinpeRepository _sinpeRepository;
        private readonly ICajaRepository _cajaRepository;
        private readonly IConfiguracionComercioRepository _configuracionComercioRepository;
        private readonly IBitacoraService _bitacoraService;

        public ReportesMensualesService(
            IReportesMensualesRepository reportesMensualesRepository,
            IComercioRepository comercioRepository,
            ISinpeRepository sinpeRepository,
            ICajaRepository cajaRepository,
            IConfiguracionComercioRepository configuracionComercioRepository,
            IBitacoraService bitacoraService)
        {
            _reportesMensualesRepository = reportesMensualesRepository;
            _comercioRepository = comercioRepository;
            _sinpeRepository = sinpeRepository;
            _cajaRepository = cajaRepository;
            _configuracionComercioRepository = configuracionComercioRepository;
            _bitacoraService = bitacoraService;
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
            try
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
                        
                        _bitacoraService.RegisterEvento(new BitacoraEventoDto
                        {
                            TablaDeEvento = "REPORTES_MENSUALES",
                            TipoDeEvento = "Actualizar",
                            DescripcionDeEvento = $"Actualización de reporte mensual para comercio {comercio.Nombre} (ID: {comercio.IdComercio})",
                            StackTrace = "",
                            DatosAnteriores = $"Comercio: {comercio.IdComercio}, Fecha: {DateTime.Now.ToString("yyyy-MM-dd")}",
                            DatosPosteriores = JsonConvert.SerializeObject(new
                            {
                                IdReporte = reporteExistente.IdReporte,
                                IdComercio = reporteExistente.IdComercio,
                                CantidadDeCajas = cantidadDeCajas,
                                MontoTotalRecaudado = montoTotalRecaudado,
                                CantidadDeSINPES = cantidadDeSINPES,
                                MontoTotalComision = montoTotalComision,
                                FechaDelReporte = DateTime.Now
                            })
                        });
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
                        
                        _bitacoraService.RegisterEvento(new BitacoraEventoDto
                        {
                            TablaDeEvento = "REPORTES_MENSUALES",
                            TipoDeEvento = "Crear",
                            DescripcionDeEvento = $"Creación de nuevo reporte mensual para comercio {comercio.Nombre} (ID: {comercio.IdComercio})",
                            StackTrace = "",
                            DatosAnteriores = "N/A",
                            DatosPosteriores = JsonConvert.SerializeObject(new
                            {
                                IdComercio = comercio.IdComercio,
                                CantidadDeCajas = cantidadDeCajas,
                                MontoTotalRecaudado = montoTotalRecaudado,
                                CantidadDeSINPES = cantidadDeSINPES,
                                MontoTotalComision = montoTotalComision,
                                FechaDelReporte = DateTime.Now
                            })
                        });
                    }
                }
                
                // Registrar evento exitoso de generacion
                _bitacoraService.RegisterEvento(new BitacoraEventoDto
                {
                    TablaDeEvento = "REPORTES_MENSUALES",
                    TipoDeEvento = "Generar",
                    DescripcionDeEvento = "Generacion de reportes mensuales completada exitosamente",
                    StackTrace = "",
                    DatosAnteriores = "N/A",
                    DatosPosteriores = "Proceso de generacion de reportes completado"
                });
            }
            catch (Exception ex)
            {
                _bitacoraService.RegisterEvento(new BitacoraEventoDto
                {
                    TablaDeEvento = "REPORTES_MENSUALES",
                    TipoDeEvento = "Error",
                    DescripcionDeEvento = "Error al generar reportes mensuales",
                    StackTrace = ex.ToString(),
                    DatosAnteriores = "N/A",
                    DatosPosteriores = ex.Message
                });
                throw;
            }
        }
    }
}
