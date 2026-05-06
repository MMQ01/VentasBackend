using BackVentasADO.Clases.DTO;
using BackVentasADO.Models;
using BackVentasADO.Models.Clases.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BackVentasADO.Services
{
    public class generalServices
    {

        public csEstadisticaEnc getEstadisticasEnc()
        {

            csEstadisticaEnc res = new csEstadisticaEnc();
            try
            {
                VentasEntities _context = new VentasEntities();

                // ← Extraer el connection string puro desde el de Entity Framework
                var efString = _context.Database.Connection.ConnectionString;

                using (SqlConnection con = new SqlConnection(efString))
                using (SqlCommand cmd = new SqlCommand("SPU_ESTADISTICA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros OUTPUT
                    SqlParameter pVentas = new SqlParameter("@oVentas", SqlDbType.Decimal);
                    pVentas.Precision = 18;
                    pVentas.Scale = 2;
                    pVentas.Direction = ParameterDirection.Output;

                    SqlParameter pPedidos = new SqlParameter("@oPedidos", SqlDbType.Int);
                    pPedidos.Direction = ParameterDirection.Output;

                    SqlParameter pClientes = new SqlParameter("@oClientes", SqlDbType.Int);
                    pClientes.Direction = ParameterDirection.Output;

                    SqlParameter pProductos = new SqlParameter("@oProductos", SqlDbType.Int);
                    pProductos.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(pVentas);
                    cmd.Parameters.Add(pPedidos);
                    cmd.Parameters.Add(pClientes);
                    cmd.Parameters.Add(pProductos);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Leer los valores OUTPUT
                    res.ventaTotales = Convert.ToDouble(pVentas.Value);
                    res.pedidoTotales = Convert.ToInt32(pPedidos.Value);
                    res.clientesTotales = Convert.ToInt32(pClientes.Value);
                    res.productosTotales = Convert.ToInt32(pProductos.Value);
                    res.Respuesta = "OK";
                }
            }
            catch (Exception ex)
            {

                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }
            return res;
        }


        public csUltimasVentas getUltimasVentas()
        {

            csUltimasVentas res = new csUltimasVentas();
            res.Detalle = new List<csUltimasVentasDetalle>();
            try
            {
                VentasEntities _context = new VentasEntities();
                var lRegistros = (from VEN in _context.SPU_ULTIMAS_VENTAS()
                                  select VEN);

                //Recorre los registros encontrados
                foreach (var lRegistro in lRegistros)
                {
                    //Instancia clase para almacenar los datos del domiciliario
                    csUltimasVentasDetalle lVenta = new csUltimasVentasDetalle();
                    lVenta.Id = lRegistro.Id;
                    lVenta.NombreCliente = lRegistro.NombreCliente;
                    lVenta.NombreUsuario = lRegistro.NombreUsuario;
                    lVenta.Origen = lRegistro.Origen;
                    lVenta.Total = lRegistro.Total;

                    //Adiciona el elemento a la lista
                    res.Detalle.Add(lVenta);
                }

                res.Respuesta = "OK";

            }
            catch (Exception ex)
            {
                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }
            return res;
        }

        public csTopUsuarios getTopUsuarios()
        {

            csTopUsuarios res = new csTopUsuarios();
            res.Detalle = new List<csTopUsuarioDetalle>();
            try
            {
                VentasEntities _context = new VentasEntities();
                var lRegistros = (from VEN in _context.SPU_TOP_USUARIO()
                                  select VEN);

                //Recorre los registros encontrados
                foreach (var lRegistro in lRegistros)
                {
                    //Instancia clase para almacenar los datos del domiciliario
                    csTopUsuarioDetalle lUsuario = new csTopUsuarioDetalle();
                    lUsuario.IdUsuario = lRegistro.IdUsuario;
                    lUsuario.NombreUsuario = lRegistro.NombreUsuario;
                    lUsuario.CantidadPedidos = (int)lRegistro.CantidadPedidos;

                    //Adiciona el elemento a la lista
                    res.Detalle.Add(lUsuario);
                }

                res.Respuesta = "OK";

            }
            catch (Exception ex)
            {
                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }
            return res;
        }


        public csReportesStock getReporteStock()
        {

            csReportesStock res = new csReportesStock();
            res.Detalle = new List<csReporteStockDetalle>();
            try
            {
                VentasEntities _context = new VentasEntities();
                var lRegistros = (from STO in _context.SPU_STOCK()
                                  select STO);

                //Recorre los registros encontrados
                foreach (var lRegistro in lRegistros)
                {
                    //Instancia clase para almacenar los datos del domiciliario
                    csReporteStockDetalle lUsuario = new csReporteStockDetalle();
                    lUsuario.IdProducto = lRegistro.Id;
                    lUsuario.NombreProducto = lRegistro.Nombre;
                    lUsuario.Stock = (double)lRegistro.Stock;
                    lUsuario.Msg = lRegistro.Msg;

                    //Adiciona el elemento a la lista
                    res.Detalle.Add(lUsuario);
                }

                res.Respuesta = "OK";

            }
            catch (Exception ex)
            {
                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }
            return res;
        }

    }
    
}
