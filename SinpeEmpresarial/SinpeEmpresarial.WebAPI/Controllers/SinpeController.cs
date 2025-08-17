using SinpeEmpresarial.Application.Dtos.Sinpe;
using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Shared.Models;
using System;
using System.Net;
using System.Web.Http;

namespace SinpeEmpresarial.API.Controllers
{
    public class SinpeController : ApiController
    {
        private readonly ISinpeService _sinpeService;

        public SinpeController(ISinpeService sinpeService)
        {
            _sinpeService = sinpeService ?? throw new ArgumentNullException(nameof(sinpeService));
        }

        [HttpGet]
        [Route("api/sinpe/consultar")]
        public IHttpActionResult ConsultarSinpe([FromUri] string telefonoCaja)
        { 
            if (string.IsNullOrWhiteSpace(telefonoCaja))
                return BadRequest("El teléfono de caja es requerido.");
            try
            {
                var lista = _sinpeService.GetByCajaTelefono(telefonoCaja);
                return Ok(lista);
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.BadRequest, new ResponseModel(false, ex.Message));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new ResponseModel(false, "Error inesperado: " + ex.Message));
            }
        }

        [HttpPost]
        [Route("api/sinpe/sincronizar/")]
        public IHttpActionResult SincronizarSinpe([FromBody] SinpeSyncRequestDto request)
        {
            if (request == null)
                return BadRequest("IdSinpe es requerido.");
            try
            {
                var resultado = _sinpeService.Sincronizar(request.IdSinpe);
                if (resultado.Success) return Ok(resultado);
                return Content(HttpStatusCode.BadRequest, resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.BadRequest, new ResponseModel(false, ex.Message));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new ResponseModel(false, "Error inesperado: " + ex.Message));
            }
        }

        [HttpPost]
        [Route("api/sinpe/recibir/")]
        public IHttpActionResult RecibirSinpe([FromBody] SinpeCreateDto dto)
        {
            if (dto == null) return BadRequest("El cuerpo de la solicitud es requerido.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var resultado = _sinpeService.RegisterSinpe(dto);
                if (resultado.Success) return Ok(resultado);
                return Content(HttpStatusCode.BadRequest, resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.BadRequest, new ResponseModel(false, ex.Message));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new ResponseModel(false, "Error inesperado: " + ex.Message));
            }
        }
    }
}