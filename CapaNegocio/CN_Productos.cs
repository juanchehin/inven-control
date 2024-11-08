using CapaDatos;
using System.Data;


namespace CapaNegocio
{
    public class CN_Productos
    {
        private CD_Productos objetoCD = new CD_Productos();

        //Método Insertar que llama al método Insertar de la clase DArticulo
        //de la CapaDatos
        public static string Insertar(string nombre, string Codigo, string PrecioCompra, string PrecioVenta, string PrecioOferta,
            string Descripcion, string Stock, string StockAlerta, string Categoria, string unidad, string nro_rack, string nivel)
        {
            CD_Productos Obj = new CD_Productos();
            Obj.Producto = nombre;
            Obj.Descripcion = Descripcion;
            Obj.Codigo = Codigo;
            Obj.PrecioCompra = PrecioCompra;
            Obj.PrecioVenta = PrecioVenta;
            Obj.PrecioOferta = PrecioOferta;
            Obj.Stock = Stock;
            Obj.StockAlerta = StockAlerta;
            Obj.Categoria = Categoria;
            Obj.Unidad = unidad;
            Obj.NroRack = nro_rack;
            Obj.Nivel = nivel;

            return Obj.Insertar(Obj);
        }

        public DataSet ListarProductos(int pDesde)
        {

            DataSet tabla = new DataSet();
            tabla = objetoCD.ListarProductos(pDesde);
            return tabla;
        }

        public DataSet ListarTodosProductos()
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.ListarTodosProductos();
            return tabla;
        }

        // Devuelve solo un producto
        public DataTable MostrarProducto(int IdProducto)
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.MostrarProducto(IdProducto);
            return tabla;
        }
        public static string Eliminar(int IdProducto)
        {
            CD_Productos Obj = new CD_Productos();
            Obj.IdProducto = IdProducto;
            return Obj.Eliminar(Obj);
        }


        public static string Editar(int IdProducto, string Producto, string Codigo, string PrecioCompra, string PrecioVenta,
            string PrecioOferta, string Descripcion, string Stock, string StockAlerta, string Categoria, string unidad
            , string nro_rack, string nivel)
        {
            CD_Productos Obj = new CD_Productos();
            Obj.IdProducto = IdProducto;

            Obj.Producto = Producto;
            Obj.Codigo = Codigo;
            Obj.PrecioCompra = PrecioCompra;
            Obj.PrecioVenta = PrecioVenta;
            Obj.PrecioOferta = PrecioOferta;
            Obj.Descripcion = Descripcion;
            Obj.Stock = Stock;
            Obj.StockAlerta = StockAlerta;
            Obj.Categoria = Categoria;
            Obj.Unidad = unidad;
            Obj.NroRack = nro_rack;
            Obj.Nivel = nivel;

            return Obj.Editar(Obj);
        }

        public DataTable BuscarProducto(string textobuscar)
        {
            CD_Productos Obj = new CD_Productos();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarProducto(Obj);
        }

        /*
        public DataTable BuscarProductoAutoComplete(string textobuscar)
        {
            CD_Productos Obj = new CD_Productos();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarProductoAutoComplete(Obj);
        }
        */
        public DataTable BuscarProductoPorCodigo(string codigo)
        {
            CD_Productos Obj = new CD_Productos();
            Obj.Codigo = codigo;
            return Obj.BuscarProductoPorCodigo(Obj);
        }


        public static string ActualizacionPorcentual(decimal pPorcentaje, int desde, int hasta)
        {
            CD_Productos Obj = new CD_Productos();

            return Obj.ActualizacionPorcentual(pPorcentaje, desde, hasta);
        }

        public static string ActualizacionLineal(decimal pValor, int desde, int hasta)
        {
            CD_Productos Obj = new CD_Productos();

            return Obj.ActualizacionLineal(pValor, desde, hasta);
        }

        public DataSet productos_rack()
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.productos_rack();
            return tabla;
        }

        public DataSet cargar_ingresos_egresos()
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.cargar_ingresos_egresos();
            return tabla;
        }

        public DataSet top_ingresos_productos()
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.top_ingresos_productos();
            return tabla;
        }

        public DataSet top_egresos_productos()
        {
            DataSet tabla = new DataSet();
            tabla = objetoCD.top_egresos_productos();
            return tabla;
        }
        // =========================
        // Categorias
        // =======================

        public DataTable DameCategorias()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.DameCategorias();
            return tabla;
        }

        public static string AltaCategoria(string Categoria)
        {
            CD_Productos Obj = new CD_Productos();

            return Obj.AltaCategoria(Categoria);
        }

        public static string DameCategoria(string Categoria)
        {
            CD_Productos Obj = new CD_Productos();

            return Obj.DameCategoria(Categoria);
        }

        public static string DameCategoriaPorId(int IdCategoria)
        {
            CD_Productos Obj = new CD_Productos();

            return Obj.DameCategoriaPorId(IdCategoria);
        }

        public static string EditarCategoria(int IdCategoria, string Categoria)
        {
            CD_Productos Obj = new CD_Productos();

            return Obj.EditarCategoria(IdCategoria, Categoria);
        }

        public DataTable BuscarCategoria(string textobuscar)
        {
            CD_Productos Obj = new CD_Productos();

            return Obj.BuscarCategoria(textobuscar);
        }
        public static string EliminarCategoria(int IdCategoria)
        {
            CD_Productos Obj = new CD_Productos();

            return Obj.EliminarCategoria(IdCategoria);
        }

        // =========================
        // Categorias
        // =======================

        public DataTable listar_unidades()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.listar_unidades();
            return tabla;
        }

        public static string alta_unidad(string nombre, string nombre_corto)
        {
            CD_Productos Obj = new CD_Productos();

            return Obj.alta_unidad(nombre, nombre_corto);

        }


    }
}
