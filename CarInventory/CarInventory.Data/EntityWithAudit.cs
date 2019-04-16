using System;

namespace CarInventory.Data
{
    public abstract class EntityWithAudit : Entity
    {
        public virtual string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
