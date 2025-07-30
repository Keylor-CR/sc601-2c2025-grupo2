using Newtonsoft.Json;
using SinpeEmpresarial.Application.Dtos;
using SinpeEmpresarial.Application.Dtos.Bitacora;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Domain.Interfaces.Repositories;
using SinpeEmpresarial.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SinpeEmpresarial.Web.Filters
{
    public class BitacoraExceptionFilter : FilterAttribute, IExceptionFilter
    {
        private readonly IBitacoraService _bitacoraService;

        public BitacoraExceptionFilter(IBitacoraService bitacoraService)
        {
            _bitacoraService = bitacoraService;
        }

        public void OnException(ExceptionContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"]?.ToString().ToUpper();
            var actionName = filterContext.RouteData.Values["action"]?.ToString();
            var exception = filterContext.Exception;
            var evento = new BitacoraEventoDto
            {
                TablaDeEvento = $"{controllerName}S",
                TipoDeEvento = "Error",
                DescripcionDeEvento = exception.Message,
                StackTrace = exception.StackTrace.ToString(),
                DatosAnteriores = null,
                DatosPosteriores = null
            };
            _bitacoraService.RegisterEvento(evento);
            var errorModel = new ErrorViewModel
            {
                Title = "Error",
                Message = filterContext.Exception.Message
            };
            var result = new ViewResult
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary<ErrorViewModel>(errorModel)
            };
            filterContext.Result = result;
            filterContext.ExceptionHandled = true;
        }
    }
}