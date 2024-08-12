using System;

public class Categoria
{
    public int IDCategoria { get; set; }
    public string Nombre { get; set; }

    // Navigation property
    public ICollection<Subcategoria> Subcategorias { get; set; }
}
