using System;
using System.Collections.Generic;

class Miembro
{
    public string Nombre;
    public List<Miembro> Padres;

    public Miembro(string nombre)
    {
        Nombre = nombre;
        Padres = new List<Miembro>();
    }
}

class ArbolGenealogico
{
    public Miembro Raiz;

    public ArbolGenealogico(string nombreRaiz)
    {
        Raiz = new Miembro(nombreRaiz);
    }

    public Miembro BuscarMiembro(Miembro actual, string nombre)
    {
        if (actual == null) return null;
        if (actual.Nombre == nombre) return actual;
        foreach (var padre in actual.Padres)
        {
            var encontrado = BuscarMiembro(padre, nombre);
            if (encontrado != null) return encontrado;
        }
        return null;
    }

    public void InsertarPadre(string nombreHijo, string nombrePadre)
    {
        var hijo = BuscarMiembro(Raiz, nombreHijo);
        if (hijo != null)
        {
            hijo.Padres.Add(new Miembro(nombrePadre));
        }
    }

    public void RecorridoPreorden(Miembro actual, int nivel = 0)
    {
        if (actual == null) return;
        Console.WriteLine(new string('-', nivel * 2) + actual.Nombre);
        foreach (var padre in actual.Padres)
        {
            RecorridoPreorden(padre, nivel + 1);
        }
    }

    public void MostrarPadresDe(string nombre)
    {
        var miembro = BuscarMiembro(Raiz, nombre);
        if (miembro != null && miembro.Padres.Count > 0)
        {
            Console.WriteLine($"Padres de {nombre}:");
            foreach (var padre in miembro.Padres)
            {
                Console.WriteLine(padre.Nombre);
            }
        }
        else
        {
            Console.WriteLine($"No se encontraron padres para {nombre}.");
        }
    }
}

class Program
{
    static void Main()
    {
        var arbol = new ArbolGenealogico("HijoPrincipal");

        arbol.InsertarPadre("HijoPrincipal", "Padre1");
        arbol.InsertarPadre("HijoPrincipal", "Madre1");
        arbol.InsertarPadre("Padre1", "Abuelo1");
        arbol.InsertarPadre("Padre1", "Abuela1");
        arbol.InsertarPadre("Madre1", "Abuelo2");
        arbol.InsertarPadre("Madre1", "Abuela2");

        Console.WriteLine("Recorrido preorden del árbol:");
        arbol.RecorridoPreorden(arbol.Raiz);

        Console.WriteLine("\nBuscar padres de 'Padre1':");
        arbol.MostrarPadresDe("Padre1");

        Console.WriteLine("\nBuscar padres de 'HijoPrincipal':");
        arbol.MostrarPadresDe("HijoPrincipal");
    }
}
