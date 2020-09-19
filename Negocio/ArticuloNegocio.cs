﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;


namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulos> Listar()
        {
            ///Declarar la shit necesaria para poder conectarse 
            SqlConnection Conexion = new SqlConnection();
            SqlCommand Comando = new SqlCommand();
            SqlDataReader Lector;
            List<Articulos> lista = new List<Articulos>();
            //Configurando conexion
            Conexion.ConnectionString = "data source = .\\SQLEXPRESS; initial catalog = CATALOGO_DB; integrated security = sspi";
            Comando.CommandType = System.Data.CommandType.Text;
            Comando.CommandText = "select  a.id, a.nombre, a.descripcion, a.imagenurl, a.precio, c.Descripcion as Categoria,m.Descripcion as Marca from ARTICULOS as a join CATEGORIAS c on a.IdCategoria = c.id join MARCAS as m on m.Id = a.IdMarca";
            ///se ejecuta la conexion
            Comando.Connection = Conexion;
            Conexion.Open();
            ///se manda la query a seleccionar registros
            Lector = Comando.ExecuteReader();
            

            //Logica una vez conectado, ¿qué tenemos que hacer?
            while (Lector.Read())
            {
                Articulos Aux = new Articulos();
                ///Tenemos que Instanciar un objeto auxiliar al que asignarle los valores
                ///leidos y de ahí sumarlo a la lista
                
               
                Aux.Nombre = Lector.GetString(1);
                Aux.Descripcion = Lector.GetString(2);
                Aux.Precio = Lector.GetSqlMoney(4);
               
                Aux.Imagen = (string)Lector["ImagenUrl"];

                Aux.categoria = new Categoria();
                Aux.categoria.Descripcion = (string)Lector["categoria"];

                Aux.Marca = new Marca();
                Aux.Marca.Descripcion = (string) Lector ["marca"];
                



                lista.Add(Aux);

            }
            Conexion.Close();
            return lista;
        }

        public void agregar(Articulos nuevo)
        {
            
            ///Para agregar registros, no hay que tener un "Lector"
            SqlConnection Conexion = new SqlConnection();
            SqlCommand Comando = new SqlCommand();
            List<Articulos> lista = new List<Articulos>();
            //Configurando conexion

            Conexion.ConnectionString = "data source = .\\SQLEXPRESS; initial catalog = CATALOGO_DB; integrated security = sspi";
            Comando.CommandType = System.Data.CommandType.Text;
            //Comando.CommandText = "insert into ARTICULOS (Nombre, Descripcion, ImagenUrl, IdCategoria ) values (@Nombre,@Descripcion,@ImagenUrl,@IdCategoria)";
            Comando.CommandText = "insert into ARTICULOS (Nombre, Descripcion, ImagenUrl, IdCategoria, IdMarca ) values (@Nombre,@Descripcion,'',@IdCategoria,'1')";

            Comando.Parameters.AddWithValue("@Nombre", nuevo.Nombre);
            Comando.Parameters.AddWithValue("@Descripcion", nuevo.Descripcion);
            //Comando.Parameters.AddWithValue("@ImagenUrl", nuevo.Imagen);
            Comando.Parameters.AddWithValue("@IdCategoria", nuevo.categoria.Id);
            //Comando.Parameters.AddWithValue("@IdMarca", nuevo.Marca.Id);
            Comando.Connection = Conexion;
            Conexion.Open();
            Comando.ExecuteNonQuery();

        }
    }







}

   