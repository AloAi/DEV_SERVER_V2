using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloai.Entity
{
    public class CommonEntity
    {
        public List<UnitEntity> unitList { get; set; }

        public List<CatalogEntity> catalogList { get; set; }

        public List<DefineEntity> commonList { get; set; }
    }
}