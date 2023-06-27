using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using Backend_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;

namespace Backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorAPIController : ControllerBase
    {

        [HttpGet]
        [Route("api/v1/emisores")]
        public async Task<ActionResult<List<Emisor>>> GetEmisoresAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/GetEmisor");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return Ok(json);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        private readonly HttpClient _httpClient;

        public ControladorAPIController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel login)
        {
            _httpClient.BaseAddress = new Uri("http://apiservicios.ecuasolmovsa.com:3009");

            var response = await _httpClient.GetAsync($"/api/Usuarios?usuario={login.usuario}&password={login.contrasena}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/v1/centrocostos")]
        public async Task<ActionResult<List<CentroCostos>>> GetCentroCostosAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosSelect");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpGet]
        [Route("CentroCostosInsert")]
        public async Task<ActionResult> AgregarCentroCostoAsync(int codigoCentroCostos, string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de codigoCentroCostos es: " + codigoCentroCostos);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosInsert?codigocentrocostos={codigoCentroCostos}&descripcioncentrocostos={descripcionCentroCostos}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/centrocostos/delete")]
        public async Task<ActionResult> DeleteCentroCostosAsync(int codigoCentroCostos, string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de codigoCentroCostos es: " + codigoCentroCostos);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosDelete?codigocentrocostos={codigoCentroCostos}&descripcioncentrocostos={descripcionCentroCostos}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/centrocostos/search")]
        public async Task<ActionResult> SearchCentroCostosAsync(string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de descripcion es: " + descripcionCentroCostos);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosSearch?descripcioncentrocostos={descripcionCentroCostos}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("CentroCostosEdit")]
        public async Task<ActionResult> EditarCentroCostoAsync(int codigoCentroCostos, string descripcionCentroCostos)
        {
            Console.WriteLine("El valor de codigoCentroCostos es: " + codigoCentroCostos);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CentroCostosUpdate?codigocentrocostos={codigoCentroCostos}&descripcioncentrocostos={descripcionCentroCostos}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/GetMovimientosPlanilla")]
        public async Task<ActionResult<string>> GetMovimientosPlanillaAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimientoPlanillaSelect");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpGet("loginAutorizador")]
        public async Task<ActionResult> Login(string usuario, string password)
        {
            _httpClient.BaseAddress = new Uri("http://apiservicios.ecuasolmovsa.com:3009");

            var response = await _httpClient.GetAsync($"/api/Varios/GetAutorizador?usuario={usuario}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("MovimientoPlanillaInsert")]
        public async Task<ActionResult> InsertarMovimientoPlanillaAsync(string conceptos, int prioridad, string tipooperacion, int cuenta1, int cuenta2, int cuenta3, int cuenta4, string MovimientoExcepcion1, string MovimientoExcepcion2, string MovimientoExcepcion3, int Traba_Aplica_iess, int Traba_Proyecto_imp_renta, int Aplica_Proy_Renta, int Empresa_Afecta_Iess)
        {
            Console.WriteLine("El valor de conceptos es: " + conceptos);

            var httpClient = new HttpClient();
            var url = $"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimientoPlanillaInsert?conceptos={conceptos}&prioridad={prioridad}&tipooperacion={tipooperacion}&cuenta1={cuenta1}&cuenta2={cuenta2}&cuenta3={cuenta3}&cuenta4={cuenta4}&MovimientoExcepcion1={MovimientoExcepcion1}&MovimientoExcepcion2={MovimientoExcepcion2}&MovimientoExcepcion3={MovimientoExcepcion3}&Traba_Aplica_iess={Traba_Aplica_iess}&Traba_Proyecto_imp_renta={Traba_Proyecto_imp_renta}&Aplica_Proy_Renta={Aplica_Proy_Renta}&Empresa_Afecta_Iess={Empresa_Afecta_Iess}";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("ObtenerMovimientosExcepcion1y2")]
        public async Task<ActionResult> ObtenerMovimientosExcepcionAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimientosExcepcion1y2";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("ObtenerMovimientosExcepcion3")]
        public async Task<ActionResult> ObtenerMovimientosExcepcion3Async()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimientosExcepcion3";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetTipoOperacion")]
        public async Task<ActionResult> GetTipoOperacionAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoOperacion";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetTrabaAfectaIESS")]
        public async Task<ActionResult> GetTrabaAfectaIessAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabaAfectaIESS";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetTrabAfecImpuestoRenta")]
        public async Task<ActionResult> GetTrabAfecImpuestoRentaAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabAfecImpuestoRenta";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/movimientoPlanilla/delete")]
        public async Task<ActionResult> DeleteMovimientoPlanillaAsync(int codigomovimiento, string descripcionomovimiento)
        {
            Console.WriteLine("El valor de codigo es: " + codigomovimiento);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimeintoPlanillaDelete?codigomovimiento={codigomovimiento}&descripcionomovimiento={descripcionomovimiento}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/movimientoPlanilla/edit")]
        public async Task<ActionResult> EditarMovimientoPlanillaAsync(int codigoplanilla, string conceptos, int prioridad, string tipooperacion, int cuenta1, int cuenta2, int cuenta3, int cuenta4, string MovimientoExcepcion1, string MovimientoExcepcion2, string MovimientoExcepcion3, int Traba_Aplica_iess, int Traba_Proyecto_imp_renta, int Aplica_Proy_Renta, int Empresa_Afecta_Iess)
        {
            Console.WriteLine("El valor de codigoMovimientoPlanilla es: " + codigoplanilla);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimientoPlanillaUpdate?codigoplanilla={codigoplanilla}&conceptos={conceptos}&prioridad={prioridad}&tipooperacion={tipooperacion}&cuenta1={cuenta1}&cuenta2={cuenta2}&cuenta3={cuenta3}&cuenta4={cuenta4}&MovimientoExcepcion1={MovimientoExcepcion1}&MovimientoExcepcion2={MovimientoExcepcion2}&MovimientoExcepcion3={MovimientoExcepcion3}&Traba_Aplica_iess={Traba_Aplica_iess}&Traba_Proyecto_imp_renta={Traba_Proyecto_imp_renta}&Aplica_Proy_Renta={Aplica_Proy_Renta}&Empresa_Afecta_Iess={Empresa_Afecta_Iess}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/movimientoPlanilla/search")]
        public async Task<ActionResult> SearchMovimientoPlanillasAsync(string concepto)
        {
            Console.WriteLine("El valor de descripcion es: " + concepto);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/MovimientoPlanillaSearch?Concepto={concepto}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/select")]
        public async Task<ActionResult<string>> GetTrabajadorAsync(int sucursal)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorSelect?sucursal={sucursal}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpGet]
        [Route("trabajador/delete")]
        public async Task<ActionResult> DeleteTrabajadorAsync(int sucursal, string codigoempleado)
        {

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorDelete?sucursal={sucursal}&codigoempleado={codigoempleado}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetTipoTrabajador")]
        public async Task<ActionResult> GetTipoTrabajadorAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoTrabajador";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetGenero")]
        public async Task<ActionResult> GetGeneroAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/Genero";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetEstadoTrabajador")]
        public async Task<ActionResult> GetEstadoTrabajadorAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/EstadoTrabajador";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetTipoContrato")]
        public async Task<ActionResult> GetTipoContratoAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoContrato";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetTipoCese")]
        public async Task<ActionResult> GetTipoCeseAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoCese";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetEstadoCivil")]
        public async Task<ActionResult> GetEstadoCivilAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/EstadoCivil";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetEsReingreso")]
        public async Task<ActionResult> GetEsReingresoAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/EsReingreso";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetTipoCuenta")]
        public async Task<ActionResult> GetTipoCuentaAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoCuenta";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetPeriodoVacaciones")]
        public async Task<ActionResult> GetPeriodoVacacionesAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/PeriodoVacaciones";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetTipoComision")]
        public async Task<ActionResult> GetTipoComisionAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TipoComision";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetDecimoTerceroDecimoCuarto")]
        public async Task<ActionResult> GetDecimoTerceroDecimoCuartoAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/DecimoTerceroDecimoCuarto";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetFondoReserva")]
        public async Task<ActionResult> GetFondoReservaAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/FondoReserva";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetCategoriaOcupacional")]
        public async Task<ActionResult> GetCategoriaOcupacionalAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/CategoriaOcupacional";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("trabajador/GetNivelSalarial")]
        public async Task<ActionResult> GetNivelSalarialAsync()
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/NivelSalarial";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("trabajador/Insert")]
        public async Task<ActionResult> InsertarTrabajadorAsync(int COMP_Codigo, string Tipo_trabajador, string Apellido_Paterno, string Apellido_Materno, string Nombres, string Identificacion,
        string Entidad_Bancaria, string CarnetIESS, string Direccion, string Telefono_Fijo, string Telefono_Movil, string Genero, string Nro_Cuenta_Bancaria, string Codigo_Categoria_Ocupacion,
        string Ocupacion, string Centro_Costos, string Nivel_Salarial, string EstadoTrabajador, string Tipo_Contrato, string? Tipo_Cese, string EstadoCivil, string? TipodeComision, DateTime FechaNacimiento,
        DateTime FechaIngreso, DateTime FechaCese, int PeriododeVacaciones, DateTime FechaReingreso, DateTime Fecha_Ult_Actualizacion, string EsReingreso, string Tipo_Cuenta,
        int FormaCalculo13ro, int FormaCalculo14ro, int BoniComplementaria, int BoniEspecial, int Remuneracion_Minima, string Fondo_Reserva)
        {
            var httpClient = new HttpClient();

            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorInsert";

            var requestData = new Dictionary<string, string>
            {
                { "COMP_Codigo", COMP_Codigo.ToString() },
                { "Tipo_trabajador", Tipo_trabajador },
                { "Apellido_Paterno", Apellido_Paterno },
                { "Apellido_Materno", Apellido_Materno },
                { "Nombres", Nombres },
                { "Identificacion", Identificacion },
                { "Entidad_Bancaria", Entidad_Bancaria },
                { "CarnetIESS", CarnetIESS },
                { "Direccion", Direccion },
                { "Telefono_Fijo", Telefono_Fijo },
                { "Telefono_Movil", Telefono_Movil },
                { "Genero", Genero },
                { "Nro_Cuenta_Bancaria", Nro_Cuenta_Bancaria },
                { "Codigo_Categoria_Ocupacion", Codigo_Categoria_Ocupacion },
                { "Ocupacion", Ocupacion },
                { "Centro_Costos", Centro_Costos },
                { "Nivel_Salarial", Nivel_Salarial },
                { "EstadoTrabajador", EstadoTrabajador },
                { "Tipo_Contrato", Tipo_Contrato },
                { "Tipo_Cese", Tipo_Cese },
                { "EstadoCivil", EstadoCivil },
                { "TipodeComision", TipodeComision },
                { "FechaNacimiento", FechaNacimiento.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                { "FechaIngreso", FechaIngreso.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                { "FechaCese", FechaCese.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                { "PeriododeVacaciones", PeriododeVacaciones.ToString() },
                { "FechaReingreso", FechaReingreso.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                { "Fecha_Ult_Actualizacion", Fecha_Ult_Actualizacion.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                { "EsReingreso", EsReingreso },
                { "Tipo_Cuenta", Tipo_Cuenta },
                { "FormaCalculo13ro", FormaCalculo13ro.ToString() },
                { "FormaCalculo14ro", FormaCalculo14ro.ToString() },
                { "BoniComplementaria", BoniComplementaria.ToString() },
                { "BoniEspecial", BoniEspecial.ToString() },
                { "Remuneracion_Minima", Remuneracion_Minima.ToString() },
                { "Fondo_Reserva", Fondo_Reserva }
            };

            var content = new FormUrlEncodedContent(requestData);

            var response = await httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (responseContent.Contains("Ingreso Exitoso"))
            {
                return Ok("Ingreso Exitoso");
            }
            else
            {
                return BadRequest("Error en la API: " + responseContent);
            }
        }


        [HttpPost]
        [Route("trabajador/Edit")]
        public async Task<ActionResult> EditarTrabajadorAsync(int COMP_Codigo, int Id_Trabajador, string Tipo_trabajador, string Apellido_Paterno, string Apellido_Materno, string Nombres, string Identificacion,
        string Entidad_Bancaria, string CarnetIESS, string Direccion, string Telefono_Fijo, string Telefono_Movil, string Genero, string Nro_Cuenta_Bancaria, string Codigo_Categoria_Ocupacion,
        string Ocupacion, string Centro_Costos, string Nivel_Salarial, string EstadoTrabajador, string Tipo_Contrato, string? Tipo_Cese, string EstadoCivil, string? TipodeComision, DateTime FechaNacimiento,
        DateTime FechaIngreso, DateTime FechaCese, int PeriododeVacaciones, DateTime FechaReingreso, DateTime Fecha_Ult_Actualizacion, string EsReingreso, string Tipo_Cuenta,
        int FormaCalculo13ro, int FormaCalculo14ro, int BoniComplementaria, int BoniEspecial, int Remuneracion_Minima, string Fondo_Reserva)
        {
            var httpClient = new HttpClient();
            var url = "http://apiservicios.ecuasolmovsa.com:3009/api/Varios/TrabajadorUpdate";

            var requestData = new Dictionary<string, string>
            {
                { "COMP_Codigo", COMP_Codigo.ToString() },
                { "Id_Trabajador", Id_Trabajador.ToString() },
                { "Tipo_trabajador", Tipo_trabajador },
                { "Apellido_Paterno", Apellido_Paterno },
                { "Apellido_Materno", Apellido_Materno },
                { "Nombres", Nombres },
                { "Identificacion", Identificacion },
                { "Entidad_Bancaria", Entidad_Bancaria },
                { "CarnetIESS", CarnetIESS },
                { "Direccion", Direccion },
                { "Telefono_Fijo", Telefono_Fijo },
                { "Telefono_Movil", Telefono_Movil },
                { "Genero", Genero },
                { "Nro_Cuenta_Bancaria", Nro_Cuenta_Bancaria },
                { "Codigo_Categoria_Ocupacion", Codigo_Categoria_Ocupacion },
                { "Ocupacion", Ocupacion },
                { "Centro_Costos", Centro_Costos },
                { "Nivel_Salarial", Nivel_Salarial },
                { "EstadoTrabajador", EstadoTrabajador },
                { "Tipo_Contrato", Tipo_Contrato },
                { "Tipo_Cese", Tipo_Cese },
                { "EstadoCivil", EstadoCivil },
                { "TipodeComision", TipodeComision },
                { "FechaNacimiento", FechaNacimiento.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                { "FechaIngreso", FechaIngreso.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                { "FechaCese", FechaCese.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                { "PeriododeVacaciones", PeriododeVacaciones.ToString() },
                { "FechaReingreso", FechaReingreso.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                { "Fecha_Ult_Actualizacion", Fecha_Ult_Actualizacion.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
                { "EsReingreso", EsReingreso },
                { "Tipo_Cuenta", Tipo_Cuenta },
                { "FormaCalculo13ro", FormaCalculo13ro.ToString() },
                { "FormaCalculo14ro", FormaCalculo14ro.ToString() },
                { "BoniComplementaria", BoniComplementaria.ToString() },
                { "BoniEspecial", BoniEspecial.ToString() },
                { "Remuneracion_Minima", Remuneracion_Minima.ToString() },
                { "Fondo_Reserva", Fondo_Reserva }
            };

            var content = new FormUrlEncodedContent(requestData);

            var response = await httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (responseContent.Contains("Ingreso Exitoso"))
            {
                return Ok("Ingreso Exitoso");
            }
            else
            {
                return BadRequest("Error en la API: " + responseContent);
            }
        }


    }
}