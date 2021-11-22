using System;
using System.Collections.Generic;

namespace LitExplore.Persistence.Entities
{
    public class Publication
    {
        public int Id { get; set; }
        public String Author { get; set; }
        
        public ICollection<Reference> References;
        
        

    }
}