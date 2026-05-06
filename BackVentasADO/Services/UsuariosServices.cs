using BackVentasADO.Models;
using BackVentasADO.Models.Clases;
using BackVentasADO.Models.Clases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackVentasADO.Controllers.Services
{
    public class UsuariosServices
    {

        public ListaUsuarios GetListaUsuarios()
        {
                ListaUsuarios resultado = new ListaUsuarios();

                VentasEntities _context = new VentasEntities();
            try
            {


                var lista =
                        (from usu in _context.Usuarios
                         join cat in _context.Categorias
                            on usu.Categoria equals cat.Id
                         select new UsuarioDTO
                         {
                             Id = usu.Id,
                             Login = usu.Login,
                             Nombres = usu.Nombres,
                             Apellidos = usu.Apellidos,
                             Estado = usu.Estado ? true : false,
                             CategoriaId = usu.Categoria,
                             NombreCategoria = cat.Nombre
                         }).ToList();


                resultado.Respuesta = "OK";
                resultado.Lista_Usuarios = lista;

            }
            catch (Exception ex)
            {
                resultado.Respuesta = ex.Message;
                resultado.Mensaje = "ERROR";
                return resultado;
            }


            return resultado;
        }

        public ResultadoUsuarioDTO GetUsuario(int ID)
        {
            ResultadoUsuarioDTO resultado = new ResultadoUsuarioDTO();

            VentasEntities _context = new VentasEntities();
            try
            {


                var usuario =
                        (from usu in _context.Usuarios
                         join cat in _context.Categorias
                            on usu.Categoria equals cat.Id
                         where 
                            usu.Id == ID
                            
                         select new UsuarioDTO
                         {
                             Id = usu.Id,
                             Login = usu.Login,
                             Nombres = usu.Nombres,
                             Apellidos = usu.Apellidos,
                             Estado = usu.Estado ? true : false,
                             CategoriaId = usu.Categoria,
                             NombreCategoria = cat.Nombre
                         }).FirstOrDefault();


                if (usuario == null)
                {
                    resultado.Respuesta = "ERROR";
                    resultado.Mensaje = "Usuario no existe o está inactivo";
                    return resultado;
                }

                resultado.Respuesta = "OK";
                resultado.Usuario = usuario;
   

            }
            catch (Exception ex)
            {
                resultado.Respuesta = ex.Message;
                resultado.Mensaje = "ERROR";
                return resultado;
            }


            return resultado;
        }

        public Resultado CrearUsuario(crearUsuario Usu)
        {
            Resultado resultado = new Resultado();

            VentasEntities _context = new VentasEntities();
            try
            {

                var usuExiste = (from USU in _context.Usuarios
                                 where USU.Login == Usu.Login
                                 select USU).FirstOrDefault();

                if (usuExiste != null)
                {
                    if (!usuExiste.Estado)
                    {
                        resultado.Respuesta = "ERROR";
                        resultado.Mensaje = "El usuario ya existe y esta inactivo";
                    }
                    else
                    {
                        resultado.Respuesta = "ERROR";
                        resultado.Mensaje = "El usuario ya existe";
                    }

                    return resultado;
                }

                Usuarios newUsuario = new Usuarios();

                newUsuario.Nombres = Usu.Nombres;
                newUsuario.Apellidos = Usu.Apellidos;
                newUsuario.Login = Usu.Login;
                newUsuario.Contrasena = Usu.Contrasena;
                newUsuario.Estado = true;
                newUsuario.Categoria = Usu.CategoriaId;

                _context.Usuarios.Add(newUsuario);
                
                _context.SaveChanges();

                resultado.Respuesta = "OK";
            }
            catch (Exception ex)
            {
                resultado.Respuesta = ex.Message;
                resultado.Mensaje = "ERROR";
                return resultado;
            }


            return resultado;
        }

        public Resultado EditarUsuario(UsuarioDTO Usu)
        {
            Resultado resultado = new Resultado();

            VentasEntities _context = new VentasEntities();
            try
            {

                var usuExiste = (from USU in _context.Usuarios
                                 where  USU.Login == Usu.Login &&
                                        USU.Id == Usu.Id
                                 select USU).Count();

                if (usuExiste == 0)
                {
                    resultado.Respuesta = "ERROR";
                    resultado.Mensaje = "El usuario no existe";

                    return resultado;
                }


                var editUsu = (from USU in _context.Usuarios
                                 where USU.Login == Usu.Login &&
                                        USU.Id == Usu.Id
                                 select USU).FirstOrDefault();



                editUsu.Nombres = Usu.Nombres;
                editUsu.Apellidos = Usu.Apellidos;
                editUsu.Categoria = Usu.CategoriaId;
                editUsu.Estado = true;

                _context.SaveChanges();

                resultado.Respuesta = "OK";

            }
            catch (Exception ex)
            {
                resultado.Respuesta = ex.Message;
                resultado.Mensaje = "ERROR";
                return resultado;
            }


            return resultado;
        }

        public Resultado InactivarUsuario(string Login, int ID)
        {
            Resultado resultado = new Resultado();

            VentasEntities _context = new VentasEntities();
            try
            {

                var usuExiste = (from USU in _context.Usuarios
                                 where USU.Login == Login &&
                                        USU.Id == ID
                                 select USU).FirstOrDefault();

                if (usuExiste == null)
                {
                    resultado.Respuesta = "ERROR";
                    resultado.Mensaje = "El usuario no existe";

                
                }
                else if (usuExiste != null && !usuExiste.Estado)
                {
                    resultado.Respuesta = "ERROR";
                    resultado.Mensaje = "El usuario ya se encuentra inactivo";
                }


                usuExiste.Estado = false;


                _context.SaveChanges();

                resultado.Respuesta = "OK";

            }
            catch (Exception ex)
            {
                resultado.Respuesta = ex.Message;
                resultado.Mensaje = "ERROR";
                return resultado;
            }


            return resultado;
        }

        public Resultado ActivarUsuario(string Login, int ID)
        {
            Resultado resultado = new Resultado();

            VentasEntities _context = new VentasEntities();
            try
            {

                var usuExiste = (from USU in _context.Usuarios
                                 where USU.Login == Login &&
                                        USU.Id == ID
                                 select USU).FirstOrDefault();

                if (usuExiste == null)
                {
                    resultado.Respuesta = "ERROR";
                    resultado.Mensaje = "El usuario no existe";


                }
                else if (usuExiste != null && usuExiste.Estado)
                {
                    resultado.Respuesta = "ERROR";
                    resultado.Mensaje = "El usuario ya se encuentra activo";
                }


                usuExiste.Estado = true;


                _context.SaveChanges();

                resultado.Respuesta = "OK";

            }
            catch (Exception ex)
            {
                resultado.Respuesta = ex.Message;
                resultado.Mensaje = "ERROR";
                return resultado;
            }


            return resultado;
        }

        public ResultadoAsignacionCliente AsignarUsuarioCliente(int UsuarioID, int ClienteID)
        {
            ResultadoAsignacionCliente resultado = new ResultadoAsignacionCliente();

            VentasEntities _context = new VentasEntities();
            try
            {

               
                    csAsignacioCliente asignacionCliente = new csAsignacioCliente();

                    asignacionCliente.UsuarioID = UsuarioID;
                    asignacionCliente.ClienteID = ClienteID;

                    var lAsignacion = (from ASI in _context.Asignacion_Clientes
                                       where UsuarioID == ASI.Usuario_Id &&
                                             ClienteID == ASI.Cliente_Id
                                       select ASI).FirstOrDefault();


                    if (lAsignacion != null)
                    {
                        if (lAsignacion.Estado)
                        {
                            asignacionCliente.Resultado = "ERROR";
                            asignacionCliente.Mensaje = "La asignación al cliente " + ClienteID + " ya existe";
                        }
                        else
                        {
                            lAsignacion.Estado = true;
                        }

                        resultado.Asignacion_Cliente = asignacionCliente;

                        _context.SaveChanges();
                        return resultado; ;

                    }

                    Asignacion_Clientes newAsignacion = new Asignacion_Clientes();
                    newAsignacion.Usuario_Id = UsuarioID;
                    newAsignacion.Cliente_Id = ClienteID;
                    newAsignacion.Estado = true;


                    asignacionCliente.Resultado = "OK";
                    asignacionCliente.Mensaje = "Asignado correctamente";

                    resultado.Asignacion_Cliente = asignacionCliente;
                    _context.Asignacion_Clientes.Add(newAsignacion);

                


                resultado.Respuesta = "OK";

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Respuesta = "ERROR";
                return resultado;
            }


            return resultado;
        }

        public ResultadoAsignacionCliente InactivarUsuarioCliente(int UsuarioID, int ClienteID)
        {
            ResultadoAsignacionCliente resultado = new ResultadoAsignacionCliente();

            VentasEntities _context = new VentasEntities();
            try
            {

          
                    csAsignacioCliente asignacionCliente = new csAsignacioCliente();

                    asignacionCliente.UsuarioID = UsuarioID;
                    asignacionCliente.ClienteID = ClienteID;

                    var lAsignacion = (from ASI in _context.Asignacion_Clientes
                                       where UsuarioID == ASI.Usuario_Id &&
                                             ClienteID == ASI.Cliente_Id
                                       select ASI).FirstOrDefault();


                    if (lAsignacion == null)
                    {
                 
                            asignacionCliente.Resultado = "ERROR";
                            asignacionCliente.Mensaje = "La asignación al cliente " + ClienteID + "no existe";

                            resultado.Asignacion_Cliente = asignacionCliente;


                    }

                    lAsignacion.Estado = false;


                    asignacionCliente.Resultado = "OK";
                    asignacionCliente.Mensaje = "Asignado correctamente";
                    resultado.Asignacion_Cliente = asignacionCliente;

           

                resultado.Respuesta = "OK";
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Respuesta = "ERROR";
                return resultado;
            }


            return resultado;
        }


        public csListaCliente getClientesXUsuario(int UsuarioID)
        {
            csListaCliente resultado = new csListaCliente();

            VentasEntities _context = new VentasEntities();
            try
            {


              

                var lClientes = (from ASI in _context.Asignacion_Clientes
                                 join CLI in _context.Clientes
                                     on ASI.Cliente_Id equals CLI.Id
                                 where ASI.Estado == true &&
                                       CLI.Estado == true &&
                                       ASI.Usuario_Id ==UsuarioID
                                 select CLI).ToList();


                if (!lClientes.Any())
                {
                    resultado.Respuesta = "OK";
                    return resultado;

                }

                foreach (Clientes lCliente in lClientes)
                {
                    ClienteDTO cliente = new ClienteDTO();

                    cliente.Id = lCliente.Id;
                    cliente.Nombre = lCliente.Nombre;
                    cliente.Nit = lCliente.Nit;
                    cliente.Direccion = lCliente.Direccion;
                    cliente.Estado = lCliente.Estado;

                    resultado.Lista_Clientes.Add(cliente);

                }


                resultado.Respuesta = "OK";

            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Respuesta = "ERROR";
                return resultado;
            }


            return resultado;
        }  
        
        
        public csListaCliente getClientesSINAsignarXUsuario(int UsuarioID)
        {
            csListaCliente resultado = new csListaCliente();

            VentasEntities _context = new VentasEntities();
            try
            {




                var lClientes = (from CLI in _context.Clientes
                                 join ASI in _context.Asignacion_Clientes
                                     on new { Cliente_Id = CLI.Id, Usuario_Id = UsuarioID, Estado = true }
                                     equals new { ASI.Cliente_Id, ASI.Usuario_Id, ASI.Estado }
                                     into ASI_LEFT
                                 from ASI in ASI_LEFT.DefaultIfEmpty()
                                 where ASI == null &&
                                       CLI.Estado == true
                                 select CLI).ToList();


                if (!lClientes.Any())
                {
                    resultado.Respuesta = "OK";
                    return resultado;

                }

                foreach (Clientes lCliente in lClientes)
                {
                    ClienteDTO cliente = new ClienteDTO();

                    cliente.Id = lCliente.Id;
                    cliente.Nombre = lCliente.Nombre;
                    cliente.Nit = lCliente.Nit;
                    cliente.Direccion = lCliente.Direccion;
                    cliente.Estado = lCliente.Estado;

                    resultado.Lista_Clientes.Add(cliente);

                }


                resultado.Respuesta = "OK";

            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
                resultado.Respuesta = "ERROR";
                return resultado;
            }


            return resultado;
        }



    }
}