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

                var usuario = (from USU in _context.Usuarios
                               where USU.Login == login.Login &&
                                      USU.Contrasena == login.Password
                               select USU).FirstOrDefault();
         


                if (usuario == null)
                {
                    resultado.Mensaje = "Usuario no existe";
                    resultado.Respuesta = "ERROR";
                    return resultado;
                }
                if (!usuario.Estado)
                {
                    resultado.Mensaje = "Usuario Inactivo";
                    resultado.Respuesta = "ERROR";
                    return resultado;
                }
                if (usuario.Contrasena != login.Password)
                {
                    resultado.Mensaje = "Error en la contraseña";
                    resultado.Respuesta = "ERROR";
                    return resultado;
                }

                var token = GenerarTokenJWT(usuario);

                UsuarioDTO usuarioData = new UsuarioDTO();

                usuarioData.Id = usuario.Id;
                usuarioData.Login = usuario.Login;
                usuarioData.Nombres = usuario.Nombres;
                usuarioData.Apellidos = usuario.Apellidos;
                usuarioData.Estado = usuario.Estado ? true : false;
                usuarioData.CategoriaId = usuario.Categoria;

                var NombreCategoria = _context.Categorias.FirstOrDefault(x => x.Id == usuario.Categoria).Nombre;

                usuarioData.NombreCategoria = string.IsNullOrEmpty(NombreCategoria) ? "Sin categoria" : NombreCategoria;


                resultado.Respuesta = "OK";
                resultado.Usuario = usuarioData;
                resultado.Token = token;
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Respuesta = ex.Message;
                resultado.Mensaje = "ERROR";
                return resultado;
            }


        }


        private string GenerarTokenJWT(Usuarios usuarioInfo)
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
                new Claim(JwtRegisteredClaimNames.Name, usuarioInfo.Login),
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
