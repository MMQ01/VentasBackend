using BackVentasADO.Models.Clases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using BackVentasADO.Models.Clases;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;
using BackVentasADO.Models;


namespace BackVentasADO.Controllers
{
    public class LoginController : ApiController
    {

        private readonly string _claveJWT = ConfigurationManager.AppSettings["JWT:ClaveJWT"];
        private readonly string _issuer = ConfigurationManager.AppSettings["JWT:Issuer"];
        private readonly string _audience = ConfigurationManager.AppSettings["JWT:Audience"];

        [HttpPost]
        [Route("api/Login")]
        public ResultadoToken Login(LoginViewModel login)
        {
            VentasEntities _context = new VentasEntities();
            ResultadoToken resultado = new ResultadoToken();
            try
            {

                var cliente = _context.Cliente.FirstOrDefault(x => x.Identificacion == login.identificacion);


                if (cliente == null)
                {
                    resultado.respuesta = "Cliente no existe";
                    resultado.mensaje = "Error";
                    return resultado;
                }
                if (cliente.Estado != "SI")
                {
                    resultado.respuesta = "Cliente Inactivo";
                    resultado.mensaje = "Error";
                    return resultado;
                }
                if (cliente.Contraseña != login.contraseña)
                {
                    resultado.respuesta = "Error en la contraseña";
                    resultado.mensaje = "Error";
                    return resultado;
                }

                var token = GenerarTokenJWT(cliente);

                var clienteDto = new ClienteDTO
                {
                    id = cliente.Id,
                    identificacion = cliente.Identificacion,
                    nombre = cliente.Nombre,
                    estado = cliente.Estado,
                    email = cliente.Email,
                    fechaCreacion = cliente.FechaCreacion,
                    idCategoria = cliente.IdCategoria
                    
                };
                resultado.respuesta = clienteDto;

                resultado.mensaje = "OK";
                resultado.token = token;
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.respuesta = ex.Message;
                resultado.mensaje = "Error";
                return resultado;
            }


        }


        private string GenerarTokenJWT(Cliente usuarioInfo)
        {
            try
            {
                // Cabecera
                var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_claveJWT));

                var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var _Header = new JwtHeader(_signingCredentials);

                // Claims
                var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.Email),
            };

                // Payload
                var _Payload = new JwtPayload(
                    issuer: _issuer,
                    audience: _audience,
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddHours(24));

                // Token
                var _Token = new JwtSecurityToken(_Header, _Payload);
                string token = new JwtSecurityTokenHandler().WriteToken(_Token);

                return token;
            }
            catch (Exception ex)
            {
                // Manejar la excepción y registrar el error
                Console.WriteLine(ex);
                throw new InvalidOperationException("Error al generar el token JWT.", ex);
            }
        }
    }
}
