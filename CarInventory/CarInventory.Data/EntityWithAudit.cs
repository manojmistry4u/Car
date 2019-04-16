using System;

namespace CarInventory.Data
{
    public abstract class EntityWithAudit : Entity
    {
        public virtual int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual int LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
