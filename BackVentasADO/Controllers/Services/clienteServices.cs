using BackVentasADO.Models;
using BackVentasADO.Models.Clases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackVentasADO.Controllers.Services
{
    public class clienteServices
    {

        public List<ClienteViewModel> GetClientes()
        {
            VentasEntities _context = new VentasEntities();
            var lista = _context.Cliente.Join(_context.Categorias,
                          cliente => cliente.IdCategoria,
                          categoria => categoria.Id,
                  (cliente, categoria) => new ClienteViewModel
                  {
                      id = cliente.Id,
                      nombre = cliente.Nombre,
                      email = cliente.Email,
                      fechaCreacion = cliente.FechaCreacion,
                      estado = cliente.Estado,
                      identificacion = cliente.Identificacion,
                      nombreCategoria = categoria.Nombre,
                  }).ToList();

            return lista;
        }


        public crearClienteViewModel crearCliente(crearClienteViewModel cliente)
        {
            VentasEntities _context = new VentasEntities();
            var validacion = _context.Cliente.FirstOrDefault(cli => cli.Identificacion == cliente.identificacion);

            //validacion cliente existe
            if (validacion != null)
            {
                return null;
            }

            Cliente clienteCreado = new Cliente
            {
                Id = cliente.id,
                Nombre = cliente.nombre,
                Email = cliente.email,
                Contraseña = cliente.contraseña,
                Estado = "SI",
                FechaCreacion = DateTime.Today,
                IdCategoria = cliente.idCategoria,
                Identificacion = cliente.identificacion,
            };



            _context.Cliente.Add(clienteCreado);
            _context.SaveChanges();


            return cliente;

        }

        public ClienteDTO getCliente(int id)
        {
            VentasEntities _context = new VentasEntities();

            var cliente = _context.Cliente.FirstOrDefault(x => x.Id == id);

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
            return clienteDto;

        }

        public editarClienteViewModel editarCliente(editarClienteViewModel cliente)
        {
            VentasEntities _context = new VentasEntities();
            Cliente clienteEdit = _context.Cliente.Single(cli => cliente.id == cli.Id);


            clienteEdit.Nombre = cliente.nombre;
            clienteEdit.Email = cliente.email;
            clienteEdit.IdCategoria = cliente.idCategoria;




            //_context.Clientes.Entry(clienteEdit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();


            return cliente;
        }

        public string activarCliente(int id)
        {
            VentasEntities _context = new VentasEntities();
            var cliente = _context.Cliente.Where(x => x.Id == id && x.Estado == "NO").FirstOrDefault();

            if (cliente == null)
            {
                return "El Cliente ya ha sido activado o no existe";
            }
            cliente.Estado = "SI";
            _context.SaveChanges();

            return "El Cliente activado exitosamente";
        }

        public string inactivarCliente(int id)
        {
            VentasEntities _context = new VentasEntities();
            var cliente = _context.Cliente.Where(x => x.Id == id && x.Estado == "SI").FirstOrDefault();

            if (cliente == null)
            {
                return "El Cliente ya ha sido inactivado o no existe";
            }
            cliente.Estado = "NO";
            _context.SaveChanges();

            return "El Cliente inactivado exitosamente";
        }

        public string deleteCliente(int id)
        {
            VentasEntities _context = new VentasEntities();
            var cliente = _context.Cliente.FirstOrDefault(x => x.Id == id);

            _context.Cliente.Remove(cliente);
            _context.SaveChanges();

            if (cliente == null)
            {
                return "Cliente no existe";

            }
            else
            {

                return "Cliente borrado existosamente";
            }


        }
    }
}