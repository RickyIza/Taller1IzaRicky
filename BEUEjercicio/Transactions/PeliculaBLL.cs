using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUEjercicio.Transactions
{
    public class PeliculaBLL
    {

     //BLL Bussiness Logic Layer
       //Capa de Logica del Negocio

    public static void Create(Pelicula p)
    {
        using (Entities db = new Entities())
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Peliculas.Add(p);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }

    public static Pelicula Get(int? id)
    {
        Entities db = new Entities();
        return db.Peliculas.Find(id);
    }

    public static void Update(Pelicula pelicula)
    {
        using (Entities db = new Entities())
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Peliculas.Attach(pelicula);
                    db.Entry(pelicula).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }

    public static void Delete(int? id)
    {
        using (Entities db = new Entities())
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Pelicula pelicula = db.Peliculas.Find(id);
                    db.Entry(pelicula).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }

    public static List<Pelicula> List()
    {
        Entities db = new Entities();
        //Instancia del contexto

        /*List<Alumno> alumons = db.Alumnoes.ToList();
        List<Alumno> resultado = new List<Alumno>();
        foreach (Alumno a in alumons) {
            if (a.sexo == "M") {
                resultado.Add(a);
            }
        }
        return resultado;*/
        //SQL -> SELECT * FROM dbo.Alumno WHERE sexo = 'M'
        //return db.Alumnoes.Where(x => x.sexo == "M").ToList();

        return db.Peliculas.ToList();

        //Los metodos del EntityFramework se denomina Linq, 
        //y la evluacion de condiciones lambda
    }


    private static List<Pelicula> GetPeliculas(string criterio)
    {
        //Ejemplo: criterio = 'quin'
        //Posibles resultados => Quintana, Quintero, Pulloquinga, Quingaluisa...
        Entities db = new Entities();
        return db.Peliculas.Where(x => x.nombre.ToLower().Contains(criterio)).ToList();
    }

    private static Pelicula GetPelicula(string nombre)
    {
        Entities db = new Entities();
        return db.Peliculas.FirstOrDefault(x => x.nombre== nombre);
    }

}
}