using BackVentasADO.Models.Clases.DTO;
using BackVentasADO.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BackVentasADO.Controllers.Services
{
    public class productosServices
    {


        public productoViewModel crearProducto(productoViewModel prod)
        {
            VentasEntities _context = new VentasEntities();

            Productos producto = new Productos
            {
                Id = prod.id,
                Nombre = prod.nombre,
                Precio = prod.precio,
                Descripcion = prod.descripcion,
                Estado = "SI"
            };

            _context.Productos.Add(producto);
            _context.SaveChanges();

            productoViewModel productoCreado = new productoViewModel
            {
                id = producto.Id,
                nombre = producto.Nombre,
                precio = (decimal) producto.Precio,
                descripcion = producto.Descripcion,
            };

            return productoCreado;
        }


        public List<productoViewModel> getProductosActivos()
        {
            VentasEntities _context = new VentasEntities();

            var lista = _context.Productos
                .Where(x => x.Estado == "SI")
                .Select(x => new productoViewModel
                {
                    id = x.Id,
                    nombre = x.Nombre,
                    precio = (decimal) x.Precio,
                    descripcion = x.Descripcion,
                    estado = x.Estado

                })
                .ToList();

            return lista;
        }

        public List<productoViewModel> getProductosAll()
        {
            VentasEntities _context = new VentasEntities();
            var lista = _context.Productos
                .Select(x => new productoViewModel
                {
                    id = x.Id,
                    nombre = x.Nombre,
                    precio = (decimal)x.Precio,
                    descripcion = x.Descripcion,
                    estado = x.Estado

                })
                .ToList();

            return lista;
        }

        public productoViewModel getProducto(int id)
        {
            VentasEntities _context = new VentasEntities();

            var producto = _context.Productos
                .Where(x => x.Id == id && x.Estado == "SI")
                .Select(x => new productoViewModel
                {
                    id = x.Id,
                    nombre = x.Nombre,
                    precio = (decimal) x.Precio,
                    descripcion = x.Descripcion,

                }).FirstOrDefault();



            return producto;
        }

        public string inactivarProducto(int id)
        {
            VentasEntities _context = new VentasEntities();
            var producto = _context.Productos.Where(x => x.Id == id && x.Estado == "SI").FirstOrDefault();

            if (producto == null)
            {
                return "Producto ya ha sido inactivado o no existe";
            }
            producto.Estado = "NO";
            _context.SaveChanges();

            return "Producto inactivado exitosamente";

        }

        public string activarProducto(int id)
        {
            VentasEntities _context = new VentasEntities();
            var producto = _context.Productos.Where(x => x.Id == id && x.Estado == "NO").FirstOrDefault();

            if (producto == null)
            {
                return "Producto ya ha sido activado o no existe";
            }
            producto.Estado = "SI";
            _context.SaveChanges();

            return "Producto activado exitosamente";
        }


        public productoViewModel editProducto(productoViewModel prod)
        {
            VentasEntities _context = new VentasEntities();
            Productos producto = _context.Productos.Single(p => prod.id == p.Id && p.Estado == "SI");
            producto.Nombre = prod.nombre;
            producto.Precio = prod.precio;
            producto.Descripcion = prod.descripcion;

            
            _context.SaveChanges();

            productoViewModel productoEditado = new productoViewModel
            {
                id = producto.Id,
                nombre = producto.Nombre,
                precio = (decimal) producto.Precio,
                descripcion = producto.Descripcion,
            };

            return productoEditado;
        }


        public string deleteProducto(int id)
        {
            VentasEntities _context = new VentasEntities();
            var producto = _context.Productos.Where(x => x.Id == id && x.Estado == "SI").FirstOrDefault();

            if (producto == null)
            {
                return "Producto ya ha sido eliminado o no existe";
            }
            _context.Productos.Remove(producto);
            _context.SaveChanges();

            return "Producto eliminado exitosamente";
        }
    }
}