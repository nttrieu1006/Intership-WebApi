using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public interface IEntity<T>
    {
        T Id { set; get; }
    }

    public interface IUser
    {
        string HoTen { set; get; }
        string NTNS { set; get; }
    }

    public abstract class Entity<T> : IEntity<T>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { set; get; }
    }
}