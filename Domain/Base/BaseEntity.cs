﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Base
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }

    public class BaseEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T Id { get; set; }
    }
}
