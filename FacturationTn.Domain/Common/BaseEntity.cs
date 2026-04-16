using System;

namespace FacturationTn.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    }
}