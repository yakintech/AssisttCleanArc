using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.Application.DTO
{
    public class GetAllProductsWithCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime AddDate { get; set; }

        public bool IsDeleted { get; set; }

        public string CategoryName { get; set; }
    }
}
