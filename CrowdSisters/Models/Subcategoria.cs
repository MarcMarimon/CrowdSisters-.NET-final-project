using System;

public class Subcategoria
{
    public int IDSubcategoria { get; set; }
    public string Nombre { get; set; }
    public int FKCategoria { get; set; }

    // Navigation properties
    public Categoria Categoria { get; set; }
    public ICollection<Proyecto> Proyectos { get; set; }
}
