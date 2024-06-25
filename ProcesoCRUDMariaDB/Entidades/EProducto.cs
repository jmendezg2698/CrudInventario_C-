using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesoCRUDMariaDB.Entidades
{
    public class EProducto
    {
        public int codigoProduto { get; set; }
        public string producto { get; set; } 
        public string marca { get; set; }
        public int codigoMedida { get; set; }
        public int codigoCatalogo { get; set; }
        public decimal stockActual { get; set; }
        
    }
}
