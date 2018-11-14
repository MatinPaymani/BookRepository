using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core
{
   public class BaseEntity: System.Object
    {
     
        public DateTime? UpdateTime{ get; set; }
        public DateTime CreateTime{ get; set; }
   
        public BaseEntity()
        {
            UpdateTime = DateTime.Now;
            CreateTime = DateTime.Now;
        }


    }
}
