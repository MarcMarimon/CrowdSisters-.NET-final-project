using CrowdSisters.Conections;
using CrowdSisters.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrowdSisters.DAL
{
    public class DALProyecto
    {
        private readonly Connection _connection;

        public DALProyecto(Connection connection)
        {
            _connection = connection;
        }

        // Crear
        public async Task<Proyecto> CreateAsync(Proyecto proyecto)
        {
            const string query = @"
        INSERT INTO Proyecto (
            FKUsuario, 
            FKSubcategoria, 
            Titulo, 
            DescripcionGeneral, 
            FechaCreacion, 
            FechaFinalizacion, 
            MontoObjetivo, 
            MontoRecaudado, 
            Subtitulo, 
            DescripcionFinalidad, 
            DescripcionPresupuesto, 
            UrlFotoEncabezado, 
            UrlFoto1, 
            UrlFoto2, 
            UrlFoto3
        ) 
        VALUES (
            @FKUsuario, 
            @FKSubcategoria, 
            @Titulo, 
            @DescripcionGeneral, 
            @FechaCreacion, 
            @FechaFinalizacion, 
            @MontoObjetivo, 
            @MontoRecaudado, 
            @Subtitulo, 
            @DescripcionFinalidad, 
            @DescripcionPresupuesto, 
            @UrlFotoEncabezado, 
            @UrlFoto1, 
            @UrlFoto2, 
            @UrlFoto3
        );
        SELECT SCOPE_IDENTITY();"; // Obtiene el último ID generado en este ámbito

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión

                    // Añadir los parámetros al comando
                    command.Parameters.AddWithValue("@FKUsuario", proyecto.FKUsuario);
                    command.Parameters.AddWithValue("@FKSubcategoria", proyecto.FKSubcategoria);
                    command.Parameters.AddWithValue("@Titulo", proyecto.Titulo);
                    command.Parameters.AddWithValue("@DescripcionGeneral", proyecto.DescripcionGeneral);
                    command.Parameters.AddWithValue("@FechaCreacion", proyecto.FechaCreacion);
                    command.Parameters.AddWithValue("@FechaFinalizacion", proyecto.FechaFinalizacion);
                    command.Parameters.AddWithValue("@MontoObjetivo", proyecto.MontoObjetivo);
                    command.Parameters.AddWithValue("@MontoRecaudado", proyecto.MontoRecaudado);
                    command.Parameters.AddWithValue("@Subtitulo", proyecto.Subtitulo);
                    command.Parameters.AddWithValue("@DescripcionFinalidad", proyecto.DescripcionFinalidad);
                    command.Parameters.AddWithValue("@DescripcionPresupuesto", proyecto.DescripcionPresupuesto);
                    command.Parameters.AddWithValue("@UrlFotoEncabezado", proyecto.UrlFotoEncabezado);
                    command.Parameters.AddWithValue("@UrlFoto1", proyecto.UrlFoto1);
                    command.Parameters.AddWithValue("@UrlFoto2", proyecto.UrlFoto2);
                    command.Parameters.AddWithValue("@UrlFoto3", proyecto.UrlFoto3);

                    // Ejecutar la consulta y obtener el ID generado automáticamente
                    proyecto.IDProyecto = Convert.ToInt32(await command.ExecuteScalarAsync());

                    return proyecto; // Devuelve el proyecto con el ID asignado por la base de datos
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Devuelve null en caso de error
            }
        }

        // Leer todos
        public async Task<IEnumerable<Proyecto>> GetAllAsync()
        {
            List<Proyecto> proyectos = new List<Proyecto>();

            const string query = @"SELECT * FROM Proyecto;";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            proyectos.Add(new Proyecto
                            {
                                IDProyecto = reader.GetInt32(reader.GetOrdinal("IDProyecto")),
                                FKUsuario = reader.GetInt32(reader.GetOrdinal("FKUsuario")),
                                FKSubcategoria = reader.GetInt32(reader.GetOrdinal("FKSubcategoria")),
                                Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                DescripcionGeneral = reader.GetString(reader.GetOrdinal("DescripcionGeneral")),
                                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                                FechaFinalizacion = reader.GetDateTime(reader.GetOrdinal("FechaFinalizacion")),
                                MontoObjetivo = reader.GetDecimal(reader.GetOrdinal("MontoObjetivo")),
                                MontoRecaudado = reader.GetDecimal(reader.GetOrdinal("MontoRecaudado")),
                                Subtitulo = reader.GetString(reader.GetOrdinal("Subtitulo")),
                                DescripcionFinalidad = reader.GetString(reader.GetOrdinal("DescripcionFinalidad")),
                                DescripcionPresupuesto = reader.GetString(reader.GetOrdinal("DescripcionPresupuesto")),
                                UrlFotoEncabezado = reader.GetString(reader.GetOrdinal("UrlFotoEncabezado")),
                                UrlFoto1 = reader.GetString(reader.GetOrdinal("UrlFoto1")),
                                UrlFoto2 = reader.GetString(reader.GetOrdinal("UrlFoto2")),
                                UrlFoto3 = reader.GetString(reader.GetOrdinal("UrlFoto3")),
                            });
                        }
                    }
                }
                return proyectos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Leer por ID
        public async Task<Proyecto> GetByIdAsync(int id)
        {
            const string query = @"SELECT * FROM Proyecto WHERE IDProyecto = @IDProyecto";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDProyecto", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Proyecto
                            {
                                IDProyecto = reader.GetInt32(reader.GetOrdinal("IDProyecto")),
                                FKUsuario = reader.GetInt32(reader.GetOrdinal("FKUsuario")),
                                FKSubcategoria = reader.GetInt32(reader.GetOrdinal("FKSubcategoria")),
                                Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                DescripcionGeneral = reader.GetString(reader.GetOrdinal("DescripcionGeneral")),
                                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                                FechaFinalizacion = reader.GetDateTime(reader.GetOrdinal("FechaFinalizacion")),
                                MontoObjetivo = reader.GetDecimal(reader.GetOrdinal("MontoObjetivo")),
                                MontoRecaudado = reader.GetDecimal(reader.GetOrdinal("MontoRecaudado")),
                                Subtitulo = reader.GetString(reader.GetOrdinal("Subtitulo")),
                                DescripcionFinalidad = reader.GetString(reader.GetOrdinal("DescripcionFinalidad")),
                                DescripcionPresupuesto = reader.GetString(reader.GetOrdinal("DescripcionPresupuesto")),
                                UrlFotoEncabezado = reader.GetString(reader.GetOrdinal("UrlFotoEncabezado")),
                                UrlFoto1 = reader.GetString(reader.GetOrdinal("UrlFoto1")),
                                UrlFoto2 = reader.GetString(reader.GetOrdinal("UrlFoto2")),
                                UrlFoto3 = reader.GetString(reader.GetOrdinal("UrlFoto3")),
                            };
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Actualizar
        public async Task<bool> UpdateAsync(Proyecto proyecto)
        {
            const string query = @"
                UPDATE Proyecto
                SET 
                    FKUsuario = @FKUsuario,
                    FKSubcategoria = @FKSubcategoria,
                    Titulo = @Titulo,
                    DescripcionGeneral = @DescripcionGeneral,
                    FechaCreacion = @FechaCreacion,
                    FechaFinalizacion = @FechaFinalizacion,
                    MontoObjetivo = @MontoObjetivo,
                    MontoRecaudado = @MontoRecaudado,
                    Subtitulo = @Subtitulo,
                    DescripcionFinalidad = @DescripcionFinalidad,
                    DescripcionPresupuesto = @DescripcionPresupuesto,
                    UrlFotoEncabezado = @UrlFotoEncabezado,
                    UrlFoto1 = @UrlFoto1,
                    UrlFoto2 = @UrlFoto2,
                    UrlFoto3 = @UrlFoto3
                WHERE IDProyecto = @IDProyecto";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDProyecto", proyecto.IDProyecto);
                    command.Parameters.AddWithValue("@FKUsuario", proyecto.FKUsuario);
                    command.Parameters.AddWithValue("@FKSubcategoria", proyecto.FKSubcategoria);
                    command.Parameters.AddWithValue("@Titulo", proyecto.Titulo);
                    command.Parameters.AddWithValue("@DescripcionGeneral", proyecto.DescripcionGeneral);
                    command.Parameters.AddWithValue("@FechaCreacion", proyecto.FechaCreacion);
                    command.Parameters.AddWithValue("@FechaFinalizacion", proyecto.FechaFinalizacion);
                    command.Parameters.AddWithValue("@MontoObjetivo", proyecto.MontoObjetivo);
                    command.Parameters.AddWithValue("@MontoRecaudado", proyecto.MontoRecaudado);
                    command.Parameters.AddWithValue("@Subtitulo", proyecto.Subtitulo);
                    command.Parameters.AddWithValue("@DescripcionFinalidad", proyecto.DescripcionFinalidad);
                    command.Parameters.AddWithValue("@DescripcionPresupuesto", proyecto.DescripcionPresupuesto);
                    command.Parameters.AddWithValue("@UrlFotoEncabezado", proyecto.UrlFotoEncabezado);
                    command.Parameters.AddWithValue("@UrlFoto1", proyecto.UrlFoto1);
                    command.Parameters.AddWithValue("@UrlFoto2", proyecto.UrlFoto2);
                    command.Parameters.AddWithValue("@UrlFoto3", proyecto.UrlFoto3);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateMontoRecaudadoAsync(decimal resta, int idProyecto)
        {
            const string query = @"
                UPDATE Proyecto
                SET
                    MontoRecaudado = MontoRecaudado + @Resta
                WHERE IDProyecto = @IDProyecto";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDProyecto", idProyecto);
                    command.Parameters.AddWithValue("@Resta", resta);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            const string query = @"DELETE FROM Proyecto WHERE IDProyecto = @IDProyecto";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDProyecto", id);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
