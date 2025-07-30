using SinpeEmpresarial.Application.Interfaces;
using SinpeEmpresarial.Web.Filters;
using System.Web.Mvc;

namespace SinpeEmpresarial.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //Filtro para excepciones de Bitácora
            var bitacoraService = DependencyResolver.Current.GetService<IBitacoraService>();
            filters.Add(new BitacoraExceptionFilter(bitacoraService));
        }
    }
}
