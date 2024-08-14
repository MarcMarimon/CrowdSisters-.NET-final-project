﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CrowdSisters.Conections;
using CrowdSisters.Models;

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
        public async Task<bool> CreateAsync(Proyecto proyecto)
        {
            const string query = @"
                INSERT INTO Proyecto (FKUsuario, FKSubcategoria, Titulo, Descripcion, FechaCreacion, FechaFinalizacion, MontoObjetivo, MontoRecaudado, Estado)
                VALUES (@FKUsuario, @FKSubcategoria, @Titulo, @Descripcion, @FechaCreacion, @FechaFinalizacion, @MontoObjetivo, @MontoRecaudado, @Estado)";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    command.Parameters.AddWithValue("@FKUsuario", proyecto.FKUsuario);
                    command.Parameters.AddWithValue("@FKSubcategoria", proyecto.FKSubcategoria);
                    command.Parameters.AddWithValue("@Titulo", proyecto.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", proyecto.Descripcion);
                    command.Parameters.AddWithValue("@FechaCreacion", proyecto.FechaCreacion);
                    command.Parameters.AddWithValue("@FechaFinalizacion", proyecto.FechaFinalizacion);
                    command.Parameters.AddWithValue("@MontoObjetivo", proyecto.MontoObjetivo);
                    command.Parameters.AddWithValue("@MontoRecaudado", proyecto.MontoRecaudado);
                    command.Parameters.AddWithValue("@Estado", proyecto.Estado);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el proyecto: {ex.Message}");
                return false;
            }
        }

        // Leer
        public async Task<Proyecto> GetByIdAsync(int id)
        {
            const string query = @"
                SELECT * FROM Proyecto
                WHERE IDProyecto = @IDProyecto";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
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
                                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                                FechaFinalizacion = reader.GetDateTime(reader.GetOrdinal("FechaFinalizacion")),
                                MontoObjetivo = reader.GetDecimal(reader.GetOrdinal("MontoObjetivo")),
                                MontoRecaudado = reader.GetDecimal(reader.GetOrdinal("MontoRecaudado")),
                                Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                            };
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el proyecto: {ex.Message}");
                return null;
            }
        }

        // Actualizar
        public async Task<bool> UpdateAsync(Proyecto proyecto)
        {
            const string query = @"
                UPDATE Proyecto
                SET FKSubcategoria = @FKSubcategoria,
                    Titulo = @Titulo,
                    Descripcion = @Descripcion,
                    FechaCreacion = @FechaCreacion,
                    FechaFinalizacion = @FechaFinalizacion,
                    MontoObjetivo = @MontoObjetivo,
                    MontoRecaudado = @MontoRecaudado,
                    Estado = @Estado
                WHERE IDProyecto = @IDProyecto";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    command.Parameters.AddWithValue("@IDProyecto", proyecto.IDProyecto);
                    command.Parameters.AddWithValue("@FKUsuario", proyecto.FKUsuario);
                    command.Parameters.AddWithValue("@FKSubcategoria", proyecto.FKSubcategoria);
                    command.Parameters.AddWithValue("@Titulo", proyecto.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", proyecto.Descripcion);
                    command.Parameters.AddWithValue("@FechaCreacion", proyecto.FechaCreacion);
                    command.Parameters.AddWithValue("@FechaFinalizacion", proyecto.FechaFinalizacion);
                    command.Parameters.AddWithValue("@MontoObjetivo", proyecto.MontoObjetivo);
                    command.Parameters.AddWithValue("@MontoRecaudado", proyecto.MontoRecaudado);
                    command.Parameters.AddWithValue("@Estado", proyecto.Estado);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el proyecto: {ex.Message}");
                return false;
            }
        }

        // Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            const string query = @"
                DELETE FROM Proyecto
                WHERE IDProyecto = @IDProyecto";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    command.Parameters.AddWithValue("@IDProyecto", id);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el proyecto: {ex.Message}");
                return false;
            }
        }

        // Obtener todos los proyectos
        public async Task<IEnumerable<Proyecto>> GetAllAsync()
        {
            const string query = @"
                SELECT * FROM Proyecto";

            var proyectos = new List<Proyecto>();

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
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
                            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                            FechaFinalizacion = reader.GetDateTime(reader.GetOrdinal("FechaFinalizacion")),
                            MontoObjetivo = reader.GetDecimal(reader.GetOrdinal("MontoObjetivo")),
                            MontoRecaudado = reader.GetDecimal(reader.GetOrdinal("MontoRecaudado")),
                            Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                        });
                    }
                }

                return proyectos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los proyectos: {ex.Message}");
                return null;
            }
        }
    }
}
